using CurtLog;
using ShadyPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ListView = System.Windows.Forms.ListView;

namespace CoD_Widescreen_Suite
{
    public partial class ServersForm : ExtendedForm
    {
        public ServersForm()
        {
            InitializeComponent();
        }

       
        private readonly Dictionary<string, Server> _hostNameToInfo = new Dictionary<string, Server>();
      

        public string GameVersion { get; set; }
        public string GameName { get; set; }

        private PingOptions PingOptions { get; set; } = new PingOptions { DontFragment = true, Ttl = 128 };

        private const string PING_DATA = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private byte[] GetPingBuffer() => Encoding.ASCII.GetBytes(PING_DATA);

        public static ServerListViewFilter ServerListFilter { get; private set; }

        public class ServerListViewFilter
        {
            public bool FilterNoPing { get; set; } = true;
            public int FilterMaxPing { get; set; } = 200;

            public bool FilterEmptyServers { get; set; } = false;

            public string MapNameFilter { get; set; } = string.Empty;
            public string GameTypeFilter { get; set; } = string.Empty;
            public string HostnameFilter { get; set; } = string.Empty;

            public ListView ListView { get; private set; }

            /// <summary>
            /// All items in the list view. Not the filtered list.
            /// </summary>
            public List<ListViewItem> ListViewItems { get; private set; } = new List<ListViewItem>();
            

            public ServerListViewFilter(ListView listView)
            {
                ListView = listView;
                ListViewItems.AddRange(listView.Items.Cast<ListViewItem>());
            }

            public List<ListViewItem> GetFilteredItems()
            {
                var filtered = new List<ListViewItem>();

                GetFilteredItemsNoAlloc(ref filtered);

                return filtered;
            }

            public void GetFilteredItemsNoAlloc(ref List<ListViewItem> items)
            {

                var toRemove = new List<ListViewItem>();


                for (int i = 0; i < ListViewItems.Count; i++)
                {
                    var item = ListViewItems[i];


                    if (FilterNoPing && !int.TryParse(item.SubItems[4].Text, out _))
                        toRemove.Add(item);

                    if (FilterMaxPing > 0 && int.TryParse(item.SubItems[4].Text, out var ping) && ping > FilterMaxPing)
                        toRemove.Add(item);

                    if (FilterEmptyServers && item.SubItems[2].Text.StartsWith("0"))
                        toRemove.Add(item);

                    // So, since all of our text-based filtering is done through one textbox1,
                    // We need to check to make sure that *none* of the filters are a matched for the textbox.
                    // For example, if the user types "carentan" into the textbox, we *should* show all servers that are playing carentan.
                    // Alternatively, if the user types something like "gamers" into the textbox, we should show all servers with that in their name.
                    
                    // Therefore, we need to check if NONE of the first 3 subitems contain any of the filter text.

                    if (item.SubItems[0].Text.IndexOf(HostnameFilter, StringComparison.OrdinalIgnoreCase) == -1 
                        && item.SubItems[1].Text.IndexOf(MapNameFilter, StringComparison.OrdinalIgnoreCase) == -1 
                        && item.SubItems[3].Text.IndexOf(GameTypeFilter, StringComparison.OrdinalIgnoreCase) == -1)
                        toRemove.Add(item);

                }

                for (int i = 0; i < toRemove.Count; i++)
                {
                    var filter = toRemove[i];
                    items.Remove(filter);

                }


            }

            public void ClearAll()
            {
                if (ListView?.Items != null)
                {
                    if (ListView.InvokeRequired)
                        ListView.BeginInvoke((MethodInvoker)delegate { ListView.Items.Clear(); });
                    else ListView.Items.Clear();
                }

                if (ListViewItems != null)
                    ListViewItems.Clear();
                else ListViewItems = new List<ListViewItem>();

            }

            public void ApplyFilters()
            {

                if (ListView.InvokeRequired)
                    ListView.BeginInvoke((MethodInvoker)delegate { ListView.Items.Clear(); });
                else ListView.Items.Clear();


                var newItems = new List<ListViewItem>(ListViewItems);

                GetFilteredItemsNoAlloc(ref newItems);

                for (int i = 0; i < newItems.Count; i++)
                {
                    var item = newItems[i];
                    if (ListView.InvokeRequired)
                        ListView.BeginInvoke((MethodInvoker)delegate { ListView.Items.Add(item); });
                    else ListView.Items.Add(item);
                }
                    


            }

        }

