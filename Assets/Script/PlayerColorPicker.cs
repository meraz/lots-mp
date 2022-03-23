using UnityEngine;
using Unity.Netcode;

namespace Lots
{
    public class PlayerColorPicker : NetworkBehaviour
    {
        NetworkColor networkColor;
        bool networkStarted = false;

        void Awake()
        {
          //  Debug.Log("PlayerColorPicker:Awake");
            networkColor = GetComponent<NetworkColor>();
        }

        public override void OnNetworkSpawn()
        {
         //   Debug.Log("PlayerColorPicker:OnNetworkSpawn");
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