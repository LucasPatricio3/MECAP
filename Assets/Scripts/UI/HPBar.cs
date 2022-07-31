using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private AnimationCurve updateCurve;
    [SerializeField] private BaseCharacter character;
    [SerializeField] private float updateSpeed;
    internal bool healthChanged = false;
    internal float pct;
    void Awake()
    {
        character.OnHealthChanged += HandleHealthChange;
    }

    private void HandleHealthChange(float currentHP, float maxHP)
    {
        healthChanged = true;
        pct = currentHP / maxHP;
    }
    private IEnumerator ChangeToHPPercent(float percent)
    {
        float elapsedTime = 0;
        float currentFill = foregroundImage.fillAmount;
        while (elapsedTime < updateSpeed)
        {
            elapsedTime += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(currentFill, percent, elapsedTime / updateSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(updateSpeed);
    }
    private void Update()
    {
        if (healthChanged == true)
        {
            healthChanged = false;
            StartCoroutine(ChangeToHPPercent(pct));
        }
    }
    void LateUpdate()
    {
        if (cam != null)
        {
            this.transform.LookAt(transform.position + cam.transform.forward);
        }
    }
}
