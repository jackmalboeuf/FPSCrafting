using UnityEngine;
using System.Collections;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject AoEObject;
    [SerializeField]
    GameObject dustParticle;

    public float projectileRange;
    public float projectileDamage;
    public float projectileDamageFallOff;
    public bool projectileDropOn;
    public float projectileDrop;
    public bool projectileAoEOn;
    public float projectileAoESize;
    public Vector3 projectileEndPoint;

    Vector3 startPosition;
    Vector3 fallOffStartPosition;
    float fallOffStartDistance;
    float projectileDamageReset;
    bool canChangeFallOffStartPosition = true;
    float projectileDropDistance;

    void Start()
    {
        startPosition = transform.position;
        fallOffStartDistance = projectileDamageFallOff * 0.01f * projectileRange;
        projectileDropDistance = projectileDrop * 0.01f * projectileRange;
        projectileDamageReset = projectileDamage;
    }

    void Update()
    {
        HandleProjectileRange();
        HandleProjectileDrop();
    }

    void OnTriggerEnter(Collider collider)
    {
        HandleProjectileDamage(collider);
    }

    void HandleProjectileRange()
    {
        if (projectileRange != null && projectileEndPoint != null)
        {
            float projectileActualRange = Vector3.Distance(projectileEndPoint, startPosition);

            if (Vector3.Distance(startPosition, transform.position) >= projectileActualRange)
            {
                if (projectileAoEOn && projectileAoESize != null)
                {
                    HandleProjectileAoE();
                }

                Destroy(gameObject);
            }

            if (Vector3.Distance(startPosition, transform.position) >= fallOffStartDistance)
            {
                if (canChangeFallOffStartPosition)
                {
                    fallOffStartPosition = transform.position;
                    canChangeFallOffStartPosition = false;
                }

                HandleProjectileDamageFallOff();
            }
        }
    }

    void HandleProjectileDamage(Collider target)
    {
        if (projectileDamage != null && !projectileAoEOn)
        {
            if (target.GetComponent<Damagable>())
            {
                target.GetComponent<Damagable>().TakeDamage(projectileDamage);
                Destroy(gameObject);
            }
            else if (!target.GetComponent<Damagable>() && target.gameObject.tag != "Player" && target.gameObject.tag != "Damager")
            {
                GameObject dustObject = Instantiate(dustParticle, GetComponent<PreventCollisionPassThrough>().previousPosition, transform.rotation, null) as GameObject;
                Destroy(gameObject);
            }
        }
        else if (projectileAoEOn && projectileAoESize != null && projectileDamage != null)
        {
            if (target.gameObject.tag != "Player" && target.gameObject.tag != "Damager")
            {
                HandleProjectileAoE();
                Destroy(gameObject);
            }
        }
    }

    void HandleProjectileDamageFallOff()
    {
        if (projectileDamageFallOff != null)
        {
            float damageFallOffPeriod = projectileRange - fallOffStartDistance;
            float fallOffDistanceTraveled = Vector3.Distance(fallOffStartPosition, transform.position);
            float damageFallOffPercent = fallOffDistanceTraveled / damageFallOffPeriod;
            projectileDamage = projectileDamageReset;
            projectileDamage -= Mathf.Round(damageFallOffPercent * 0.75f * projectileDamage);
        }
    }

    void HandleProjectileDrop()
    {
        if (projectileDropOn && projectileDrop != null)
        {
            if (Vector3.Distance(startPosition, transform.position) >= projectileDropDistance)
            {
                GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    void HandleProjectileAoE()
    {
        if (projectileAoEOn && projectileAoESize != null)
        {
            GameObject AoE = Instantiate(AoEObject, GetComponent<PreventCollisionPassThrough>().previousPosition, transform.rotation, null) as GameObject;
            AoE.GetComponent<AoEDamage>().AoEDamageValue = projectileDamage;
            AoE.GetComponent<AoEDamage>().AoESize = projectileAoESize;
        }
    }
}
