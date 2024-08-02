



using Sons.Items.Core;

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
