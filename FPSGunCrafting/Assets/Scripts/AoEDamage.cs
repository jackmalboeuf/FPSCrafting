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
            Transform childParticle = transform.GetChild(0);

            transform.localScale = new Vector3(transform.localScale.x * AoESize, transform.localScale.y * AoESize, transform.localScale.z * AoESize);
            childParticle.localScale = new Vector3(childParticle.localScale.x * AoESize, childParticle.localScale.y * AoESize, childParticle.localScale.z * AoESize);
        }

        Invoke("DeactivateCollider", 0.2f);

        Destroy(gameObject, 0.5f);
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

    void DeactivateCollider()
    {
        GetComponent<Collider>().enabled = false;
    }
}
