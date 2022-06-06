using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class PlayerColorPicker : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;
        NetworkEntityStatus networkEntityStatus;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer, "SpriteRenderer cannot be null");
            networkEntityStatus = GetComponent<NetworkEntityStatus>();
            Assert.IsNotNull(networkEntityStatus, "NetworkEntityStatus cannot be null");
        }

        void OnGUI()
        {
            if(networkEntityStatus.IsOwner)
            {
                GUILayout.BeginArea(new Rect(10, 300, 300, 300));
                if (GUILayout.Button("New Color"))
                {
                    spriteRenderer.color = ComputePlayerColor();
                } 
                GUILayout.EndArea();
            }
        }

        Color ComputePlayerColor()
        {
            Vector3 color = Random.onUnitSphere * 255f;
            float colorW = Random.Range(128, 255);
            return new Color(color.x, color.y, color.z, colorW);
        }
    }
}