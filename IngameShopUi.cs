namespace IngameShop;

using SUI;
using static SUI.SUI;

public class IngameShopUi
{
    public static void Create()
    {
        var panel = RegisterNewPanel("ShopAdminUi")
           .Dock(EDockType.Fill).OverrideSorting(100);

        ClosePanel("ShopAdminUi");
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