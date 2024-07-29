using Sons.Multiplayer;
using TheForest.Utils;
using UnityEngine;

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
            }
        }

        public static GameObject SpawnShopPrefab(Vector3 position, Quaternion rotation, bool isOwner = true, string ownerName = "", ulong steamId = 0, string unqueId = null)
        {
            if (shop != null)
            {
                if (!isOwner) { Misc.Msg("[SpawnShopPrefab] Spawning Shop From Network"); }
                else { if (Network.Manager.MaxShopsAllowed != null) { if (myShopAmount >= Network.Manager.MaxShopsAllowed) { Misc.Msg("Maximum Amount Of Shops Create, Contact Server Owner/Host To Adjust Limit"); return null; } } }
                GameObject shopCopy = GameObject.Instantiate(shop);
                if (Config.NetworkDebugIngameShop.Value) { if (shopCopy == null) { Misc.Msg("[SpawnShopPrefab] shopCopy == null!"); } }
                shopCopy.transform.position = position;
                shopCopy.transform.rotation = rotation;
                if (Config.NetworkDebugIngameShop.Value) { Misc.Msg($"[SpawnShopPrefab] Moved Prefab To Pos: {position}, Rot: {rotation}"); }
                Mono.ShopGeneric shopGeneric = shopCopy.GetComponent<Mono.ShopGeneric>();
                if (isOwner && shopGeneric != null)
                {
                    if (!string.IsNullOrEmpty(unqueId))
                    {
                        shopGeneric.UniqueId = unqueId;
                    }
                    else
                    {
                        shopGeneric.UniqueId = Guid.NewGuid().ToString();
                    }
                    string username = Misc.GetLocalPlayerUsername();
                    if (username != null) { shopGeneric.SetOwner(username); }
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

                }
                else if (!isOwner && shopGeneric != null)
                {
                    shopGeneric.SetOwner(ownerName);
                    shopGeneric.SteamId = steamId;
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
                    shopCopy.AddComponent<Mono.NetworkManager>();
                }

                return shopCopy;
            }
            else { Misc.Msg("[SpawnShopPrefab] Cant Spawn Shop Prefab, Shop is null!"); return null; }
        }
    }
}
