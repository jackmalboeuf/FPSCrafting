using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject toolTipObject;
    [SerializeField]
    toolTipString TTString = 0;

    enum toolTipString { DamageDesc = 0, FirerateDesc = 1, RangeDesc = 2, AccuracyDesc = 3, EnergyDesc = 4, CooldownSpeedDesc = 5, BulletVelocity = 6 }
    Text toolTipText;

    private void Start()
    {
        toolTipText = toolTipObject.transform.GetChild(0).GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipObject.SetActive(true);
        toolTipObject.transform.position = transform.position;
        SetToolTipText();
        StopAllCoroutines();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(HideObject());
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(0.3f);

        if (toolTipObject.transform.position == transform.position)
            toolTipObject.SetActive(false);
    }

    public void SetToolTipText()
    {
        if (TTString == toolTipString.DamageDesc)
        {
            toolTipText.text = "Amount of damage dealt by each shot. ";
        }
        else if (TTString == toolTipString.FirerateDesc)
        {
            toolTipText.text = "Number of shots fired over 5 seconds. ";
        }
        else if (TTString == toolTipString.RangeDesc)
        {
            toolTipText.text = "Distance at which damage begins to fall off. ";
        }
        else if (TTString == toolTipString.AccuracyDesc)
        {
            toolTipText.text = "Radius of bullet spread. ";
        }
        else if (TTString == toolTipString.EnergyDesc)
        {
            toolTipText.text = "Number of shots that can be fired before overheating. ";
        }
        else if (TTString == toolTipString.CooldownSpeedDesc)
        {
            toolTipText.text = "Amount of time it takes to fully cooldown. ";
        }
        else if (TTString == toolTipString.BulletVelocity)
        {
            toolTipText.text = "Speed at which bullets travel. ";
        }
    }
}
