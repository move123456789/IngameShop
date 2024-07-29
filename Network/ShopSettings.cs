using SimpleNetworkEvents;



namespace IngameShop.Network
{
    internal class ShopSettings : SimpleEvent<ShopSettings>
    {
        public string MaxShopsAllowed { get; set; }
        public override void OnReceived()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg($"Recived MaxShops Event, Max Shops Allowed: {MaxShopsAllowed}"); }
                int maxAllowedShops;
                if (int.TryParse(MaxShopsAllowed, out maxAllowedShops))
                {
                    Network.Manager.MaxShopsAllowed = maxAllowedShops;
                }
                
            }
        }
    }
}
