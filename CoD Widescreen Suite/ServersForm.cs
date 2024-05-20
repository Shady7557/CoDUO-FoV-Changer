using ClampExt;
using CurtLog;
using ProcessExtensions;
using ShadyPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimerExtensions;
using ListView = System.Windows.Forms.ListView;

namespace CoD_Widescreen_Suite
{
    public partial class ServersForm : ExtendedForm
    {

        // Oh my goodness
        // Oh my
        // Wow
        // I wrote all of this
        // I don't know what to say of it
        // It just works
        // Please don't think twice about it

        public ServersForm()
        {
            InitializeComponent();
        }

       
        private Settings Settings => Settings.Instance;

        //private readonly Dictionary<string, Server> _hostNameToInfo = new Dictionary<string, Server>();

        private readonly Dictionary<ListViewItem, int> _listItemToServerId = new Dictionary<ListViewItem, int>();
        private readonly Dictionary<int, ListViewItem> _serverIdToListItem = new Dictionary<int, ListViewItem>();
        private readonly Dictionary<int, Server> _serverIdToServer = new Dictionary<int, Server>();

        private Server SelectedServer { get; set; }

        public string GameVersion { get; set; }
        public string GameName { get; set; }

        private PingOptions PingOptions { get; set; } = new PingOptions { DontFragment = true, Ttl = 128 };

        private const string PING_DATA = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private byte[] GetPingBuffer() => Encoding.ASCII.GetBytes(PING_DATA);

        public static ServerListViewFilter ServerListFilter { get; private set; }

        public class ServerListViewFilter
        {
            public bool FilterNoPing { get; set; } = false;
            public int FilterMaxPing { get; set; } = 0;

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
            private readonly int columnToSort;
            private readonly SortOrder sortOrder;

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

        private async void ServersForm_Load(object sender, EventArgs e)
        {
            ServerListFilter = new ServerListViewFilter(ServerListView);

            ServerListView.ColumnClick += new ColumnClickEventHandler(ColumnClick_SortHandler);
            PlayerListView.ColumnClick += new ColumnClickEventHandler(ColumnClick_SortHandler);

            ServerListView.ListViewItemSorter = new ListViewItemComparer(4, SortOrder.Ascending);


            MapNameLabel.Text = string.Empty;

            GameVersionBox.SelectedIndex = ClampEx.Clamp(Settings?.ServerListGameIndex ?? 0, 0, (GameVersionBox.Items.Count - 1));
            HideEmptyCheckbox.Checked = Settings?.ServerListHideEmpty ?? false;
            HideNoPingCheckbox.Checked = Settings?.ServerListHideNoPing ?? false;
            MaxPingNumeric.Value = ClampEx.Clamp(Settings?.ServerListMaxPing ?? 200, MaxPingNumeric.Minimum, MaxPingNumeric.Maximum);
            FilterPlayerNamesCheckbox.Checked = Settings?.ServerListFilterPlayerNames ?? false;
            FilterBotPlayersCheckbox.Checked = Settings?.ServerListFilterBots ?? false;

            ToggleButton(RefreshButton, 5f);

            await Task.Run(async () =>
            {
                var infos = await CodPmApi.GetMasterList(GameName, GameVersion);

                BeginInvoke((MethodInvoker)delegate { RefreshAllServers(infos); });
            });

        }

        private void PlayerListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        // Move?: A ButtonHelper class or extension?
        private void ToggleButton(Button button, float reToggleAfterSeconds = 0f)
        {
            if (button is null)
                throw new ArgumentNullException(nameof(button));

            if (button.IsDisposed || button.Disposing)
                return;

           if (button.InvokeRequired)
                button.BeginInvoke((MethodInvoker)delegate { button.Enabled = !button.Enabled; });
            else button.Enabled = !button.Enabled;

            if (reToggleAfterSeconds > 0f)
                TimerEx.Once(reToggleAfterSeconds, () => ToggleButton(button));
        }

        private void ColumnClick_SortHandler(object sender, ColumnClickEventArgs e)
        {
            if (!(sender is ListView listView))
                return;

            // Determine the new sort order.
            SortOrder sortOrder;
            if (lastSortColumn == listView.Columns[e.Column])
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
            lastSortColumn = listView.Columns[e.Column];
            lastSortOrder = sortOrder;

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            listView.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
            listView.Sort();
        }

        private async void RefreshButton_Click(object sender, EventArgs e)
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

            try
            {
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

                RefreshAllServers(infos);
            }
            finally { ToggleButton(sender as Button, 5f); }
        }

