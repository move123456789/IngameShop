
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
                Misc.Msg("Player Joined, Sending ShopSettings Event");
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.ShopSettings
                {
                    MaxShopsAllowed = Config.MaxShops.Value.ToString(),

                });
            }
        }
    }
}
