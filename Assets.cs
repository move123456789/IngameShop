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
    }
}
