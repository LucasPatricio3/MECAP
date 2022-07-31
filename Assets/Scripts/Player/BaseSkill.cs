using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseSkill
{
    public string skillName;
    public string descripton;
    public float? cooldown;
    public float? focusCost;
    public abstract void DoSkill(BaseCharacter character);
}