        public class ListViewItemComparer : IComparer
        {
            private int columnToSort;
            private SortOrder sortOrder;

            public ListViewItemComparer()
            {
                columnToSort = 0; // Default sorting column
                sortOrder = SortOrder.Ascending; // Default sorting order
            }

            public ListViewItemComparer(int column, SortOrder order)
            {
                columnToSort = column;
                sortOrder = order;
            }

            public int Compare(object x, object y)
            {
                var listViewItemX = (ListViewItem)x;
                var listViewItemY = (ListViewItem)y;

                // Compare subitems based on their text values
                string xText = listViewItemX.SubItems[columnToSort].Text;
                string yText = listViewItemY.SubItems[columnToSort].Text;

                Console.WriteLine(nameof(xText) + ": " + xText + " " + nameof(yText) + ": " + yText);

                // Perform comparison based on the column type (e.g., text, numeric)

                if (xText.Contains("/"))
                    xText = xText.Split('/')[0];
                
                if (yText.Contains("/"))
                    yText = yText.Split('/')[0];


                if (double.TryParse(xText, out double xValue) && double.TryParse(yText, out double yValue))
                {
                    // Numeric comparison
                    return sortOrder == SortOrder.Ascending ? xValue.CompareTo(yValue) : yValue.CompareTo(xValue);
                }
                else
                {
                    // Text comparison
                    return sortOrder == SortOrder.Ascending ? string.Compare(xText, yText) : string.Compare(yText, xText);
                }
            }
        }

        private ColumnHeader lastSortColumn;
        private SortOrder lastSortOrder;

