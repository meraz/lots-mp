using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lots
{
public abstract class DamageStat
{
    public struct Damage
    {
        float physicalDamage;
        float elementalDamage;
        float mysticalDamage;
    }

    public struct HitMeta // TODO add this to hit so that meta exists
    {
        public string originName;
    }

    public struct Hit
    {
        public Hit(float damage, bool hit, bool crit)
        {
            this.damage = damage;
            this.hit = hit;
            this.crit = crit;
        }
        public float damage;
        public bool hit;
        public bool crit;
    }

    public enum StatType
    {
        Uninitialized = 0,
        CritChance,
        CritDamageMultiplier,
        ArmorPenetrationPercentage,
        ArmorPenetrationFlat,
        HitChanceIncrease // TODO meraz Need to fix this
        //Conversion // TODO how to solve conversion?
    };

    public int lastLayer = 10; // TODO meraz perhaps this should be static

    /**
        Entity origin can be an projectile as well?
    */
    public Hit ResolveAttack(int layer, Entity origin, Entity target)
    {
        /*
        for each layer
            hit = computeHitChance
            if hit
                baseDamamge = getDamamgeFromLastLayer() + baseDamageIncrease
                crit = computeCritChance
                if crit
                    critDamageMultiplier = computeCritDamageMultiplier
                    damage *= (1 + CritDamageMultiplier)
                    percentageDamageMitigation = calculatePercentageDamageMitigation

                    flatDamageMitigation = calculateflatDamageMitigation
                    damage = (damage * percentageDamageMitigation) - flatDamageMitigation
                    store
        **/
        return new Hit(0, true, false);
    }
}

}