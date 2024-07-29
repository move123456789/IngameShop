

using RedLoader;
using Sons.Items.Core;
using SonsSdk;
using TheForest.Utils;
using UnityEngine;

namespace IngameShop.IngameTools
{
    public static class AttatchToShop
    {
        public static GameObject GetHeldPrefab(int itemId)
        {
            return ItemDatabaseManager.ItemById(itemId).HeldPrefab.gameObject;
        }

        public static GameObject GetPickupPrefab(int itemId)
        {
            return ItemDatabaseManager.ItemById(itemId).PickupPrefab.gameObject;
        }

        public static string AttatchToCloseStore(GameObject gameObject)
        {
            if (gameObject == null) return "Failed";
            {
                RaycastHit[] hits = Physics.SphereCastAll(LocalPlayer.Transform.position, 10f, LocalPlayer.Transform.forward, LayerMask.GetMask(new string[]
                {
                        "Default"
                }));
                foreach (var hit in hits)
                {
                    if (hit.transform.root.name != null)
                    {
                        if (hit.transform.root.name.Contains("Shop"))
                        {
                            GameObject shop = hit.transform.root.gameObject;
                            GameObject item1 = shop.transform.FindChild("BuyStation").FindChild("1").FindChild("Item").gameObject;
                            item1.AddGo("AddedByAttatch");
                            GameObject addedByAttatch = item1.transform.FindChild("AddedByAttatch").gameObject;
                            GameObject spawnedItem = GameObject.Instantiate(gameObject, addedByAttatch.transform);

                            Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<Component> list = spawnedItem.GetComponents<Component>();
                            for (int i = 0; i < list.Length; i++)
                            {
                                Component item = list[i];
                                if (item.GetType() != typeof(Transform))
                                {
                                    GameObject.Destroy(item);
                                }
                            }

                            spawnedItem.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                            GameObject pickupGui = spawnedItem.transform.FindChild("_PickupGui_").gameObject;
                            pickupGui.SetActive(false);

                            GameObject grassPusher = spawnedItem.transform.FindChild("GrassPusherPiv").gameObject;
                            grassPusher.SetActive(false);

                            GameObject trigger = spawnedItem.transform.FindChild("Trigger").gameObject;
                            trigger.SetActive(false);

                            GameObject dragLocation = spawnedItem.transform.FindChild("DragLocation").gameObject;
                            dragLocation.SetActive(false);

                            GameObject grabPosition = spawnedItem.transform.FindChild("GrabPosition").gameObject;
                            grabPosition.SetActive(false);

                            GameObject vailPickupTransform = spawnedItem.transform.FindChild("VailPickupTransform").gameObject;
                            vailPickupTransform.SetActive(false);

                            GameObject meleeCollider = spawnedItem.transform.FindChild("MeleeCollider").gameObject;
                            meleeCollider.SetActive(false);

                            GameObject waterSensor = spawnedItem.transform.FindChild("WaterSensor").gameObject;
                            waterSensor.SetActive(false);

                            GameObject.Destroy(pickupGui);
                            GameObject.Destroy(grassPusher);
                            GameObject.Destroy(trigger);
                            GameObject.Destroy(dragLocation);
                            GameObject.Destroy(grabPosition);
                            GameObject.Destroy(vailPickupTransform);
                            GameObject.Destroy(meleeCollider);
                            GameObject.Destroy(waterSensor);

                            return "Added To Slot 1";
                        }
                    }
                }
                return "Failed To Find Shop";
            }
        }

        public static string RemoveFromCloseStore(GameObject gameObject)
        {
            if (gameObject == null) return "Failed";
            {
                RaycastHit[] hits = Physics.SphereCastAll(LocalPlayer.Transform.position, 10f, LocalPlayer.Transform.forward, LayerMask.GetMask(new string[]
                {
                        "Default"
                }));
                foreach (var hit in hits)
                {
                    if (hit.transform.root.name != null)
                    {
                        if (hit.transform.root.name.Contains("Shop"))
                        {
                            GameObject shop = hit.transform.root.gameObject;
                            GameObject item1 = shop.transform.FindChild("BuyStation").FindChild("1").FindChild("Item").gameObject;
                            GameObject addedByAttatch = item1.transform.FindChild("AddedByAttatch").gameObject;
                            if (addedByAttatch != null)
                            {
                                addedByAttatch.SetActive(false);
                                GameObject.Destroy(addedByAttatch);
                                return "Removed GameObject";
                            }
                            return "Failed To Find AddedByAttatch GameObject";

                        }
                    }
                }
                return "Failed To Find Shop";
            }
        }
    }
}
