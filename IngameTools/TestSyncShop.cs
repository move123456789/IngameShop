using IngameShop.Network;
using TheForest.Utils;
using UnityEngine;


namespace IngameShop.IngameTools
{
    public static class TestSyncShop
    {
        public static string SendSyncEvent(GameObject shop)
        {
            Misc.Msg("[TestSyncShop] [SendSyncEvent] Sending Event");
            if (shop != null)
            {
                if (CommonSendSyncEvent(shop)) { return "SUCCESS Sending Event"; }
                else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }
            } else { return "FAILED, Input GameObject is empty"; }
        }
        public static string SendSyncEventLookingAt()
        {
            Misc.Msg("[TestSyncShop] [SendSyncEventLookingAt] Sending Event");
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
                if (CommonSendSyncEvent(shop)) { return "SUCCESS Sending Event"; }
                else { return "FAILED, Something went wrong in CommonSendSyncEvent"; }

            }
            else { return "FAILED, Shop Not Found In Hit"; }
        }

        public static bool CommonSendSyncEvent(GameObject shop)
        {
            Mono.ShopGeneric shopGeneric = shop.GetComponent<Mono.ShopGeneric>();
            if (shopGeneric == null) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] ShopGeneric Component Not Found On Object, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.UniqueId)) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] UniqueId Null/Empty, Aborting"); return false; }
            if (shopGeneric.gameObject.transform.position == Vector3.zero) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] Shop Pos == Vector3.zero, Aborting"); return false; }
            Mono.ShopInventory shopInventory = shop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] Shop Inventory Is Null, Aborting"); return false; }
            (Dictionary<int, int> purchashedItemsDict, Dictionary<int, int> heldInventoryDict, Dictionary<int, int> pricesDict) = shopInventory.GetAllDicts();
            if (string.IsNullOrEmpty(shopGeneric.SteamId.ToString())) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] SteamId Null/Empty, Aborting"); return false; }
            if (shopGeneric.SteamId.ToString() == "0") { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] SteamId 0, Aborting"); return false; }
            if (string.IsNullOrEmpty(shopGeneric.GetOwner())) { Misc.Msg("[TestSyncShop] [CommonSendSyncEvent()] Owner Name Null/Empty, Aborting"); return false; }
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
    }
}
