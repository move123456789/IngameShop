using RedLoader;
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

        private void Start ()
        {

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
