using SimpleNetworkEvents;
using Sons.Multiplayer;
using TheForest.Utils;



namespace IngameShop.Network
{
    public class SpawnShop : SimpleEvent<SpawnShop>
    {
        public string NameOfObject { get; set; }

        public string Vector3Position { get; set; }
        public string QuaternionRotation { get; set; }
        public string UniqueId { get; set; }
        public string Sender { get; set; }
        public string SenderName { get; set; }

        public override void OnReceived()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.SinglePlayer)
            {
                Misc.Msg("[SpawnShop] [OnReceived()] Skipped Reciving Network Event On SinglePlayer");
                return;
            }
            if (ShopPrefabs.shop == null)
            {
                Misc.Msg("[SpawnShop] [OnReceived()] Shop Prefab has not been created yet, skipped");
                return;
            }
            ulong? mySteamId = MultiplayerUtilities.GetSteamId(LocalPlayer.Entity);
            ulong zero = 0;
            if (mySteamId == null || mySteamId == zero)
            {
                if (Sender == mySteamId.ToString())
                {
                    Misc.Msg("[SpawnShop] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }
            }
            else
            {
                Misc.Msg("[SpawnShop] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            
            if (NameOfObject.ToLower() == "shop")
            {
                ShopPrefabs.SpawnShopPrefab()
            }
        }
    }
}
