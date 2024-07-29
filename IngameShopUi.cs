namespace IngameShop;

using Sons.Gui;
using Sons.Items.Core;
using SonsSdk;
using SUI;
using TheForest.Items.Special;
using TheForest.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SUI.SUI;

public class IngameShopUi
{
    private static readonly Color MainBgBlack = new(0, 0, 0, 0.8f);
    private static readonly Color PanelBg = ColorFromString("#111111");
    private static readonly Color ComponentBlack = new(0, 0, 0, 0.6f);
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
    internal static SUiElement<SContainerOptions> Item1RowContainer;
    internal static SUiElement<SContainerOptions> Item2RowContainer;
    internal static SUiElement<SContainerOptions> Item3RowContainer;
    internal static SUiElement<SContainerOptions> Item4RowContainer;
    internal static SUiElement<SContainerOptions> Item5RowContainer;
    internal static SUiElement<SContainerOptions> InputItemIdContainer;
    internal static SUiElement<SContainerOptions> AddItemContainer;
    //.Background(ColorFromString("#4287f5"))
    public static void Create()
    {
        var panel = RegisterNewPanel("ShopAdminUi")
           .Dock(EDockType.Fill).OverrideSorting(100);

        ClosePanel("ShopAdminUi");

        var mainContainer = SContainer
            .Dock(EDockType.Fill)
            .Background(MainBgBlack).Margin(200);
        panel.Add(mainContainer);

        //var title = SLabel.Text("IngameShop")
        //    .FontColor("#444").Font(EFont.RobotoRegular)
        //    .PHeight(100).FontSize(32)
        //    .HFill().Position(null, -40)
        //    .FontSpacing(10);
        //title.SetParent(mainContainer);

        var exitButton = SBgButton
            .Text("x").Background(GetBackgroundSprite(EBackground.Round28), Image.Type.Sliced).Color(ColorFromString("#FF234B"))
            .Pivot(1, 1).Anchor(AnchorType.TopRight).Position(-60, -60)
            .Size(60, 60).Ppu(1.7f).Notify(CloseAddPanel);
        exitButton.SetParent(mainContainer);

        AddedItemsLabel = SLabel.Text("Added Items:")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(32).Anchor(AnchorType.TopCenter).Position(0, -60)
            .FontSpacing(10);
        AddedItemsLabel.SetParent(mainContainer);

        var CoulumContainer = SScrollContainer
            .Dock(EDockType.Fill)
            .Vertical(10, "EX")
            .Padding(100, 100, 150, 0)
            .As<SScrollContainerOptions>();
        CoulumContainer.ContainerObject.Spacing(4);
        CoulumContainer.SetParent(mainContainer);

        // Item 1
        Item1RowContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50);
        Item1RowContainer.SetParent(CoulumContainer);
        Item1 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1.SetParent(Item1RowContainer);

        var setItem1Price = STextbox
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(24).Text("Price ID:")
            .HFill().Notify(SetPrice1FromUI);
        setItem1Price.SetParent(Item1RowContainer);

        var Item1Btn = SLabel.Text("Remove x1").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1Btn.OnClick(() =>
        {
            RemoveItemFromUi(Item1);
        });
        Item1Btn.SetParent(Item1RowContainer);

