

using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace IngameShop.UI
{
    public class Helpers
    {
        public static string GetInputFieldText(InputField inputField)
        {
            // Get the text from the InputField
            string inputText = inputField.text;

            // Return the text or null if it's empty
            return string.IsNullOrEmpty(inputText) ? null : inputText;
        }

        public static string GetDropDownValue(Dropdown dropdown)
        {
            // Check if the dropdown has any options
            if (dropdown.options.Count > 0)
            {
                // Get the index of the selected option
                int index = dropdown.value;

                // Get the text of the selected option
                string selectedText = dropdown.options[index].text;

                // Return the selected text or null if it's empty
                return string.IsNullOrEmpty(selectedText) ? null : selectedText;
            }
            return null;
        }

        public static string GetDropDownValue(DropDownTypeAddMenu dropDownType, IndexType indexType)
        {
            int? index = null;
            switch (indexType)
            {
                case IndexType.Left:
                    index = 2;
                    break;
                case IndexType.Center:
                    index = 0;
                    break;
                case IndexType.Right:
                    index = 1;
                    break;
            }
            if (index != null)
            {
                Dropdown dd = null;
                switch (dropDownType)
                {
                    case DropDownTypeAddMenu.ItemToSellDropDown:
                        dd = UI.Setup.uiArray[(int)index].ItemToSellDropDown;
                        break;
                    case DropDownTypeAddMenu.ItemPriceDropDown:
                        dd = UI.Setup.uiArray[(int)index].SellItemPriceDropDown;
                        break;
                    case DropDownTypeAddMenu.PriceAmount:
                        dd = UI.Setup.uiArray[(int)index].PricePerItem;
                        break;
                }
                if (dd != null)
                {
                    string value = GetDropDownValue(dd);
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
            }
            return null;
        }

        public static string GetDropDownValue(DropDownTypeAddedMenu dropDownType, IndexType indexType)
        {
            int? index = null;
            switch (indexType)
            {
                case IndexType.Left:
                    index = 2;
                    break;
                case IndexType.Center:
                    index = 0;
                    break;
                case IndexType.Right:
                    index = 1;
                    break;
            }
            if (index != null)
            {
                Dropdown dd = null;
                switch (dropDownType)
                {
                    case DropDownTypeAddedMenu.ItemPriceDropDown:
                        dd = UI.Setup.uiArray[(int)index].ItemPriceDropDown;
                        break;
                    case DropDownTypeAddedMenu.PriceAmount:
                        dd = UI.Setup.uiArray[(int)index].PriceAmount;
                        break;
                }
                if (dd != null)
                {
                    string value = GetDropDownValue(dd);
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
            }
            return null;
        }


        public static string GetInputFieldValue(InputFieldType inputFieldType, IndexType indexType)
        {
            int? index = null;
            switch (indexType)
            {
                case IndexType.Left:
                    index = 2;
                    break;
                case IndexType.Center:
                    index = 0;
                    break;
                case IndexType.Right:
                    index = 1;
                    break;
            }
            if (index != null)
            {
                InputField inputField = null;
                switch (inputFieldType)
                {
                    case InputFieldType.AmountToAdd:
                        inputField = UI.Setup.uiArray[(int)index].AmountToAdd;
                        break;
                    case InputFieldType.InputItemId:
                        inputField = UI.Setup.uiArray[(int)index].InputItemId;
                        break;
                }
                if (inputField != null)
                {
                    string value = inputField.text;
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                }
            }
            return null;
        }

        public static void SetFeedBackText(string feedbackText, UiType uiType)
        {
            if (!string.IsNullOrEmpty(feedbackText))
            {
                int? index = null;
                string addOrAdded = null;
                switch (uiType)
                {
                    case UiType.AddLeft:
                        addOrAdded = "Add";
                        index = 2;
                        break;
                    case UiType.AddCenter:
                        addOrAdded = "Add";
                        index = 0;
                        break;
                    case UiType.AddRight:
                        addOrAdded = "Add";
                        index = 1;
                        break;
                    case UiType.AddedLeft:
                        addOrAdded = "Added";
                        index = 2;
                        break;
                    case UiType.AddedCenter:
                        addOrAdded = "Added";
                        index = 0;
                        break;
                    case UiType.AddedRight:
                        addOrAdded = "Added";
                        index = 1;
                        break;
                }
                if (index != null && addOrAdded != null)
                {
                    if (addOrAdded.ToLower() == "add")
                    {
                        UI.Setup.uiArray[(int)index].MessageText.text = feedbackText;
                    } 
                    else if (addOrAdded.ToLower() == "added")
                    {
                        UI.Setup.uiArray[(int)index].MessageTextAddedItems.text = feedbackText;
                    }
                }
            }
        }

        public static void SetItemText(string textValue, UiType uiType, UiTextType uiTextType)
        {
            if (!string.IsNullOrEmpty(textValue))
            {
                int? index = null;
                switch (uiType)
                {
                    case UiType.AddedLeft:
                        index = 2;
                        break;
                    case UiType.AddedCenter:
                        index = 0;
                        break;
                    case UiType.AddedRight:
                        index = 1;
                        break;
                    default:
                        return;
                }
                if (index != null)
                {
                    switch (uiTextType)
                    {
                        case UiTextType.AddedItemAmount:
                            UI.Setup.uiArray[(int)index].AddedItemAmount.text = textValue;
                            break;
                        case UiTextType.AddedItemName:
                            UI.Setup.uiArray[(int)index].AddedItemName.text = textValue;
                            break;
                    }
                }
            }
        }

        public static string GetItemText(UiType uiType, UiTextType uiTextType)
        {
            int? index = null;
            switch (uiType)
            {
                case UiType.AddedLeft:
                    index = 2;
                    break;
                case UiType.AddedCenter:
                    index = 0;
                    break;
                case UiType.AddedRight:
                    index = 1;
                    break;
                default:
                    return null;
            }
            if (index != null)
            {
                switch (uiTextType)
                {
                    case UiTextType.AddedItemAmount:
                        string text = UI.Setup.uiArray[(int)index].AddedItemAmount.text;
                        if (!string.IsNullOrEmpty(text))
                        {
                            return text;
                        }
                        else
                        {
                            return null;
                        }
                    case UiTextType.AddedItemName:
                        string text1 = UI.Setup.uiArray[(int)index].AddedItemName.text;
                        if (!string.IsNullOrEmpty(text1))
                        {
                            return text1;
                        }
                        else
                        {
                            return null;
                        }
                }
            }
            return null;
        }

        public static int ExtractNumberFromParentheses(string input)
        {
            // Define a regular expression to match the number inside parentheses
            Regex regex = new Regex(@"\((\d+)\)$");

            // Match the input string against the regular expression
            Match match = regex.Match(input);

            if (match.Success)
            {
                // Extract the captured group, which is the number inside the parentheses
                string numberString = match.Groups[1].Value;

                // Parse the number string to an integer
                if (int.TryParse(numberString, out int number))
                {
                    return number;
                }
            }

            // If no match is found or parsing fails, return a default value (e.g., 0 or throw an exception)
            Misc.Msg("Input string does not contain a valid number in parentheses.");
            return 0;
        }

        public enum IndexType
        {
            Left,
            Center,
            Right
        }

        public enum InputFieldType
        {
            InputItemId,
            AmountToAdd
        }

        public enum DropDownTypeAddMenu
        {
            ItemToSellDropDown,
            ItemPriceDropDown,
            PriceAmount
        }

        public enum DropDownTypeAddedMenu
        {
            PriceAmount,
            ItemPriceDropDown
        }

        public enum UiType
        {
            AddLeft,
            AddedLeft,
            AddRight,
            AddedRight,
            AddCenter,
            AddedCenter,
            All
        }

        public enum UiTextType
        {
            AddedItemAmount,
            AddedItemName
        }
    }
}
