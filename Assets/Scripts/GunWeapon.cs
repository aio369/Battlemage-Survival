using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeapon : WeaponBase
{

    [SerializeField] PoolObjectData bulletPrefab;
    [SerializeField] float spread = 0.5f;

    public override void Attack()
{
    UpdateVectorOfAttack();
    for (int i = 0; i < weaponStats.numberOfAttacks; i++)
    {
        Vector3 newBulletPosition = transform.position + transform.up * i * spread;
        SpawnProjectile(bulletPrefab, newBulletPosition);
    }
}
}
