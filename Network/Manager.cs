

namespace IngameShop.Network
{
    public class Manager
    {
        public static void RegisterEvents()
        {
            // Network Stuff
            if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer || Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                Misc.Msg("Registerd Events");
                SimpleNetworkEvents.EventDispatcher.RegisterEvent<Network.SpawnShop>();
                SimpleNetworkEvents.EventDispatcher.RegisterEvent<Network.ShopSettings>();
                SimpleNetworkEvents.EventDispatcher.RegisterEvent<Network.JoinedServer>();
                SimpleNetworkEvents.EventDispatcher.RegisterEvent<Network.SyncSingeShop>();
            }
        }
        public static void UnregisterEvents()
        {
            // Network Stuff
            if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer || Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                Misc.Msg("Unregisterd Events");
                SimpleNetworkEvents.EventDispatcher.UnregisterEvent<Network.SpawnShop>();
                SimpleNetworkEvents.EventDispatcher.UnregisterEvent<Network.ShopSettings>();
                SimpleNetworkEvents.EventDispatcher.UnregisterEvent<Network.JoinedServer>();
                SimpleNetworkEvents.EventDispatcher.RegisterEvent<Network.SyncSingeShop>();
            }
        }

        public static void SendJoinedServerEvent()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.JoinedServer
                {
                    SenderName = Misc.GetLocalPlayerUsername(),
                    SenderId = stringSteamId,

                });
            }
        }

        public static int? MaxShopsAllowed = null;
    }
}
