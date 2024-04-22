using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{

    public float timeToAttack;
    public int numberOfAttacks;
    public int numberOfHits;
    public float projectileSpeed;
    public float stun;
    public float knockback;
    public float knockbackTimeWeight;

    public float attackAreaSize;

    public int minDamage = 1;
    public int maxDamage = 10;



    public WeaponStats(WeaponStats stats)
    {
        
        this.timeToAttack = stats.timeToAttack;
        this.numberOfAttacks = stats.numberOfAttacks;
        this.numberOfHits = stats.numberOfHits;
        this.projectileSpeed = stats.projectileSpeed;
        this.stun = stats.stun;
        this.knockback = stats.knockback;
        this.knockbackTimeWeight = stats.knockbackTimeWeight;

        this.minDamage = stats.minDamage;
        this.maxDamage = stats.maxDamage;
    }

    internal void Sum(WeaponStats weaponUpgradeStats)
    {
        
        this.timeToAttack  += weaponUpgradeStats.timeToAttack;
        this.numberOfAttacks += weaponUpgradeStats.numberOfAttacks;
        this.numberOfHits += weaponUpgradeStats.numberOfHits;
        this.projectileSpeed += weaponUpgradeStats.projectileSpeed;
        this.stun += weaponUpgradeStats.stun;
        this.knockback += weaponUpgradeStats.knockback;
        this.knockbackTimeWeight += weaponUpgradeStats.knockbackTimeWeight;

        this.minDamage += weaponUpgradeStats.minDamage;
        this.maxDamage += weaponUpgradeStats.maxDamage;
    }
}



[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string Name;
    public string description;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
    
    public float attackAreaSize;


    public void InitFromEnergyFieldWeapon(EnergyFieldWeapon energyFieldWeapon)
    {
        attackAreaSize = energyFieldWeapon.attackAreaSize;
    }
}
