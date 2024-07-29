using Bolt;
using RedLoader;
using Sons.Items.Core;
using SonsSdk;
using UnityEngine;
using UnityEngine.UI;

namespace IngameShop.Mono
{
    [RegisterTypeInIl2Cpp]
    public class ShopWorldUi : MonoBehaviour
    {
        private Mono.ShopInventory inventory = null;

        // Item 1
        private GameObject item1 = null;
        private Text item1Quantity = null;
        private Text item1Price = null;
        private GameObject previewItem = null;

        private void Start ()
        {
            if (inventory == null) { inventory = gameObject.GetComponent<Mono.ShopInventory>(); }
            GameObject buyStation = gameObject.transform.FindChild("BuyStation").gameObject;
            if (buyStation != null)
            {
                item1 = buyStation.transform.FindChild("1").gameObject;
                item1Quantity = item1.transform.FindDeepChild("Quantity").GetComponent<Text>();
                item1Price = item1.transform.FindDeepChild("Price").GetComponent<Text>();
                previewItem = item1.transform.FindChild("Item").gameObject;
            }
            else { RLog.Error("[ShopWorldUi] [Start] GameObject BuyStation Not Found! Critical, Ui Won't Work"); }
            
        }

        private void AddPreviewItem(int itemId, int position)
        {
            Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Start, ItemId: {itemId}, ListPosition: {position}");

            // Ensure the previewItemSlots array is properly initialized
            var previewItemSlots = new GameObject[] { previewItem, null, null, null, null };
            if (previewItemSlots[position] == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] previewItemSlots[position] == null Returning");
                return;
            }

            // Ensure that the GameObject and its Transform components exist
            Transform slotTransform = previewItemSlots[position].transform;
            if (slotTransform == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] slotTransform is NULL Returning");
                return;
            }

