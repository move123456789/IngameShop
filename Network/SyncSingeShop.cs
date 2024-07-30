using SimpleNetworkEvents;
using UnityEngine;


namespace IngameShop.Network
{
    public class SyncSingeShop : SimpleEvent<SyncSingeShop>
    {
        public string UniqueId { get; set; }  // UNIQUE GUID
        public string Sender { get; set; }  // STEAM ID
        public string SenderName { get; set; }  // INGAME NAME
        public string Vector3Position { get; set; }  // SHOP LOCATION
        public string QuaternionRotation { get; set; }  // SHOP ROTATION
        public Dictionary<int, int> PurchashedItems { get; set; }  // All Items Purchashed From Shop (ItemId, Amount)
        public Dictionary<int, int> HeldInventory { get; set; }  // All Items That Are Placed In The Shop (ItemId, Amount)
        public Dictionary<int, int> Prices { get; set; }  // All Item Prices (ItemId, ItemIdToPay)
        public string ShopOwner { get; set; }  // Steam ID Owner Of Shop
        public string ShopOwnerName { get; set; }  // Name Owner Of Shop
        public string ToPlayerName { get; set; }  // To Spesific Player
        public string ToPlayerId { get; set; }  // To Spesific Player
        public override void OnReceived()
        {
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[SyncSingeShop] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[SyncSingeShop] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }

            }
            if (ToPlayerName != "")
            {
                if (ToPlayerName != Misc.GetLocalPlayerUsername())
                {
                    Misc.Msg("[SyncSingeShop] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            if (ToPlayerId != "")
            {
                if (ToPlayerId != sSteamId)
                {
                    Misc.Msg("[SyncSingeShop] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            GameObject syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null)
            {
                Misc.Msg("[SyncSingeShop] [OnReceived()] Could Not Found Shop By Unique Id, Creating One");
                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg($"[SyncSingeShop] [OnReceived()] Spawning Prefab From Network Event"); }
                ulong resultSteamID;
                if (!ulong.TryParse(ShopOwner, out resultSteamID)) { Misc.Msg($"Failed To Parse SenderId: {Sender} To String"); return; }
                Misc.Msg($"[SyncSingeShop] [OnReceived()] Spawn Shop To STRING Pos: {Vector3Position}, STRING Rot: {QuaternionRotation}");
                Vector3 pos = Network.CustomSerializable.Vector3FromString(Vector3Position);
                Quaternion rot = Network.CustomSerializable.QuaternionFromString(QuaternionRotation);
                Misc.Msg($"[SyncSingeShop] [OnReceived()] Spawn Shop To Pos: {pos}, Rot: {rot}");
                ShopPrefabs.SpawnShopPrefab(pos, rot, false, ShopOwnerName, resultSteamID, UniqueId);
            }
            syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null) { Misc.Msg("[SyncSingeShop] [OnReceived()] Could Still Not Find Shop By ID Aborting Event"); return; }
            if (syncShop != null)
            {
                Mono.ShopGeneric shopGeneric = syncShop.GetComponent<Mono.ShopGeneric>();
                if (shopGeneric == null) { Misc.Msg("[SyncSingeShop] [OnReceived()] ShopGeneric Component Not Found On Object, Aborting"); return; }
                shopGeneric.UpdateShopFromNetwork(ShopOwner, ShopOwnerName, UniqueId, Vector3Position, QuaternionRotation);
                Mono.ShopInventory shopInventory = syncShop.GetComponent<Mono.ShopInventory>();
                if (shopInventory == null) { Misc.Msg("[SyncSingeShop] [OnReceived()] ShopInventory Component Not Found On Object, Aborting"); return; }
                shopInventory.UpdateShopInventoryFromNetwork(PurchashedItems, HeldInventory, Prices);

            }
        }
    }
}
