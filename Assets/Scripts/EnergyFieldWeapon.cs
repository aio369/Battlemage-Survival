using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFieldWeapon : WeaponBase
{
    [SerializeField] public float attackAreaSize = 3f;

    public override void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackAreaSize);
        ApplyDamage(colliders);
    }


    public WeaponData CreateWeaponData()
    {
        WeaponData data = ScriptableObject.CreateInstance<WeaponData>();
        data.InitFromEnergyFieldWeapon(this);
        return data;
    }

}
