using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class PlayerAnimationController : MonoBehaviour
    {
        AnimatorTriggerState animatorTriggerState;
        PlayerState playerState;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            animatorTriggerState = GetComponent<AnimatorTriggerState>();
            Assert.IsNotNull(animatorTriggerState, "AnimatorTriggerState cannot be null.");

            playerState = GetComponent<PlayerState>();
            Assert.IsNotNull(playerState, "Playerstate cannot be null.");

            spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(spriteRenderer, "SpriteRenderer cannot be null.");
        }

        void FixedUpdate()
        {
            if (playerState.IsMovingLeft())
            {
                spriteRenderer.flipX = true;
            }
            else if (playerState.IsMovingRight())
            {
                spriteRenderer.flipX = false;
            }

            animatorTriggerState.SetFloat("movementSpeed", playerState.movingSpeed);
        }
    }
}