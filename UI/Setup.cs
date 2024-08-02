using SonsSdk;
using UnityEngine;
using UnityEngine.UI;




namespace IngameShop.UI
{
    public static class Setup
    {
        public static GameObject AddUI = null;
        public static UIData[] uiArray = new UIData[3];

        public static void SetupUI()
        {
            if (AddUI == null)
            {
                Misc.Msg("Setup Ui");
                AddUI = GameObject.Instantiate(Assets.InterActiveUi);
            }
            GameObject additemsGo = AddUI.transform.GetChild(0).FindChild("AddItems").gameObject;  // Should Always Be As Many Childs Here as in AddedItems
            List<GameObject> itemContainers = new List<GameObject>(); 
            foreach (Transform childT in additemsGo.GetChildren())
            {
                itemContainers.Add(childT.gameObject);
            }

            foreach (GameObject itemContainer in itemContainers)
            {
                UIData uIData = new UIData();
                string containerName = itemContainer.gameObject.name;
                char containerNumberChar = containerName[containerName.Length - 1];
                int containerNumber;
                if (int.TryParse(containerNumberChar.ToString(), out containerNumber))
                {
                    Transform itemToSellDropDown = itemContainer.transform.FindChild("ItemToSellDropDown");
                    uIData.ItemToSellDropDown = itemToSellDropDown.GetComponent<Dropdown>();
                    uIData.ItemToSellDropDownGo = itemToSellDropDown.gameObject;

                    Transform itemIdInpu = itemContainer.transform.FindChild("ItemIDInput");
                    uIData.InputItemId = itemIdInpu.GetComponent<InputField>();
                    uIData.InputItemIdGo = itemIdInpu.gameObject;

                    Transform amountToAddInput = itemContainer.transform.FindChild("AmountToAddInput");
                    uIData.AmountToAdd = amountToAddInput.GetComponent<InputField>();
                    uIData.AmountToAddGo = amountToAddInput.gameObject;

                    Transform pricePerItem = itemContainer.transform.FindChild("PriceAmount");
                    uIData.PricePerItem = pricePerItem.GetComponent<Dropdown>();
                    uIData.PricePerItemGo = pricePerItem.gameObject;

                    Transform sellItemPriceDropDown = itemContainer.transform.FindChild("ItemPriceDropDown");
                    uIData.SellItemPriceDropDown = sellItemPriceDropDown.GetComponent<Dropdown>();
                    uIData.SellItemPriceDropDownGo = sellItemPriceDropDown.gameObject;

                    Transform addItemsFromInventory = itemContainer.transform.FindChild("AddFromInventoryButton");
                    uIData.AddItemsFromInventory = addItemsFromInventory.GetComponent<Button>();
                    uIData.AddItemsFromInventoryGo = addItemsFromInventory.gameObject;

                    Transform messageText = itemContainer.transform.FindChild("MessageText");
                    uIData.MessageText = messageText.GetComponent<Text>();
                    uIData.MessageTextGo = messageText.gameObject;

                    uIData.AddItemsContainer = itemContainer;

                    if (uIData.AddItemsFromInventory != null)
                    {
                        switch (containerNumber - 1)
                        {
                            case 0:
                                Action value = () => UI.Listener.OnItemSpotCenterAddClick(uIData.AddItemsFromInventory);
                                uIData.AddItemsFromInventory.onClick.AddListener(value);
                                break; 
                            case 1:
                                Action value1 = () => UI.Listener.OnItemSpotRightAddClick(uIData.AddItemsFromInventory);
                                uIData.AddItemsFromInventory.onClick.AddListener(value1);
                                break;
                            case 2:
                                Action value2 = () => UI.Listener.OnItemSpotLeftAddClick(uIData.AddItemsFromInventory);
                                uIData.AddItemsFromInventory.onClick.AddListener(value2);
                                break;
                            default: break;
                        }
                    }

                    if (uIData.MessageText != null)
                    {
                        uIData.MessageText.text = "";  // Hide Message Text Since No Text
                    }

                    // Add To Array
                    uiArray[containerNumber - 1] = uIData;
                }
            }

            GameObject adddeditemsGo = AddUI.transform.GetChild(0).FindChild("AddedItems").gameObject;  // Should Always Be As Many Childs Here as in AddItems
            List<GameObject> addedItemContainers = new List<GameObject>();
            foreach (Transform childT in adddeditemsGo.GetChildren())
            {
                itemContainers.Add(childT.gameObject);
            }
            foreach (GameObject itemContainer in addedItemContainers)
            {
                string containerName = itemContainer.gameObject.name;
                char containerNumberChar = containerName[containerName.Length - 1];
                int containerNumber;
                if (int.TryParse(containerNumberChar.ToString(), out containerNumber))
                {
                    UIData uIData = uiArray[containerNumber - 1];
                    if (uIData == null) { Misc.Msg("UIData is null, Abort"); continue; }

                    Transform addedItemAmount = itemContainer.transform.FindChild("AddedItemAmount");
                    uIData.AddedItemAmount = addedItemAmount.GetComponent<Text>();

                    Transform addedItemName = itemContainer.transform.FindChild("AddedItemName");
                    uIData.AddedItemName = addedItemName.GetComponent<Text>();

                    Transform priceAmount = itemContainer.transform.FindChild("PriceAmount");
                    uIData.PriceAmount = priceAmount.GetComponent<Dropdown>();

                    Transform itemPriceDropDown = itemContainer.transform.FindChild("ItemPriceDropDown");
                    uIData.ItemPriceDropDown = itemPriceDropDown.GetComponent<Dropdown>();

                    Transform updateItemPrice = itemContainer.transform.FindChild("UpdateItemPrice");
                    uIData.UpdateItemPrice = updateItemPrice.GetComponent<Button>();

                    Transform removeFromStoreButton = itemContainer.transform.FindChild("RemoveFromStoreButton");
                    uIData.RemoveFromStoreButton = removeFromStoreButton.GetComponent<Button>();

                    Transform addFromInventoryButton = itemContainer.transform.FindChild("AddFromInventoryButton");
                    uIData.AddFromInventoryButton = addFromInventoryButton.GetComponent<Button>();

                    Transform messageTextAddedItems = itemContainer.transform.FindChild("MessageText");
                    uIData.MessageTextAddedItems = messageTextAddedItems.GetComponent<Text>();

                    uIData.AddedItemsContainer = itemContainer;

                    if (uIData.UpdateItemPrice != null && uIData.RemoveFromStoreButton != null && uIData.AddFromInventoryButton != null)
                    {
                        switch (containerNumber - 1)
                        {
                            case 0: // Center
                                Action value = () => UI.Listener.OnItemSpotCenterUpdateClick(uIData.UpdateItemPrice);
                                uIData.UpdateItemPrice.onClick.AddListener(value);

                                Action value1 = () => UI.Listener.OnItemSpotCenterRemoveFromStoreClick(uIData.RemoveFromStoreButton);
                                uIData.RemoveFromStoreButton.onClick.AddListener(value1);

                                Action value2 = () => UI.Listener.OnItemSpotCenterAddToStoreClick(uIData.AddFromInventoryButton);
                                uIData.AddFromInventoryButton.onClick.AddListener(value2);
                                break;
                            case 1: // Right
                                Action value3 = () => UI.Listener.OnItemSpotRightUpdateClick(uIData.UpdateItemPrice);
                                uIData.UpdateItemPrice.onClick.AddListener(value3);

                                Action value4 = () => UI.Listener.OnItemSpotRightRemoveFromStoreClick(uIData.RemoveFromStoreButton);
                                uIData.RemoveFromStoreButton.onClick.AddListener(value4);

                                Action value5 = () => UI.Listener.OnItemSpotRightAddToStoreClick(uIData.AddFromInventoryButton);
                                uIData.AddFromInventoryButton.onClick.AddListener(value5);
                                break;
                            case 2: // Left
                                Action value6 = () => UI.Listener.OnItemSpotLeftUpdateClick(uIData.UpdateItemPrice);
                                uIData.UpdateItemPrice.onClick.AddListener(value6);

                                Action value7 = () => UI.Listener.OnItemSpotLeftRemoveFromStoreClick(uIData.RemoveFromStoreButton);
                                uIData.RemoveFromStoreButton.onClick.AddListener(value7);

                                Action value8 = () => UI.Listener.OnItemSpotLeftAddToStoreeClick(uIData.AddFromInventoryButton);
                                uIData.AddFromInventoryButton.onClick.AddListener(value8);
                                break;
                            default: break;
                        }
                    }

                    if (uIData.MessageTextAddedItems != null)
                    {
                        uIData.MessageTextAddedItems.text = "";  // Hide Message Text Since No Text
                    }
                }
            }
        }

