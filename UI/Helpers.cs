

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

        public static string GetDropDownValue(DropDownType dropDownType, IndexType indexType)
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
                    case DropDownType.ItemToSellDropDown:
                        dd = UI.Setup.uiArray[(int)index].ItemToSellDropDown;
                        break;
                    case DropDownType.ItemPriceDropDown:
                        dd = UI.Setup.uiArray[(int)index].SellItemPriceDropDown;
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
                    case InputFieldType.PriceAmount:
                        inputField = UI.Setup.uiArray[(int)index].PricePerItem;
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

        public static void SetFeedBackText(string feedbackText, IndexType indexType)
        {
            if (!string.IsNullOrEmpty(feedbackText))
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
                    UI.Setup.uiArray[(int)index].MessageText.text = feedbackText;
                }
            }
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
            AmountToAdd,
            PriceAmount
        }

        public enum DropDownType
        {
            ItemToSellDropDown,
            ItemPriceDropDown,
        }
    }
}
