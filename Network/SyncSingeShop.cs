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
                if (pos == Vector3.zero) { Misc.Msg("[SyncSingeShop] [OnReceived()] NetworkPos == Vector3.Zero, Aborting"); return; }
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
    public static class SyncSingeShopEvent
    {
        public static void RaiseSyncSingeShopEvent(string uniqueId, Vector3? pos, Quaternion? rot, string sPos, string sRot, Dictionary<int, int> purchasedItems, Dictionary<int, int> inventoryItems, Dictionary<int, int> prices, string shopOwnerId, string shopOwnerName, string toPlayerId = null, string toPlayerName = null)
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient || Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                string vector3Pos = null;
                string rotation = null;
                if (pos != null || pos != Vector3.zero)
                {
                    vector3Pos = Network.CustomSerializable.Vector3ToString((Vector3)pos);
                }
                if (rot != null || rot != Quaternion.identity)
                {
                    rotation = Network.CustomSerializable.QuaternionToString((Quaternion)rot);
                }
                if (string.IsNullOrEmpty(vector3Pos))
                {
                    if (!string.IsNullOrEmpty(sPos))
                    {
                        vector3Pos = sPos;
                    } else { Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] No Valid Vector3 Pos is Given, Aborting 1:{pos}, 2:{sPos}"); return; }
                }
                if (string.IsNullOrEmpty(rotation))
                {
                    if (!string.IsNullOrEmpty(sRot))
                    {
                        rotation = sRot;
                    }
                    else { Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] No Valid Rotation is Given, Aborting 1:{rot}, 2:{sRot}"); return; }
                }
                string sendToPlayerName = "";
                string sendToPlayerID = "";
                if (!string.IsNullOrEmpty(toPlayerName))
                {
                    sendToPlayerName = toPlayerName;
                }
                if (!string.IsNullOrEmpty(toPlayerId))
                {
                    sendToPlayerID = toPlayerId;
                }
                if (string.IsNullOrEmpty(shopOwnerId))
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] Owner SteamId Requried, Aborting"); return;
                }
                if (string.IsNullOrEmpty(shopOwnerName))
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] Owner Name Requried, Aborting"); return;
                }
                if (purchasedItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] PurchasedItems, Aborting"); return;
                }
                if (inventoryItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] InventoryItems, Aborting"); return;
                }
                if (prices == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseSyncSingeShopEvent] Prices, Aborting"); return;
                }
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.SyncSingeShop
                {
                    UniqueId = uniqueId,
                    Sender = stringSteamId,
                    SenderName = Misc.GetLocalPlayerUsername(),
                    Vector3Position = vector3Pos,
                    QuaternionRotation = rotation,
                    PurchashedItems = purchasedItems,
                    HeldInventory = inventoryItems,
                    Prices = prices,
                    ShopOwner = shopOwnerId,
                    ShopOwnerName = shopOwnerName,
                    ToPlayerName = sendToPlayerName,
                    ToPlayerId = sendToPlayerID,
                });
            }
        }

        public static void RaiseUpdateSingeShopDictEvent(string uniqueId, Dictionary<int, int> purchasedItems, Dictionary<int, int> inventoryItems, Dictionary<int, int> prices, string toPlayerId = null, string toPlayerName = null)
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient || Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                string sendToPlayerName = "";
                string sendToPlayerID = "";
                if (!string.IsNullOrEmpty(toPlayerName))
                {
                    sendToPlayerName = toPlayerName;
                }
                if (!string.IsNullOrEmpty(toPlayerId))
                {
                    sendToPlayerID = toPlayerId;
                }
                if (purchasedItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopDictEvent] PurchasedItems, Aborting"); return;
                }
                if (inventoryItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopDictEvent] InventoryItems, Aborting"); return;
                }
                if (prices == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopDictEvent] Prices, Aborting"); return;
                }
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.UpdateSingeShopDict
                {
                    UniqueId = uniqueId,
                    Sender = stringSteamId,
                    PurchashedItems = purchasedItems,
                    HeldInventory = inventoryItems,
                    Prices = prices,
                    ToPlayerName = sendToPlayerName,
                    ToPlayerId = sendToPlayerID,
                });
            }
        }

        public static void RaiseUpdateSingeShopPurchasedItemsDictEvent(string uniqueId, Dictionary<int, int> purchasedItems, string toPlayerId = null, string toPlayerName = null)
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient || Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                string sendToPlayerName = "";
                string sendToPlayerID = "";
                if (!string.IsNullOrEmpty(toPlayerName))
                {
                    sendToPlayerName = toPlayerName;
                }
                if (!string.IsNullOrEmpty(toPlayerId))
                {
                    sendToPlayerID = toPlayerId;
                }
                if (purchasedItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopPurchasedItemsDictEvent] PurchasedItems, Aborting"); return;
                }
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.UpdateSingeShopPurchasedItemsDict
                {
                    UniqueId = uniqueId,
                    Sender = stringSteamId,
                    PurchashedItems = purchasedItems,
                    ToPlayerName = sendToPlayerName,
                    ToPlayerId = sendToPlayerID,
                });
            }
        }

        public static void RaiseUpdateSingeShopInventoryItemsDictEvent(string uniqueId, Dictionary<int, int> inventoryItems, string toPlayerId = null, string toPlayerName = null)
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient || Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                string sendToPlayerName = "";
                string sendToPlayerID = "";
                if (!string.IsNullOrEmpty(toPlayerName))
                {
                    sendToPlayerName = toPlayerName;
                }
                if (!string.IsNullOrEmpty(toPlayerId))
                {
                    sendToPlayerID = toPlayerId;
                }
                if (inventoryItems == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopInventoryItemsDictEvent] InventoryItems, Aborting"); return;
                }
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.UpdateSingeShopInventoryItemsDict
                {
                    UniqueId = uniqueId,
                    Sender = stringSteamId,
                    HeldInventory = inventoryItems,
                    ToPlayerName = sendToPlayerName,
                    ToPlayerId = sendToPlayerID,
                });
            }
        }

        public static void RaiseUpdateSingeShopPricesDictEvent(string uniqueId, Dictionary<int, int> prices, string toPlayerId = null, string toPlayerName = null)
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient || Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
            {
                string sendToPlayerName = "";
                string sendToPlayerID = "";
                if (!string.IsNullOrEmpty(toPlayerName))
                {
                    sendToPlayerName = toPlayerName;
                }
                if (!string.IsNullOrEmpty(toPlayerId))
                {
                    sendToPlayerID = toPlayerId;
                }
                if (prices == null)
                {
                    Misc.Msg($"[SyncSingeShopEvent] [RaiseUpdateSingeShopDictEvent] Prices, Aborting"); return;
                }
                (ulong steamId, string stringSteamId) = Misc.MySteamId();
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.UpdateSingeShopPricesDict
                {
                    UniqueId = uniqueId,
                    Sender = stringSteamId,
                    Prices = prices,
                    ToPlayerName = sendToPlayerName,
                    ToPlayerId = sendToPlayerID,
                });
            }
        }
    }
}
