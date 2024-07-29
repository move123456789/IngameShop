namespace IngameShop;

using Sons.Items.Core;
using SonsSdk;
using SUI;
using TheForest.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SUI.SUI;

public class IngameShopUi
{
    private static readonly Color MainBgBlack = new(0, 0, 0, 0.8f);
    public static Mono.ShopInventory inventory;

    // Add Item UI
    internal static SUiElement<STextboxOptions> InputItemId;
    internal static SUiElement<SLabelOptions> AddedItemsLabel;
    internal static SUiElement<SLabelOptions> Item1;
    internal static SUiElement<SLabelOptions> Item2;
    internal static SUiElement<SLabelOptions> Item3;
    internal static SUiElement<SLabelOptions> Item4;
    internal static SUiElement<SLabelOptions> Item5;
    internal static SUiElement<SLabelOptions> FoundItem;
    internal static SUiElement<SLabelOptions> AddItemButton;

    public static void Create()
    {
        var panel = RegisterNewPanel("ShopAdminUi")
           .Dock(EDockType.Fill).OverrideSorting(100);

        ClosePanel("ShopAdminUi");

        var mainContainer = SContainer
            .Dock(EDockType.Fill)
            .Background(MainBgBlack).Margin(400);
        panel.Add(mainContainer);

        var title = SLabel.Text("IngameShop")
            .FontColor("#444").Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(32)
            .HFill().Position(null, -80)
            .FontSpacing(10);
        title.SetParent(mainContainer);

        AddedItemsLabel = SLabel.Text("Added Items:")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(32)
            .HFill().Position(null, -40)
            .FontSpacing(10);
        AddedItemsLabel.SetParent(mainContainer);

        Item1 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        Item1.SetParent(mainContainer);

        Item2 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        Item2.SetParent(mainContainer);

        Item3 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        Item3.SetParent(mainContainer);

        Item4 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        Item4.SetParent(mainContainer);

        Item5 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        Item5.SetParent(mainContainer);

        InputItemId = STextbox.Text("Input Item ID: ")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40).Notify(UpdateItemIdText);
        InputItemId.SetParent(mainContainer);

        FoundItem = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(28)
            .HFill().Position(null, -40)
            .FontSpacing(10).Visible(false);
        FoundItem.SetParent(mainContainer);

        AddItemButton = SLabel.Text("Add 1x Item").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(28).PHeight(10).Alignment(TextAlignmentOptions.MidlineRight).Margin(50).Visible(false);
        AddItemButton.OnClick(() =>
        {
            AddItemFromUi();
        });

        var exitButton = SBgButton
            .Text("x").Background(GetBackgroundSprite(EBackground.Round28), Image.Type.Sliced).Color(ColorFromString("#FF234B"))
            .Pivot(1, 1).Anchor(AnchorType.TopRight).Position(-60, -60)
            .Size(60, 60).Ppu(1.7f).Notify(CloseAddPanel);
        exitButton.SetParent(mainContainer);
    }

    internal static void TogglePanelUi(string panelName)
    {
        TogglePanel(panelName);
    }

    internal static void ClosePanel(string panelName)
    {
        TogglePanel(panelName, false);
    }

    internal static void OpenPanel(string panelName)
    {
        TogglePanel(panelName, true);
    }

    internal static void CloseAddPanel()
    {
        TogglePanel("ShopAdminUi", false);
    }

    internal static bool IsPanelActive()
    {
        return GetPanel("ShopAdminUi").Root.activeSelf;
    }

    internal static async Task SendUiMessage(SUiElement<SLabelOptions> sLabel, string message)
    {
        if (isUiMessageRunning) { return; }
        sLabel.Visible(true);
        sLabel.Text(message);
        isUiMessageRunning = true;
        await Task.Run(Timer);
        sLabel.Visible(false);
    }

    internal static async Task Timer()
    {
        await Task.Delay(2500);
        isUiMessageRunning = false;
    }


    private static void UpdateItemIdText(string text)
    {
        _itemIdText = text;
        if (ItemIdTextInt != 0)
        {
            ItemData item = ItemDatabaseManager.ItemById(ItemIdTextInt);
            if (item != null)
            {
                if (item.Id != 0)
                {
                    FoundItem.Visible(true);
                    AddItemButton.Visible(true);
                    FoundItem.Text($"Item: {item.Name}, ItemId: {ItemIdTextInt}");
                }
            }
            
            
        } else { FoundItem.Visible(false); AddItemButton.Visible(false); }
    }
    private static string _itemIdText;
    public static string ItemIdText { get { return _itemIdText; } }
    public static int ItemIdTextInt
    {
        get
        {
            if (string.IsNullOrEmpty(_itemIdText))
            {
                return 0;
            }
            else
            {
                int itemID;
                if (int.TryParse(_itemIdText, out itemID))
                {
                    return itemID;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public static void AddItemFromUi()
    {
        if (inventory != null && ItemIdTextInt != 0)
        {
            if (LocalPlayer.Inventory.AmountOf(ItemIdTextInt) >= 1)
            {
                SonsTools.ShowMessage($"Added 1x: {ItemIdTextInt} To Shop");
                inventory.AddItem(ItemIdTextInt, 1);
                LocalPlayer.Inventory.RemoveItem(ItemIdTextInt, 1);
            }
            
        }
    }

    public static void UpdateItemsUI()
    {
        if (inventory != null)
        {
            foreach (KeyValuePair<int, int> item in inventory.HeldInventory)
            {
                int key = item.Key;
                int value = item.Value;
                Item1.Visible(true);
                Item1.Text($"ItemId: {key}, Amount: {value}");

                //Misc.Msg($"Key: {key}, Value: {value}");
            }
        }
    }











    private static bool isUiMessageRunning;

    //internal static void SetFloorNumber(int floorNumber)
    //{
    //    ElevatorCurrentFloor.Text($"Floor: {floorNumber}");
    //}

    //internal static void SetGotoFloorMessage(int message)
    //{
    //    MainMessage.Text($"Selected Floor: {message}");
    //}
}