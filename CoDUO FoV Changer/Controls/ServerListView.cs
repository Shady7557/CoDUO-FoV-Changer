using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer.Controls
{
    public class ServerListView : ListView
    {
        private Dictionary<ListViewItem, int> ItemToServerID { get; set; } = new Dictionary<ListViewItem, int>();
        private Dictionary<int, ListViewItem> ServerIdToItem { get; set; } = new Dictionary<int, ListViewItem>();
        private Dictionary<int, Server> ServerIdToServer { get; set; } = new Dictionary<int, Server>();

        public Server GetServer(int serverId)
        {
            if (serverId < 0)
                throw new ArgumentOutOfRangeException(nameof(serverId));

            ServerIdToServer.TryGetValue(serverId, out var server);
            return server;
        }

        public Server GetServer(ListViewItem item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            ItemToServerID.TryGetValue(item, out var serverId);
            return GetServer(serverId);
        }

        public ListViewItem GetItem(int serverId)
        {
            if (serverId < 0)
                throw new ArgumentOutOfRangeException(nameof(serverId));

            ServerIdToItem.TryGetValue(serverId, out var item);
            return item;
        }

        public int GetServerId(ListViewItem item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            ItemToServerID.TryGetValue(item, out var serverId);
            return serverId;
        }

        public void SetItemToServer(ListViewItem item, Server server)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (server == null)
                throw new ArgumentNullException(nameof(server));

            ItemToServerID[item] = server.Id;
            ServerIdToItem[server.Id] = item;
            ServerIdToServer[server.Id] = server;
        }

    }
}
