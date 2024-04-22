using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashWeapon : WeaponBase
{
    [SerializeField] GameObject leftSlashObject;
    [SerializeField] GameObject rightSlashObject;


    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);



    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if(playerMove.lastHorizontalDeCoupledVector > 0)
            {
                rightSlashObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightSlashObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else {
                leftSlashObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftSlashObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