        private void ServersForm_Load(object sender, EventArgs e)
        {
            MapNameLabel.Text = string.Empty;

            GameVersionBox.SelectedIndex = 0;

            ServerListFilter = new ServerListViewFilter(ServerListView);
            ServerListView.ColumnClick += new ColumnClickEventHandler(listView1_ColumnClick);
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine the new sort order.
            SortOrder sortOrder;
            if (lastSortColumn == ServerListView.Columns[e.Column])
            {
                // Switch the sort order.
                sortOrder = lastSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Set the sort order to ascending by default.
                sortOrder = SortOrder.Ascending;
            }

            // Update the last sort column and order.
            lastSortColumn = ServerListView.Columns[e.Column];
            lastSortOrder = sortOrder;

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            ServerListView.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            ServerListView.Sort();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GameName))
            {
                MessageBox.Show("Unable to determine game name!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(GameVersion))
            {
                MessageBox.Show("Unable to determine game version!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var infos = await CodPmApi.GetMasterList(GameName, GameVersion);

            if (infos?.Servers is null)
            {
                MessageBox.Show("Failed to grab server list!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (infos.Servers.Count < 1)
            {
                MessageBox.Show("No servers were found.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ServerListView.Items.Clear();

            ServerListFilter?.ClearAll();

            var sb = Pool.Get<StringBuilder>();
            try 
            {
                // Add all the items without ping displayed at first.
                for (int i = 0; i < infos.Servers.Count; i++)
                {
                    var info = infos.Servers[i];


                    var prettyHostName = CodPmApi.GetFilteredHostname(info.Hostname);

                    _hostNameToInfo[prettyHostName] = info;

                    var item = new ListViewItem(prettyHostName);
                    item.SubItems.Add(CodPmApi.GetPrettyMapName(info.MapName));
                    item.SubItems.Add(sb.Clear().Append(info.PlayerInfo.Count).Append("/").Append(info.MaxClients).ToString());
                    item.SubItems.Add(info.GameType);
                    item.SubItems.Add("Pinging...");


                    ServerListView.Items.Add(item);
                    ServerListFilter.ListViewItems.Add(item);

                }
            }
            finally { Pool.Free(ref sb); }

           

            // Now, we do the penguin.

            var pingTasks = new Task[infos.Servers.Count];
            var pingBuffer = GetPingBuffer();

            for (int i = 0; i < infos.Servers.Count; i++)
            {
                var info = infos.Servers[i];

                var item = ServerListView.Items[i];

                pingTasks[i] = Task.Run(async () =>
                {
                    try
                    {
                        var pinger = new Ping();

                        var response = await pinger.SendPingAsync(info.Ip, 1500, pingBuffer, PingOptions);


                        Invoke((MethodInvoker)delegate
                        {
                            item.SubItems[4].Text = (response.Status == IPStatus.Success) ? response.RoundtripTime.ToString("N0") : "*";
                        });

                    }
                    catch(Exception ex) { Console.WriteLine(ex.ToString()); }
                   
                   
                });
            }

            await Task.Run(() =>
            {
                Task.WaitAll(pingTasks);

                try { ServerListFilter.ApplyFilters(); }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }

                
          
            });

            UpdateListedServersCountLabel();

            // Default: sort by ping.
            ServerListView.ListViewItemSorter = new ListViewItemComparer(4, SortOrder.Ascending);
            ServerListView.Sort();

        }

        private void UpdateListedServersCountLabel()
        {
            var sb = Pool.Get<StringBuilder>();
            try
            {
                var countLblTxt = sb
               .Clear()
               .Append(ServerListFilter.ListView.Items.Count.ToString("N0"))
               .Append("/")
               .Append(ServerListFilter.ListViewItems.Count.ToString("N0"))
               .ToString();

                if (CountServersLabel.InvokeRequired)
                    CountServersLabel.BeginInvoke((MethodInvoker)delegate { CountServersLabel.Text = countLblTxt; });
                else CountServersLabel.Text = countLblTxt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }
            finally { Pool.Free(ref sb); }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ServerListFilter.FilterMaxPing = (int)MaxPingNumeric.Value;
                ServerListFilter.ApplyFilters();
                UpdateListedServersCountLabel();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            try 
            {
                ServerListFilter.FilterNoPing = HideNoPingCheckbox.Checked;
                ServerListFilter.ApplyFilters();
                UpdateListedServersCountLabel();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ServerListFilter.GameTypeFilter = SearchFilterTextbox.Text;
                ServerListFilter.MapNameFilter = SearchFilterTextbox.Text;
                ServerListFilter.HostnameFilter = SearchFilterTextbox.Text;
                ServerListFilter.ApplyFilters();
                UpdateListedServersCountLabel();
            }
            catch (Exception ex) 
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void GameVersionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Try to parse game name and version from the selected combobox item.

            var selected = GameVersionBox.SelectedItem.ToString();

            if (!selected.Contains("(") || !selected.Contains(")"))
                return;

            var sb = Pool.Get<StringBuilder>();

            try 
            {
                var sanitizedStr = sb.Clear().Append(selected).Replace(")", string.Empty).Replace(":", string.Empty);

                var gameVersion = sanitizedStr.ToString().Split('(')[1];
                var gameName = sanitizedStr.ToString().Split('(')[0].Trim().ToLower();

                GameVersion = gameVersion;
                GameName = gameName.ToLower();
            }
            catch(Exception ex) 
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
            finally { Pool.Free(ref sb); }

        }

        

        private async void ServerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServerListView?.SelectedItems == null || ServerListView.SelectedItems.Count < 1)
                return;

            if (!_hostNameToInfo.TryGetValue(ServerListView.SelectedItems[0].Text, out var info))
                return;

            // Handle setting properties:

            MapNameLabel.Text = CodPmApi.GetPrettyMapName(info.MapName);

            // Handle image loading:

            MapImageBox.Image = Properties.Resources.loading_map_image_133x;

            MapImageBox.Image = await CodMapImage.GetMapImage(info.MapName) ?? Properties.Resources.no_map_image_found_133x;
        }

        private void CloseButton_Click(object sender, EventArgs e) => Close();

        private void HideEmptyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ServerListFilter.FilterEmptyServers = HideEmptyCheckbox.Checked;
                ServerListFilter.ApplyFilters();
                UpdateListedServersCountLabel();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
