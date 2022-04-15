using UnityEngine.Assertions;
using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

namespace Lots
{
    public class NetworkAnimatorTrigger : NetworkBehaviour
    {
        private AnimatorTriggerState animatorTriggerState; // Origin data to read by owner
        private Animator animator; // Target data data to write by client

        void Awake()
        {
            animatorTriggerState = GetComponent<AnimatorTriggerState>();
            Assert.IsNotNull(animatorTriggerState, "AnimatorTriggerState cannot be null.");

            animator = GetComponent<Animator>();
            Assert.IsNotNull(animator, "Animator cannot be null.");
        }
    }
}