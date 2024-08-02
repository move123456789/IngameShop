using Sons.Gui.Input;
using Sons.Multiplayer;
using SonsSdk;
using TheForest.Items.Craft;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace IngameShop
{
    public class ShopPrefabs
    {
        public static GameObject shop;
        public static Dictionary<string, GameObject> spawnedShops = new Dictionary<string, GameObject>();
        public static int myShopAmount = 0;

        public static void SetupShopPrefab()
        {
            if (shop == null)
            {
                if (Assets.Shop == null) { Misc.Msg("Cant Setup Shop Prefab, Shop Asset is null!"); return; }
                shop = GameObject.Instantiate(Assets.Shop);
                Mono.ShopInventory shopMono = shop.AddComponent<Mono.ShopInventory>();
                Mono.ShopGeneric shopGeneric = shop.AddComponent<Mono.ShopGeneric>();
                Mono.ShopWorldUi shopWorldUi = shop.AddComponent<Mono.ShopWorldUi>();
            }
        }

        public static GameObject SpawnShopPrefab(Vector3 position, Quaternion rotation, bool isOwner = true, string ownerName = "", ulong steamId = 0, string unqueId = null)
        {
            if (shop != null)
            {
                if (!isOwner) { Misc.Msg("[SpawnShopPrefab] Spawning Shop From Network"); }
                // Shop Amount Limit
                else { if (Network.Manager.MaxShopsAllowed != null) { if (myShopAmount >= Network.Manager.MaxShopsAllowed) { Misc.Msg("Maximum Amount Of Shops Create, Contact Server Owner/Host To Adjust Limit"); SonsTools.ShowMessage("Maximum Amount Of Shops Create, Contact Server Owner/Host To Adjust Limit"); return null; } } }
                GameObject shopCopy = GameObject.Instantiate(shop); // Creating Shop
                if (Config.NetworkDebugIngameShop.Value) { if (shopCopy == null) { Misc.Msg("[SpawnShopPrefab] shopCopy == null!"); } }  // Extra Logging

                shopCopy.transform.position = position;  // Set New Positiom
                shopCopy.transform.rotation = rotation;  // Set New Rotation

                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg($"[SpawnShopPrefab] Moved Prefab To Pos: {position}, Rot: {rotation}"); }  // Extra Logging
                Mono.ShopGeneric shopGeneric = shopCopy.GetComponent<Mono.ShopGeneric>();  // Generic Shop Component
                if (isOwner && shopGeneric != null)
                {
                    if (!string.IsNullOrEmpty(unqueId))
                    {
                        shopGeneric.UniqueId = unqueId;  // Set UniqueId if a valid one is recived from loading
                    }
                    else
                    {
                        shopGeneric.UniqueId = Guid.NewGuid().ToString();  // If you are owner and no id is supplied create new id
                    }
                    string username = Misc.GetLocalPlayerUsername();  // Get Player Usernmae
                    if (username != null) { shopGeneric.SetOwner(username); }  // SetUsername On Object And Update Shop Name UI
                    if (Misc.hostMode == Misc.SimpleSaveGameType.SinglePlayer)
                    {
                        shopGeneric.SteamId = 100; // FAKE ID
                    }
                    else
                    {
                        (ulong uSteamId, string sSteamId) = Misc.MySteamId();
                        if (string.IsNullOrEmpty(sSteamId))
                        {
                            shopGeneric.SteamId = 100;
                            Misc.Msg("[SpawnShopPrefab] Error Getting mySteamId from LocalPlayer");
                        }
                        else { shopGeneric.SteamId = uSteamId; }
                        
                    }
                    myShopAmount += 1;
                    GameObject add = shopCopy.transform.FindChild("Admin").FindChild("Add").gameObject;
                    GameObject recive = shopCopy.transform.FindChild("Admin").FindChild("Recive").gameObject;
                    LinkUiElement addLinkUi = add.AddComponent<LinkUiElement>(); ;
                    addLinkUi._applyMaterial = false;
                    addLinkUi._applyText = false;
                    addLinkUi._applyTexture = true;
                    addLinkUi._texture = Assets.InsertIcon;
                    addLinkUi._maxDistance = 2;
                    addLinkUi._worldSpaceOffset = new Vector3(0, 0, 0);
                    addLinkUi._text = "";
                    addLinkUi._uiElementId = "screen.take";
                    addLinkUi.enabled = false;
                    addLinkUi.enabled = true;

                    LinkUiElement reciveLinkUi = recive.AddComponent<LinkUiElement>(); ;
                    reciveLinkUi._applyMaterial = false;
                    reciveLinkUi._applyText = false;
                    reciveLinkUi._applyTexture = true;
                    reciveLinkUi._texture = Assets.TakeIcon;
                    reciveLinkUi._maxDistance = 2;
                    reciveLinkUi._worldSpaceOffset = new Vector3(0, 0, 0);
                    reciveLinkUi._text = "";
                    reciveLinkUi._uiElementId = "screen.take";
                    reciveLinkUi.enabled = false;
                    reciveLinkUi.enabled = true;

                    shopGeneric.add = addLinkUi;
                    shopGeneric.remove = reciveLinkUi;

                }
                else if (!isOwner && shopGeneric != null)
                {
                    if (!string.IsNullOrEmpty(unqueId))
                    {
                        shopGeneric.UniqueId = unqueId;  // Set UniqueId if a valid one is recived from loading
                    } else { Misc.Msg("[SpawnShopPrefab] Invalid UniqueId, Aborting"); GameObject.Destroy(shopCopy); return null; }
                    shopGeneric.SetOwner(ownerName);
                    shopGeneric.SteamId = steamId;

                    LinkUiElement item1LinkUi = shopCopy.transform.FindChild("BuyStation").FindChild("1").FindChild("LinkUI").gameObject.AddComponent<LinkUiElement>(); ;
                    item1LinkUi._applyMaterial = false;
                    item1LinkUi._applyText = false;
                    item1LinkUi._applyTexture = true;
                    item1LinkUi._texture = Assets.BuyIcon;
                    item1LinkUi._maxDistance = 2;
                    item1LinkUi._worldSpaceOffset = new Vector3(0, 0, 0);
                    item1LinkUi._text = "";
                    item1LinkUi._uiElementId = "screen.take";
                    item1LinkUi.enabled = false;
                    item1LinkUi.enabled = true;
                    shopGeneric.item1 = item1LinkUi;

                    LinkUiElement item2LinkUi = shopCopy.transform.FindChild("BuyStation").FindChild("2").FindChild("LinkUI").gameObject.AddComponent<LinkUiElement>(); ;
                    item1LinkUi._applyMaterial = false;
                    item1LinkUi._applyText = false;
                    item1LinkUi._applyTexture = true;
                    item1LinkUi._texture = Assets.BuyIcon;
                    item1LinkUi._maxDistance = 2;
                    item1LinkUi._worldSpaceOffset = new Vector3(0, 0, 0);
                    item1LinkUi._text = "";
                    item1LinkUi._uiElementId = "screen.take";
                    item1LinkUi.enabled = false;
                    item1LinkUi.enabled = true;
                    shopGeneric.item2 = item2LinkUi;

                    LinkUiElement item3LinkUi = shopCopy.transform.FindChild("BuyStation").FindChild("3").FindChild("LinkUI").gameObject.AddComponent<LinkUiElement>(); ;
                    item1LinkUi._applyMaterial = false;
                    item1LinkUi._applyText = false;
                    item1LinkUi._applyTexture = true;
                    item1LinkUi._texture = Assets.BuyIcon;
                    item1LinkUi._maxDistance = 2;
                    item1LinkUi._worldSpaceOffset = new Vector3(0, 0, 0);
                    item1LinkUi._text = "";
                    item1LinkUi._uiElementId = "screen.take";
                    item1LinkUi.enabled = false;
                    item1LinkUi.enabled = true;
                    shopGeneric.item3 = item3LinkUi;
                }

                if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer)
                {
                    // ADD TO SAVING
                }
                if (spawnedShops.ContainsKey(shopGeneric.UniqueId))
                {
                    Misc.Msg("[SpawnShopPrefab] List Already Contains Unique Id Destroying Object");
                    GameObject.Destroy(shopCopy); shopCopy = null;
                }
                else
                {
                    spawnedShops.Add(shopGeneric.UniqueId, shopCopy);
                }
                
                if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer || Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
                {
                    shopCopy.AddComponent<Mono.NetworkManager>();  // Add Network Component (This Syncs The Shop Over The Network)
                }

                return shopCopy;
            }
            else { Misc.Msg("[SpawnShopPrefab] Cant Spawn Shop Prefab, Shop is null!"); return null; }
        }

        public static GameObject FindShopByUniqueId(string uniqueId)
        {
            if (spawnedShops.TryGetValue(uniqueId, out GameObject shop))
            {
                return shop;
            }
            else
            {
                Misc.Msg($"Shop with unique ID {uniqueId} not found.");
                return null;
            }
        }

        public static bool DoesShopWithUniqueIdExist(string uniqueId)
        {
            if (spawnedShops.ContainsKey(uniqueId))
            {
                return true;
            }
            else
            {
                Misc.Msg($"Shop with unique ID {uniqueId} not found.");
                return false;
            }
        }
    }
}
