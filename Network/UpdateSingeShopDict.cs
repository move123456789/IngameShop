using SimpleNetworkEvents;
using UnityEngine;


namespace IngameShop.Network
{
    public class UpdateSingeShopDict : SimpleEvent<UpdateSingeShopDict>
    {
        public string UniqueId { get; set; }  // UNIQUE GUID
        public string Sender { get; set; }  // STEAM ID
        public Dictionary<int, int> PurchashedItems { get; set; }  // All Items Purchashed From Shop (ItemId, Amount)
        public Dictionary<int, int> HeldInventory { get; set; }  // All Items That Are Placed In The Shop (ItemId, Amount)
        public Dictionary<int, int> Prices { get; set; }  // All Item Prices (ItemId, ItemIdToPay)
        public string ToPlayerName { get; set; }  // To Spesific Player
        public string ToPlayerId { get; set; }  // To Spesific Player

        public override void OnReceived()
        {
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[UpdateSingeShopDict] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopDict] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }

            }
            if (ToPlayerName != "")
            {
                if (ToPlayerName != Misc.GetLocalPlayerUsername())
                {
                    Misc.Msg("[UpdateSingeShopDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            if (ToPlayerId != "")
            {
                if (ToPlayerId != sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }

            GameObject syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null)
            {
                Misc.Msg("[UpdateSingeShopDict] [OnReceived()] Could Not Found Shop By Unique Id, CRITICAL - Aborting");
                return;
            }
            Mono.ShopInventory shopInventory = syncShop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[UpdateSingeShopDict] [OnReceived()] ShopInventory Component Not Found On Object, Aborting"); return; }
            shopInventory.UpdateShopInventoryFromNetwork(PurchashedItems, HeldInventory, Prices);
        }
    }

    public class UpdateSingeShopPurchasedItemsDict : SimpleEvent<UpdateSingeShopPurchasedItemsDict>
    {
        public string UniqueId { get; set; }  // UNIQUE GUID
        public string Sender { get; set; }  // STEAM ID
        public Dictionary<int, int> PurchashedItems { get; set; }  // All Items Purchashed From Shop (ItemId, Amount)
        public string ToPlayerName { get; set; }  // To Spesific Player
        public string ToPlayerId { get; set; }  // To Spesific Player
        public override void OnReceived()
        {
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }

            }
            if (ToPlayerName != "")
            {
                if (ToPlayerName != Misc.GetLocalPlayerUsername())
                {
                    Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            if (ToPlayerId != "")
            {
                if (ToPlayerId != sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }

            GameObject syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null)
            {
                Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] Could Not Found Shop By Unique Id, CRITICAL - Aborting");
                return;
            }
            Mono.ShopInventory shopInventory = syncShop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[UpdateSingeShopPurchasedItemsDict] [OnReceived()] ShopInventory Component Not Found On Object, Aborting"); return; }
            shopInventory.UpdateShopInventoryFromNetwork(PurchashedItems, null, null);
        }
    }

    public class UpdateSingeShopInventoryItemsDict : SimpleEvent<UpdateSingeShopInventoryItemsDict>
    {
        public string UniqueId { get; set; }  // UNIQUE GUID
        public string Sender { get; set; }  // STEAM ID
        public Dictionary<int, int> HeldInventory { get; set; }  // All Items That Are Placed In The Shop (ItemId, Amount)
        public string ToPlayerName { get; set; }  // To Spesific Player
        public string ToPlayerId { get; set; }  // To Spesific Player
        public override void OnReceived()
        {
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }

            }
            if (ToPlayerName != "")
            {
                if (ToPlayerName != Misc.GetLocalPlayerUsername())
                {
                    Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            if (ToPlayerId != "")
            {
                if (ToPlayerId != sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }

            GameObject syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null)
            {
                Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] Could Not Found Shop By Unique Id, CRITICAL - Aborting");
                return;
            }
            Mono.ShopInventory shopInventory = syncShop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[UpdateSingeShopInventoryItemsDict] [OnReceived()] ShopInventory Component Not Found On Object, Aborting"); return; }
            shopInventory.UpdateShopInventoryFromNetwork(null, HeldInventory, null);
        }
    }

    public class UpdateSingeShopPricesDict : SimpleEvent<UpdateSingeShopPricesDict>
    {
        public string UniqueId { get; set; }  // UNIQUE GUID
        public string Sender { get; set; }  // STEAM ID
        public Dictionary<int, int> Prices { get; set; }  // All Item Prices (ItemId, ItemIdToPay)
        public string ToPlayerName { get; set; }  // To Spesific Player
        public string ToPlayerId { get; set; }  // To Spesific Player
        public override void OnReceived()
        {
            (ulong uSteamId, string sSteamId) = Misc.MySteamId();
            if (string.IsNullOrEmpty(sSteamId))
            {
                Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] Could Not Get My SteamId, Cant Be Sure Who Has The Shop, Skipped");
                return;
            }
            else
            {
                if (Sender == sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] Not Creating Shop Over Network When Its From My SteamID, skipped");
                    return;
                }

            }
            if (ToPlayerName != "")
            {
                if (ToPlayerName != Misc.GetLocalPlayerUsername())
                {
                    Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }
            if (ToPlayerId != "")
            {
                if (ToPlayerId != sSteamId)
                {
                    Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] Not Creating Shop Event Meant For Someone else");
                    return;
                }
            }

            GameObject syncShop = ShopPrefabs.FindShopByUniqueId(UniqueId);
            if (syncShop == null)
            {
                Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] Could Not Found Shop By Unique Id, CRITICAL - Aborting");
                return;
            }
            Mono.ShopInventory shopInventory = syncShop.GetComponent<Mono.ShopInventory>();
            if (shopInventory == null) { Misc.Msg("[UpdateSingeShopPricesDict] [OnReceived()] ShopInventory Component Not Found On Object, Aborting"); return; }
            shopInventory.UpdateShopInventoryFromNetwork(null, null, Prices);
        }
    }
}
