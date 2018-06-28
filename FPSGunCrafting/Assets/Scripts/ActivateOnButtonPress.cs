using UnityEngine;
using System.Collections;

public class ActivateOnButtonPress : MonoBehaviour
{
    [SerializeField]
    string tuningMenuButton;
    [SerializeField]
    CursorLock cLock;
    
    public GameObject tuningMenu;

    [HideInInspector]
    public bool canShowMenu = true;
    
    void Update()
    {
        if (Input.GetButtonDown(tuningMenuButton) && canShowMenu)
        {
            if (!tuningMenu.activeSelf)
            {
                tuningMenu.SetActive(true);
                cLock.ToggleCursorState();
            }
            else
            {
                tuningMenu.SetActive(false);
                cLock.ToggleCursorState();
            }
        }
    }
}
