using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongswordSkill : BaseSkill
{
    public override void DoSkill(BaseCharacter character)
    {
        
    }
    public LongswordSkill()
    {
        skillName = "Steel Thrust";
        descripton = "";
        focusCost = 30;
        cooldown = 6;
    }
}
