using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusBar : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Image foregroundImage;
    [SerializeField] private AnimationCurve updateCurve;
    [SerializeField] private BaseCharacter character;
    [SerializeField] private float updateSpeed;
    internal bool focusChanged = false;
    internal float pct;
    void Awake()
    {
        character.OnFocusChanged += HandleFocusChange;
    }

    private void HandleFocusChange(float currentFocus, float maxFocus)
    {
        focusChanged = true;
        pct = currentFocus / maxFocus;
    }
    private IEnumerator ChangeToFocusPercent(float percent)
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
        if (focusChanged == true)
        {
            focusChanged = false;
            StartCoroutine(ChangeToFocusPercent(pct));
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
