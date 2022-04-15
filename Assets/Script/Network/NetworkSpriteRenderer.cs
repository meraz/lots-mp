
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class NetworkSpriteRenderer : NetworkBehaviour
    {
        public struct LotsSpriteRenderer // TODO meraz better name and perhaps move it
        {
            public LotsSpriteRenderer(bool flipX)
            {
                this.flipX = flipX;
            }
            public bool flipX;
        }

        //public bool syncFlipX = true; // TODO meraz use this
        //public bool syncFlipY = true; // TODO meraz implement support for even syncing more fields

        private readonly NetworkVariable<LotsSpriteRenderer> networkSpriteRenderer = new NetworkVariable<LotsSpriteRenderer>();
        private SpriteRenderer spriteRenderer;

        // Variables only needed for the owner
        private LotsSpriteRenderer lastState = new LotsSpriteRenderer(false);

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer, "SpriteRenderer cannot be null.");

            lastState.flipX = spriteRenderer.flipX;

            if (IsOwner)
            {
                GetNewState();
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                // No need to register/deregister callbacks as this change has been applied locally already
                return; 
            }
            networkSpriteRenderer.OnValueChanged += onNetworkChange;
        }

        public override void OnNetworkDespawn()
        {
            if (IsOwner)
            {
                 // No need to register/deregister callbacks as this change has been applied locally already
                return;
            }
            networkSpriteRenderer.OnValueChanged -= onNetworkChange;
        }

        public void onNetworkChange(LotsSpriteRenderer oldValue, LotsSpriteRenderer newValue)
        {
            spriteRenderer.flipX = newValue.flipX;
        }

        void Update()
        {
            if (!IsOwner)
            {
                // Only the owner needs to check for statechanges
                return;
            }

            if (hasStateChanged())
            {
                LotsSpriteRenderer newState = GetNewState();
                PropagateState(newState);
                lastState = newState;
            }
        }

        private bool hasStateChanged()
        {
            return lastState.flipX != spriteRenderer.flipX;
        }

        private LotsSpriteRenderer GetNewState()
        {
            LotsSpriteRenderer newState = lastState;
            newState.flipX = spriteRenderer.flipX;
            return newState;
        }

        private void PropagateState(LotsSpriteRenderer newState)
        {
            if (!IsHost)
            {
                PropagateStateServerRpc(newState);
            }
            else
            {
                // In-case the client is also the host then there is no need to do an RPC to the server
                networkSpriteRenderer.Value = newState;
            }
        }

        [ServerRpc]
        public void PropagateStateServerRpc(LotsSpriteRenderer newState)
        {
            networkSpriteRenderer.Value = newState;
        }
    }
}