using IngameShop.IngameTools;
using Sons.Items.Core;
using Sons.Prefabs;
using SonsSdk;
using SonsSdk.Attributes;
using SUI;
using TheForest.Utils;
using UnityEngine;

namespace IngameShop;

public class IngameShop : SonsMod
{
    public IngameShop()
    {

        // Uncomment any of these if you need a method to run on a specific update loop.
        //OnUpdateCallback = Update;
        //OnLateUpdateCallback = MyLateUpdateMethod;
        //OnFixedUpdateCallback = MyFixedUpdateMethod;
        //OnGUICallback = MyGUIMethod;

        // Uncomment this to automatically apply harmony patches in your assembly.
        //HarmonyPatchAll = true;
    }

    protected override void OnInitializeMod()
    {
        // Do your early mod initialization which doesn't involve game or sdk references here
        Config.Init();
    }

    protected override void OnSdkInitialized()
    {
        // Do your mod initialization which involves game or sdk references here
        // This is for stuff like UI creation, event registration etc.
        IngameShopUi.Create();

        // Adding Ingame CFG
        SettingsRegistry.CreateSettings(this, null, typeof(Config));
    }

    protected override void OnGameStart()
    {
        // This is called once the player spawns in the world and gains control.

        // Adding Quit Event
        SonsSdk.SdkEvents.OnInWorldUpdate.Subscribe(Misc.CheckHostModeOnWorldUpdate);
        Misc.OnHostModeGotten += Misc.OnHostModeGottenCorrectly;
    }

    internal static void OnLeaveWorld()
    {
        Misc.Msg("OnLeaveWorld");
        Misc.OnHostModeGotten -= Misc.OnHostModeGottenCorrectly;
        Misc.dialogManager.QuitGameConfirmDialog.remove_OnOption1Clicked((Il2CppSystem.Action)OnLeaveWorld);

        // Unregistering Network Events
        Network.Manager.UnregisterEvents();

        foreach (GameObject gameObject in ShopPrefabs.spawnedShops.Values)
        {
            GameObject.Destroy(gameObject);
        }
        ShopPrefabs.spawnedShops.Clear();
        GameObject.Destroy(ShopPrefabs.shop);
        ShopPrefabs.myShopAmount = 0;

    }

    [DebugCommand("shop")]
    private void shop(string args)
    {
        Misc.Msg("Spawning prefab");
        Transform transform = LocalPlayer._instance._mainCam.transform;
        RaycastHit raycastHit;
        Physics.Raycast(transform.position, transform.forward, out raycastHit, 25f, LayerMask.GetMask(new string[]
        {
                "Terrain",
                "Default",
                "Prop"
        }));
        switch (args.ToLower())
        {
            case "spawn":
                ShopPrefabs.SpawnShopPrefab(raycastHit.point + Vector3.up * 0.1f, LocalPlayer.Transform.rotation);
                Misc.Msg("Spawning Shop");
                break;
            case "s":
                ItemTesting(392, raycastHit, "held");
                break;
            case "s1":
                ItemTesting(78, raycastHit, "held");
                break;
            case "sync":
                IngameTools.TestSyncShop.SendSyncEventLookingAt();
                break;
            default:
                break;
        }
    }

    private void ItemTesting(int itemId, RaycastHit hit, string prefab)
    {
        Misc.Msg($"Spawning Object: {itemId}");
        string prefabName = prefab.ToLower();
        GameObject toBePreviewItem;
        if ( prefabName == "pickup")
        {
            toBePreviewItem = ItemDatabaseManager.ItemById(itemId).PickupPrefab.gameObject;
        } else if ( prefabName == "held")
        {
            toBePreviewItem = ItemDatabaseManager.ItemById(itemId).HeldPrefab.gameObject;
        } else if ( prefabName == "prop")
        {
            toBePreviewItem = ItemDatabaseManager.ItemById(itemId).PropPrefab.gameObject;
        }
        else { return; }
        
        if (toBePreviewItem != null)
        {
            Misc.Msg("[ShopWorldUi] [AddPreviewItem] Adding Preview Item");
            GameObject mjau = new GameObject("TestGameObject");

            mjau.transform.position = hit.point + Vector3.up * 0.1f;
            mjau.transform.rotation = LocalPlayer.Transform.rotation;

            mjau.AddGo($"{itemId}");

            GameObject toBeAddedTo = mjau.transform.FindChild($"{itemId}").gameObject;

            if (toBeAddedTo == null) { Misc.Msg("Something went worng in gettig gameobject to place preview prefab on"); return; }

            GameObject spawnedItem = GameObject.Instantiate(toBePreviewItem, toBeAddedTo.transform);

            Il2CppInterop.Runtime.InteropTypes.Arrays.Il2CppArrayBase<Component> list  = spawnedItem.GetComponents<Component>();
            for (int i = 0; i < list.Length; i++)
            {
                Component item = list[i];
                if (item.GetType() != typeof(Transform))
                {
                    GameObject.Destroy(item);
                }
            }

            spawnedItem.SetActive(true);
        }
        else { Misc.Msg("[ShopWorldUi] [AddPreviewItem] toBePreviewItem is NULL"); }
    }
}