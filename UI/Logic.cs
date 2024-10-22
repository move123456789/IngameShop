



using IngameShop.Mono;
using Sons.Items.Core;
using Sons.Weapon;
using TheForest.Items.Special;
using TheForest.Utils;

namespace IngameShop.UI
{
    public static class Logic
    {
        public static void UpdateUI(UI.Helpers.UiType uiType)
        {

        }

        public static void AddItemFromInventory(UI.Helpers.UiType uiType)
        {
            int? index = null;
            int? amount = null;
            int? selectedSellItem = null;
            switch (uiType)
            {
                case Helpers.UiType.AddLeft:
                    index = 2;
                    string inputItemIdField = UI.Helpers.GetInputFieldValue(Helpers.InputFieldType.InputItemId, Helpers.IndexType.Left);
                    int itemIdFromString = 0;
                    if (string.IsNullOrEmpty(inputItemIdField) || string.IsNullOrWhiteSpace(inputItemIdField))
                    {
                        string dropDownValue = UI.Helpers.GetDropDownValue(Helpers.DropDownTypeAddMenu.ItemToSellDropDown, Helpers.IndexType.Left);
                        if (string.IsNullOrEmpty(dropDownValue)) { Misc.Msg("[Logic] [AddItemFromInventory] No Valid Items, inputItemIdField/dropDownValue Null/Empty"); Helpers.SetFeedBackText("No Valied Sell Items", uiType); return; }
                        int id = UI.Helpers.ExtractNumberFromParentheses(dropDownValue);
                        if (id == 0) { Misc.Msg("[Logic] [AddItemFromInventory] No Valid Items, ItemId From ExtractNumberFromParentheses == 0"); Helpers.SetFeedBackText("No Valied Sell Items", uiType); return; }
                        itemIdFromString = id;
                    }
                    else
                    {
                        int inputItemIdFieldToInt;
                        if (int.TryParse(inputItemIdField, out inputItemIdFieldToInt))
                        {
                            itemIdFromString = inputItemIdFieldToInt;
                        }
                    }
                    if (itemIdFromString != 0)
                    {
                        selectedSellItem = itemIdFromString;
                    }
                    string amountOfitemsToAdd = UI.Helpers.GetInputFieldValue(Helpers.InputFieldType.AmountToAdd, Helpers.IndexType.Left);
                    if (string.IsNullOrEmpty(amountOfitemsToAdd) || string.IsNullOrWhiteSpace(amountOfitemsToAdd))
                    {
                        Misc.Msg("[Logic] [AddItemFromInventory] Incorrect Add Item Amount Given");
                        Helpers.SetFeedBackText("Amount To Add Must Be Entered", uiType);
                        return;
                    }
                    else
                    {
                        int inputItemAmountFieldToInt;
                        if (int.TryParse(inputItemIdField, out inputItemAmountFieldToInt))
                        {
                            amount = inputItemAmountFieldToInt;
                        }
                        else
                        {
                            Misc.Msg("[Logic] [AddItemFromInventory] Item Amount Must Be A Number");
                            Helpers.SetFeedBackText("Item Amount Must Be A Number", uiType);
                            return;
                        }
                    }
                    break;
                case Helpers.UiType.AddedLeft:
                    index = 2;
                    amount = 1;
                    break;
                case Helpers.UiType.AddRight:
                    index = 1;
                    break;
                case Helpers.UiType.AddedRight:
                    index = 1;
                    amount = 1;
                    break;
                case Helpers.UiType.AddCenter:
                    index = 0;
                    break;
                case Helpers.UiType.AddedCenter:
                    index = 0;
                    amount = 1;
                    break;
                default:
                    break;
            }
            if (index != null && amount != null && selectedSellItem != null)
            {
                var itemData = ItemDatabaseManager.ItemById((int)selectedSellItem);
                if (itemData == null)
                {
                    Misc.Msg("[Logic] [AddItemFromInventory] ItemData is NULL");
                    Helpers.SetFeedBackText("Invalid ItemId Entered", uiType);
                    return;
                }
                IHeldOnlyItemController heldC = LocalPlayer.Inventory.HeldOnlyItemController;
                if (heldC != null)
                {
                    if (heldC.Amount > 0)
                    {
                        if (heldC.HeldItem == null) { Misc.Msg("[Logic] [AddItemFromInventory] heldController.HeldItem Is Null!"); return; }
                        int item = heldC.HeldItem._itemID;
                        if (item == 0) { Misc.Msg("[Logic] [AddItemFromInventory] heldController.HeldItem._itemID == 0"); return; }
                        if (item == itemData.Id)
                        {
                            if (amount > heldC.Amount) { Misc.Msg("[Logic] [AddItemFromInventory] Amount To Add Is Greater Than Amount Held"); Helpers.SetFeedBackText("Amount To Add Is Greater Than Amount Held", uiType); return; }
                            heldC.PutDown(false, false, false);
                            //shopInventory.AddItem(item, heldController.Amount);
                            UI.Helpers.SetItemText();
                            return;
                        }


                    }
                }

                string itemName = itemData.Name;


            }
            else
            {
                Misc.Msg("[Logic] [AddItemFromInventory] Failed To Add Item To inventory, some values are null");
                Helpers.SetFeedBackText("Something went wrong when adding items to store", uiType);
                return;
            }
        }
    }
}
