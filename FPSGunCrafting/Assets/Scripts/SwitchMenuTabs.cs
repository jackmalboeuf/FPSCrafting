using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenuTabs : MonoBehaviour
{
    [SerializeField]
    GameObject tuningPanel;

    private void Start()
    {
        if (GetComponent<Toggle>().isOn)
            GetComponent<Image>().color = Color.white;
    }

    public void PressTabButton()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<SwitchMenuTabs>().tuningPanel.SetActive(false);
        }

        tuningPanel.SetActive(true);
        GetComponent<Image>().color = Color.white;
    }
}
