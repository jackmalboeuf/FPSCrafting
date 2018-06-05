using UnityEngine;
using System.Collections;

public class ActivateOnButtonPress : MonoBehaviour
{
    [SerializeField]
    string tuningMenuButton;
    [SerializeField]
    GameObject tuningMenu;
    
    void Update()
    {
        if (Input.GetButtonDown(tuningMenuButton))
        {
            if (!tuningMenu.activeSelf)
            {
                tuningMenu.SetActive(true);
            }
            else
            {
                tuningMenu.SetActive(false);
            }
        }
    }
}
