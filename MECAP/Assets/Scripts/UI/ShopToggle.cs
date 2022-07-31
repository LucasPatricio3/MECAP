using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopToggle : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }
}
