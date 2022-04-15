using UnityEngine;
using UnityEngine.Assertions;

namespace Lots
{
    public class NetworkAnimatorTrigger : MonoBehaviour
    {
        Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
            Assert.IsNotNull(animator, "Animation cannot be null.");
            // TODO meraz not complete
        }
    }
}