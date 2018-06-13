using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderDamage : MonoBehaviour
{
    [HideInInspector]
    public float meleeDamage;

    private void OnTriggerEnter(Collider target)
    {
        if (target.GetComponent<Damagable>())
        {
            target.GetComponent<Damagable>().TakeDamage(meleeDamage);
        }
    }
}
