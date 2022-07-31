using System.Collections;
using System.Collections.Generic;

public struct Damage
{
    public float physicalDamage;
    public float elementalDamage;
    public float trueDamage;
    public static Damage operator +(Damage baseDamage, Damage additionalDamage) => new Damage(baseDamage.physicalDamage + additionalDamage.physicalDamage, baseDamage.elementalDamage + additionalDamage.elementalDamage, baseDamage.trueDamage + additionalDamage.trueDamage);
    public Damage(float PhysicalDamage)
    {
        physicalDamage = PhysicalDamage;
        elementalDamage = 0;
        trueDamage = 0;
    }
    public Damage(float PhysicalDamage, float MagicalDamage)
    {
        physicalDamage = PhysicalDamage;
        elementalDamage = MagicalDamage;
        trueDamage = 0;
    }
    public Damage(float PhysicalDamage, float MagicalDamage, float TrueDamage)
    {
        physicalDamage = PhysicalDamage;
        elementalDamage = MagicalDamage;
        trueDamage = TrueDamage;
    }
}
