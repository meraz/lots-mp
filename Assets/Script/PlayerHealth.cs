using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Lots
{
    public class PlayerHealth : NetworkBehaviour
    {
        private readonly NetworkVariable<int> health_ = new NetworkVariable<int>(100);

        public override void OnNetworkSpawn()
        {
            if (!IsClient)
            {
                enabled = false;
            }
        }
    }
}
