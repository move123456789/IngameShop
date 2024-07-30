using Bolt;
using Endnight.Animation;
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
        private GameObject previewItem1 = null;
        private GameObject[] previewItemSlots;
        private Text[] itemQuantity;
        private Text[] itemPrice;
        private GameObject[] itemGameObject;

        private void Start ()
        {
            if (inventory == null) { inventory = gameObject.GetComponent<Mono.ShopInventory>(); }
            GameObject buyStation = gameObject.transform.FindChild("BuyStation").gameObject;
            if (buyStation != null)
            {
                item1 = buyStation.transform.FindChild("1").gameObject;
                item1Quantity = item1.transform.FindDeepChild("Quantity").GetComponent<Text>();
                item1Price = item1.transform.FindDeepChild("Price").GetComponent<Text>();
                previewItem1 = item1.transform.FindChild("Item").gameObject;
            }
            else { RLog.Error("[ShopWorldUi] [Start] GameObject BuyStation Not Found! Critical, Ui Won't Work"); }

            itemQuantity = new[] { item1Quantity, null, null, null, null };
            itemPrice = new[] { item1Price, null, null, null, null };
            itemGameObject = new[] { item1, null, null, null, null };
            previewItemSlots = new GameObject[] { previewItem1, null, null, null, null };
        }

        private void AddPreviewItem(int itemId, int position)
        {
            // Ensure the previewItemSlots array is properly initialized
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
            if (itemId == 640 || itemId == 78)
            {
                AddPreviewItemPickup(itemId, position, previewItemSlots, slotTransform);
                return;
            }
            Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Start, ItemId: {itemId}, ListPosition: {position}");


            // Retrieve the item from the database and check for nulls
            var itemData = ItemDatabaseManager.ItemById(itemId);
            if (itemData == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] ItemData is NULL");
                return;
            }
            if (itemData.HeldPrefab == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] HeldPrefab is NULL");
                return;
            }

            GameObject toBePreviewItem = itemData.HeldPrefab.gameObject;
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
                if (spawnedItem != null)
                {
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
                    Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<Animator> animators = spawnedItem.GetComponentsInChildren<Animator>();
                    for (int i = 0; i < animators.Length; i++)
                    {
                        Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Animator Loop: {i}");
                        Animator anim = animators[i];
                        if (anim != null) { anim.enabled = false; GameObject.Destroy(anim); }
                        else { Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Animator Loop: Animator = null!"); }
                    }
                    Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<AnimationSync> animatorssync = spawnedItem.GetComponentsInChildren<AnimationSync>();
                    for (int i = 0; i < animatorssync.Length; i++)
                    {
                        Misc.Msg($"[ShopWorldUi] [AddPreviewItem] AnimationSync Loop: {i}");
                        AnimationSync anims = animatorssync[i];
                        if (anims != null) { anims.enabled = false; GameObject.Destroy(anims); }
                        else { Misc.Msg($"[ShopWorldUi] [AddPreviewItem] AnimationSync Loop: AnimationSync = null!"); }
                    }
                    Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Length, Animator: {animators.Count}, AnimationSync: {animatorssync.Count}");
                }
                else
                {
                    Misc.Msg("[ShopWorldUi] [AddPreviewItem] Spawned GameObject == NULL!");
                }
                
            }
            else
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] toBePreviewItem is NULL");
            }
        }

        private void AddPreviewItemPickup(int itemId, int position, GameObject[] slots, Transform slotTransform)
        {
            Misc.Msg($"[ShopWorldUi] [AddPreviewItemPickup] Start, ItemId: {itemId}, ListPosition: {position}");
            var itemData = ItemDatabaseManager.ItemById(itemId);
            if (itemData == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItemPickup] ItemData is NULL");
                return;
            }
            if (itemId != 640)
            {
                if (itemId != 78)
                {
                    Misc.Msg($"[ShopWorldUi] [AddPreviewItemPickup] ItemId Not Matching: {itemId}");
                    return;
                }
            }
            if (itemData.PickupPrefab == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItemPickup] PickupPrefab is NULL");
                return;
            }
            GameObject toBePreviewItem = itemData.PickupPrefab.gameObject;
            if (toBePreviewItem == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItemPickup] ItemData.PickupPrefab == NuLL!");
                return;
            }

            Misc.Msg("[ShopWorldUi] [AddPreviewItem] Adding Preview Item");
            // Assuming AddGo is a method that adds the GameObject to the slot
            slots[position].AddGo($"{itemId}");

            // Recheck if the item was added properly
            Transform toBeAddedToTransform = slotTransform.Find($"{itemId}");
            if (toBeAddedToTransform == null)
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] Something went wrong in getting GameObject to place preview prefab on");
                return;
            }

            // Instantiate the preview item
            GameObject spawnedItem = GameObject.Instantiate(toBePreviewItem, toBeAddedToTransform);
            if (spawnedItem != null)
            {
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

                spawnedItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

                List<Transform> childs = spawnedItem.transform.GetChildren();
                foreach (Transform child in childs)
                {
                    if (!child.name.Contains("Render") && !child.name.Contains("Clone"))
                    {
                        child.gameObject.SetActive(false);
                        GameObject.Destroy(child.gameObject);
                    }
                }

            }
            else
            {
                Misc.Msg("[ShopWorldUi] [AddPreviewItem] Spawned GameObject == NULL!");
            }
        }

        public void UpdateUi()
        {
            if (inventory == null) { RLog.Error("[ShopWorldUi] [UpdateUi] Critical inventory = NULL!"); return; }
            Dictionary<int, int> priceDict = inventory.GetPriceList();
            Dictionary<int, int> storedItems = inventory.HeldInventory;
            int index = 0;
            foreach (KeyValuePair<int, int> item in storedItems)
            {
                if (itemQuantity[index] == null || itemPrice == null)
                {
                    if (itemGameObject[index] != null)
                    {
                        itemGameObject[index].SetActive(false);
                    }
                    RemovePreviewItem(index);
                    continue;
                }

                if (itemGameObject[index] != null)
                {
                    itemGameObject[index].SetActive(true);
                }

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
                } else { itemPrice[index].text = $"Price: 1x Stick (Default)"; }
                
                
                index++;
            }

            // Deactivate the remaining items in itemGameObject
            for (int i = index; i < itemGameObject.Length; i++)
            {
                if (itemGameObject[i] != null)
                {
                    itemGameObject[i].SetActive(false);
                }
                RemovePreviewItem(i);
            }
        }

        public void UpdatePriceUiOnly()
        {
            if (inventory == null) { RLog.Error("[ShopWorldUi] [UpdateUi] Critical inventory = NULL!"); return; }
            Dictionary<int, int> priceDict = inventory.GetPriceList();
            int index = 0;
            foreach (KeyValuePair<int, int> item in priceDict)
            {
                if (itemPrice[index] == null || itemPrice == null)  // Before Adding All Only Way To Deactive UI Before Skipping
                {
                    if (itemGameObject[index] != null)
                    {
                        itemGameObject[index].SetActive(false);
                    }
                    continue;
                }

                if (itemGameObject[index] != null)
                {
                    itemGameObject[index].SetActive(true);
                }

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

            // Deactivate the remaining items in itemGameObject
            for (int i = index; i < itemGameObject.Length; i++)
            {
                if (itemGameObject[i] != null)
                {
                    itemGameObject[i].SetActive(false);
                }
            }
        }

        private void RemovePreviewItem(int index)
        {
            // Ensure the previewItemSlots array is properly initialized
            if (previewItemSlots[index] == null)
            {
                //Misc.Msg("[ShopWorldUi] [RemovePreviewItem] previewItemSlots[position] == null Returning");
                return;
            }

            // Ensure that the GameObject and its Transform components exist
            Transform slotTransform = previewItemSlots[index].transform;
            if (slotTransform == null)
            {
                Misc.Msg("[ShopWorldUi] [RemovePreviewItem] slotTransform is NULL Returning");
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
        }
    }
}
