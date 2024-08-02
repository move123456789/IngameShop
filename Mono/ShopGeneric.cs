using RedLoader;
using Sons.Gui;
using Sons.Gui.Input;
using TheForest.Items.Special;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.UI;
using SonsSdk;

namespace IngameShop.Mono
{
    [RegisterTypeInIl2Cpp]
    public class ShopGeneric : MonoBehaviour
    {
        private Text _shopText;
        private string _owner;
        public ulong? SteamId = null;
        public string UniqueId = null;
        private ShopInventory inventory;

        public LinkUiElement add;
        public LinkUiElement remove;
        public LinkUiElement item1;
        public LinkUiElement item2;
        public LinkUiElement item3;
        public bool shouldRunAdminUi = false;
        private GameObject Shop = null;

        private void Start ()
        {
            if (inventory == null) { inventory = GetComponent<ShopInventory>(); }  // Finds Inventory
            if (_owner == Misc.GetLocalPlayerUsername())
            {
                if (add != null && remove != null)
                {
                    shouldRunAdminUi = true;  // Makes Admin Code Runnable
                }
            }
            if (Shop == null) { Shop = gameObject; }
        }

        public void SetOwner (string owner)
        {
            _owner = owner;
            if (!string.IsNullOrEmpty(owner))
            {
                SetShopName(name: owner);
            }
            else { SetShopName(name: "Name Not Found"); }
            
        }

        public void SetShopName(string name)
        {
            if (_shopText == null)
            {
                Transform ownerTextTransform = gameObject.transform.FindDeepChild("OwnerText");
                if (ownerTextTransform != null)
                {
                    _shopText = ownerTextTransform.GetComponent<Text>();
                }
            }

            if (_shopText != null)
            {
                _shopText.text = $"{name}'s\nShop";
            }
        }

        public void UpdateShopFromNetwork(string ShopOwner, string ShopOwnerName, string UniqueID, string Vector3Position, string QuaternionRotation)
        {
            if (Config.NetworkDebugIngameShop.Value) { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Updating Shop From Network"); }

            ulong resultSteamID;
            if (ulong.TryParse(ShopOwner, out resultSteamID))
            {
                SteamId = resultSteamID;
            } else { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] ShopOwner SteamID Not Valid"); }
            if (!string.IsNullOrEmpty(ShopOwnerName))
            {
                SetOwner(ShopOwnerName);
            } else { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] ShopOwnerName Not Valid"); }
            if (!string.IsNullOrEmpty(UniqueID))
            {
                if (ShopPrefabs.DoesShopWithUniqueIdExist(UniqueID))
                {
                    GameObject shopByUniqueId = ShopPrefabs.FindShopByUniqueId(UniqueID);
                    if (shopByUniqueId != null)
                    {
                        if (shopByUniqueId != gameObject)
                        {
                            Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Shop GameObject In List Vs Attatched GameObject Has Mismatch! Trying To Fix");
                            if (ShopPrefabs.spawnedShops.ContainsValue(shopByUniqueId))
                            {
                                var keyValuePair = ShopPrefabs.spawnedShops.FirstOrDefault(x => x.Value == shopByUniqueId);
                                if (keyValuePair.Value != null) // Ensure the GameObject was found
                                {
                                    string key = keyValuePair.Key;
                                    GameObject.Destroy(keyValuePair.Value);
                                    ShopPrefabs.spawnedShops[key] = gameObject;
                                }
                                else
                                {
                                    Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Shop Not Found In Dict");
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (UniqueId != UniqueID)
                    {
                        UniqueId = UniqueID;
                        ShopPrefabs.spawnedShops.Add(UniqueID, gameObject);
                        if (Config.NetworkDebugIngameShop.Value) { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Changed UniqueId And Added Shop To SpawnedShops"); }

                    }
                }
                
            } else { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] UniqueID Not Valid"); }
            if (!string.IsNullOrEmpty(Vector3Position))
            {
                Vector3 pos = Network.CustomSerializable.Vector3FromString(Vector3Position);
                if (pos == Vector3.zero) { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Recived Vector3.zero From Network"); }
                if (!AreVectorsClose(pos, gameObject.transform.position, 0.5f))
                {
                    Misc.Msg($"[ShopGeneric] [UpdateShopFromNetwork] Shop Position is Not Simular. CurrentPos: {gameObject.transform.position}, NetworkPos: {pos}");
                    if (!AreVectorsClose(pos, Vector3.zero, 0.5f))
                    {
                        Misc.Msg($"[ShopGeneric] [UpdateShopFromNetwork] Moving Shop To: {pos}");
                        gameObject.transform.position = pos;
                    }
                }
            } else { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Vector3Position Not Valid"); }
            if (!string.IsNullOrEmpty(QuaternionRotation))
            {
                Quaternion rot = Network.CustomSerializable.QuaternionFromString(QuaternionRotation);
                if (rot == Quaternion.identity) { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] Recived Vector3.zero From Network"); }
                if (!AreQuaternionsClose(rot, gameObject.transform.rotation, 0.5f))
                {
                    Misc.Msg($"[ShopGeneric] [UpdateShopFromNetwork] Shop Rotation is Not Simular. CurrentRot: {gameObject.transform.rotation}, NetworkRot: {rot}");
                    if (!AreQuaternionsClose(rot, Quaternion.identity, 0.5f))
                    {
                        Misc.Msg($"[ShopGeneric] [UpdateShopFromNetwork] Rotating Shop To: {rot}");
                        gameObject.transform.rotation = rot;
                    }
                }
            } else { Misc.Msg("[ShopGeneric] [UpdateShopFromNetwork] QuaternionRotation Not Valid"); }
        }
        private bool AreVectorsClose(Vector3 vec1, Vector3 vec2, float threshold)
        {
            return Vector3.Distance(vec1, vec2) < threshold;
        }

        private bool AreQuaternionsClose(Quaternion quat1, Quaternion quat2, float threshold)
        {
            // Normalize the quaternions to avoid numerical precision issues
            quat1.Normalize();
            quat2.Normalize();

            // Calculate the absolute value of the dot product
            float dotProduct = Mathf.Abs(Quaternion.Dot(quat1, quat2));

            // Check if the dot product is greater than the threshold
            return dotProduct > threshold;
        }

        public string GetOwner()
        {
            return _owner;
        }

    }
}

