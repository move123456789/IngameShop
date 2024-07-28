using RedLoader;

namespace IngameShop;

public static class Config
{
    internal static ConfigCategory IngameShopCategory { get; private set; }

    public static ConfigEntry<bool> DebugLoggingIngameShop { get; private set; }

    internal static void Init()
    {
        IngameShopCategory = ConfigSystem.CreateCategory("ingameShop", "IngameShop");

        DebugLoggingIngameShop = IngameShopCategory.CreateEntry(
            "enable_logging_advanced_ingameshop",
            false,
            "Enable Debug Logs",
            "Enables SimpleNetworkEvents Debug Logs of the game to the console.");
    }

    // Same as the callback in "CreateSettings". Called when the settings ui is closed.
    public static void OnSettingsUiClosed()
    {
    }
}