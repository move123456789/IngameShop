using RedLoader;
using UnityEngine;

namespace IngameShop.Mono
{
    [RegisterTypeInIl2Cpp]
    public class ShopInventory : MonoBehaviour
    {
        private Dictionary<int, int> purchashedItems = new Dictionary<int, int>();
        public Dictionary<int, int> HeldInventory = new Dictionary<int, int>();
        private Dictionary<int, int> PriceDict = new Dictionary<int, int>();
        private Mono.ShopWorldUi shopWorldUi = null;

        private void Start()
        {
            if (shopWorldUi == null) { shopWorldUi = gameObject.GetComponent<Mono.ShopWorldUi>(); }
        }

        public void AddItem(int itemID, int quantity)
        {
            if (HeldInventory.Keys.Count >= 5)
            {
                Misc.Msg("Can't Add More Than 5 Items To The Shop");
                return;
            }
            if (HeldInventory.ContainsKey(itemID))
            {
                HeldInventory[itemID] += quantity;
            }
            else
            {
                HeldInventory.Add(itemID, quantity);
            }
            UpdateUi();
        }

        public void RemoveItem(int itemID, int quantity)
        {
            if (HeldInventory.ContainsKey(itemID))
            {
                int currentQuantity = HeldInventory[itemID];
                if (currentQuantity >= quantity)
                {
                    HeldInventory[itemID] -= quantity;
                    if (HeldInventory[itemID] <= 0)
                    {
                        HeldInventory.Remove(itemID); // Optionally remove the item if quantity is zero or less
                    }
                    UpdateUi();
                }
                else
                {
                    Misc.Msg($"Not enough quantity of item {itemID} to remove.");
                }
            }
            else
            {
                Misc.Msg($"{itemID} Not Found In Inventory");
            }
        }

        public int GetItemAmountOf(int itemID)
        {
            if (HeldInventory.TryGetValue(itemID, out int quantity))
            {
                return quantity;
            }
            else
            {
                Misc.Msg($"{itemID} Not Found In Inventory");
                return 0;
            }
        }

        public void ClearInventory()
        {
            HeldInventory.Clear();
            UpdateUi();
        }

        public void SetInventoryItems(Dictionary<int, int> items)
        {
            HeldInventory.Clear();
            HeldInventory = items;
            UpdateUi();
        }

        public void SetPrice(int itemId, int priceItemId)
        {
            if (PriceDict.ContainsKey(itemId))
            {
                PriceDict[itemId] = priceItemId;
            } else
            {
                PriceDict.Add(itemId, priceItemId);
            }
            UpdateUi();
        }

        public void SetPriceList(Dictionary<int, int> items)
        {
            PriceDict.Clear();
            PriceDict = items;
            UpdateUi();
        }

        public int GetPrice(int itemId) {  return PriceDict[itemId]; }

        public Dictionary<int, int> GetPriceList() { return PriceDict; }

        private void UpdateUi()
        {
            if (shopWorldUi != null) { shopWorldUi.UpdateUi(); }
        }

        public void UpdatePriceUi()
        {
            if (shopWorldUi != null) { shopWorldUi.UpdatePriceUiOnly(); }
        }

        public void PurchaseItem(int itemId)
        {

        }

        public void UpdateShopInventoryFromNetwork(Dictionary<int, int> purchasedItems, Dictionary<int, int> inventoryItems, Dictionary<int, int> prices)
        {
            if (Config.NetworkDebugIngameShop.Value) { Misc.Msg("[ShopInventory] [UpdateShopInventoryFromNetwork] Updating Inventory From Network"); }
            if (purchasedItems != null)
            {
                purchashedItems.Clear();
                purchashedItems = purchasedItems;
            }
            if (inventoryItems != null)
            {
                HeldInventory.Clear();
                HeldInventory = inventoryItems;
            }
            if (prices != null)
            {
                PriceDict.Clear();
                PriceDict = prices;
            }
            if (purchasedItems != null || inventoryItems != null || prices != null)
            {
                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg("[ShopInventory] [UpdateShopInventoryFromNetwork] Inventory Update From Network Complete"); }
                UpdateUi();
            }
            
        }
    }
}
