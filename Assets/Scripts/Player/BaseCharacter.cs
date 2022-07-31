using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public enum Team
    {
        Blue,
        Red,
        Neutral
    }
    public Team team;
    public string charName;
    public int level;
    [System.NonSerialized] public BaseStat attackPower;
    [System.NonSerialized] public BaseStat elementalPower;
    [System.NonSerialized] public BaseStat maxHP;
    [SerializeField] protected float currentHP;
    public event Action<float,float> OnHealthChanged = delegate { };
    [System.NonSerialized] public BaseStat healthRegen;
    [System.NonSerialized] public BaseStat maxFocus;
    [SerializeField] protected float currentFocus;
    public event Action<float, float> OnFocusChanged = delegate { };
    [System.NonSerialized] public BaseStat armor;
    [System.NonSerialized] public BaseStat elementalResistance;
    [System.NonSerialized] public BaseStat cooldownReduction;
    [System.NonSerialized] public BaseStat critChance;
    [System.NonSerialized] public BaseStat critDamage;
    [System.NonSerialized] public BaseStat speed;
    [System.NonSerialized] public BaseStat attackSpeed;

    public BaseItem mainWeapon;
    public BaseItem relic1;
    public BaseItem relic2;
    public BaseItem relic3;

    public void TakeDamage(Damage damage)
    {
        if (currentHP > 0)
        {
            currentHP -= damage.physicalDamage + damage.elementalDamage + damage.trueDamage;
            OnHealthChanged(currentHP, maxHP.Value);
        }
        if (currentHP <= 0)
        {
            currentHP = 0;
            gameObject.SetActive(false);
        }
    }
    protected float RefillHealthPercentage(float percentage)
    {
        return (maxHP.Value * percentage);
    }
    protected float RefillManaPercentage(float percentage)
    {
        return (maxFocus.Value * percentage);
    }
    public void PrintAllStatValues()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print(
            $"Character Level: {level}\n" +
            $"Character Attack: {attackPower.Value}\n" +
            $"Character Spell Power: {elementalPower.Value}\n" +
            $"Character Max Health: {maxHP.Value}\n" +
            $"Character Current Health: {currentHP}\n" +
            $"Character Health Regeneration: {healthRegen.Value}\n" +
            $"Character Armor: {armor.Value}\n" +
            $"Character Spell Resistance: {elementalResistance.Value}\n" +
            $"Character Cooldown Reduction: {cooldownReduction.Value}\n" +
            $"Character Critical Chance: {critChance.Value}\n" +
            $"Character Critical Damage: {critDamage.Value}\n" +
            $"Character Movement Speed: {speed.Value}\n" +
            $"Character Attack Speed: {attackSpeed.Value}\n"
            );
        }
    }
    public IEnumerator BasicAttack(BaseCharacter target, bool performAttack)
    {
        int secondToWait = (int)(10 / (attackSpeed.Value * (mainWeapon == null? 1 : mainWeapon.attacksPerSecond)));
        yield return new WaitForSeconds(secondToWait);
        Damage weaponDamage = mainWeapon.MainWeaponDamageCalculation();
        target.TakeDamage(weaponDamage);
        yield return null;
    }
    public void Skill1()
    {
        if (Input.GetKeyDown(KeyCode.Q) && mainWeapon != null)
        {
            mainWeapon.itemSkill.DoSkill(this);
        }
    }
    public void Skill2()
    {
        if (Input.GetKeyDown(KeyCode.W) && relic1 != null)
        {
            relic1.itemSkill.DoSkill(this);
        }
    }
    public void Skill3()
    {
        if (Input.GetKeyDown(KeyCode.E) && relic2 != null)
        {
            relic2.itemSkill.DoSkill(this);
        }
    }
    public void Skill4()
    {
        if (Input.GetKeyDown(KeyCode.R) && relic3 != null)
        {
            relic3.itemSkill.DoSkill(this);
        }
    }
    private void Update()
    {
        Skill1();
        Skill2();
        Skill3();
        Skill4();
        PrintAllStatValues();
    }
    private void Awake()
    {
        attackPower = new BaseStat(10);
        elementalPower = new BaseStat(0);
        maxHP = new BaseStat(400);
        healthRegen = new BaseStat(1.5f);
        maxFocus = new BaseStat(200);
        armor = new BaseStat(10);
        elementalResistance = new BaseStat(10);
        cooldownReduction = new BaseStat(0);
        critChance = new BaseStat(0);
        critDamage = new BaseStat(150);
        speed = new BaseStat(7);
        attackSpeed = new BaseStat(10);
        currentHP = maxHP.Value;
        currentFocus = maxFocus.Value;
    }
}
