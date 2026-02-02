using CoDUO_FoV_Changer.Controls;
using CoDUO_FoV_Changer.Util;
using CurtLog;
using ProcessExtensions;
using ShadyPool;
using StringExtension;
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

namespace CoDUO_FoV_Changer
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

        public ServersForm() => InitializeComponent();

        private Settings Settings => Settings.Instance;

        public bool HasLoaded { get; private set; } = false;

        private Server SelectedServer { get; set; }

        public string GameVersion { get; set; }
        public string GameName { get; set; }

        private PingOptions PingOptions { get; set; } = new PingOptions { DontFragment = true, Ttl = 128 };

        private const string PING_DATA = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

        private string PingData => PING_DATA;

        private byte[] _pingBuffer = null;
        private byte[] GetPingBuffer()
        {
            if (_pingBuffer == null)
                _pingBuffer = Encoding.ASCII.GetBytes(PingData);

            return _pingBuffer;
        }

        public static ServerListViewFilter ServerListFilter { get; private set; }


        public class ServerListViewFilter
        {
            public bool FilterNoPing { get; set; } = false;
            public int FilterMaxPing { get; set; } = 0;

            public bool FilterEmptyServers { get; set; } = false;
            public bool FilterBotServers { get; set; } = false;

            public string MapNameFilter { get; set; } = string.Empty;
            public string GameTypeFilter { get; set; } = string.Empty;
            public string HostnameFilter { get; set; } = string.Empty;
            public string IpAddressFilter { get; set; } = string.Empty;

            public bool OnlyFavorites { get; set; } = false;

            public ServerListView ListView { get; private set; }

            /// <summary>
            /// All items in the list view. Not the filtered list.
            /// </summary>
            public List<ListViewItem> ListViewItems { get; private set; } = new List<ListViewItem>();


            public ServerListViewFilter(ServerListView listView)
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

            public bool ShouldFilter(Server server, int? ping = null)
            {
                if (server is null)
                    throw new ArgumentNullException(nameof(server));

                // This is the only part where we don't query the server object, instead,
                // we query the listview item because we don't store the ping response anywhere else.

                if (OnlyFavorites && !SettingsExt.IsFavoriteServer(server))
                    return true;

                if (FilterMaxPing > 0 && ping > FilterMaxPing)
                    return true;
                else if (FilterNoPing && ping != null && ping.HasValue && ping.Value <= 0)
                    return true;



                if (FilterEmptyServers && server.Clients < 1)
                    return true;

                if (FilterBotServers && server.PlayerInfo != null)
                {
                    for (int j = 0; j < server.PlayerInfo.Count; j++)
                    {
                        var player = server.PlayerInfo[j];
                        if (player is null || string.IsNullOrWhiteSpace(player.Name))
                            continue;

                        // Match the bot detection logic used in UpdatePlayersListViewAndLabel
                        if ((player.Ping <= 0 || player.Ping >= 999) && player.Name.StartsWith("bot", StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                }

                // So, since all of our text-based filtering is done through one textbox1,
                // We need to check to make sure that *none* of the filters are a matched for the textbox.
                // For example, if the user types "carentan" into the textbox, we *should* show all servers that are playing carentan.
                // Alternatively, if the user types something like "gamers" into the textbox, we should show all servers with that in their name.

                // Therefore, we need to check if NONE of the first 3 subitems contain any of the filter text.

                if ((server.Hostname ?? string.Empty).IndexOf(HostnameFilter, StringComparison.OrdinalIgnoreCase) == -1
                                           && (server.MapName ?? string.Empty).IndexOf(MapNameFilter, StringComparison.OrdinalIgnoreCase) == -1
                                                                  && (server.GameType ?? string.Empty).IndexOf(GameTypeFilter, StringComparison.OrdinalIgnoreCase) == -1
                                                                  && (server.Ip ?? string.Empty).IndexOf(IpAddressFilter, StringComparison.OrdinalIgnoreCase) == -1)
                    return true;

                return false;
            }

            public void GetFilteredItemsNoAlloc(ref List<ListViewItem> items)
            {
                if (items is null)
                    throw new ArgumentNullException(nameof(items));

                if (ListViewItems is null)
                    return;

                for (int i = 0; i < ListViewItems.Count; i++)
                {
                    var item = ListViewItems[i];

                    var server = ListView.GetServer(item);

                    int? ping;

                    if (item.SubItems.Count > 4 && int.TryParse(item.SubItems[4].Text, out var fakePing))
                        ping = fakePing;
                    else ping = -1;

                    // Add items that should NOT be filtered (i.e., items that pass the filter)
                    if (server != null && !ShouldFilter(server, ping))
                        items.Add(item);
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
                if (ListView is null)
                    return;

                if (ListView.IsDisposed || ListView.Disposing)
                    return;

                // Build a set of items that should be filtered out (hidden)
                var filteredOutItems = new HashSet<ListViewItem>();
                for (int i = 0; i < ListViewItems.Count; i++)
                {
                    var item = ListViewItems[i];
                    var server = ListView.GetServer(item);

                    int? ping = null;
                    if (item.SubItems.Count > 4 && int.TryParse(item.SubItems[4].Text, out var fakePing))
                        ping = fakePing;
                    else
                        ping = -1;

                    if (server != null && ShouldFilter(server, ping))
                        filteredOutItems.Add(item);
                }

                // Build a set of currently visible items for O(1) lookup
                var currentItems = new HashSet<ListViewItem>();
                for (int i = 0; i < ListView.Items.Count; i++)
                    currentItems.Add(ListView.Items[i]);

                // Collect items to remove and add
                var itemsToRemove = new List<ListViewItem>();
                var itemsToAdd = new List<ListViewItem>();

                // Find items to remove (currently visible but should be filtered)
                for (int i = 0; i < ListView.Items.Count; i++)
                {
                    var item = ListView.Items[i];
                    if (filteredOutItems.Contains(item))
                        itemsToRemove.Add(item);
                }

                // Find items to add (should be visible but not currently in the list)
                for (int i = 0; i < ListViewItems.Count; i++)
                {
                    var item = ListViewItems[i];
                    if (!filteredOutItems.Contains(item) && !currentItems.Contains(item))
                        itemsToAdd.Add(item);
                }

                // Apply changes with BeginUpdate/EndUpdate to prevent flickering
                if (ListView.InvokeRequired)
                {
                    ListView.BeginInvoke((MethodInvoker)delegate
                    {
                        ListView.BeginUpdate();
                        try
                        {
                            foreach (var item in itemsToRemove)
                                ListView.Items.Remove(item);
                            foreach (var item in itemsToAdd)
                                ListView.Items.Add(item);
                        }
                        finally
                        {
                            ListView.EndUpdate();
                        }
                    });
                }
                else
                {
                    ListView.BeginUpdate();
                    try
                    {
                        foreach (var item in itemsToRemove)
                            ListView.Items.Remove(item);
                        foreach (var item in itemsToAdd)
                            ListView.Items.Add(item);
                    }
                    finally
                    {
                        ListView.EndUpdate();
                    }
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
                var xText = listViewItemX.SubItems[columnToSort].Text;
                var yText = listViewItemY.SubItems[columnToSort].Text;

                // Perform comparison based on the column type (e.g., text, numeric)

                if (xText.Contains("/"))
                    xText = xText.Split('/')[0];

                if (yText.Contains("/"))
                    yText = yText.Split('/')[0];


                // Numeric comparison
                if (double.TryParse(xText, out double xValue) && double.TryParse(yText, out double yValue))
                    return sortOrder == SortOrder.Ascending ? xValue.CompareTo(yValue) : yValue.CompareTo(xValue);


                // Text comparison
                return sortOrder == SortOrder.Ascending ? string.Compare(xText, yText) : string.Compare(yText, xText);
            }
        }

        // Track sort order per ListView, per column index
        private readonly Dictionary<ListView, Dictionary<int, SortOrder>> _columnSortOrders =
            new Dictionary<ListView, Dictionary<int, SortOrder>>();

        // Track the currently active sort column per ListView
        private readonly Dictionary<ListView, int> _currentSortColumn =
            new Dictionary<ListView, int>();

        private void ServersForm_Load(object sender, EventArgs e)
        {
            ServerListFilter = new ServerListViewFilter(ServerListView);
            ServerListView.ContextMenuStrip = contextMenuStrip1;

            ServerListView.ColumnClick += ColumnClick_SortHandler;
            PlayerListView.ColumnClick += ColumnClick_SortHandler;

            // Initialize default sorting: Players (col 2) descending, Score (col 1) descending
            ServerListView.ListViewItemSorter = new ListViewItemComparer(2, SortOrder.Descending);
            PlayerListView.ListViewItemSorter = new ListViewItemComparer(1, SortOrder.Descending);

            // Initialize sort tracking to match default sorting
            _columnSortOrders[ServerListView] = new Dictionary<int, SortOrder> { { 2, SortOrder.Descending } };
            _columnSortOrders[PlayerListView] = new Dictionary<int, SortOrder> { { 1, SortOrder.Descending } };
            _currentSortColumn[ServerListView] = 2;
            _currentSortColumn[PlayerListView] = 1;

            MapNameLabel.Text = string.Empty;

            GameVersionBox.SelectedIndex = ClampEx.Clamp(Settings?.ServerListGameIndex ?? 0, 0, GameVersionBox.Items.Count - 1);
            HideEmptyCheckbox.Checked = Settings?.ServerListHideEmpty ?? false;
            HideNoPingCheckbox.Checked = Settings?.ServerListHideNoPing ?? false;
            MaxPingNumeric.Value = ClampEx.Clamp(Settings?.ServerListMaxPing ?? 200, MaxPingNumeric.Minimum, MaxPingNumeric.Maximum);
            FilterPlayerNamesCheckbox.Checked = Settings?.ServerListFilterPlayerNames ?? false;
            FilterBotPlayersCheckbox.Checked = Settings?.ServerListFilterBots ?? false;
            FilterBotServersCheckbox.Checked = Settings?.ServerListFilterBotServers ?? false;

            ServerListFilter.OnlyFavorites = Settings?.ServerListShowFavorites ?? false;

            AutoRefreshSettingComboBox.SelectedIndex = Settings?.ServerListAutoRefreshSetting ?? 0;

            UpdateFavoritesButtonText();
            UpdateFormText();



            Task.Run(async () =>
            {
                try
                {
                    var infos = await CodPmApi.GetMasterList(GameName, GameVersion);

                    BeginInvoke((MethodInvoker)async delegate
                    {
                        try { await RefreshAllServers(infos); }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Log.WriteLine(ex.ToString());
                        }
                    });
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());

                    Console.WriteLine(ex.ToString());
                }
                finally { HasLoaded = true; }
            });
        }

        // Move?: A class or extension?
        private void ToggleControl(Control control, float reToggleAfterSeconds = 0f)
        {
            if (control is null)
                throw new ArgumentNullException(nameof(control));

            if (control.IsDisposed || control.Disposing)
                return;

            if (control.InvokeRequired)
                control.BeginInvoke((MethodInvoker)delegate { control.Enabled = !control.Enabled; });
            else control.Enabled = !control.Enabled;

            if (reToggleAfterSeconds > 0f)
                TimerEx.Once(reToggleAfterSeconds, () => ToggleControl(control));
        }

        /// <summary>
        /// Gets the "most favorable" default sort order for a column.
        /// Players/Score columns default to Descending (highest first).
        /// All other columns (including Ping) default to Ascending.
        /// </summary>
        private SortOrder GetFavorableDefaultSortOrder(ListView listView, int columnIndex)
        {
            // ServerListView: col 2 = Players (most players first)
            // PlayerListView: col 1 = Score (highest score first)
            if ((listView == ServerListView && columnIndex == 2) ||
                (listView == PlayerListView && columnIndex == 1))
                return SortOrder.Descending;

            return SortOrder.Ascending;
        }

        private void ColumnClick_SortHandler(object sender, ColumnClickEventArgs e)
        {
            if (!(sender is ListView listView))
                return;

            var clickColumn = listView.Columns[e.Column];

            if (clickColumn.Text == "IP")
                return;

            if (!_columnSortOrders.TryGetValue(listView, out var columnOrders))
            {
                columnOrders = new Dictionary<int, SortOrder>();
                _columnSortOrders[listView] = columnOrders;
            }

            _currentSortColumn.TryGetValue(listView, out var activeColumn);
            columnOrders.TryGetValue(e.Column, out var currentOrder);

            // Same column: toggle sort order. Different column: use favorable default.
            var newOrder = activeColumn == e.Column
                ? (currentOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending)
                : GetFavorableDefaultSortOrder(listView, e.Column);

            columnOrders[e.Column] = newOrder;
            _currentSortColumn[listView] = e.Column;

            listView.ListViewItemSorter = new ListViewItemComparer(e.Column, newOrder);
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


                // TODO: If currently only showing favorites - only refresh favorites:


                await RefreshAllServers(infos);

                if (SelectedServer != null)
                    UpdatePlayersListViewAndLabel(SelectedServer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// Adds (or updates an existing) server item based on a given Server. Does not ping. 
        /// Accesses UI elements; must be invoked if ran from another thread.
        /// </summary>
        /// <param name="server"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void AddOrUpdateServerListItem(Server server, bool addToActiveItems = true)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            // If we can't get the server from the dictionary, it does not already exist. We'll have to add it!

            if (SelectedServer != null && SelectedServer.Id == server.Id)
                SelectedServer = server;
            

            try
            {

                var item = ServerListView?.GetItem(server.Id);

                if (item == null)
                {
                    var prettyHostName = ServerUtil.GetFilteredHostname(server.Hostname);

                    item = new ListViewItem(prettyHostName);

                    ServerListView.SetItemToServer(item, server);

                    item.SubItems.Add(ServerUtil.GetPrettyMapName(server.MapName));

                    item.SubItems.Add(ServerUtil.GetPlayersString(server));

                    item.SubItems.Add(server.GameType.ToUpper());
                    item.SubItems.Add("Pinging...");
                    item.SubItems.Add(ServerUtil.GetIpAndPort(server));

                    ServerListFilter.ListViewItems.Add(item);

                    // Whether we should add it to the displayed items.
                    if (addToActiveItems)
                        ServerListView.Items.Add(item);


                    //    await PingServer(server, item);

                    return;
                }

                if (!ServerListFilter.ListViewItems.Contains(item))
                    ServerListFilter.ListViewItems.Add(item);

                if (addToActiveItems && !ServerListView.Items.Contains(item))
                    ServerListView.Items.Add(item);

                ServerListView.SetItemToServer(item, server);

                item.SubItems[0].Text = ServerUtil.GetFilteredHostname(server.Hostname);
                item.SubItems[1].Text = ServerUtil.GetPrettyMapName(server.MapName);
                item.SubItems[2].Text = ServerUtil.GetPlayersString(server);
                item.SubItems[3].Text = server.GameType.ToUpper();
                item.SubItems[4].Text = "Pinging...";
                item.SubItems[5].Text = ServerUtil.GetIpAndPort(server);

                // Update the item in the list view.
                 // TODO TEMP: item.ListView?.Refresh();

                //UpdatePlayersListViewAndLabel(server);

                //PingServer(server, item);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }


        }

        private async Task<long> PingServer(Server server, ListViewItem item)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            if (string.IsNullOrWhiteSpace(server.Ip))
                throw new Exception("Server IP was null or empty.");

            try
            {
                using (var pinger = new Ping())
                {
                    var pingBuffer = GetPingBuffer();
                    var response = await pinger.SendPingAsync(server.Ip, 1500, pingBuffer, PingOptions);

                    if (IsDisposed || Disposing)
                        return response?.RoundtripTime ?? -1;

                    BeginInvoke((MethodInvoker)delegate
                    {
                        if (item.SubItems.Count > 4)
                            item.SubItems[4].Text = (response.Status == IPStatus.Success) ? response.RoundtripTime.ToString("N0") : "*";
                    });

                    return response?.RoundtripTime ?? -1;
                }
            }
            catch (ObjectDisposedException)
            {
                // Form was disposed while ping was in progress
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }

            return -1;
        }

        private async Task RefreshAllServers()
        {
            Console.WriteLine(nameof(RefreshAllServers));
            if (string.IsNullOrWhiteSpace(GameName) || string.IsNullOrWhiteSpace(GameVersion))
                return;

           // OnRefresh();

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


            // TODO: If currently only showing favorites - only refresh favorites:

            if (ServerListFilter.OnlyFavorites)
            {
                // filter infos to only show favorites.

                infos.Servers = infos.Servers
                    .Where(x => SettingsExt.IsFavoriteServer(x))
                    .ToList();

            }

            await RefreshAllServers(infos);
        }

        /// <summary>
        /// Refreshs all servers and changes UI elements, must be invoked if ran on another thread.
        /// </summary>
        /// <param name="infos"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private async Task RefreshAllServers(MasterServerInfo infos)
        {
            if (infos is null)
                throw new ArgumentNullException(nameof(infos));

            if (infos?.Servers.Count < 1)
                return;

            Console.WriteLine(nameof(RefreshAllServers) + " infos count: " + infos.Servers.Count);

            OnRefresh();

            for (int i = 0; i < infos.Servers.Count; i++)
            {
                var server = infos.Servers[i];

                var shouldFilter = ServerListFilter?.ShouldFilter(server) ?? false;

                Console.WriteLine($"{server.Hostname}: ShouldFilter: {shouldFilter}");

                AddOrUpdateServerListItem(server, !shouldFilter);
            }

           // UpdateListedServersCountLabel();

            var pingTasks = new List<Task>(ServerListFilter.ListViewItems.Count);

            for (int i = 0; i < ServerListFilter.ListViewItems.Count; i++)
            {
                var item = ServerListFilter.ListViewItems[i];

                var server = ServerListView.GetServer(item);

                if (server is null)
                    continue;

                pingTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var latency = await PingServer(server, item);

                        if (!ServerListFilter.ShouldFilter(server, (int)latency))
                            return;

                        if (ServerListFilter.ListView.IsDisposed || ServerListFilter.ListView.Disposing)
                            return;

                        ServerListFilter.ListView.BeginInvoke((MethodInvoker)delegate
                        {
                            try
                            {
                                ServerListFilter.ListView.Items.Remove(item);
                                // UpdateListedServersCountLabel();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                Log.WriteLine(ex.ToString());
                            }
                        });


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Log.WriteLine(ex.ToString());
                    }
                }));

            }

            await Task.WhenAll(pingTasks);
            UpdateListedServersCountLabel();

            ServerListView.Sort();
        }

        private void UpdateListedServersCountLabel()
        {
            try
            {
                var countLblTxt = StringBuilderCache.GetStringAndRelease(StringBuilderCache.Acquire(7)
               .Clear()
               .Append(ServerListFilter.ListView.Items.Count.ToString("N0"))
               .Append("/")
               .Append(ServerListFilter.ListViewItems.Count.ToString("N0")));

                if (CountServersLabel.InvokeRequired)
                    CountServersLabel.BeginInvoke((MethodInvoker)delegate { CountServersLabel.Text = countLblTxt; });
                else CountServersLabel.Text = countLblTxt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteLine(ex.ToString());
            }
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

        private void HideNoPingCheckbox_CheckedChanged(object sender, EventArgs e)
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

        private void SearchFilterTextbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // we could add another property that is just a single string
                // i.e "Filter" - then all the logic
                // can be handled in the actual filter, rather than having to 
                // set each one of these to the same value lol

                ServerListFilter.GameTypeFilter = SearchFilterTextbox.Text;
                ServerListFilter.MapNameFilter = SearchFilterTextbox.Text;
                ServerListFilter.HostnameFilter = SearchFilterTextbox.Text;
                ServerListFilter.IpAddressFilter = SearchFilterTextbox.Text;
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

            var selected = GameVersionBox?.SelectedItem?.ToString() ?? string.Empty;

            if (!selected.Contains("(") || !selected.Contains(")"))
                return;


            Task.Run(() =>
            {
                try
                {
                    var sb = StringBuilderCache.Acquire(selected.Length);
                    sb
                        .Append(selected)
                        .Replace(")", string.Empty)
                        .Replace(":", string.Empty);


                    var split = StringBuilderCache.GetStringAndRelease(sb).Split('(');

                    var gameVersion = split[1];
                    var gameName = split[0].Trim().ToLower();

                    GameVersion = gameVersion;
                    GameName = gameName.ToLower();

                    BeginInvoke((MethodInvoker)delegate
                    {
                        try { CoDPictureBox.Image = GameName == "coduo" ? Properties.Resources.CoDUO : Properties.Resources.CoD1; }
                        catch (Exception ex)
                        {
                            Log.WriteLine(ex.ToString());
                            Console.WriteLine(ex.ToString());
                        }
                    });

                    if (HasLoaded)
                        BeginInvoke((MethodInvoker)async delegate
                        {
                            ClearServerList();
                            await RefreshAllServers();
                        });
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    Console.WriteLine(ex.ToString());
                }
            });
        }

        /// <summary>
        /// Called when a refresh occurs. If a server is specified, it was a single server refreshed. If server is null, it was the master list.
        /// </summary>
        /// <param name="server"></param>
        private void OnRefresh(Server server = null)
        {
            try { ToggleControl(GameVersionBox, 5f); }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            try { ToggleControl(RefreshButton, 5f); }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            try { ToggleControl(RefreshServerButton, 5f); }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }

            // update selected server by getting the "new" server with the same ID:
            // may not work/be uyseless - todo: testing.
            if (SelectedServer != null)
            {
                var updated = ServerListView.GetServer(SelectedServer.Id);


                if (updated != null)
                    SelectedServer = updated;

            }

        }

        private void UpdatePlayersListViewAndLabel(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            PlayerListView.Items.Clear();

            if (server.PlayerInfo is null)
            {
                PlayerCountLabel.Text = ServerUtil.GetPlayersString(server);
                return;
            }

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
                    // Ensure name matches.
                    if ((player.Ping <= 0 || player.Ping >= 999) && player.Name.StartsWith("bot", StringComparison.OrdinalIgnoreCase))
                        continue;
                }

                // Yes, we filter carats twice. Players names may contain two, like: ^^22PlayerName.
                var item = new ListViewItem(filterNames ? ServerUtil.FilterCaratColors(ServerUtil.FilterCaratColors(player.Name)) : player.Name);
                item.SubItems.Add(player.Score.ToString("N0"));
                item.SubItems.Add(player.Ping.ToString("N0"));

                PlayerListView.Items.Add(item);
            }

            PlayerCountLabel.Text = ServerUtil.GetPlayersString(server);

        }

        private Task MapImageLoadTask { get; set; }
        private CancellationTokenSource MapImageLoadCancellationToken { get; set; }

        private void ServerListView_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ServerListView?.SelectedItems == null || ServerListView.SelectedItems.Count < 1)
                return;

            var server = ServerListView.GetServer(ServerListView.SelectedItems[0]);

            if (server is null)
                return;

            // Handle setting properties:

            SelectedServer = server;

            FavoriteServerCheckbox.Checked = SettingsExt.IsFavoriteServer(server);

            MapNameLabel.Text = ServerUtil.GetPrettyMapName(server.MapName);

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
                // Cancel and dispose the existing token source
                MapImageLoadCancellationToken?.Cancel();
                MapImageLoadCancellationToken?.Dispose();
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
            catch (TaskCanceledException)
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

        private void ClearServerList()
        {
            ServerListFilter?.ListViewItems?.Clear();
            ServerListView?.Items?.Clear();
            SelectedServer = null;

            ClearCurrentMap();
            ClearPlayersList();


            UpdateListedServersCountLabel();
        }

        private void ClearCurrentMap()
        {
            MapImageBox.Image = null;
            MapNameLabel.Text = string.Empty;
        }

        private void ClearPlayersList()
        {
            PlayerListView.Items.Clear();
            PlayerCountLabel.Text = "0/0";
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


        private void ConnectToServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            var mainForm = GetInstance<MainForm>();

            if (mainForm is null || mainForm.IsDisposed || mainForm.Disposing)
                return;

            var sb = Pool.Get<StringBuilder>();

            try
            {
                mainForm.StartGame(false, sb
                .Clear()
                .Append(" +connect ")
                .Append(server.Ip)
                .Append(":")
                .Append(server.Port)
                .ToString());
            }
            finally { Pool.Free(ref sb); }
        }

        private void ServerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selectedItem = (sender as ListView).GetItemAt(e.X, e.Y);

            if (selectedItem == null || ProcessExtension.IsAnyCoDProcessRunning())
                return;

            var server = ServerListView.GetServer(selectedItem);

            if (server is null)
                return;

            // Improve this message:
            var msgBox = MessageBox.Show("Would you like to connect to this server?" + Environment.NewLine + ServerUtil.GetFilteredHostname(server.Hostname) + " (" + server.Ip + ": " + server.Port + ")", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (msgBox != DialogResult.Yes)
                return;

            ConnectToServer(server);
        }

        private void ServersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.ServerListGameIndex = GameVersionBox.SelectedIndex;
            Settings.ServerListHideEmpty = HideEmptyCheckbox.Checked;
            Settings.ServerListHideNoPing = HideNoPingCheckbox.Checked;
            Settings.ServerListMaxPing = (int)MaxPingNumeric.Value;
            Settings.ServerListAutoRefreshSetting = AutoRefreshSettingComboBox.SelectedIndex;
            Settings.ServerListFilterBots = FilterBotPlayersCheckbox.Checked;
            Settings.ServerListFilterPlayerNames = FilterPlayerNamesCheckbox.Checked;

            // Hide the form instead of closing it, so we don't allow the user
            // To repeatedly open & close and refresh the server list too quickly.
            // Also inadvertently helps with performance.
            // CloseReason must be UserClosing, otherwise we should not interrupt the closing call.

            if (e.CloseReason != CloseReason.UserClosing)
            {
                // Cleanup resources when actually closing
                MapImageLoadCancellationToken?.Cancel();
                MapImageLoadCancellationToken?.Dispose();
                MapImageLoadCancellationToken = null;
                return;
            }

            e.Cancel = true;
            Hide();
        }

        private void FilterPlayerNamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            UpdatePlayersListViewAndLabel(SelectedServer);
        }

        private void FilterBotPlayersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            UpdatePlayersListViewAndLabel(SelectedServer);
        }

        private void RefreshServerButton_Click(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            Task.Run(async () =>
            {
                await RefreshServer(SelectedServer);
                BeginMapImageLoad(SelectedServer.MapName);
            });
        }

        private async Task RefreshServer(Server server)
        {
            if (server is null)
                throw new ArgumentNullException(nameof(server));

            OnRefresh(server);

            // Refresh the server's info.
            var newInfo = await CodPmApi.GetServer(server.Ip, server.Port);

            if (newInfo?.Server is null)
            {
                Log.WriteLine($"Failed to refresh server {server.Ip}:{server.Port} - API returned null");
                return;
            }

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
                PlayerInfo = newInfo.PlayerInfo,
                Clients = newInfo.Server.Clients
            };

            var shouldFilter = ServerListFilter?.ShouldFilter(newServer) ?? false;


            AddOrUpdateServerListItem(newServer, !shouldFilter);

            var item = ServerListView.GetItem(newServer.Id);
            if (item != null)
                await PingServer(newServer, item);
        }

        private void FilterBotServersCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Settings.ServerListFilterBotServers = FilterBotServersCheckbox.Checked;

                ServerListFilter.FilterBotServers = FilterBotServersCheckbox.Checked;
                ServerListFilter.ApplyFilters();
                UpdateListedServersCountLabel();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void ConnectServerButton_Click(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            ConnectToServer(SelectedServer);
        }

        private void FavoriteServerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedServer is null)
                return;

            if (FavoriteServerCheckbox.Checked)
                SettingsExt.AddFavoriteServer(SelectedServer);
            else SettingsExt.RemoveFavoriteServer(SelectedServer);

            GetInstance<MainForm>()?.UpdateStartGameButtonContextOptions();

        }

        private void FavoritesButton_Click(object sender, EventArgs e)
        {
            if (ServerListFilter is null)
                return;

            ServerListFilter.OnlyFavorites = !ServerListFilter.OnlyFavorites;
            Settings.ServerListShowFavorites = ServerListFilter.OnlyFavorites;

            UpdateFavoritesButtonText();
            UpdateFormText();

            ServerListFilter.ApplyFilters();
            UpdateListedServersCountLabel();
        }

        private void UpdateFavoritesButtonText() =>
            FavoritesButton.Text = ServerListFilter.OnlyFavorites ? "Show All" : "Show Favorites";


        private void UpdateFormText() =>
            Text = "Server List" + (ServerListFilter.OnlyFavorites ? " (Favorites)" : string.Empty);


        private void ServerListView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || ServerListView.Items.Count < 1)
            {
                contextMenuStrip1.Hide();
                return;
            }


            var hitTestInfo = ServerListView.HitTest(e.X, e.Y);
            var clickedItem = hitTestInfo.SubItem;
            var clickedSubItemIndex = hitTestInfo.Item?.SubItems.IndexOf(hitTestInfo.SubItem) ?? -1;

            if (clickedItem is null || clickedSubItemIndex < 0)
            {
                contextMenuStrip1.Hide();
                return;
            }

            contextMenuStrip1.Items[0].Text = "Copy " + ServerListView.Columns[clickedSubItemIndex].Text;

            Clipboard.SetText(clickedItem.Text);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ServerListView?.Items?.Count < 1 || (sender as ContextMenuStrip).Items[0].Text.Contains("COLUMN_NAME", StringComparison.OrdinalIgnoreCase))
                e.Cancel = true;
        }

        private void RefreshSelectedCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            RefreshTimer.Enabled = AutoRefreshCheckBox.Checked;

            if (AutoRefreshSettingComboBox.SelectedIndex <= 0 && AutoRefreshCheckBox.Checked)
                AutoRefreshSettingComboBox.SelectedIndex = 1;
            else if (!AutoRefreshCheckBox.Checked)
                AutoRefreshSettingComboBox.SelectedIndex = 0;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            var selIndex = AutoRefreshSettingComboBox.SelectedIndex;

            if (selIndex <= 0)
                return;

            Task.Run(async () =>
            {
                try
                {
                    if (selIndex == 2)
                    {
                        await Task.Run(async () =>
                        {
                            if (InvokeRequired)
                            {
                                await Task.Factory.FromAsync(
                                    BeginInvoke((MethodInvoker)async delegate { await RefreshAllServers(); }),
                                    EndInvoke);
                            }
                            else
                                await RefreshAllServers();
                        });
                    }
                    else if (selIndex == 1 && SelectedServer != null)
                    {
                        await RefreshServer(SelectedServer);
                    }

                    // Update UI after refresh completes
                    if (SelectedServer != null && !IsDisposed && !Disposing)
                    {
                        BeginInvoke((MethodInvoker)delegate
                        {
                            try
                            {
                                MapNameLabel.Text = ServerUtil.GetPrettyMapName(SelectedServer.MapName);
                                UpdatePlayersListViewAndLabel(SelectedServer);
                                BeginMapImageLoad(SelectedServer.MapName);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                Log.WriteLine(ex.ToString());
                            }
                        });
                    }
                }
                catch (ObjectDisposedException)
                {
                    // Form was disposed
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Log.WriteLine(ex.ToString());
                }
            });
        }

        private void AutoRefreshSettingComboBox_SelectedIndexChanged(object sender, EventArgs e) =>
            AutoRefreshCheckBox.Checked = AutoRefreshSettingComboBox.SelectedIndex > 0;
        
    }
}
