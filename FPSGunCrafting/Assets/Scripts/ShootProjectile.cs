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
    //[SerializeField]
    float fireRate = 0.3f;
    //[SerializeField]
    float accuracy = 5;
    //[SerializeField]
    float bulletVelocity = 5000;
    //[SerializeField]
    float size = 1;
    //[SerializeField]
    float magazineSize = 1;
    //[SerializeField]
    float reloadSpeed = 10;
    //[SerializeField]
    float energy = 100;
    //[SerializeField]
    float cooldownSpeed = 10;
    [SerializeField]
    Slider damageSlider;
    [SerializeField]
    Slider fireRateSlider;
    [SerializeField]
    Slider accuracySlider;
    //[SerializeField]
    //Slider distanceSlider;
    [SerializeField]
    Slider magazineSizeSlider;
    [SerializeField]
    Slider reloadSpeedSlider;
    [SerializeField]
    Slider bulletVelocitySlider;
    //[SerializeField]
    //Slider projectileWidthSlider;
    [SerializeField]
    Slider rangeSlider;
    [SerializeField]
    Slider energySlider;
    [SerializeField]
    Slider cooldownSpeedSlider;
    [SerializeField]
    bool usesOverheat;
    [SerializeField]
    bool usesReload;

    [HideInInspector]
    public float damage = 1;
    [HideInInspector]
    public float distance = 40;
    [HideInInspector]
    public float range = 0;
    [HideInInspector]
    public float bulletDrop = 100;
    [HideInInspector]
    public float AoESize = 1;
    [HideInInspector]
    public Vector3 rangeEndPoint;
    [HideInInspector]
    public bool canFire;

    public bool bulletDropOn;
    public bool AoEOn;

    enum gunType { Auto = 0, SemiAuto = 1, Lazer = 2, Bow = 3, Launcher = 4 }
    float nextFireTime;
    float currentMagazineCount;
    bool isReloading;
    float currentHeat;
    bool isCoolingDown;
    bool isReducingHeat;
    float maximumHeat = 100;
    float cooldownPauseTime = 0.5f;

    void Start()
    {
        isReloading = false;
        currentMagazineCount = magazineSize;
        canFire = true;
        currentHeat = 0;
        isCoolingDown = false;
        isReducingHeat = false;
        overheatSlider.maxValue = maximumHeat;
        overheatSlider.value = currentHeat;
        overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;

        if (damageSlider != null)
        {
            damageSlider.value = FindHalfwayPoint(damageSlider.minValue, damageSlider.maxValue);
            damage = damageSlider.value;
        }

        if (fireRateSlider != null)
        {
            fireRateSlider.value = FindHalfwayPoint(fireRateSlider.minValue, fireRateSlider.maxValue);
            fireRate = fireRateSlider.value;
        }

        if (accuracySlider != null)
        {
            accuracySlider.value = FindHalfwayPoint(accuracySlider.minValue, accuracySlider.maxValue);
            accuracy = accuracySlider.value;
        }

        /*if (distanceSlider != null)
        {
            distanceSlider.value = FindHalfwayPoint(distanceSlider.minValue, distanceSlider.maxValue);
            distance = distanceSlider.value;
        }*/

        if (magazineSizeSlider != null)
        {
            magazineSizeSlider.value = FindHalfwayPoint(magazineSizeSlider.minValue, magazineSizeSlider.maxValue);
            magazineSize = magazineSizeSlider.value;
        }

        if (reloadSpeedSlider != null)
        {
            reloadSpeedSlider.value = FindHalfwayPoint(reloadSpeedSlider.minValue, reloadSpeedSlider.maxValue);
            reloadSpeed = reloadSpeedSlider.value;
        }

        if (bulletVelocitySlider != null)
        {
            bulletVelocitySlider.value = FindHalfwayPoint(bulletVelocitySlider.minValue, bulletVelocitySlider.maxValue);
            bulletVelocity = bulletVelocitySlider.value;
        }

        /*if (projectileWidthSlider != null)
        {
            projectileWidthSlider.value = FindHalfwayPoint(projectileWidthSlider.minValue, projectileWidthSlider.maxValue);
            size = projectileWidthSlider.value;
        }*/

        if (rangeSlider != null)
        {
            rangeSlider.value = FindHalfwayPoint(rangeSlider.minValue, rangeSlider.maxValue);
            range = rangeSlider.value;
        }

        if (energySlider != null)
        {
            energySlider.value = FindHalfwayPoint(energySlider.minValue, energySlider.maxValue);
            energy = energySlider.value;
        }

        if (cooldownSpeedSlider != null)
        {
            cooldownSpeedSlider.value = FindHalfwayPoint(cooldownSpeedSlider.minValue, cooldownSpeedSlider.maxValue);
            cooldownSpeed = cooldownSpeedSlider.value;
        }
    }

    void Update()
    {
        FireProjectile();

        if (isCoolingDown || isReducingHeat)
        {
            Overheat();
        }
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
                    
                    rangeEndPoint = lookRay.GetPoint(distance);

                    if (Physics.Raycast(lookRay, out rayHit, distance) && rayHit.collider.tag != "Damager")
                    {
                        transform.parent.LookAt(rayHit.point);
                    }
                    else
                    {
                        transform.parent.LookAt(lookRay.GetPoint(distance));
                    }

                    float xRand = Random.Range(0, accuracy);
                    float zRand = Random.Range(0, 359);

                    transform.localEulerAngles = new Vector3(xRand, transform.localEulerAngles.y, transform.localEulerAngles.z);
                    transform.Rotate(transform.parent.forward, zRand, Space.World);
                    
                    Rigidbody bullet = Instantiate(projectile, transform.position, transform.rotation, null) as Rigidbody;
                    bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * size, bullet.transform.localScale.y * size, bullet.transform.localScale.z * size);

                    bullet.GetComponent<ProjectileBehavior>().projectileDamage = damage;
                    bullet.GetComponent<ProjectileBehavior>().projectileRange = distance;
                    bullet.GetComponent<ProjectileBehavior>().projectileDamageFallOff = range;
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


                    bullet.AddForce(transform.forward * bulletVelocity);

                    if (usesOverheat)
                    {
                        currentHeat += energy;
                        overheatSlider.value = currentHeat;

                        if (currentHeat >= maximumHeat)
                        {
                            currentHeat = maximumHeat;
                            StopAllCoroutines();
                            StartCoroutine(Cooldown(true));
                        }
                        else if (currentHeat < maximumHeat)
                        {
                            isReducingHeat = false;
                            StopAllCoroutines();
                            StartCoroutine(Cooldown(false));
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

    void Overheat()
    {
        currentHeat -= maximumHeat / (cooldownSpeed / Time.deltaTime);
        overheatSlider.value = currentHeat;
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

    IEnumerator Cooldown(bool fullHeat)
    {
        if (fullHeat)
            overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.red;

        yield return new WaitForSeconds(cooldownPauseTime);

        if (fullHeat)
            isCoolingDown = true;

        if (!fullHeat)
            isReducingHeat = true;

        float amountToCoolDown = currentHeat / maximumHeat * cooldownSpeed;
        yield return new WaitForSeconds(amountToCoolDown);

        currentHeat = 0;
        overheatSlider.value = currentHeat;

        if (fullHeat)
        {
            overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;
            isCoolingDown = false;
        }

        if (!fullHeat)
            isReducingHeat = false;
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

    public void ChangeDistance()
    {
        //distance = distanceSlider.value;
    }

    public void ChangeMagazineSize()
    {
        magazineSize = magazineSizeSlider.value;
    }

    public void ChangeReloadSpeed()
    {
        reloadSpeed = reloadSpeedSlider.value;
    }

    public void ChangeBulletVelocity()
    {
        bulletVelocity = bulletVelocitySlider.value;
    }

    public void ChangeProjectileWidth()
    {
        //size = projectileWidthSlider.value;
    }

    public void ChangeRange()
    {
        range = rangeSlider.value;
    }

    public void ChangeEnergy()
    {
        energy = energySlider.value;
    }

    public void ChangeCooldownSpeed()
    {
        cooldownSpeed = cooldownSpeedSlider.value;
    }

    float FindHalfwayPoint(float min, float max)
    {
        return (min + max) / 2;
    }
}