        public static void ToggleUi()
        {
            if (AddUI != null)
            {
                if (AddUI.active) { AddUI.SetActive(false); }
                if (!AddUI.active) { AddUI.SetActive(true); }
            }
        }
        public static void OpenUI()
        {
            if (AddUI != null)
            {
                if (!AddUI.active) { AddUI.SetActive(true); }
            }
        }
        public static void CloseUI()
        {
            if (AddUI != null)
            {
                if (AddUI.active) { AddUI.SetActive(false); }
            }
        }

        public class UIData
        {
            // Add Items
            public Dropdown ItemToSellDropDown { get; set; }
            public InputField InputItemId { get; set; }
            public InputField AmountToAdd { get; set; }
            public Dropdown PricePerItem { get; set; }
            public Dropdown SellItemPriceDropDown { get; set; }
            public Button AddItemsFromInventory { get; set; }
            public Text MessageText { get; set; }

            public GameObject AddItemsContainer { get; set; }
            public GameObject ItemToSellDropDownGo { get; set; }
            public GameObject InputItemIdGo { get; set; }
            public GameObject AmountToAddGo { get; set; }
            public GameObject PricePerItemGo { get; set; }
            public GameObject SellItemPriceDropDownGo { get; set; }
            public GameObject AddItemsFromInventoryGo { get; set; }
            public GameObject MessageTextGo { get; set; }

            // Added Items
            public Text AddedItemAmount { get; set; }
            public Text AddedItemName { get; set; }
            public Dropdown PriceAmount { get; set; }
            public Dropdown ItemPriceDropDown { get; set; }
            public Button UpdateItemPrice { get; set; }
            public Button RemoveFromStoreButton { get; set; }
            public Button AddFromInventoryButton { get; set; }
            public Text MessageTextAddedItems { get; set; }

            public GameObject AddedItemsContainer { get; set; }
        }
    }
}
