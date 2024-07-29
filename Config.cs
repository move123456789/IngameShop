using RedLoader;

namespace IngameShop;

public static class Config
{
    internal static ConfigCategory IngameShopCategory { get; private set; }

    public static ConfigEntry<bool> DebugLoggingIngameShop { get; private set; }
    public static ConfigEntry<bool> NetworkDebugIngameShop { get; private set; }
    public static ConfigEntry<int> MaxShops { get; private set; }
    internal static void Init()
    {
        IngameShopCategory = ConfigSystem.CreateCategory("ingameShop", "IngameShop");

        MaxShops = IngameShopCategory.CreateEntry(
            "max_shop_ingameshop",
            2,
            "Max Shops Player Can Have (Host Only)",
            "Max Amount Of Shops A Player Can Have, Can Only Be Adjusted By Host");
        MaxShops.SetRange(0, 10);

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
    }

    // Same as the callback in "CreateSettings". Called when the settings ui is closed.
    public static void OnSettingsUiClosed()
    {
    }
}