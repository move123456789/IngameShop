﻿using SonsSdk;
using SonsSdk.Attributes;
using UnityEngine;

namespace IngameShop
{
    [AssetBundle("shop")]
    public static class Assets
    {
        [AssetReference("Shop")]
        public static GameObject Shop { get; set; }

        [AssetReference("InsertWhite")]
        public static Texture2D InsertIcon { get; set; }

        [AssetReference("TakeWhite")]
        public static Texture2D TakeIcon { get; set; }

        [AssetReference("Buy")]
        public static Texture2D BuyIcon { get; set; }

        [AssetReference("InterActiveUI")]
        public static GameObject InterActiveUi { get; set; }
    }
}
