using Unity.Netcode;
using UnityEngine;
using UnityEngine.Assertions;
using ExtensionMethods;

namespace Lots
{
    public class PlayerClientMovement : NetworkBehaviour
    {
        private Vector2 desiredPosition = Vector2.zero;
        public float movementSpeed = 5f;

        PlayerState playerState;

        void Awake()
        {
            playerState = GetComponent<PlayerState>();
            Assert.IsNotNull(playerState, "Playerstate cannot be null.");
        }

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
            desiredPosition = GetRandomPosition();
            ApplyDesiredPosition();
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
            float horizontalDirection = Input.GetAxis("Horizontal");
            float verticalDirection = Input.GetAxis("Vertical");
            //Debug.Log($"horizontalDirection={horizontalDirection}");
            //Debug.Log($"verticalDirection={verticalDirection}");
            float x = transform.position.x + movementSpeed * horizontalDirection * Time.deltaTime;
            float y = transform.position.y + movementSpeed * verticalDirection * Time.deltaTime;
            desiredPosition = new Vector2(x, y);

            Vector2 deltaStep = desiredPosition - transform.position.xy();
            playerState.UpdateState(deltaStep);
        }

        void ApplyDesiredPosition()
        {
            transform.position = desiredPosition;
        }
    }
}