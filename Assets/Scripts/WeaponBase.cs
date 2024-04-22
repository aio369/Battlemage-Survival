using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public enum DirectionOfAttack
{
    None,
    Forward,
    LeftRight,
    UpDown
}

public abstract class WeaponBase : MonoBehaviour
{
    public PlayerMove playerMove;
    public WeaponData weaponData;

    public WeaponStats weaponStats;

    public GameObject homingProjectilePrefab;

    float timer;

    Character wielder;
    public Vector2 vectorOfAttack;
    [SerializeField] DirectionOfAttack attackDirection;

    PoolManager poolManager;

    float spread = 1f;


    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }


   public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Attack();
            timer = weaponStats.timeToAttack;
        }
    }

    public void ApplyDamage(Collider2D[] colliders)
    {
        int damage = GetDamage();        
        for(int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if (e != null)
            {
                ApplyDamage(colliders[i].transform.position, damage, e);
            }
        }
    }

    public void ApplyDamage(Vector3 position, int damage, IDamageable e)
    {
        PostDamage(damage, position);
        e.TakeDamage(damage);
        ApplyAdditionalEffects(e, position);
    }

    private void ApplyAdditionalEffects(IDamageable e, Vector3 enemyPosition)
    {
        e.Stun(weaponStats.stun);
        e.Knockback((enemyPosition - transform.position).normalized, weaponStats.knockback, weaponStats.knockbackTimeWeight);

    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;

        weaponStats = new WeaponStats(wd.stats);
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    public virtual void Attack()
    {
        // Your existing attack logic

        if (homingProjectilePrefab != null)
        {
            LaunchHomingProjectile();
        }
    }

    public void LaunchHomingProjectile()
    {
        GameObject homingProjectileGO = Instantiate(homingProjectilePrefab, transform.position, Quaternion.identity);
        HomingProjectile homingProjectile = homingProjectileGO.GetComponent<HomingProjectile>();

        homingProjectile.target = FindNearestEnemy().transform;
        homingProjectile.speed = 5f; // You can adjust the speed as needed
        homingProjectile.enemyTag = "EnemyTag"; // Set the target tag for homing projectiles
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyTag");
        GameObject nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    //public abstract void Attack();


    public int GetDamage()
{
    int minDamage = (int)(weaponData.stats.minDamage * wielder.damageBonus);
    int maxDamage = (int)(weaponData.stats.maxDamage * wielder.damageBonus);

    // Return a random value between minDamage and maxDamage (inclusive)
    return UnityEngine.Random.Range(minDamage, maxDamage + 1);
}



    public virtual void PostDamage(int damage, Vector3 targetPosition)
    {
        MessageSystem.instance.PostMessage(damage.ToString(), targetPosition);
    }

    
    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

    public void AddOwnerCharacter(Character character)
    {
        wielder = character;
    }

    public void UpdateVectorOfAttack()
{
    if (attackDirection == DirectionOfAttack.None)
    {
        vectorOfAttack = Vector2.zero;
        return;
    }

    switch (attackDirection)
    {
        case DirectionOfAttack.Forward:
            vectorOfAttack.x = playerMove.lastHorizontalCoupledVector;
            vectorOfAttack.y = playerMove.lastVerticalCoupledVector;
            break;
        case DirectionOfAttack.LeftRight:
            vectorOfAttack.x = playerMove.lastHorizontalDeCoupledVector;
            vectorOfAttack.y = 0f;
            break;
        case DirectionOfAttack.UpDown:
            vectorOfAttack.x = 0f;
            vectorOfAttack.y = playerMove.lastVerticalDeCoupledVector;
            break;
    }

    // Apply spread
    vectorOfAttack = ApplySpread(vectorOfAttack, spread);
    vectorOfAttack = vectorOfAttack.normalized;
}

private Vector2 ApplySpread(Vector2 inputVector, float spreadAmount)
{
    // Rotate the input vector by a random angle within the spread range
    float spreadAngle = Random.Range(-spreadAmount / 2f, spreadAmount / 2f);
    Quaternion rotation = Quaternion.Euler(0f, 0f, spreadAngle);
    return rotation * inputVector;
}

    public GameObject SpawnProjectile(PoolObjectData poolObjectData, Vector3 position)
{
    GameObject projectileGO = poolManager.GetObject(poolObjectData);

    // Adjust the projectile's position based on the spread
    float spreadOffset = Random.Range(-spread / 2f, spread / 2f);
    Vector3 spreadPosition = new Vector3(position.x, position.y + spreadOffset, position.z);

    projectileGO.transform.position = spreadPosition;

    Projectile projectile = projectileGO.GetComponent<Projectile>();
    projectile.SetDirection(
        vectorOfAttack.x,
        vectorOfAttack.y
    );

    projectile.SetStats(this);

    return projectileGO;
}
}