            // Check if the item is already added
            Transform addedPreviewTransform = slotTransform.Find($"{itemId}");
            if (addedPreviewTransform != null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] Item Already Added");
                return;
            }

            // Ensure that if there's an existing child, it gets destroyed
            if (slotTransform.childCount > 0)
            {
                GameObject addedPreviewByChildNumber = slotTransform.GetChild(0).gameObject;
                if (addedPreviewByChildNumber != null)
                {
                    GameObject.Destroy(addedPreviewByChildNumber);
                }
            }

            // Retrieve the item from the database and check for nulls
            var itemData = ItemDatabaseManager.ItemById(itemId);
            if (itemData == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] ItemData is NULL");
                return;
            }
            bool heldOrPickupPrefab;
            if (itemId == 640 || itemId == 78)
            {
                if (itemData.PickupPrefab == null)
                {
                    Misc.Msg("[ShopWorldUi] [AddPreviewItem] PickupPrefab is NULL");
                    return;
                }
                heldOrPickupPrefab = false;  // False For Pickup
            }
            else
            {
                if (itemData.HeldPrefab == null)
                {
                    Misc.Msg("[ShopWorldUi] [AddPreviewItem] HeldPrefab is NULL");
                    return;
                }
                heldOrPickupPrefab = true;  // True For HeldPrefab
            }

            GameObject toBePreviewItem;
            if (!heldOrPickupPrefab)
            {
                toBePreviewItem = itemData.HeldPrefab.gameObject;
            }
            else
            {
                toBePreviewItem = itemData.PickupPrefab.gameObject;
            }
            if (toBePreviewItem != null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] Adding Preview Item");
                // Assuming AddGo is a method that adds the GameObject to the slot
                previewItemSlots[position].AddGo($"{itemId}");

                // Recheck if the item was added properly
                Transform toBeAddedToTransform = slotTransform.Find($"{itemId}");
                if (toBeAddedToTransform == null)
                {
                    Misc.Msg("[ShopWorldUi] [AddPreviewItem] Something went wrong in getting GameObject to place preview prefab on");
                    return;
                }

                // Instantiate the preview item
                GameObject spawnedItem = GameObject.Instantiate(toBePreviewItem, toBeAddedToTransform);
                spawnedItem.SetActive(true);

                Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<Component> list = spawnedItem.GetComponents<Component>();
                for (int i = 0; i < list.Length; i++)
                {
                    Component item = list[i];
                    if (item.GetType() != typeof(Transform))
                    {
                        GameObject.Destroy(item);
                    }
                }

                if (!heldOrPickupPrefab)
                {
                    spawnedItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    GameObject pickupGui = spawnedItem.transform.FindChild("_PickupGui").gameObject;
                    pickupGui.SetActive(false);
                    
                    GameObject grassPusher = spawnedItem.transform.FindChild("GrassPusherPiv").gameObject;
                    grassPusher.SetActive(false);

                    GameObject trigger = spawnedItem.transform.FindChild("Trigger").gameObject;
                    trigger.SetActive(false);

                    GameObject dragLocation = spawnedItem.transform.FindChild("DragLocation").gameObject;
                    dragLocation.SetActive(false);

                    GameObject grabPosition = spawnedItem.transform.FindChild("GrabPosition").gameObject;
                    grabPosition.SetActive(false);

                    GameObject vailPickupTransform = spawnedItem.transform.FindChild("VailPickupTransform").gameObject;
                    vailPickupTransform.SetActive(false);

                    GameObject meleeCollider = spawnedItem.transform.FindChild("MeleeCollider").gameObject;
                    meleeCollider.SetActive(false);

                    GameObject waterSensor = spawnedItem.transform.FindChild("WaterSensor").gameObject;
                    waterSensor.SetActive(false);

                }
                else
                {
                    spawnedItem.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
                }
            }
            else
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] toBePreviewItem is NULL");
            }
        }

        public void UpdateUi()
        {
            if (inventory == null) { RLog.Error("[ShopWorldUi] [UpdateUi] Critical inventory = NULL!"); return; }
            Dictionary<int, int> priceDict = inventory.GetPriceList();
            Dictionary<int, int> storedItems = inventory.HeldInventory;
            var itemQuantity = new[] { item1Quantity, null, null, null, null };
            var itemPrice = new[] { item1Price, null, null, null, null };
            int index = 0;
            foreach (KeyValuePair<int, int> item in storedItems)
            {
                if (itemQuantity[index] == null || itemPrice == null) {  continue; }
                int itemId = item.Key;
                int quantity = item.Value;

                //Misc.Msg($"Item ID: {itemId}, Quantity: {quantity}");
                itemQuantity[index].text = $"Available: {quantity}";
                int priceItemId;
                if (priceDict.TryGetValue(itemId, out priceItemId))
                {
                    string name = ItemDatabaseManager.ItemById(priceItemId).Name;
                    if (string.IsNullOrEmpty(name))
                    {
                        itemPrice[index].text = $"Price: 1x UnkownItem??";
                    }
                    else
                    {
                        itemPrice[index].text = $"Price: 1x {name}";
                        AddPreviewItem(itemId, index);
                    }
                } else { itemPrice[index].text = $"Price: 1x STICK (Defauly)"; }
                
                
                index++;
            }

        }

        public void UpdatePriceUiOnly()
        {
            if (inventory == null) { RLog.Error("[ShopWorldUi] [UpdateUi] Critical inventory = NULL!"); return; }
            Dictionary<int, int> priceDict = inventory.GetPriceList();
            var itemPrice = new[] { item1Price, null, null, null, null };
            int index = 0;
            foreach (KeyValuePair<int, int> item in priceDict)
            {
                if (itemPrice[index] == null || itemPrice == null) { continue; }
                int itemId = item.Key;
                int itemPriceId = item.Value;

                int priceItemId;

                if (priceDict.TryGetValue(itemId, out priceItemId))
                {
                    string name = ItemDatabaseManager.ItemById(priceItemId).Name;
                    if (string.IsNullOrEmpty(name))
                    {
                        itemPrice[index].text = $"Price: 1x UnkownItem??";
                    }
                    else
                    {
                        itemPrice[index].text = $"Price: 1x {name}";
                    }
                }
                else { itemPrice[index].text = $"Price: 1x STICK"; }

                index++;
            }
        }
    }
}
