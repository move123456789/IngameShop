using TheForest.Utils;
using UnityEngine;
using static IngameShop.IngameTools.SyncShopTools;



namespace IngameShop.IngameTools
{
    public static class UETools
    {
        public static string SendJoinEvent()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                Network.Manager.SendJoinedServerEvent();
                return "Event Sent";
            }
            else
            {
                Misc.Msg("[UE Tools] [SendJoinEvent] Not Sending Event Since HostMode is Not MultiplayerClient");
                return "Failed To Send Event";
            }
        }
        public static GameObject GetLookinAtShop()
        {
            return SyncShopTools.GetLookingAtShop();
        }
        public static string SendSyncEvent(GameObject shop)
        {
            return SyncShopTools.SendSyncEvent(shop);
        }
        public static string SendSyncEventLookingAt(SyncShopTools.ShopEventType eventType)
        {
            return SyncShopTools.SendSyncEventLookingAt(eventType);
        }

        public static string CommonSendSyncDictEvent(GameObject shop, ShopEventType eventType)
        {
            bool status = SyncShopTools.CommonSendSyncDictEvent(shop, eventType);
            if (status) { return "Success"; }
            if (!status) { return "Failed"; }
            return "Failed";
        }

        public static Vector3 GetPlayerPosition()
        {
            return LocalPlayer.Transform.position;
        }
    }
}
