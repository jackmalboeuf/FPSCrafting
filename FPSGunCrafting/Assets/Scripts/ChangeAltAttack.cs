using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAltAttack : MonoBehaviour
{
    [SerializeField]
    ShootProjectile projectileSpawn;
    [SerializeField]
    Transform rifleSpawnHolder;
    [SerializeField]
    GameObject rifleArms;
    [SerializeField]
    Transform pistolSpawnHolder;
    [SerializeField]
    GameObject pistolArms;

    Dropdown altAttackDropdown;

    private void Start()
    {
        altAttackDropdown = GetComponent<Dropdown>();
    }

    public void SwitchAttack()
    {
        if (altAttackDropdown.value == 0)
        {
            projectileSpawn.transform.SetParent(rifleSpawnHolder);
            rifleArms.SetActive(true);
            pistolArms.SetActive(false);
        }
        else if (altAttackDropdown.value == 1)
        {
            projectileSpawn.transform.SetParent(pistolSpawnHolder);
            pistolArms.SetActive(true);
            rifleArms.SetActive(false);
        }
    }
}
