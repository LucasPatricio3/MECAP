using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaster : MonoBehaviour
{
    [SerializeField] private BaseCharacter player;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            player.TakeDamage(new Damage(100));
        }
    }
}
