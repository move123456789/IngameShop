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
        }
    }

