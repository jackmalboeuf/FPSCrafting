using UnityEngine;
using System.Collections;

public class AoEDamage : MonoBehaviour
{
    public float AoEDamageValue;
    public float AoESize;

    void Start()
    {
        if (AoESize != null)
        {
            transform.localScale = new Vector3(transform.localScale.x * AoESize, transform.localScale.y * AoESize, transform.localScale.z * AoESize);
        }

        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (AoEDamageValue != null)
        {
            if (collider.GetComponent<Damagable>())
            {
                collider.GetComponent<Damagable>().TakeDamage(AoEDamageValue);
            }
        }
    }
}
