using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class AddPlayerComponents : MonoBehaviour
    {
        NetworkEntityStatus networkEntityStatus;

        void Awake()
        {
            networkEntityStatus = GetComponent<NetworkEntityStatus>();
            Assert.IsNotNull(networkEntityStatus, "NetworkEntityStatus cannot be null");

            if (networkEntityStatus.IsOwner)
            {
                gameObject.AddComponent<PlayerSpawnProjectile>();
            }
            Destroy(this);
        }
    }

}