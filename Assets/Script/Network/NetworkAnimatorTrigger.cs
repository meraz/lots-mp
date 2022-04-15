using UnityEngine.Assertions;
using Unity.Netcode;

namespace Lots
{
    public class NetworkAnimatorTrigger : NetworkBehaviour
    {
        AnimatorTriggerState animatorTriggerState;

        void Awake()
        {
            animatorTriggerState = GetComponent<AnimatorTriggerState>();
            Assert.IsNotNull(animatorTriggerState, "AnimatorTriggerState cannot be null.");
        }

    }
}