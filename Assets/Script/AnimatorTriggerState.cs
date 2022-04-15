using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using Unity.Netcode;

namespace Lots
{
    public class AnimatorTriggerState : NetworkBehaviour
    {
        public Animator animator;

        private Dictionary<string, float> floatTriggers = new Dictionary<string, float>();
        private Dictionary<string, bool> boolTriggers = new Dictionary<string, bool>();

        void Awake()
        {
            if (!animator)
            {
                animator = GetComponent<Animator>();
                Assert.IsNotNull(animator, "Animation cannot be null.");
            }
        }

        public void SetFloat(string name, float value)
        {
            if (!IsOwner)
            {
                return;
            }

            // TODO check if historical value is same
            floatTriggers[name] = value;

            animator.SetFloat(name, value);

            if (!IsHost)
            {
                SetFloatTriggerServerRpc(name, value);
            }
            else
            {
                SetFloatTriggerClientRpc(name, value);
            }
        }

        public void SetBool(string name, bool value)
        {
            if (!IsOwner)
            {
                return;
            }

            // TODO check if historical value is same
            boolTriggers[name] = value;
            animator.SetBool(name, value);

            if (!IsHost)
            {
                SetBoolTriggerServerRpc(name, value);
            }
            else
            {
                SetBoolTriggerClientRpc(name, value);
            }
        }

        [ServerRpc]
        public void SetFloatTriggerServerRpc(string name, float value)
        {
            SetFloatTriggerClientRpc(name, value);
        }

        [ClientRpc(Delivery = RpcDelivery.Unreliable)]
        public void SetFloatTriggerClientRpc(string name, float value)
        {
            if (!IsOwner)
            {
                // Only overwrite data if you're not the owner, the owner will have the value already set.
                animator.SetFloat(name, value);
            }
        }

        [ServerRpc]
        public void SetBoolTriggerServerRpc(string name, bool value)
        {
            SetBoolTriggerClientRpc(name, value);
        }

        [ClientRpc(Delivery = RpcDelivery.Unreliable)]
        public void SetBoolTriggerClientRpc(string name, bool value)
        {
            if (!IsOwner)
            {
                // Only overwrite data if you're not the owner, the owner will have the value already set.
                animator.SetBool(name, value);
            }
        }
    }
}