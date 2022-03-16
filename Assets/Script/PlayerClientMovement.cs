using Unity.Netcode;
using UnityEngine;

namespace Lots
{
    public class PlayerClientMovement : NetworkBehaviour
    {
        private Vector2 desiredPosition = Vector2.zero;

        public override void OnNetworkSpawn()
        {
            enabled = IsOwner;
            if (IsOwner)
            {
                ManualMove();
            }
        }

        public void ManualMove()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                SubmitPositionRequestClientRpc(GetRandomPosition());
            }
            else
            {
                desiredPosition = GetRandomPosition();
                ApplyDesiredPosition();
            }
        }

        [ClientRpc]
        void SubmitPositionRequestClientRpc(Vector2 position, ClientRpcParams rpcParams = default)
        {
            transform.position = position;
        }

        static Vector2 GetRandomPosition()
        {
            return new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
        }

        void FixedUpdate()
        {
            ComputeDesiredPosition();
            ApplyDesiredPosition();
        }

        void ComputeDesiredPosition()
        {
            float x = transform.position.x + 50f * Input.GetAxis("Horizontal") * Time.deltaTime;
            float y = transform.position.y + 50f * Input.GetAxis("Vertical") * Time.deltaTime;

            desiredPosition = new Vector2(x, y);
        }

        void ApplyDesiredPosition()
        {
            transform.position = desiredPosition;
        }
    }
}