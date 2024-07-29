﻿using Bolt;
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

        private void AddPreviewItem(int itemId, int posistion)
        {
            Misc.Msg($"[ShopWorldUi] [AddPreviewItem] Start, ItemId: {itemId}, ListPosition: {posistion}");
            var previewItemSlots = new[] { previewItem, null, null, null, null };
            if (previewItemSlots[posistion] == null) { Misc.Msg("[ShopWorldUi] [AddPreviewItem] previewItemSlots[posistion] == null Returning");  return; }
            GameObject addedPreview = previewItemSlots[posistion].transform.FindChild($"{itemId}").gameObject;
            GameObject addedPreviewByChildNumber = previewItemSlots[posistion].transform.GetChild(0).gameObject;
            if (addedPreview != null) { Misc.Msg("[ShopWorldUi] [AddPreviewItem] Item Already Added"); return; }
            else
            {
                if (addedPreviewByChildNumber != null) { GameObject.Destroy(addedPreviewByChildNumber); }

                GameObject toBePreviewItem = ItemDatabaseManager.ItemById(itemId).PickupPrefab.gameObject;
                if (toBePreviewItem != null)
                {
                    Misc.Msg("[ShopWorldUi] [AddPreviewItem] Adding Preview Item");
                    previewItemSlots[posistion].AddGo($"{itemId}");
                    GameObject toBeAddedTo = previewItemSlots[posistion].transform.FindChild($"{itemId}").gameObject;
                    if (toBeAddedTo == null) { Misc.Msg("Something went worng in gettig gameobject to place preview prefab on"); return; }
                    GameObject spawnedItem = GameObject.Instantiate(toBePreviewItem, toBeAddedTo.transform);
                    spawnedItem.SetActive(true);
                    spawnedItem.SetParent(toBeAddedTo.transform);
                } else { Misc.Msg("[ShopWorldUi] [AddPreviewItem] toBePreviewItem is NULL"); }
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
