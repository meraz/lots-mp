using UnityEngine;
using Unity.Netcode;
using UnityEngine.Assertions;

namespace Lots
{
    public class PlayerColorPicker : NetworkBehaviour
    {
        NetworkColor networkColor;
        bool networkStarted = false;

        void Awake()
        {
            networkColor = GetComponent<NetworkColor>();
            Assert.IsNotNull(networkColor, "Network color cannot be null.");
        }

        public override void OnNetworkSpawn()
        {
            networkStarted = true;
        }

        void LateUpdate()
        {
            if (!networkStarted)
            {
                return;
            }

            if (IsOwner)
            {
                SetPlayerColor(ComputePlayerColor());
            }

            Destroy(this);
        }

        void SetPlayerColor(Color color)
        {
            networkColor.SetColorRequestServerRpc(color);
        }

        Color ComputePlayerColor()
        {
            Vector3 color = Random.onUnitSphere * 255f;
            float colorW = Random.Range(128, 255);
            return new Color(color.x, color.y, color.z, colorW);
        }
    }
}