
using SimpleNetworkEvents;

namespace IngameShop.Network
{
    internal class JoinedServer : SimpleEvent<JoinedServer>
    {
        public string SenderName { get; set; }
        public string SenderId { get; set; }
        public override void OnReceived()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                Misc.Msg("[JoinedServer] Player Joined, Sending ShopSettings Event");
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.ShopSettings
                {
                    MaxShopsAllowed = Config.MaxShops.Value.ToString(),

                });
                if (ShopPrefabs.spawnedShops.Count > 0)
                {
                    (ulong steamId, string stringSteamId) = Misc.MySteamId();
                    if (SenderId != stringSteamId)
                    {
                        Misc.Msg("[JoinedServer] Player Joined, Sending All Shops Sync Event");
                        foreach (var shop in ShopPrefabs.spawnedShops)
                        {
                            string uniqueId = shop.Key.ToString();
                            if (!string.IsNullOrEmpty(uniqueId))
                            {
                                IngameTools.SyncShopTools.CommonSendSyncEvent(uniqueId, SenderId);  // Sends Event To User Who Sent JoinedServer Event, Only If Host Recived Tho
                            }
                            else
                            {
                                Misc.Msg("[JoinedServer] UniqueId is Null/Empty Witch It Shoud Never Be!");
                            }
                        }
                    }
                    else { Misc.Msg("[JoinedServer] Got Joined Server Event From MySelf, while being host, Skipped"); }
                }
            }
        }
    }
}
