using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 100;
    public int currentHp = 100;

    public int armor = 0;

    public float hpRegenretationRate = 1f;
    public float hpRegenerationTimer;

    public float damageBonus;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;
    private bool isDead;

    [SerializeField] DataContainer dataContainer;
    
    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }


    private void Start()
    {
        LoadSelectedCharacter(dataContainer.selectedCharacter);
        ApplyPersistantUpgrades();
        hpBar.SetState(currentHp, maxHp);
    }

    private void LoadSelectedCharacter(CharacterData selectedCharacter)
    {
        InitAnimation(selectedCharacter.spritePrefab);
        GetComponent<WeaponManager>().AddWeapon(selectedCharacter.startingWeapon);
    }

    private void InitAnimation(GameObject spritePrefab)
    {
        GameObject animObject = Instantiate(spritePrefab, transform);
        GetComponent<Animate>().SetAnimate(animObject);
    }

    private void ApplyPersistantUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);

        maxHp =+ maxHp / 10 + hpUpgradeLevel;
        currentHp = maxHp;

        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);

        damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    private void Update()
    {
        hpRegenerationTimer += Time.deltaTime * hpRegenerationTimer;

        if (hpRegenerationTimer > 1f)
        {
            Heal(1);
            hpRegenerationTimer -= 1f;
        }
    }


    public void TakeDamage(int damage)
    {
        if(isDead == true) { return; }
        ApplyArmor(ref damage);
        currentHp -= damage;

        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }
        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0) { damage = 0; }
    }

    public void Heal(int amount)
    {
        if (currentHp <= 0) { return; }

        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBar.SetState(currentHp, maxHp);
    }
}
