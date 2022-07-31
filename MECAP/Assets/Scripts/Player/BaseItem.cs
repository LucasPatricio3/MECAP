using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour
{
    public BaseCharacter player;
    public string itemName;
    public Image itemIcon;
    [System.NonSerialized] public BaseSkill itemSkill;
    public Image skillIcon;
    public float attackRange;
    public float attacksPerSecond;
    [TextArea(15, 20)] public string mainWeaponDescription;
    [TextArea(15, 20)] public string passiveDescription;
    public ReadOnlyCollection<BaseStat> statRequirements;
    public BuildPath buildPath;
    public virtual Damage MainWeaponDamageCalculation() 
    {
        return new Damage(player.attackPower.Value * 4);
    }
    public virtual void Equip(BaseCharacter character) { }
    public void Unequip(BaseCharacter character)
    {
        character.attackPower.RemoveAllModifiersFromSource(this);
        character.elementalPower.RemoveAllModifiersFromSource(this);
        character.maxHP.RemoveAllModifiersFromSource(this);
        character.healthRegen.RemoveAllModifiersFromSource(this);
        character.maxFocus.RemoveAllModifiersFromSource(this);
        character.armor.RemoveAllModifiersFromSource(this);
        character.elementalResistance.RemoveAllModifiersFromSource(this);
        character.cooldownReduction.RemoveAllModifiersFromSource(this);
        character.critChance.RemoveAllModifiersFromSource(this);
        character.critDamage.RemoveAllModifiersFromSource(this);
        character.speed.RemoveAllModifiersFromSource(this);
        character.attackSpeed.RemoveAllModifiersFromSource(this);
    }
    public T SkillType<T>() where T : BaseSkill,new()
    {
        T skillType = new T();
        return skillType;
    }
}
public struct BuildPath
{
    public BaseItem desiredItem;
    public BaseItem[] components;
    public BuildPath(BaseItem thisItem, BaseItem[] argComponents)
    {
        desiredItem = thisItem;
        components = argComponents;
    }
}
