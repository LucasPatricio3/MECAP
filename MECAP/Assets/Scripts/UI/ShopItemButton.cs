using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopItemButton : MonoBehaviour
{
	private Button button;
	public GameObject itemPrefab;
	[SerializeField]private BaseCharacter tempParent;

	void Start()
	{
		button = this.GetComponent<Button>();
		button.onClick.AddListener(TaskOnClick);
	}
	void TaskOnClick()
	{
		Debug.Log($"Debug: You have clicked the {gameObject.name}.");
        if (tempParent.mainWeapon == null || tempParent.relic1 == null || tempParent.relic2 == null || tempParent.relic3 == null)
        {
			Instantiate(itemPrefab, tempParent.transform);
			itemPrefab.GetComponent<BaseItem>().player = tempParent;
			if (tempParent.mainWeapon == null) tempParent.mainWeapon = itemPrefab.GetComponent<BaseItem>();
			else if (tempParent.mainWeapon != null && tempParent.relic1 == null) tempParent.relic1 = itemPrefab.GetComponent<BaseItem>();
			else if (tempParent.mainWeapon != null && tempParent.relic1 != null && tempParent.relic2 == null) tempParent.relic2 = itemPrefab.GetComponent<BaseItem>();
			else if (tempParent.mainWeapon != null && tempParent.relic1 != null && tempParent.relic2 != null) tempParent.relic3 = itemPrefab.GetComponent<BaseItem>();
		}
	}
}
