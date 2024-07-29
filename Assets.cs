using SonsSdk;
using SonsSdk.Attributes;
using UnityEngine;

namespace IngameShop
{
    [AssetBundle("shop")]
    public static class Assets
    {
        [AssetReference("Shop")]
        public static GameObject Shop { get; set; }

        [AssetReference("Insert")]
        public static Texture2D InsertIcon { get; set; }

        [AssetReference("Take")]
        public static Texture2D TakeIcon { get; set; }
    }
}