        private async void AddOrUpdateServerListItem(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            // If we can't get the server from the dictionary, it does not already exist. We'll have to add it!

            var sb = Pool.Get<StringBuilder>();

            try 
            {
                if (!_serverIdToListItem.TryGetValue(server.Id, out var item))
                {
                    var prettyHostName = CodPmApi.GetFilteredHostname(server.Hostname);

                    item = new ListViewItem(prettyHostName);

                    _listItemToServerId[item] = server.Id;
                    _serverIdToListItem[server.Id] = item;
                    _serverIdToServer[server.Id] = server;

                    item.SubItems.Add(CodPmApi.GetPrettyMapName(server.MapName));
                    item.SubItems.Add(sb.Clear().Append(server.PlayerInfo.Count).Append("/").Append(server.MaxClients).ToString());
                    item.SubItems.Add(server.GameType);
                    item.SubItems.Add("Pinging...");

                    ServerListView.Items.Add(item);
                    ServerListFilter.ListViewItems.Add(item);

                    await PingServer(server, item);

                    return;
                }


                _listItemToServerId[item] = server.Id;
                _serverIdToListItem[server.Id] = item;
                _serverIdToServer[server.Id] = server;

                item.SubItems[0].Text = CodPmApi.GetFilteredHostname(server.Hostname);
                item.SubItems[1].Text = CodPmApi.GetPrettyMapName(server.MapName);
                item.SubItems[2].Text = server.PlayerInfo.Count + "/" + server.MaxClients;
                item.SubItems[4].Text = "Pinging...";

                // Update the item in the list view.
                ServerListView.Invoke((MethodInvoker)delegate { item.ListView?.Refresh(); });

                UpdatePlayersListViewAndLabel(server);

                await PingServer(server, item);

            }
            finally { Pool.Free(ref sb); }

            
        }

        private async Task PingServer(Server server, ListViewItem item)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(Server));

            if (string.IsNullOrWhiteSpace(server.Ip))
                throw new Exception("Server IP was null or empty.");

