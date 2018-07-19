using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchMenuTabs : MonoBehaviour
{
    [SerializeField]
    GameObject tuningPanel;
    [SerializeField]
    ShootProjectile projectileSpawn;
    [SerializeField]
    CursorLock cLock;
    [SerializeField]
    CameraMouseLook playerCamera;

    public GameObject scopeGunArms;

    public void PressTabButton()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            transform.parent.GetChild(i).GetComponent<SwitchMenuTabs>().tuningPanel.SetActive(false);
        }

        for (int i = 0; i < scopeGunArms.transform.parent.childCount; i++)
        {
            if (scopeGunArms.transform.parent.GetChild(i) != scopeGunArms.transform.parent.GetChild(0))
                scopeGunArms.transform.parent.GetChild(i).gameObject.SetActive(false);
        }

        tuningPanel.SetActive(true);
        scopeGunArms.SetActive(true);
        cLock.projectileSpawn = projectileSpawn;
        playerCamera.projectileSpawn = projectileSpawn;
        projectileSpawn.currentEnergy = 0;
    }
}
