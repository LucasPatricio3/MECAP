using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Longsword : BaseItem
{
    private void Awake()
    {
        itemName = "Longsword";
        itemSkill = SkillType<LongswordSkill>();
        attackRange = 3;
        attacksPerSecond = 1.0f;
        passiveDescription = "Strong Will: Gain +5 Health Regen.";
        statRequirements = null;
        buildPath = new BuildPath(this, null);
    }
    private void Start()
    {
        Equip(player);
    }
    public override void Equip(BaseCharacter character)
    {
        MainWeaponDamageCalculation();
        character.healthRegen.AddModifier(new StatModifier(5, StatModifierType.Flat, this));
    }
    public override Damage MainWeaponDamageCalculation()
    {
        mainWeaponDescription = $"Adds 15% Attack Scaling to the Basic Attack({player.attackPower.Value * 4.15f}) Physical Damage to Basic Attacks and attack range become {attackRange}.";
        return new Damage(player.attackPower.Value * 4.15f);
    }
}