            try
            {
                var pinger = new Ping();
                var pingBuffer = GetPingBuffer();
                var response = await pinger.SendPingAsync(server.Ip, 1500, pingBuffer, PingOptions);

                Invoke((MethodInvoker)delegate
                {
                    item.SubItems[4].Text = (response.Status == IPStatus.Success) ? response.RoundtripTime.ToString("N0") : "*";
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Refreshs all servers and changes UI elements, must be invoked if ran on another thread.
        /// </summary>
        /// <param name="infos"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private async void RefreshAllServers(MasterServerInfo infos)
        {
            if (infos is null)
                throw new ArgumentNullException(nameof(infos));

            if (infos?.Servers.Count < 1)
                return;

            ServerListView.Items.Clear();
            ServerListFilter?.ClearAll();

            var sb = Pool.Get<StringBuilder>();
            try
            {
                for (int i = 0; i < infos.Servers.Count; i++)
                {
                    var server = infos.Servers[i];
                    AddOrUpdateServerListItem(server);
                }
            }
            finally
            {
                Pool.Free(ref sb);
            }

            var pingTasks = new Task[infos.Servers.Count];
            for (int i = 0; i < infos.Servers.Count; i++)
            {
                var server = infos.Servers[i];
                var item = ServerListView.Items[i];
                pingTasks[i] = PingServer(server, item);
            }

            await Task.WhenAll(pingTasks);

            try
            {
                ServerListFilter.ApplyFilters();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            UpdateListedServersCountLabel();
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

        private void UpdatePlayersListViewAndLabel(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            PlayerListView.Items.Clear();

            var filterNames = FilterPlayerNamesCheckbox.Checked;
            var filterBots = FilterBotPlayersCheckbox.Checked;

            for (int i = 0; i < server.PlayerInfo.Count; i++)
            {
                var player = server.PlayerInfo[i];
                if (player is null || string.IsNullOrWhiteSpace(player.Name))
                    continue;


                if (filterBots)
                {
                    // Ensure player's ping is invalid (bots never have real ping),
                    // Ensure player's name starts with bot and is followed by a number.
                    if ((player.Ping == "0" || player.Ping == "999") && player.Name.StartsWith("bot", StringComparison.OrdinalIgnoreCase)
                        && int.TryParse(player.Name.Split('t')[1], out _))
                        continue;
                }

                var item = new ListViewItem(filterNames ? CodPmApi.FilterCaratColors(player.Name) : player.Name);
                item.SubItems.Add(player.Score.ToString());
                item.SubItems.Add(player.Ping.ToString());

                PlayerListView.Items.Add(item);
            }

            var sb = Pool.Get<StringBuilder>();
            try 
            {
                PlayerCountLabel.Text = sb
                    .Clear()
                    .Append(server.PlayerInfo.Count)
                    .Append("/")
                    .Append(server.MaxClients)
                    .ToString();
            }
            finally { Pool.Free(ref sb); }

        }

        private Task MapImageLoadTask { get; set; }
        private CancellationTokenSource MapImageLoadCancellationToken { get; set; }

        private void ServerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServerListView?.SelectedItems == null || ServerListView.SelectedItems.Count < 1)
                return;

            if (!_listItemToServerId.TryGetValue(ServerListView.SelectedItems[0], out var id))
                return;

            if (!_serverIdToServer.TryGetValue(id, out var server))
                return;


            // Handle setting properties:
            SelectedServer = server;
            MapNameLabel.Text = CodPmApi.GetPrettyMapName(server.MapName);

            // Handle player list:
            UpdatePlayersListViewAndLabel(server);

            // Handle image loading:
            BeginMapImageLoad(server.MapName);
        }

        private void BeginMapImageLoad(string mapName)
        {
            if (string.IsNullOrWhiteSpace(mapName))
                throw new ArgumentNullException(nameof(mapName));

            if (MapImageLoadTask != null)
            {
                // Cancel the existing task
                MapImageLoadCancellationToken.Cancel();
                MapImageLoadTask = null;
            }

            // Display a loading image
            MapImageBox.Image = Properties.Resources.loading_map_image_133x;

            // Create a new cancellation token source
            MapImageLoadCancellationToken = new CancellationTokenSource();

            // Assign the new task to MapImageLoadTask
            MapImageLoadTask = LoadMapImageAsync(mapName, MapImageLoadCancellationToken.Token);
        }

        private async Task LoadMapImageAsync(string mapName, CancellationToken token)
        {
            try
            {
                var img = await CodMapImage.GetMapImage(mapName) ?? Properties.Resources.no_map_image_found_133x;

                // If the task was cancelled, do not update the UI
                if (token.IsCancellationRequested)
                    return;

                if (MapImageBox == null || MapImageBox.IsDisposed || MapImageBox.Disposing)
                    return;

                MapImageBox.Invoke((MethodInvoker)delegate { MapImageBox.Image = img; });
            }
            catch (OperationCanceledException)
            {
                var cancelMsg = "Map image loading was canceled.";
                Console.WriteLine(cancelMsg);
                Log.WriteLine(cancelMsg);
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
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


        private void ServerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedItem = (sender as ListView).GetItemAt(e.X, e.Y);

            if (selectedItem == null)
                return;

            if (ProcessExtension.IsAnyCoDProcessRunning())
                return;

            if (!_listItemToServerId.TryGetValue(selectedItem, out var id))
                return;

            if (!_serverIdToServer.TryGetValue(id, out var server))
                return;

            var mainForm = GetInstance<MainForm>();

            if (mainForm is null || mainForm.IsDisposed || mainForm.Disposing)
                return;

            var sb = Pool.Get<StringBuilder>();

            try { mainForm.StartGame(false, sb
                .Clear()
                .Append(" +connect ")
                .Append(server.Ip)
                .Append(":")
                .Append(server.Port)
                .ToString()); }
            finally { Pool.Free(ref sb); }
            

        }

        private void ServersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.ServerListGameIndex = GameVersionBox.SelectedIndex;
            Settings.ServerListHideEmpty = HideEmptyCheckbox.Checked;
            Settings.ServerListHideNoPing = HideNoPingCheckbox.Checked;
            Settings.ServerListMaxPing = (int)MaxPingNumeric.Value;
        }

        private void FilterPlayerNamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ServerListFilterPlayerNames = FilterPlayerNamesCheckbox.Checked;

            if (SelectedServer is null)
                return;

            UpdatePlayersListViewAndLabel(SelectedServer);
        }

        private void FilterBotPlayersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ServerListFilterBots = FilterBotPlayersCheckbox.Checked;

            if (SelectedServer is null)
                return;

            UpdatePlayersListViewAndLabel(SelectedServer);
        }

        private void RefreshServerButton_Click(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            ToggleButton(sender as Button, 5f);
            RefreshServer(SelectedServer);

        }

        private async void RefreshServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            // Refresh the server's info.
            var newInfo = await CodPmApi.GetServer(server.Ip, server.Port);

            // We have to create a new 'Server' because when we get a snapshot of a single server,
            // The 'playerinfo' field is *not* inside of the 'serverinfo' field like it is for master servers.
            // So we copy all properties from the singular ServerInfo we acquired and put it into a new 'Server':

            var newServer = new Server()
            {
                Added = newInfo.Server.Added,
                Id = newInfo.Server.Id,
                Ip = newInfo.Server.Ip,
                MaxClients = newInfo.Server.MaxClients,
                MapName = newInfo.Server.MapName,
                Port = newInfo.Server.Port,
                Updated = newInfo.Server.Updated,
                Url = newInfo.Server.Url,
                Hostname = newInfo.Server.Hostname,
                GameType = newInfo.Server.GameType,
                PlayerInfo = newInfo.PlayerInfo
            };

            AddOrUpdateServerListItem(newServer);



        }

    }
}
