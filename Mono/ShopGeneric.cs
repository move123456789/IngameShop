using RedLoader;
using Sons.Gui.Input;
using TheForest.Items.Special;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.UI;

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
        private bool shouldRunAdminUi = false;
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

        private void Update()
        {
            if (shouldRunAdminUi)
            {
                if (Vector3.Distance(LocalPlayer.Transform.position, Shop.transform.position) > 10) { return; }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (add.IsActive)
                    {
                        IHeldOnlyItemController heldController = LocalPlayer.Inventory.HeldOnlyItemController;
                        if (heldController != null)
                        {
                            if (heldController.Amount > 0)
                            {
                                if (heldController.Amount == 1)
                                {
                                    heldController.PutDown(false, false, false);
                                    int item = heldController.HeldItem._itemID;
                                    inventory.AddItem(item, 1);
                                }
                                else
                                {
                                    int item = heldController.HeldItem._itemID;
                                    heldController.PutDown(false, false, false);
                                    inventory.AddItem(item, heldController.Amount);
                                }
                            }
                            else
                            {
                                IngameShopUi.OpenPanel("ShopAdminUi");
                            }
                        }
                        //if (!LocalPlayer.Inventory.IsLeftHandEmpty())
                        //{
                        //    Misc.Msg($"Left Hand Holding: Name: {LocalPlayer.Inventory.LeftHandItem.Data.Name} ItemId: {LocalPlayer.Inventory.LeftHandItem._itemID}");
                        //}
                        //if (!LocalPlayer.Inventory.IsRightHandEmpty())
                        //{
                        //    Misc.Msg($"Right Hand Holding: Name: {LocalPlayer.Inventory.RightHandItem.Data.Name} ItemId: {LocalPlayer.Inventory.RightHandItem._itemID}");
                        }
                    }
                    else if (remove.IsActive)
                    {
                        
                    }
                }
            }
        }
    }
}
