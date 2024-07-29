using Endnight.Types;
using RedLoader;
using UnityEngine;



namespace IngameShop.Mono
{
    [RegisterTypeInIl2Cpp]
    internal class NetworkManager : MonoBehaviour
    {
        private Mono.ShopGeneric shopGeneric = null;
        private void Start()
        {
            if (Misc.hostMode == Misc.SimpleSaveGameType.Multiplayer || Misc.hostMode == Misc.SimpleSaveGameType.MultiplayerClient)
            {
                Misc.Msg("[NetworkManager] Sending Spawn Shop Network Event");
                if (shopGeneric == null) { shopGeneric = gameObject.GetComponent<Mono.ShopGeneric>(); }
                if (shopGeneric == null) { Misc.Msg("[NetworkManager] shopGeneric is NULL!"); }
                SimpleNetworkEvents.EventDispatcher.RaiseEvent(new Network.SpawnShop
                {
                    NameOfObject = "shop",
                    Vector3Position = Network.CustomSerializable.Vector3ToString(shopGeneric.transform.position),
                    QuaternionRotation = Network.CustomSerializable.QuaternionToString(shopGeneric.transform.rotation),
                    UniqueId = shopGeneric.UniqueId,
                    Sender = shopGeneric.SteamId.ToString(),
                    SenderName = Misc.GetLocalPlayerUsername(),

                });
            }
            
        }
    }
}
