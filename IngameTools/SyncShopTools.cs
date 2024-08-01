using IngameShop.Network;
using TheForest.Utils;
using UnityEngine;


namespace IngameShop.IngameTools
{
    public static class SyncShopTools
    {
        public static GameObject GetLookingAtShop()
        {
            Transform transform = LocalPlayer._instance._mainCam.transform;
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, transform.forward, out raycastHit, 25f, LayerMask.GetMask(new string[]
            {
                "Default"
            }));
            if (raycastHit.collider == null || raycastHit.collider.transform.root == null) { return null; }
            if (raycastHit.collider.transform.root.name.Contains("Shop"))
            {
                GameObject shop = raycastHit.collider.transform.root.gameObject;
                return shop;

            }
            return null;
        }
        public static string SendSyncEvent(GameObject shop)
        {
            Misc.Msg("[TestSyncShop] [SendSyncEvent] Sending Event");
            if (shop != null)
            {
                if (CommonSendSyncEvent(shop)) { return "SUCCESS Sending Event"; }
                else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
            } else { return "FAILED, Input GameObject is empty"; }
        }
        public static string SendSyncEventLookingAt(ShopEventType eventType)
        {
            Misc.Msg("[SyncShopTools] [SendSyncEventLookingAt] Sending Event");
            Transform transform = LocalPlayer._instance._mainCam.transform;
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, transform.forward, out raycastHit, 25f, LayerMask.GetMask(new string[]
            {
                "Default"
            }));
            if (raycastHit.collider == null || raycastHit.collider.transform.root == null) { return "FAILED, No Valid Hits"; }
            if (raycastHit.collider.transform.root.name.Contains("Shop"))
            {
                GameObject shop = raycastHit.collider.transform.root.gameObject;
                switch (eventType)
                {
                    case ShopEventType.Sync:
                        if (CommonSendSyncEvent(shop)) { return "SUCCESS Sending Event"; }
                        else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
                    case ShopEventType.AllDicts:
                        if (CommonSendSyncDictEvent(shop, ShopEventType.AllDicts)) { return "SUCCESS Sending Event"; }
                        else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
                    case ShopEventType.InventoryItemsDict:
                        if (CommonSendSyncDictEvent(shop, ShopEventType.InventoryItemsDict)) { return "SUCCESS Sending Event"; }
                        else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
                    case ShopEventType.PurchasedItemsDict:
                        if (CommonSendSyncDictEvent(shop, ShopEventType.PurchasedItemsDict)) { return "SUCCESS Sending Event"; }
                        else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
                    default:
                        return "FAILED, Not Valid ShopEvent Given";
                }

            }
            else { return "FAILED, Shop Not Found In Hit"; }
        }

        public static bool CommonSendSyncEvent(GameObject shop)
        {
            Mono.ShopGeneric shopGeneric = shop.GetComponent<Mono.ShopGeneric>();
            if (shopGeneric == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] ShopGeneric Component Not Found On Object, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.UniqueId)) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] UniqueId Null/Empty, Aborting"); return false; }
            if (shopGeneric.gameObject.transform.position == Vector3.zero) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Shop Pos == Vector3.zero, Aborting"); return false; }
            Mono.ShopInventory shopInventory = shop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Shop Inventory Is Null, Aborting"); return false; }
            (Dictionary<int, int> purchashedItemsDict, Dictionary<int, int> heldInventoryDict, Dictionary<int, int> pricesDict) = shopInventory.GetAllDicts();
            if (string.IsNullOrEmpty(shopGeneric.SteamId.ToString())) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] SteamId Null/Empty, Aborting"); return false; }
            if (shopGeneric.SteamId.ToString() == "0") { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] SteamId 0, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.GetOwner())) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Owner Name Null/Empty, Aborting"); return false; }
            SyncSingeShopEvent.RaiseSyncSingeShopEvent(
                uniqueId: shopGeneric.UniqueId,
                pos: shopGeneric.gameObject.transform.position,
                rot: shopGeneric.gameObject.transform.rotation,
                sPos: null,
                sRot: null,
                purchasedItems: purchashedItemsDict,
                inventoryItems: heldInventoryDict,
                prices: pricesDict,
                shopOwnerId: shopGeneric.SteamId.ToString(),
                shopOwnerName: shopGeneric.GetOwner(),
                toPlayerId: null,
                toPlayerName: null
                );
            return true;
        }

        public static bool CommonSendSyncEvent(GameObject shop, string toPlayerSteamId)  // Host Sends This Event On Player Join From public static bool CommonSendSyncEvent(string uniqueId, string playerSteamId
        {
            if (string.IsNullOrEmpty(toPlayerSteamId)) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] To Player SteamId Null/Empty, Aborting"); }
            if (string.IsNullOrWhiteSpace(toPlayerSteamId)) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] To Player SteamId Null/WhiteSpace, Aborting"); }
            Mono.ShopGeneric shopGeneric = shop.GetComponent<Mono.ShopGeneric>();
            if (shopGeneric == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] ShopGeneric Component Not Found On Object, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.UniqueId)) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] UniqueId Null/Empty, Aborting"); return false; }
            if (shopGeneric.gameObject.transform.position == Vector3.zero) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Shop Pos == Vector3.zero, Aborting"); return false; }
            Mono.ShopInventory shopInventory = shop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Shop Inventory Is Null, Aborting"); return false; }
            (Dictionary<int, int> purchashedItemsDict, Dictionary<int, int> heldInventoryDict, Dictionary<int, int> pricesDict) = shopInventory.GetAllDicts();
            if (string.IsNullOrEmpty(shopGeneric.SteamId.ToString())) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] SteamId Null/Empty, Aborting"); return false; }
            if (shopGeneric.SteamId.ToString() == "0") { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] SteamId 0, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.GetOwner())) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Owner Name Null/Empty, Aborting"); return false; }
            SyncSingeShopEvent.RaiseSyncSingeShopEvent(
                uniqueId: shopGeneric.UniqueId,
                pos: shopGeneric.gameObject.transform.position,
                rot: shopGeneric.gameObject.transform.rotation,
                sPos: null,
                sRot: null,
                purchasedItems: purchashedItemsDict,
                inventoryItems: heldInventoryDict,
                prices: pricesDict,
                shopOwnerId: shopGeneric.SteamId.ToString(),
                shopOwnerName: shopGeneric.GetOwner(),
                toPlayerId: toPlayerSteamId,
                toPlayerName: null
                );
            return true;
        }

        public static bool CommonSendSyncEvent(string uniqueId, string toPlayerSteamId) // Host Sends This Event On Player Join
        {
            GameObject shop = ShopPrefabs.FindShopByUniqueId(uniqueId);
            if (shop == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncEvent()] Shop Not Found From UniqueId, Aborting"); return false; }
            bool status = CommonSendSyncEvent(shop, toPlayerSteamId);
            return status;
        }

        public static bool CommonSendSyncDictEvent(string uniqueId, ShopEventType eventType)
        {
            GameObject shop = ShopPrefabs.FindShopByUniqueId(uniqueId);
            bool sucess = CommonSendSyncDictEvent(shop, eventType);
            return sucess;
        }

        public static bool CommonSendSyncDictEvent(GameObject shop, ShopEventType eventType)
        {
            Mono.ShopGeneric shopGeneric = shop.GetComponent<Mono.ShopGeneric>();
            if (shopGeneric == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncUpdateSingeShopDictEvent()] ShopGeneric Component Not Found On Object, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.UniqueId)) { Misc.Msg("[SyncShopTools] [CommonSendSyncUpdateSingeShopDictEvent()] UniqueId Null/Empty, Aborting"); return false; }
            Mono.ShopInventory shopInventory = shop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[SyncShopTools] [CommonSendSyncUpdateSingeShopDictEvent()] Shop Inventory Is Null, Aborting"); return false; }
            (Dictionary<int, int> purchashedItemsDict, Dictionary<int, int> heldInventoryDict, Dictionary<int, int> pricesDict) = shopInventory.GetAllDicts();
            switch (eventType)
            {
                case ShopEventType.AllDicts:
                    SyncSingeShopEvent.RaiseUpdateSingeShopDictEvent(
                        uniqueId: shopGeneric.UniqueId,
                        purchasedItems: purchashedItemsDict,
                        inventoryItems: heldInventoryDict,
                        prices: pricesDict,
                        toPlayerId: null,
                        toPlayerName: null
                        );
                    break;
                case ShopEventType.PurchasedItemsDict:
                    SyncSingeShopEvent.RaiseUpdateSingeShopPurchasedItemsDictEvent(
                        uniqueId: shopGeneric.UniqueId,
                        purchasedItems: purchashedItemsDict,
                        toPlayerId: null,
                        toPlayerName: null
                        );
                    break;
                case ShopEventType.InventoryItemsDict:
                    SyncSingeShopEvent.RaiseUpdateSingeShopInventoryItemsDictEvent(
                        uniqueId: shopGeneric.UniqueId,
                        inventoryItems: heldInventoryDict,
                        toPlayerId: null,
                        toPlayerName: null
                        );
                    break;
                case ShopEventType.PricesDict:
                    SyncSingeShopEvent.RaiseUpdateSingeShopPricesDictEvent(
                        uniqueId: shopGeneric.UniqueId,
                        prices: pricesDict,
                        toPlayerId: null,
                        toPlayerName: null
                        );
                    break;
                default:
                    return false;
            }
            return true;
        }

        public enum ShopEventType
        {
            AllDicts,
            PurchasedItemsDict,
            InventoryItemsDict,
            PricesDict,
            Sync
        }
    }
}