        // Item 2
        Item2RowContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50)
            .Visible(false);
        Item2RowContainer.SetParent(CoulumContainer);
        Item2 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2.SetParent(Item2RowContainer);

        var setItem2Price = STextbox
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(24).Text("Price ID:")
            .HFill().Notify(SetPrice2FromUI);
        setItem2Price.SetParent(Item2RowContainer);

        var Item2Btn = SLabel.Text("Remove x1").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2Btn.OnClick(() =>
        {
            RemoveItemFromUi(Item2);
        });
        Item2Btn.SetParent(Item2RowContainer);

        // Item 3
        Item3RowContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50)
            .Visible(false);
        Item3RowContainer.SetParent(CoulumContainer);
        Item3 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3.SetParent(Item3RowContainer);

        var setItem3Price = STextbox
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(24).Text("Price ID:")
            .HFill().Notify(SetPrice3FromUI);
        setItem3Price.SetParent(Item3RowContainer);

        var Item3Btn = SLabel.Text("Remove x1").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3Btn.OnClick(() =>
        {
            RemoveItemFromUi(Item3);
        });
        Item3Btn.SetParent(Item3RowContainer);

        // Item 4
        Item4RowContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50)
            .Visible(false);
        Item4RowContainer.SetParent(CoulumContainer);
        Item4 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4.SetParent(Item4RowContainer);

        var setItem4Price = STextbox
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(24).Text("Price ID:")
            .HFill().Notify(SetPrice4FromUI);
        setItem4Price.SetParent(Item4RowContainer);

        var Item4Btn = SLabel.Text("Remove x1").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4Btn.OnClick(() =>
        {
            RemoveItemFromUi(Item4);
        });
        Item4Btn.SetParent(Item4RowContainer);

        // Item 5
        Item5RowContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50)
            .Visible(false);
        Item5RowContainer.SetParent(CoulumContainer);
        Item5 = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5.SetParent(Item5RowContainer);

        var setItem5Price = STextbox
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(24).Text("Price ID:")
            .HFill().Notify(SetPrice5FromUI);
        setItem5Price.SetParent(Item5RowContainer);

        var Item5Btn = SLabel.Text("Remove x1").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5Btn.OnClick(() =>
        {
            RemoveItemFromUi(Item5);
        });
        Item5Btn.SetParent(Item5RowContainer);

        // Input Item ID
        InputItemIdContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50);
        InputItemIdContainer.SetParent(CoulumContainer);
        InputItemId = STextbox.Text("Input Item ID: ")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(28)
            .HFill().Notify(UpdateItemIdText);
        InputItemId.SetParent(InputItemIdContainer);

        FoundItem = SLabel.Text("[ITEMNAME]:[ITEM QUANTITY]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(10).FontSize(28).Alignment(TextAlignmentOptions.Midline)
            .FontSpacing(10).Visible(false);
        FoundItem.SetParent(InputItemIdContainer);

        AddItemContainer = SContainer
            .Dock(EDockType.Fill)
            .Horizontal(0, "EE")
            .Background(ComponentBlack)
            .Height(50);
        AddItemContainer.SetParent(CoulumContainer);
        AddItemButton = SLabel.Text("Add 1x Item From Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50).Visible(false);
        AddItemButton.OnClick(() =>
        {
            AddItemFromUi();
        });
        AddItemButton.SetParent(AddItemContainer);



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
        if (PauseMenu.IsActive)
        {
            PauseMenu._instance.Close();
        }
    }

    internal static bool IsPanelActive()
    {
        return GetPanel("ShopAdminUi").Root.activeSelf;
    }
    internal static void OnKeyClick()
    {
        if (!LocalPlayer.IsInWorld || LocalPlayer.IsInInventory || PauseMenu.IsActive) { return; }
        Transform transform = LocalPlayer._instance._mainCam.transform;
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, transform.forward, out raycastHit, 5f, LayerMask.GetMask(new string[]
        {
                "Default"
        }));
        if (raycastHit.collider == null || raycastHit.collider.transform.root == null) { return; }
        Misc.Msg($"Hit: {raycastHit.collider.transform.root.name}");
        if (raycastHit.collider.transform.root.name.Contains("Shop"))
        {
            GameObject shop = raycastHit.collider.transform.root.gameObject;
            Mono.ShopInventory shopInventory = shop.GetComponent<Mono.ShopInventory>();
            Mono.ShopGeneric shopGeneric = shop.GetComponent<Mono.ShopGeneric>();
            if (shopInventory != null && shopGeneric != null)
            {
                if (shopGeneric.shouldRunAdminUi)
                {

                    if (shopGeneric.add.IsActive)
                    {
                        IHeldOnlyItemController heldController = LocalPlayer.Inventory.HeldOnlyItemController;
                        if (heldController != null)
                        {
                            if (heldController.Amount > 0)
                            {
                                if (heldController.Amount == 1)
                                {
                                    heldController.PutDown(false, false, false);
                                    int item = heldController.HeldItem._itemID;
                                    shopInventory.AddItem(item, 1);
                                }
                                else
                                {
                                    int item = heldController.HeldItem._itemID;
                                    heldController.PutDown(false, false, false);
                                    shopInventory.AddItem(item, heldController.Amount);
                                }
                            }
                            else
                            {
                                IngameShopUi.OpenPanel("ShopAdminUi");
                                if (!PauseMenu.IsActive && PauseMenu._instance.CanBeOpened())
                                {
                                    PauseMenu._instance.Open();
                                }
                                IngameShopUi.inventory = shopInventory;
                                IngameShopUi.UpdateItemsUI();

                            }
                        }
                    }

                    else if (shopGeneric.remove.IsActive)
                    {

                    }
                }
            }
            
        }
        
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
                Misc.Msg($"Added 1x: {ItemIdTextInt} To Shop");
                inventory.AddItem(ItemIdTextInt, 1);
                LocalPlayer.Inventory.RemoveItem(ItemIdTextInt, 1);
                UpdateItemsUI();
            }
            
        }
    }

    public static void SetPrice1FromUI(string text)
    {
        int itemId;
        if (int.TryParse(text, out itemId))
        {
            SetPrice(itemId, 1);
        }
    }
    private static void SetPrice2FromUI(string text)
    {
        int itemId;
        if (int.TryParse(text, out itemId))
        {
            SetPrice(itemId, 2);
        }
    }
    private static void SetPrice3FromUI(string text)
    {
        int itemId;
        if (int.TryParse(text, out itemId))
        {
            SetPrice(itemId, 3);
        }
    }
    private static void SetPrice4FromUI(string text)
    {
        int itemId;
        if (int.TryParse(text, out itemId))
        {
            SetPrice(itemId, 4);
        }
    }
    private static void SetPrice5FromUI(string text)
    {
        int itemId;
        if (int.TryParse(text, out itemId))
        {
            SetPrice(itemId, 5);
        }
    }

    public static void SetPrice(int itemId, int element)
    {
        ItemData item = ItemDatabaseManager.ItemById(itemId);
        if (item != null)
        {
            if (item.Id != 0)
            {
                var itemUIElements = new[] { Item1, Item2, Item3, Item4, Item5 };
                string text = itemUIElements[element - 1].TextObject.text;
                int? itemIdExtracted = ExtractItemKey(text);
                if (itemIdExtracted != null)
                {
                    inventory.SetPrice((int)itemIdExtracted, itemId);
                    inventory.UpdatePriceUi();
                }
                
            }
        }
    }

    public static void RemoveItemFromUi(SUiElement<SLabelOptions> item)
    {
        if (inventory != null)
        {
            string text = item.TextObject.text;
            int? itemIdExtracted = ExtractItemKey(text);
            if (itemIdExtracted != null)
            {
                int maxAmount = LocalPlayer.Inventory.GetMaxAmountOf((int)itemIdExtracted);
                int currentAmount = LocalPlayer.Inventory.AmountOf((int)itemIdExtracted);

                if (currentAmount < maxAmount)
                {
                    SonsTools.ShowMessage($"Removed 1x: {itemIdExtracted} To Shop");
                    Misc.Msg($"Removed 1x: {itemIdExtracted} To Shop");
                    inventory.RemoveItem((int)itemIdExtracted, 1);
                }
                else
                {
                    SonsTools.ShowMessage($"Inventory Full Can't Remove Item");
                    Misc.Msg($"Inventory Full Can't Remove Item");
                }

                UpdateItemsUI();
            }
            
        }
    }

    public static void UpdateItemsUI()
    {
        if (inventory != null)
        {
            // Assuming you have an array or list of UI elements for the items
            var itemUIElements = new[] { Item1, Item2, Item3, Item4, Item5 };
            var containerUIElements = new[] { Item1RowContainer, Item2RowContainer, Item3RowContainer, Item4RowContainer, Item5RowContainer };
            int index = 0;

            foreach (KeyValuePair<int, int> item in inventory.HeldInventory)
            {
                if (index >= itemUIElements.Length)
                    break;

                var uiElement = itemUIElements[index];
                var containerUiElement = containerUIElements[index];
                containerUiElement.Visible(true);
                uiElement.Text($"ItemId: {item.Key}, Amount: {item.Value}");

                index++;
            }

            // Hide any remaining UI elements
            for (; index < itemUIElements.Length; index++)
            {
                containerUIElements[index].Visible(false);
            }
        }
    }

    public static int? ExtractItemKey(string itemString)
    {
        // Split the string by comma to separate ItemId and Amount
        string[] parts = itemString.Split(',');

        if (parts.Length > 0)
        {
            // Extract the ItemId part (assumes format is strictly followed)
            string itemIdPart = parts[0]; // "ItemId: {item.Key}"

            // Split by ':' to isolate the key value
            string[] itemIdParts = itemIdPart.Split(':');

            if (itemIdParts.Length > 1)
            {
                // Trim any whitespace and parse the key as an integer
                if (int.TryParse(itemIdParts[1].Trim(), out int itemKey))
                {
                    return itemKey;
                }
            }
        }
        Misc.Msg("The provided string does not have the expected format.");
        return null;
    }

    public static void UITesting()
    {
        OpenPanel("ShopAdminUi");
        var itemUIElements = new[] { Item1, Item2, Item3, Item4, Item5 };
        var containerUIElements = new[] { Item1RowContainer, Item2RowContainer, Item3RowContainer, Item4RowContainer, Item5RowContainer };
        foreach ( var item in containerUIElements )
        {
            item.Visible(true);
        }
        foreach ( var item in itemUIElements )
        {
            item.Text("ItemId: 392, Amount: 5");
        }
        Item1.Text("ItemId: 664, Amount: 5");
        Item2.Text("ItemId: 555, Amount: 4");
        Item3.Text("ItemId: 444, Amount: 3");

        Misc.Msg($"Item 1 Text: [{Item1.TextObject.text}]");
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