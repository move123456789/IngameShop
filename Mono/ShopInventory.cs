using RedLoader;
using UnityEngine;

namespace IngameShop.Mono
{
    [RegisterTypeInIl2Cpp]
    public class ShopInventory : MonoBehaviour
    {
        public Dictionary<int, int> HeldInventory = new Dictionary<int, int>();

        private void Start()
        {

        }

        public void AddItem(int itemID, int quantity)
        {
            if (HeldInventory.ContainsKey(itemID))
            {
                HeldInventory[itemID] += quantity;
            }
            else
            {
                HeldInventory.Add(itemID, quantity);
            }
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
        }

        public void SetInventoryItems(Dictionary<int, int> items)
        {
            HeldInventory.Clear();
            HeldInventory = items;
        }
    }
}
