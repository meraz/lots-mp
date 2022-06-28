using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lots
{
    public class Entity : MonoBehaviour
    {
    /*public enum StatType
    {
        Uninitialized = 0,
        MovementSpeed,
        PhysicalResistance,
        DodgeChance,
        MaxLife,
        LifeRegen,
        CharacterWeight,
    };
    */

        // List<> internalAffector = 
        // List<> externalBuffs = 

        void Start()
        {

        }

        void Update()
        {

        }

        void UpdateStats()
        {
        }

        public string GetName()
        {
            return gameObject.name;
        }
    }
}
