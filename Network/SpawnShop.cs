using SimpleNetworkEvents;
using Sons.Multiplayer;
using TheForest.Utils;
using UnityEngine;
using Steamworks;



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
            Misc.Msg("Recived Network SpawnShop Event");
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
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[SpawnShop] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[SpawnShop] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }
                
            }
            
            if (NameOfObject.ToLower() == "shop")
            {
                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg($"[SpawnShop] [OnReceived()] Spawning Prefab From Network Event"); }
                ulong resultSteamID;
                if (!ulong.TryParse(Sender, out resultSteamID)) { Misc.Msg($"Failed To Parse SenderId: {Sender} To String"); return; }
                Misc.Msg($"[SpawnShop] [OnReceived()] Spawn Shop To STRING Pos: {Vector3Position}, STRING Rot: {QuaternionRotation}");
                Vector3 pos = Network.CustomSerializable.Vector3FromString(Vector3Position);
                Quaternion rot = Network.CustomSerializable.QuaternionFromString(QuaternionRotation);
                Misc.Msg($"[SpawnShop] [OnReceived()] Spawn Shop To Pos: {pos}, Rot: {rot}");
                ShopPrefabs.SpawnShopPrefab(pos, rot, false, SenderName, resultSteamID, UniqueId);
            }
            else
            {
                Misc.Msg("[SpawnShop] [OnReceived()] NameOfObject Not Found");
            }
        }
    }
}
