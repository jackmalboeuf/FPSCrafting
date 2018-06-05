using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    gunType typeOfGun = gunType.Auto;
    [SerializeField]
    Rigidbody projectile;
    [SerializeField]
    Slider reloadSlider;
    [SerializeField]
    Slider overheatSlider;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float accuracy;
    [SerializeField]
    float velocity;
    [SerializeField]
    float size = 1;
    [SerializeField]
    float magazineSize;
    [SerializeField]
    float reloadSpeed;
    [SerializeField]
    float heatAmount;
    [SerializeField]
    float cooldownSpeed;
    [SerializeField]
    Slider damageSlider;
    [SerializeField]
    Slider fireRateSlider;
    [SerializeField]
    Slider accuracySlider;
    [SerializeField]
    Slider rangeSlider;
    [SerializeField]
    Slider magazineSizeSlider;
    [SerializeField]
    Slider reloadSpeedSlider;
    [SerializeField]
    Slider projectileVelocitySlider;
    [SerializeField]
    Slider projectileWidthSlider;
    [SerializeField]
    Slider damageFallOffSlider;
    [SerializeField]
    bool usesOverheat;
    [SerializeField]
    bool usesReload;

    public float damage;
    public float range;
    public float damageFallOff;
    public bool bulletDropOn;
    public float bulletDrop;
    public bool AoEOn;
    public float AoESize;

    [HideInInspector]
    public Vector3 rangeEndPoint;
    [HideInInspector]
    public bool canFire;

    enum gunType { Auto = 0, SemiAuto = 1, Lazer = 2 }
    float nextFireTime;
    float currentMagazineCount;
    bool isReloading;
    float currentHeat;
    bool isCoolingDown;
    float maximumHeat = 1000;
    float cooldownPauseTime = 0.5f;

    void Start()
    {
        isReloading = false;
        currentMagazineCount = magazineSize;
        canFire = true;
        currentHeat = 0;
        isCoolingDown = false;
        overheatSlider.maxValue = maximumHeat;
        overheatSlider.value = currentHeat;
        overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;

        damageSlider.value = FindHalfwayPoint(damageSlider.minValue, damageSlider.maxValue);
        fireRateSlider.value = FindHalfwayPoint(fireRateSlider.minValue, fireRateSlider.maxValue);
        accuracySlider.value = FindHalfwayPoint(accuracySlider.minValue, accuracySlider.maxValue);
        rangeSlider.value = FindHalfwayPoint(rangeSlider.minValue, rangeSlider.maxValue);
        magazineSizeSlider.value = FindHalfwayPoint(magazineSizeSlider.minValue, magazineSizeSlider.maxValue);
        reloadSpeedSlider.value = FindHalfwayPoint(reloadSpeedSlider.minValue, reloadSpeedSlider.maxValue);
        projectileVelocitySlider.value = FindHalfwayPoint(projectileVelocitySlider.minValue, projectileVelocitySlider.maxValue);
        projectileWidthSlider.value = FindHalfwayPoint(projectileWidthSlider.minValue, projectileWidthSlider.maxValue);
        damageFallOffSlider.value = FindHalfwayPoint(damageFallOffSlider.minValue, damageFallOffSlider.maxValue);

        damage = damageSlider.value;
        fireRate = fireRateSlider.value;
        accuracy = accuracySlider.value;
        range = rangeSlider.value;
        magazineSize = magazineSizeSlider.value;
        reloadSpeed = reloadSpeedSlider.value;
        velocity = projectileVelocitySlider.value;
        size = projectileWidthSlider.value;
        damageFallOff = damageFallOffSlider.value;
    }

    void FixedUpdate()
    {
        FireProjectile();
    }

    void FireProjectile()
    {
        if (typeOfGun == gunType.Auto)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFireTime && !isReloading && canFire && !isCoolingDown)
            {
                if (currentMagazineCount > 0 && currentHeat < maximumHeat)
                {
                    nextFireTime = Time.time + fireRate;

                    Camera playerCamera = transform.parent.parent.GetComponent<Camera>();

                    Vector3 rayStart = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit rayHit;
                    Ray lookRay = new Ray(rayStart, playerCamera.transform.forward);
                    
                    rangeEndPoint = lookRay.GetPoint(range);

                    if (Physics.Raycast(lookRay, out rayHit, range) && rayHit.collider.tag != "Damager")
                    {
                        transform.parent.LookAt(rayHit.point);
                    }
                    else
                    {
                        transform.parent.LookAt(lookRay.GetPoint(range));
                    }

                    float xRand = Random.Range(0, accuracy);
                    float zRand = Random.Range(0, 359);

                    transform.localEulerAngles = new Vector3(xRand, transform.localEulerAngles.y, transform.localEulerAngles.z);
                    transform.Rotate(transform.parent.forward, zRand, Space.World);
                    
                    Rigidbody bullet = Instantiate(projectile, transform.position, transform.rotation, null) as Rigidbody;
                    bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * size, bullet.transform.localScale.y * size, bullet.transform.localScale.z * size);

                    bullet.GetComponent<ProjectileBehavior>().projectileDamage = damage;
                    bullet.GetComponent<ProjectileBehavior>().projectileRange = range;
                    bullet.GetComponent<ProjectileBehavior>().projectileDamageFallOff = damageFallOff;
                    bullet.GetComponent<ProjectileBehavior>().projectileEndPoint = rangeEndPoint;

                    if (bulletDropOn)
                    {
                        bullet.GetComponent<ProjectileBehavior>().projectileDrop = bulletDrop;
                        bullet.GetComponent<ProjectileBehavior>().projectileDropOn = bulletDropOn;
                    }

                    if (AoEOn)
                    {
                        bullet.GetComponent<ProjectileBehavior>().projectileAoESize = AoESize;
                        bullet.GetComponent<ProjectileBehavior>().projectileAoEOn = AoEOn;
                    }


                    bullet.AddForce(transform.forward * velocity);

                    if (usesOverheat)
                    {
                        currentHeat += heatAmount;
                        overheatSlider.value = currentHeat;

                        if (currentHeat >= maximumHeat)
                        {
                            currentHeat = maximumHeat;
                            StopAllCoroutines();
                            StartCoroutine(Cooldown());
                        }
                        else if (currentHeat < maximumHeat && !isCoolingDown)
                        {
                            StopAllCoroutines();
                            StartCoroutine(ReduceHeat());
                        }
                    }
                    
                    if (usesReload)
                    {
                        currentMagazineCount--;
                        if (currentMagazineCount < 0)
                            currentMagazineCount = 0;
                    }
                }
                else if (currentMagazineCount <= 0 && usesReload)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        if (Input.GetButtonDown("Reload") && !isReloading && currentMagazineCount < magazineSize && usesReload)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        reloadSlider.maxValue = reloadSpeed;
        reloadSlider.value = reloadSlider.maxValue;
        isReloading = true;
        float timeInterval = 0.01f;

        for (float i = 0; i < reloadSpeed; i += timeInterval)
        {
            yield return new WaitForSeconds(timeInterval);
            reloadSlider.value -= timeInterval;
        }

        currentMagazineCount = magazineSize;
        isReloading = false;
    }

    IEnumerator Cooldown()
    {
        isCoolingDown = true;
        overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.red;
        float timeInterval = 0.1f;
        float loopStart = currentHeat;
        float reduceHeatIncrement = maximumHeat / (cooldownSpeed / timeInterval);
        yield return new WaitForSeconds(cooldownPauseTime);
        float time1 = Time.time;

        for (float i = 0; i < loopStart; i += reduceHeatIncrement)
        {
            currentHeat -= reduceHeatIncrement;
            overheatSlider.value = currentHeat;
            yield return new WaitForSeconds(timeInterval);
        float time2 = time1 - Time.time;
        print(time2);
        }

        currentHeat = 0;
        overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;
        overheatSlider.maxValue = maximumHeat;
        overheatSlider.value = currentHeat;
        isCoolingDown = false;
    }

    IEnumerator ReduceHeat()
    {
        yield return new WaitForSeconds(cooldownPauseTime);
        float timeInterval = 0.1f;
        float loopStart = currentHeat;
        float reduceHeatIncrement = maximumHeat / (cooldownSpeed / timeInterval);
        float time1 = Time.time;
        for (float i = 0; i < loopStart; i += reduceHeatIncrement)
        {
            currentHeat -= reduceHeatIncrement;
            overheatSlider.value = currentHeat;
            yield return new WaitForSeconds(timeInterval);
        }
        float time2 = time1 - Time.time;
        print(time2);
    }

    public void ChangeDamage()
    {
        damage = damageSlider.value;
    }

    public void ChangeFireRate()
    {
        fireRate = fireRateSlider.value;
    }

    public void ChangeAccuracy()
    {
        accuracy = accuracySlider.value;
    }

    public void ChangeRange()
    {
        range = rangeSlider.value;
    }

    public void ChangeMagazineSize()
    {
        magazineSize = magazineSizeSlider.value;
    }

    public void ChangeReloadSpeed()
    {
        reloadSpeed = reloadSpeedSlider.value;
    }

    public void ChangeProjectileVelocity()
    {
        velocity = projectileVelocitySlider.value;
    }

    public void ChangeProjectileWidth()
    {
        size = projectileWidthSlider.value;
    }

    public void ChangeDamageFallOff()
    {
        damageFallOff = damageFallOffSlider.value;
    }

    float FindHalfwayPoint(float min, float max)
    {
        return (min + max) / 2;
    }
}
