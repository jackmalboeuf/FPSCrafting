using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    [SerializeField]
    bool shouldShow;
    [SerializeField]
    ActivateOnButtonPress menuManager;

    public void ShowPanel()
    {
        menuManager.canShowMenu = !shouldShow;
        panel.SetActive(shouldShow);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
