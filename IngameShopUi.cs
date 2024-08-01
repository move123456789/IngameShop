namespace IngameShop;

using Sons.Gui;
using Sons.Items;
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
    internal static SUiElement<STextboxOptions> setItem1Price;
    internal static SUiElement<STextboxOptions> setItem2Price;
    internal static SUiElement<STextboxOptions> setItem3Price;
    internal static SUiElement<STextboxOptions> setItem4Price;
    internal static SUiElement<STextboxOptions> setItem5Price;
    internal static SUiElement<SLabelOptions> FoundItem;
    internal static SUiElement<SLabelOptions> AddItemButton;
    internal static SUiElement<SContainerOptions> Item1RowContainer;
    internal static SUiElement<SContainerOptions> Item2RowContainer;
    internal static SUiElement<SContainerOptions> Item3RowContainer;
    internal static SUiElement<SContainerOptions> Item4RowContainer;
    internal static SUiElement<SContainerOptions> Item5RowContainer;
    internal static SUiElement<SContainerOptions> InputItemIdContainer;
    internal static SUiElement<SContainerOptions> AddItemContainer;
    internal static SUiElement<STextboxOptions>[] SetItemPriceTextBoxArray = new SUiElement<STextboxOptions>[] { setItem1Price, setItem2Price, setItem3Price, setItem4Price, setItem5Price };
    //.Background(ColorFromString("#4287f5"))

    // Recive Item UI
    internal static SUiElement<SContainerOptions> Item1Recive;
    internal static SUiElement<SContainerOptions> Item2Recive;
    internal static SUiElement<SContainerOptions> Item3Recive;
    internal static SUiElement<SContainerOptions> Item4Recive;
    internal static SUiElement<SContainerOptions> Item5Recive;
    internal static SUiElement<SLabelOptions> Item1ItemName;
    internal static SUiElement<SLabelOptions> Item2ItemName;
    internal static SUiElement<SLabelOptions> Item3ItemName;
    internal static SUiElement<SLabelOptions> Item4ItemName;
    internal static SUiElement<SLabelOptions> Item5ItemName;
    internal static SUiElement<SLabelOptions> Item1ItemID;
    internal static SUiElement<SLabelOptions> Item2ItemID;
    internal static SUiElement<SLabelOptions> Item3ItemID;
    internal static SUiElement<SLabelOptions> Item4ItemID;
    internal static SUiElement<SLabelOptions> Item5ItemID;
    internal static SUiElement<SLabelOptions> Item1ItemQuantity;
    internal static SUiElement<SLabelOptions> Item2ItemQuantity;
    internal static SUiElement<SLabelOptions> Item3ItemQuantity;
    internal static SUiElement<SLabelOptions> Item4ItemQuantity;
    internal static SUiElement<SLabelOptions> Item5ItemQuantity;
    internal static SUiElement<SLabelOptions> InfoMessage;
    internal static SUiElement<SContainerOptions>[] ReciveItemArray = new SUiElement<SContainerOptions>[] { Item1Recive, Item2Recive, Item3Recive, Item4Recive, Item5Recive };
    internal static SUiElement<SLabelOptions>[] ReciveItemNameArray = new SUiElement<SLabelOptions>[] { Item1ItemName, Item2ItemName, Item3ItemName, Item4ItemName, Item5ItemName };
    internal static SUiElement<SLabelOptions>[] ReciveItemIDArray = new SUiElement<SLabelOptions>[] { Item1ItemID, Item2ItemID, Item3ItemID, Item4ItemID, Item5ItemID };
    internal static SUiElement<SLabelOptions>[] ReciveItemQuantityArray = new SUiElement<SLabelOptions>[] { Item1ItemQuantity, Item2ItemQuantity, Item3ItemQuantity, Item4ItemQuantity, Item5ItemQuantity };


    public static void Create()
    {
        var panel = RegisterNewPanel("ShopAdminUi", true)
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

        setItem1Price = STextbox
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

        setItem2Price = STextbox
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

        setItem3Price = STextbox
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

        setItem4Price = STextbox
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

        setItem5Price = STextbox
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
        // Update To Avioid NULL Throwing
        SetItemPriceTextBoxArray = new SUiElement<STextboxOptions>[] { setItem1Price, setItem2Price, setItem3Price, setItem4Price, setItem5Price };

        // ShopBougthUi // ShopBougthUi // ShopBougthUi // ShopBougthUi //
        var recivePanel = RegisterNewPanel("ShopBougthUi", true)
           .Dock(EDockType.Fill).OverrideSorting(100);

        ClosePanel("ShopBougthUi");

        var mainContainer1 = SContainer
            .Dock(EDockType.Fill)
            .Horizontal()
            .Background(MainBgBlack).Margin(200).Padding(0, 0, 150, 150);
        recivePanel.Add(mainContainer1);

        var exitButton1 = SBgButton
            .Text("x").Background(GetBackgroundSprite(EBackground.Round28), Image.Type.Sliced).Color(ColorFromString("#FF234B"))
            .Pivot(1, 1).Anchor(AnchorType.TopRight).Position(-60, -60)
            .Size(60, 60).Ppu(1.7f).Notify(CloseBougthPanel);
        exitButton.SetParent(recivePanel);

        var title1 = SLabel.Text("IngameShop")
            .FontColor("#444").Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(32)
            .Anchor(AnchorType.TopCenter)
            //.Pivot(0.5f, 0.7f)
            .Position(null, -250)
            .FontSpacing(10);
        title1.SetParent(recivePanel);

        InfoMessage = SLabel.Text("[INFO MESSAGE]")
            .FontColor(Color.white).Font(EFont.RobotoRegular)
            .PHeight(100).FontSize(32)
            .Anchor(AnchorType.TopCenter)
            .Position(null, -300)
            .FontSpacing(10);
        InfoMessage.SetParent(recivePanel);

        // Item 1
        Item1Recive = SContainer
            .Background(BG_CYAN).Vertical().PHeight(200).PWidth(200);
        Item1Recive.SetParent(mainContainer1);

        Item1ItemName = SLabel.Text("[ITEM NAME]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1ItemName.SetParent(Item1Recive);

        Item1ItemID = SLabel.Text("[ITEM ID]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1ItemID.SetParent(Item1Recive);

        Item1ItemQuantity = SLabel.Text("[ITEM AMOUT]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1ItemQuantity.SetParent(Item1Recive);

        var Item1ToInventory = SLabel.Text("1x To Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item1ToInventory.OnClick(() =>
        {
            TakeItemFromPurchasedItemsDict(index: 0, itemIdText: Item1ItemID);
        });
        Item1ToInventory.SetParent(Item1Recive);

        // Item 2
        Item2Recive = SContainer
            .Background(BG_CYAN).Vertical().PHeight(200).PWidth(200);
        Item2Recive.SetParent(mainContainer1);

        Item2ItemName = SLabel.Text("[ITEM NAME]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2ItemName.SetParent(Item2Recive);

        Item2ItemID = SLabel.Text("[ITEM ID]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2ItemID.SetParent(Item2Recive);

        Item2ItemQuantity = SLabel.Text("[ITEM AMOUT]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2ItemQuantity.SetParent(Item2Recive);

        var Item2ToInventory = SLabel.Text("1x To Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item2ToInventory.OnClick(() =>
        {
            TakeItemFromPurchasedItemsDict(index: 1, itemIdText: Item2ItemID);
        });
        Item2ToInventory.SetParent(Item2Recive);

        // Item 3
        Item3Recive = SContainer
            .Background(BG_CYAN).Vertical().PHeight(200).PWidth(200);
        Item3Recive.SetParent(mainContainer1);

        Item3ItemName = SLabel.Text("[ITEM NAME]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3ItemName.SetParent(Item3Recive);

        Item3ItemID = SLabel.Text("[ITEM ID]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3ItemID.SetParent(Item3Recive);

        Item3ItemQuantity = SLabel.Text("[ITEM AMOUT]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3ItemQuantity.SetParent(Item3Recive);

        var Item3ToInventory = SLabel.Text("1x To Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item3ToInventory.OnClick(() =>
        {
            TakeItemFromPurchasedItemsDict(index: 2, itemIdText: Item3ItemID);
        });
        Item3ToInventory.SetParent(Item3Recive);

        // Item 4
        Item4Recive = SContainer
            .Background(BG_CYAN).Vertical().PHeight(200).PWidth(200);
        Item4Recive.SetParent(mainContainer1);

        Item4ItemName = SLabel.Text("[ITEM NAME]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4ItemName.SetParent(Item4Recive);

        Item4ItemID = SLabel.Text("[ITEM ID]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4ItemID.SetParent(Item4Recive);

        Item4ItemQuantity = SLabel.Text("[ITEM AMOUT]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4ItemQuantity.SetParent(Item4Recive);

        var Item4ToInventory = SLabel.Text("1x To Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item4ToInventory.OnClick(() =>
        {
            TakeItemFromPurchasedItemsDict(index: 3, itemIdText: Item4ItemID);
        });
        Item4ToInventory.SetParent(Item4Recive);

        // Item 5
        Item5Recive = SContainer
            .Background(BG_CYAN).Vertical().PHeight(200).PWidth(200);
        Item5Recive.SetParent(mainContainer1);

        Item5ItemName = SLabel.Text("[ITEM NAME]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5ItemName.SetParent(Item5Recive);

        Item5ItemID = SLabel.Text("[ITEM ID]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5ItemID.SetParent(Item5Recive);

        Item5ItemQuantity = SLabel.Text("[ITEM AMOUT]")
            .FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10)
            .PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5ItemQuantity.SetParent(Item5Recive);

        var Item5ToInventory = SLabel.Text("1x To Inventory").FontColor(Color.white).Font(EFont.RobotoRegular).FontSize(32).FontSpacing(10).PHeight(10).Alignment(TextAlignmentOptions.Midline).Margin(50);
        Item5ToInventory.OnClick(() =>
        {
            TakeItemFromPurchasedItemsDict(index: 4, itemIdText: Item5ItemID);
        });
        Item5ToInventory.SetParent(Item5Recive);

        // Try To Update Arrays Since They Throw Null
        ReciveItemArray = new SUiElement<SContainerOptions>[] { Item1Recive, Item2Recive, Item3Recive, Item4Recive, Item5Recive };
        ReciveItemNameArray = new SUiElement<SLabelOptions>[] { Item1ItemName, Item2ItemName, Item3ItemName, Item4ItemName, Item5ItemName };
        ReciveItemIDArray = new SUiElement<SLabelOptions>[] { Item1ItemID, Item2ItemID, Item3ItemID, Item4ItemID, Item5ItemID };
        ReciveItemQuantityArray = new SUiElement<SLabelOptions>[] { Item1ItemQuantity, Item2ItemQuantity, Item3ItemQuantity, Item4ItemQuantity, Item5ItemQuantity };
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
    internal static void CloseBougthPanel()
    {
        TogglePanel("ShopBougthUi", false);
        if (PauseMenu.IsActive)
        {
            PauseMenu._instance.Close();
        }
    }

    internal static bool IsPanelActive(string panelName)
    {
        return GetPanel(panelName).Root.activeSelf;
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
        if (raycastHit.collider == null) { return; }
        if (raycastHit.collider.transform.root == null) { return; }
        if (string.IsNullOrEmpty(raycastHit.collider.transform.root.name)) { return; }
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
                                    if (heldController.HeldItem == null) { Misc.Msg("[OnKeyClick] heldController.HeldItem Is Null!"); return; }
                                    heldController.PutDown(false, false, false);
                                    int item = heldController.HeldItem._itemID;
                                    shopInventory.AddItem(item, 1);
                                }
                                else
                                {
                                    if (heldController.HeldItem == null) { Misc.Msg("[OnKeyClick] heldController.HeldItem Is Null!"); return; }
                                    int item = heldController.HeldItem._itemID;
                                    heldController.PutDown(false, false, false);
                                    shopInventory.AddItem(item, heldController.Amount);
                                }
                            }
                            else
                            {
                                IngameShopUi.OpenPanel("ShopAdminUi");
                                IngameShopUi.inventory = shopInventory;
                                IngameShopUi.UpdateItemsUI();

                            }
                        }
                    }

                    else if (shopGeneric.remove.IsActive)
                    {
                        IngameShopUi.OpenPanel("ShopBougthUi");
                        IngameShopUi.inventory = shopInventory;
                        IngameShopUi.UpdateTakeItemsUI();
                    }
                }
                Mono.ShopWorldUi shopWorldUi = shop.GetComponent<Mono.ShopWorldUi>();
                if (shopWorldUi == null) { Misc.Msg("[OnKeyClick] shopWorldUi == null [CANT BUY ITEM IN THIS CASE]"); return; }
                if (shopGeneric.item1 == null) { return; }
                else if (shopGeneric.item1.IsActive)
                {
                    shopWorldUi.PurchaseItem(0); // 0 = Item1
                }
                //else if (shopGeneric.item2.IsActive)
                //{
                //    shopWorldUi.PurchaseItem(1); // 1 = Item2
                //}
                //else if (shopGeneric.item3.IsActive)
                //{
                //    shopWorldUi.PurchaseItem(2); // 2 = Item3
                //}
                //else if (shopGeneric.item4.IsActive)
                //{
                //    shopWorldUi.PurchaseItem(3); // 3 = Item4
                //}
                //else if (shopGeneric.item5.IsActive)
                //{
                //    shopWorldUi.PurchaseItem(4); // 4 = Item5
                //}
            }

        }
        
    }
    internal static void OnEscClick()
    {
        if (IsPanelActive("ShopBougthUi"))
        {
            ClosePanel("ShopBougthUi");
        } 
        else if (IsPanelActive("ShopAdminUi"))
        {
            ClosePanel("ShopAdminUi");
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
                    LocalPlayer.Inventory.AddItem((int)itemIdExtracted, 1);
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

    public static void TakeItemFromPurchasedItemsDict(int index = -1, SUiElement<SLabelOptions> itemIdText = null)
    {
        if (index >= 0)
        {
            var itemIDElement = ReciveItemIDArray[index];
            string itemIdString = ExtractItemId(itemIDElement.TextObject.text);
            if (string.IsNullOrEmpty(itemIdString)) { Misc.Msg("[TakeItemFromPurchasedItemsDict] itemIDElement.TextObject.text is Bull/Empty"); return; }
            int itemID;
            if (int.TryParse(itemIdString, out itemID))
            {
                if (inventory == null) { Misc.Msg($"[TakeItemFromPurchasedItemsDict] Inventory Instance == null"); return; }
                int hasAmount = LocalPlayer.Inventory.AmountOf(itemID);
                int maxAmount = LocalPlayer.Inventory.GetMaxAmountOf(itemID);
                if (hasAmount < maxAmount)
                {
                    LocalPlayer.Inventory.AddItem(itemID, 1);
                    SonsTools.ShowMessage($"Added 1x {itemID} To Inventory");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Added 1x {itemID} To Inventory");
                    int? itemToRemoveFromPurchasedItems = GetKeyByValue(inventory.GetPriceDict(), itemID);
                    if (!itemToRemoveFromPurchasedItems.HasValue) { Misc.Msg("Item To Remove From Purchashed Items Is Null!"); itemToRemoveFromPurchasedItems = 0; }
                    inventory.BuyRemoveItem((int)itemToRemoveFromPurchasedItems, 1);
                    UpdateTakeItemsUI();
                    SendUiMessage(InfoMessage, $"Added 1x {itemID} To Inventory");
                    SonsTools.ShowMessage($"Added 1x {itemID} To Inventory");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Added 1x {itemID} To Inventory");
                    string uniqueIdFromInventory = inventory.GetUniqueId();
                    if (string.IsNullOrEmpty(uniqueIdFromInventory)) { Misc.Msg("[TakeItemFromPurchasedItemsDict] UniqueId is Null/Empty, Can't Send Network Event! Aborting"); return; }
                    IngameTools.SyncShopTools.CommonSendSyncDictEvent(uniqueIdFromInventory, IngameTools.SyncShopTools.ShopEventType.PurchasedItemsDict);
                }
                else
                {
                    SonsTools.ShowMessage($"Inventory Full, Can't Add 1x {itemID}");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Inventory Full, Can't Add 1x {itemID}");
                    SendUiMessage(InfoMessage, $"Inventory Full, Can't Add 1x {itemID}");
                }
            }
            else { Misc.Msg($"[TakeItemFromPurchasedItemsDict] Failed To Parse '{itemIdString}' To Int"); return; }

        }
        else if (index == -1 && itemIdText != null)
        {
            string itemIdString = ExtractItemId(itemIdText.TextObject.text);
            if (string.IsNullOrEmpty(itemIdString)) { Misc.Msg("[TakeItemFromPurchasedItemsDict] itemIDElement.TextObject.text is Bull/Empty"); return; }
            int itemID;
            if (int.TryParse(itemIdString, out itemID))
            {
                if (inventory == null) { Misc.Msg($"[TakeItemFromPurchasedItemsDict] Inventory Instance == null"); return; }
                int hasAmount = LocalPlayer.Inventory.AmountOf(itemID);
                int maxAmount = LocalPlayer.Inventory.GetMaxAmountOf(itemID);
                if (hasAmount < maxAmount)
                {
                    LocalPlayer.Inventory.AddItem(itemID, 1);
                    SonsTools.ShowMessage($"Added 1x {itemID} To Inventory");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Added 1x {itemID} To Inventory");
                    int? itemToRemoveFromPurchasedItems = GetKeyByValue(inventory.GetPriceDict(), itemID);
                    if (!itemToRemoveFromPurchasedItems.HasValue) { Misc.Msg("Item To Remove From Purchashed Items Is Null!"); itemToRemoveFromPurchasedItems = 0; }
                    inventory.BuyRemoveItem((int)itemToRemoveFromPurchasedItems, 1);
                    UpdateTakeItemsUI();
                    SendUiMessage(InfoMessage, $"Added 1x {itemID} To Inventory");
                    SonsTools.ShowMessage($"Added 1x {itemID} To Inventory");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Added 1x {itemID} To Inventory");
                    string uniqueIdFromInventory = inventory.GetUniqueId();
                    if (string.IsNullOrEmpty(uniqueIdFromInventory)) { Misc.Msg("[TakeItemFromPurchasedItemsDict] UniqueId is Null/Empty, Can't Send Network Event! Aborting"); return; }
                    IngameTools.SyncShopTools.CommonSendSyncDictEvent(uniqueIdFromInventory, IngameTools.SyncShopTools.ShopEventType.PurchasedItemsDict);
                }
                else
                {
                    SonsTools.ShowMessage($"Inventory Full, Can't Add 1x {itemID}");
                    Misc.Msg($"[TakeItemFromPurchasedItemsDict] Inventory Full, Can't Add 1x {itemID}");
                    SendUiMessage(InfoMessage, $"Inventory Full, Can't Add 1x {itemID}");
                }
            }
            else { Misc.Msg($"[TakeItemFromPurchasedItemsDict] Failed To Parse '{itemIdString}' To Int"); return; }
        }
        else
        {
            Misc.Msg("[TakeItemFromPurchasedItemsDict] Invalid Input Cant Remove Item Via Ui");
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

    public static void UpdateTakeItemsUI()
    {
        if (inventory != null)
        {
            int index = 0;
            if (inventory.GetPurchasedDict() == null) { Misc.Msg("[UpdateTakeItemsUI] Inventory.GetPurchasedDict() == Null When It Should Not, Aborting"); }
            if (inventory.GetPurchasedDict().Keys.Count < 1) { Misc.Msg("[UpdateTakeItemsUI] Inventory.GetPurchasedDict().Keys.Count Is Less Than 1 Key, Aborting"); }
            foreach (KeyValuePair<int, int> item in inventory.GetPurchasedDict())
            {
                if (index >= ReciveItemNameArray.Length)
                    break;

                var containerElement = ReciveItemArray[index];
                if (containerElement == null) { Misc.Msg("[UpdateTakeItemsUI] ContainerElement == Null When It Should Not, Skip"); continue; }
                var itemNameElement = ReciveItemNameArray[index];
                if (itemNameElement == null) { Misc.Msg("[UpdateTakeItemsUI] ItemNameElement == Null When It Should Not, Skip"); continue; }
                var itemIDElement = ReciveItemIDArray[index];
                if (itemIDElement == null) { Misc.Msg("[UpdateTakeItemsUI] ItemIDElement == Null When It Should Not, Skip"); continue; }
                var itemQuantityElement = ReciveItemQuantityArray[index];
                if (itemQuantityElement == null) { Misc.Msg("[UpdateTakeItemsUI] ItemQuantityElement == Null When It Should Not, Skip"); continue; }

                string thisItemName = "Item Name Not Found";
                int itemsToGive = inventory.GetPrice(item.Key);
                string itemNameString = ItemDatabaseManager.ItemById(itemsToGive).Name;
                if (!string.IsNullOrEmpty(itemNameString))
                {
                    thisItemName = itemNameString;
                }
                containerElement.Visible(true);
                itemNameElement.Text($"{thisItemName}");
                itemIDElement.Text($"ItemId: {itemsToGive}");
                itemQuantityElement.Text($"Amount: {item.Value}");

                index++;
            }

            // Hide any remaining UI elements
            for (; index < ReciveItemNameArray.Length; index++)
            {
                if (ReciveItemArray[index] == null) { Misc.Msg("[UpdateTakeItemsUI] ReciveItemArray[index] (CONTAINER) == Null When It Should Not, Skip"); continue; }
                ReciveItemArray[index].Visible(false);
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

    public static string ExtractItemId(string text)
    {
        // Split the string by the delimiter "ItemId: " and take the second part
        string[] parts = text.Split(new[] { "ItemId: " }, StringSplitOptions.None);

        if (parts.Length > 1)
        {
            return parts[1];
        }

        return null;
    }

    public static int? GetKeyByValue(Dictionary<int, int> dict, int value)
    {
        foreach (var kvp in dict)
        {
            if (kvp.Value == value)
            {
                return kvp.Key;
            }
        }

        // Return null if the value is not found
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

    public static void UITesting2()
    {
        OpenPanel("ShopBougthUi");
        foreach (var item in ReciveItemArray)
        {
            item.Visible(true);
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