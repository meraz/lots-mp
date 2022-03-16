using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Lots
{
    [RequireComponent(typeof(PlayerClientMovement))]
    public class Player : NetworkBehaviour
    {
        public void SetSpriteColor()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
