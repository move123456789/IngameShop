﻿

using UnityEngine.UI;

namespace IngameShop.UI
{
    public static class Listener
    {
        public static void OnItemSpotCenterAddClick(Button button)
        {
            // Index 0 in uiArray
            UI.Logic.AddItemFromInventory(UI.Helpers.UiType.AddCenter);
        }

        public static void OnItemSpotRightAddClick(Button button)
        {
            // Index 1 in uiArray
            UI.Logic.AddItemFromInventory(UI.Helpers.UiType.AddRight);
        }


        public static void OnItemSpotLeftAddClick(Button button)
        {
            // Index 2 in uiArray
            UI.Logic.AddItemFromInventory(UI.Helpers.UiType.AddLeft);
        }

        public static void OnItemSpotCenterUpdateClick(Button button)
        {
            // Index 0 in uiArray
        }

        public static void OnItemSpotRightUpdateClick(Button button)
        {
            // Index 1 in uiArray
        }

        public static void OnItemSpotLeftUpdateClick(Button button)
        {
            // Index 2 in uiArray
        }

        public static void OnItemSpotCenterRemoveFromStoreClick(Button button)
        {
            // Index 0 in uiArray
        }

        public static void OnItemSpotRightRemoveFromStoreClick(Button button)
        {
            // Index 1 in uiArray
        }

        public static void OnItemSpotLeftRemoveFromStoreClick(Button button)
        {
            // Index 2 in uiArray
        }
        public static void OnItemSpotCenterAddToStoreClick(Button button)
        {
            // Index 0 in uiArray
        }

        public static void OnItemSpotRightAddToStoreClick(Button button)
        {
            // Index 1 in uiArray
        }

        public static void OnItemSpotLeftAddToStoreClick(Button button)
        {
            // Index 2 in uiArray
        }
    }
}
