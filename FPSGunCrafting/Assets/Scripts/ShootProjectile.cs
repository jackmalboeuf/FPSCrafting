using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public gunType typeOfGun = gunType.Auto;
    [SerializeField]
    Rigidbody projectile;
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Slider reloadSlider;
    [SerializeField]
    Slider overheatSlider;
    [SerializeField]
    Slider damageSlider;
    [SerializeField]
    Slider fireRateSlider;
    [SerializeField]
    Slider rangeSlider;
    [SerializeField]
    Slider accuracySlider;
    [SerializeField]
    Slider energySlider;
    [SerializeField]
    Slider cooldownSpeedSlider;
    [SerializeField]
    Slider bulletVelocitySlider;
    [SerializeField]
    Slider magazineSizeSlider;
    [SerializeField]
    Slider reloadSpeedSlider;
    [SerializeField]
    Slider zoomDistanceSlider;
    [SerializeField]
    Slider meleeDamageSlider;
    [SerializeField]
    Slider trait1aSlider;
    [SerializeField]
    Slider trait1bSlider;
    [SerializeField]
    Slider trait1cSlider;
    [SerializeField]
    Slider trait2aSlider;
    [SerializeField]
    Slider trait2bSlider;
    [SerializeField]
    Slider trait2cSlider;
    [SerializeField]
    Text damageValueText;
    [SerializeField]
    Text fireRateValueText;
    [SerializeField]
    Text rangeValueText;
    [SerializeField]
    Text accuracyValueText;
    [SerializeField]
    Text energyValueText;
    [SerializeField]
    Text cooldownSpeedValueText;
    [SerializeField]
    Text bulletVelocityValueText;
    [SerializeField]
    Text magazineSizeValueText;
    [SerializeField]
    Text reloadSpeedValueText;
    [SerializeField]
    Text zoomDistanceValueText;
    [SerializeField]
    Text meleeDamageValueText;
    [SerializeField]
    Text trait1aValueText;
    [SerializeField]
    Text trait1bValueText;
    [SerializeField]
    Text trait1cValueText;
    [SerializeField]
    Text trait2aValueText;
    [SerializeField]
    Text trait2bValueText;
    [SerializeField]
    Text trait2cValueText;
    [SerializeField]
    Dropdown alternateAttackDropdown;
    [SerializeField]
    Dropdown trait1Dropdown;
    [SerializeField]
    Dropdown trait2Dropdown;
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
    [HideInInspector]
    public float zoomDistance = 15;
    [HideInInspector]
    public float meleeDamage = 500;
    [HideInInspector]
    public enum gunType { Auto = 0, SemiAuto = 1, Lazer = 2, Bow = 3, Launcher = 4 }

    public bool bulletDropOn;
    public bool AoEOn;
    
    float fireRate = 0.3f;
    float accuracy = 5;
    float bulletVelocity = 5000;
    float size = 1;
    float magazineSize = 1;
    float reloadSpeed = 10;
    float energy = 100;
    float cooldownSpeed = 10;
    float timeToFullSpeed = 10;
    float numberOfBullets = 1;
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

        if (damageSlider != null && damageValueText != null)
        {
            damageSlider.value = FindHalfwayPoint(damageSlider.minValue, damageSlider.maxValue);
            ChangeDamage();
        }

        if (fireRateSlider != null && fireRateValueText != null)
        {
            fireRateSlider.value = FindHalfwayPoint(fireRateSlider.minValue, fireRateSlider.maxValue);
            ChangeFireRate();
        }

        if (rangeSlider != null && rangeValueText != null)
        {
            rangeSlider.value = FindHalfwayPoint(rangeSlider.minValue, rangeSlider.maxValue);
            ChangeRange();
        }

        if (accuracySlider != null && accuracyValueText != null)
        {
            accuracySlider.value = FindHalfwayPoint(accuracySlider.minValue, accuracySlider.maxValue);
            ChangeAccuracy();
        }

        if (energySlider != null && energyValueText != null)
        {
            energySlider.value = FindHalfwayPoint(energySlider.minValue, energySlider.maxValue);
            ChangeEnergy();
        }

        if (cooldownSpeedSlider != null && cooldownSpeedValueText != null)
        {
            cooldownSpeedSlider.value = FindHalfwayPoint(cooldownSpeedSlider.minValue, cooldownSpeedSlider.maxValue);
            ChangeCooldownSpeed();
        }

        if (bulletVelocitySlider != null && bulletVelocityValueText != null)
        {
            bulletVelocitySlider.value = FindHalfwayPoint(bulletVelocitySlider.minValue, bulletVelocitySlider.maxValue);
            ChangeBulletVelocity();
        }

        if (magazineSizeSlider != null && magazineSizeValueText != null)
        {
            magazineSizeSlider.value = FindHalfwayPoint(magazineSizeSlider.minValue, magazineSizeSlider.maxValue);
            ChangeMagazineSize();
        }

        if (reloadSpeedSlider != null && reloadSpeedValueText != null)
        {
            reloadSpeedSlider.value = FindHalfwayPoint(reloadSpeedSlider.minValue, reloadSpeedSlider.maxValue);
            ChangeReloadSpeed();
        }

        if (zoomDistanceSlider != null && zoomDistanceValueText != null)
        {
            zoomDistanceSlider.value = FindHalfwayPoint(zoomDistanceSlider.minValue, zoomDistanceSlider.maxValue);
            ChangeZoomDistance();
        }

        if (meleeDamageSlider != null && meleeDamageValueText != null)
        {
            meleeDamageSlider.value = FindHalfwayPoint(meleeDamageSlider.minValue, meleeDamageSlider.maxValue);
            ChangeMeleeDamage();
        }

        if (trait1aSlider != null && trait1aValueText != null)
        {
            trait1aSlider.value = FindHalfwayPoint(trait1aSlider.minValue, trait1aSlider.maxValue);
            ChangeTrait1a();
        }

        if (trait1bSlider != null && trait1bValueText != null)
        {
            trait1bSlider.value = FindHalfwayPoint(trait1bSlider.minValue, trait1bSlider.maxValue);
            ChangeTrait1b();
        }

        if (trait1cSlider != null && trait1cValueText != null)
        {
            trait1cSlider.value = FindHalfwayPoint(trait1cSlider.minValue, trait1cSlider.maxValue);
            ChangeTrait1c();
        }

        if (trait2aSlider != null && trait2aValueText != null)
        {
            trait2aSlider.value = FindHalfwayPoint(trait2aSlider.minValue, trait2aSlider.maxValue);
            ChangeTrait2a();
        }

        if (trait2bSlider != null && trait2bValueText != null)
        {
            trait2bSlider.value = FindHalfwayPoint(trait2bSlider.minValue, trait2bSlider.maxValue);
            ChangeTrait2b();
        }

        if (trait2cSlider != null && trait2cValueText != null)
        {
            trait2cSlider.value = FindHalfwayPoint(trait2cSlider.minValue, trait2cSlider.maxValue);
            ChangeTrait2c();
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
                        float energyAmount = maximumHeat / energy;
                        currentHeat += energyAmount;
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
        damageValueText.text = damage.ToString();
    }

    public void ChangeFireRate()
    {
        fireRate = fireRateSlider.value;
        fireRateValueText.text = (Mathf.Round(100 * fireRate) / 100).ToString();
    }

    public void ChangeRange()
    {
        range = rangeSlider.value;
        rangeValueText.text = range.ToString();
    }

    public void ChangeAccuracy()
    {
        accuracy = accuracySlider.value;
        accuracyValueText.text = (Mathf.Round(100 * accuracy) / 100).ToString();
    }

    public void ChangeEnergy()
    {
        energy = energySlider.value;
        energyValueText.text = energy.ToString();
    }

    public void ChangeCooldownSpeed()
    {
        cooldownSpeed = cooldownSpeedSlider.value;
        cooldownSpeedValueText.text = (Mathf.Round(100 * cooldownSpeed) / 100).ToString();
    }

    public void ChangeBulletVelocity()
    {
        bulletVelocity = bulletVelocitySlider.value;
        bulletVelocityValueText.text = bulletVelocity.ToString();
    }

    public void ChangeMagazineSize()
    {
        magazineSize = magazineSizeSlider.value;
        magazineSizeValueText.text = magazineSize.ToString();
    }

    public void ChangeReloadSpeed()
    {
        reloadSpeed = reloadSpeedSlider.value;
        reloadSpeedValueText.text = reloadSpeed.ToString();
    }

    public void ChangeZoomDistance()
    {
        zoomDistance = zoomDistanceSlider.value;
        zoomDistanceValueText.text = zoomDistance.ToString();
    }

    public void ChangeMeleeDamage()
    {
        meleeDamage = meleeDamageSlider.value;
        meleeDamageValueText.text = meleeDamage.ToString();
    }

    public void ChangeTrait1a()
    {
        if (typeOfGun == gunType.Auto)
        {
            AoESize = trait1aSlider.value;
            trait1aValueText.text = (Mathf.Round(100 * AoESize) / 100).ToString();
        }
    }

    public void ChangeTrait1b()
    {
        if (typeOfGun == gunType.Auto)
        {
            timeToFullSpeed = trait1bSlider.value;
            trait1bValueText.text = (Mathf.Round(100 * timeToFullSpeed) / 100).ToString();
        }
    }

    public void ChangeTrait1c()
    {
        if (typeOfGun == gunType.Auto)
        {
            numberOfBullets = trait1cSlider.value;
            trait1cValueText.text = numberOfBullets.ToString();
        }
    }

    public void ChangeTrait2a()
    {
        if (typeOfGun == gunType.Auto)
        {
            AoESize = trait2aSlider.value;
            trait2aValueText.text = (Mathf.Round(100 * AoESize) / 100).ToString();
        }
    }

    public void ChangeTrait2b()
    {
        if (typeOfGun == gunType.Auto)
        {
            timeToFullSpeed = trait2bSlider.value;
            trait2bValueText.text = (Mathf.Round(100 * timeToFullSpeed) / 100).ToString();
        }
    }

    public void ChangeTrait2c()
    {
        if (typeOfGun == gunType.Auto)
        {
            numberOfBullets = trait2cSlider.value;
            trait2cValueText.text = numberOfBullets.ToString();
        }
    }

    public void ChangeTrait1Dropdown()
    {
        if (typeOfGun == gunType.Auto)
        {
            if (trait1Dropdown.value == 1)
            {
                AoEOn = true;
            }
            else if (trait1Dropdown.value == 2)
            {

            }
        }
    }

    float FindHalfwayPoint(float min, float max)
    {
        return (min + max) / 2;
    }
}
