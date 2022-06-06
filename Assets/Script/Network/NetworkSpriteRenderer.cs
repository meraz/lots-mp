
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class NetworkSpriteRenderer : NetworkBehaviour
    {
        public struct SpriteRendererData // TODO meraz perhaps move it
        {
            public static SpriteRendererData NewInstance()
            {
                return new SpriteRendererData(false);
            }

            private SpriteRendererData(bool flipX)
            {
                this.flipX = false;
                this.color = Color.magenta;
            }

            public bool flipX;
            public Color color;
        }

        // public bool syncFlipX = true; // TODO meraz
        // public bool syncFlipY = true; // TODO meraz

        private readonly NetworkVariable<SpriteRendererData> networkSpriteRenderer = new NetworkVariable<SpriteRendererData>();
        private SpriteRenderer spriteRenderer;

        // Variables only needed for the owner
        private SpriteRendererData lastState = SpriteRendererData.NewInstance();

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer, "SpriteRenderer cannot be null.");

            lastState.flipX = spriteRenderer.flipX;
            lastState.color = spriteRenderer.color;

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

        public void onNetworkChange(SpriteRendererData oldValue, SpriteRendererData newValue)
        {
            spriteRenderer.flipX = newValue.flipX;
            spriteRenderer.color = newValue.color;
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
                SpriteRendererData newState = GetNewState();
                PropagateState(newState);
                lastState = newState;
            }
        }

        private bool hasStateChanged()
        {
            return lastState.flipX != spriteRenderer.flipX || lastState.color != spriteRenderer.color;
        }

        private SpriteRendererData GetNewState()
        {
            SpriteRendererData newState = lastState;
            newState.flipX = spriteRenderer.flipX;
            newState.color = spriteRenderer.color;
            return newState;
        }

        private void PropagateState(SpriteRendererData newState)
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
        public void PropagateStateServerRpc(SpriteRendererData newState)
        {
            networkSpriteRenderer.Value = newState;
        }
    }
}