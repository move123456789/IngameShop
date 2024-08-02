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
                AddUI = GameObject.Instantiate(Assets.InterActiveUi);
            }
            GameObject canvasGo = AddUI.transform.GetChild(0).gameObject;
            List<GameObject> itemContainers = new List<GameObject>(); 
            foreach (Transform childT in canvasGo.GetChildren())
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
                    uIData.PricePerItem = pricePerItem.GetComponent<InputField>();
                    uIData.PricePerItemGo = pricePerItem.gameObject;

                    Transform sellItemPriceDropDown = itemContainer.transform.FindChild("ItemPriceDropDown");
                    uIData.SellItemPriceDropDown = sellItemPriceDropDown.GetComponent<Dropdown>();
                    uIData.SellItemPriceDropDownGo = sellItemPriceDropDown.gameObject;

                    Transform addItemsFromInventory = itemContainer.transform.FindChild("AddFromInventoryButton");
                    uIData.AddItemsFromInventory = addItemsFromInventory.GetComponent<Button>();
                    uIData.AddItemsFromInventory = addItemsFromInventory.GetComponent<Button>();

                    Transform messageText = itemContainer.transform.FindChild("MessageText");
                    uIData.MessageText = messageText.GetComponent<Text>();
                    uIData.MessageTextGo = messageText.gameObject;

                    // Add To Array
                    uiArray[containerNumber - 1] = uIData;
                }
            }
        }

        public class UIData
        {
            public Dropdown ItemToSellDropDown { get; set; }
            public InputField InputItemId { get; set; }
            public InputField AmountToAdd { get; set; }
            public InputField PricePerItem { get; set; }
            public Dropdown SellItemPriceDropDown { get; set; }
            public Button AddItemsFromInventory { get; set; }
            public Text MessageText { get; set; }

            public GameObject ItemToSellDropDownGo { get; set; }
            public GameObject InputItemIdGo { get; set; }
            public GameObject AmountToAddGo { get; set; }
            public GameObject PricePerItemGo { get; set; }
            public GameObject SellItemPriceDropDownGo { get; set; }
            public GameObject AddItemsFromInventoryGo { get; set; }
            public GameObject MessageTextGo { get; set; }
        }
    }
}
