using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace Lots
{
    public class AnimatorTriggerState : MonoBehaviour
    {
        public Animator animator;

        private Dictionary<string, float> floatTriggers = new Dictionary<string, float>();
        private Dictionary<string, bool> boolTriggers = new Dictionary<string, bool>();

        void Awake()
        {
            if(!animator)
            {
                animator = GetComponent<Animator>();
                Assert.IsNotNull(animator, "Animation cannot be null.");
            }
        }

        public void SetFloat(string name, float value)
        {
            // TODO meraz push data to network?
            floatTriggers[name] = value;
            animator.SetFloat(name, value);
        }

        public void SetBool(string name, bool value)
        {
            // TODO meraz push data to network?
            boolTriggers[name] = value;
            animator.SetBool(name, value);
        }
    }
}