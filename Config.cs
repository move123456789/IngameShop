using RedLoader;
using SonsSdk;

namespace IngameShop;

public static class Config
{
    internal static ConfigCategory IngameShopCategory { get; private set; }

    public static KeybindConfigEntry ToggleMenuKey { get; private set; }
    public static ConfigEntry<int> MaxShops { get; private set; }
    public static ConfigEntry<bool> DebugLoggingIngameShop { get; private set; }
    public static ConfigEntry<bool> NetworkDebugIngameShop { get; private set; }
    public static KeybindConfigEntry Cmd1 { get; private set; }
    public static ConfigEntry<bool> Cmd1Disable { get; private set; }
    public static KeybindConfigEntry Cmd2 { get; private set; }
    public static ConfigEntry<bool> Cmd2Disable { get; private set; }
    internal static void Init()
    {
        IngameShopCategory = ConfigSystem.CreateCategory("ingameShop", "IngameShop");

        MaxShops = IngameShopCategory.CreateEntry(
            "max_shop_ingameshop",
            2,
            "Max Shops Player Can Have (Host Only)",
            "Max Amount Of Shops A Player Can Have, Can Only Be Adjusted By Host");
        MaxShops.SetRange(0, 10);

        ToggleMenuKey = IngameShopCategory.CreateKeybindEntry(
            "menu_key",
            "e",
            "Toggle Menu Key",
            "The key that toggles the Menu (DEFAULT E).");
        ToggleMenuKey.DefaultValue = "e";
        ToggleMenuKey.Notify(() =>
        {
            IngameShopUi.OnKeyClick();
        });

        DebugLoggingIngameShop = IngameShopCategory.CreateEntry(
            "enable_logging_advanced_ingameshop",
            true,
            "Enable Debug Logs",
            "Enables SimpleNetworkEvents Debug Logs of the game to the console.");
        NetworkDebugIngameShop = IngameShopCategory.CreateEntry(
            "enable_logging_advanced_network",
            true,
            "Enable Extra Network Debug Logs",
            "Enables Extra Network Debug Logs of the game to the console.");

        Cmd1Disable = IngameShopCategory.CreateEntry(
            "enable_logging_advanced_ui",
            false,
            "Enables Ui Testing Kit",
            "Enables Ui Testing Kit");

        Cmd1 = IngameShopCategory.CreateKeybindEntry(
                  "command_1",
                  "numpad0",
                  "Command1",
                  "Command 1");
        Cmd1.Notify(() =>
        {
            if (Config.Cmd1Disable.Value)
            {
                IngameShopUi.UITesting();
            }
        });

        Cmd2Disable = IngameShopCategory.CreateEntry(
            "enable_logging_advanced_uitwo",
            false,
            "Enables Ui Testing Kit",
            "Enables Ui Testing Kit");
        Cmd2 = IngameShopCategory.CreateKeybindEntry(
                  "command_2",
                  "numpad9",
                  "Command2",
                  "Command 2");
        Cmd2.Notify(() =>
        {
            if (Config.Cmd2Disable.Value)
            {
                IngameShopUi.UITesting2();
            }
        });
    }

    // Same as the callback in "CreateSettings". Called when the settings ui is closed.
    public static void OnSettingsUiClosed()
    {
    }
}