using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRecoil : MonoBehaviour
{
    [SerializeField]
    string resetButton;
    [SerializeField]
    ShootProjectile[] projectileSpawns = new ShootProjectile[3];

    private void Update()
    {
        if (Input.GetButtonDown(resetButton))
        {
            for (int i = 0; i < projectileSpawns.Length; i++)
            {
                projectileSpawns[i].recoilAngle = 0;
                projectileSpawns[i].yMin = -90;
                projectileSpawns[i].yMax = 90;
                projectileSpawns[i].previousRecoilTime = projectileSpawns[i].recoilAngle;
                projectileSpawns[i].recoilCurveTime = 0;
                projectileSpawns[i].recoilSpreadOn = false;
                projectileSpawns[i].ChangeAccuracy();
            }
        }
    }
}
