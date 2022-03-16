using Unity.Netcode;
using UnityEngine;

namespace Lots
{
    public class Player : NetworkBehaviour
    {
        //public NetworkVariable<Vector2> desiredPosition = new NetworkVariable<Vector2>(Vector2.zero);
        Vector2 desiredPosition = Vector2.zero;
        //NetworkVariableReadPermission {WritePermission = NetworkVariableReadPermission.OwnerOnly}, Vector2.zero);

        private Vector2 lastPosition = Vector2.zero;

        public override void OnNetworkSpawn()
        {
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

        void Update()
        {
            ComputeDesiredPosition();
            ApplyDesiredPosition();
        }

        void ComputeDesiredPosition()
        {
            if (!IsOwner && NetworkManager.Singleton.IsServer)
            {
                return;
            }

            float x = transform.position.x + 50f * Input.GetAxis("Horizontal") * Time.deltaTime;
            float y = transform.position.y + 50f * Input.GetAxis("Vertical") * Time.deltaTime;
                Debug.Log("x:" + x);
                Debug.Log("y:" + y);

            desiredPosition = new Vector2(x, y);
        }

        void ApplyDesiredPosition()
        {
            lastPosition = transform.position;
            transform.position = desiredPosition;
        }
    }
}