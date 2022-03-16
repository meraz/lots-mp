using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lots
{
    public class PlayerColorPicker : MonoBehaviour
    {
        void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            Vector3 color = Random.onUnitSphere * 255f;
            float colorW = Random.Range(128, 255);
            spriteRenderer.color = new Color(color.x, color.y, color.z, colorW);
            Destroy(this);
        }
    }
}