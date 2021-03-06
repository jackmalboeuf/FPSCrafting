﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public gunType typeOfGun = gunType.Auto;
    [SerializeField]
    Rigidbody projectile;
    [SerializeField]
    LineRenderer lazerLineRenderer;
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    string fireButton;
    [SerializeField]
    string reloadButton;
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
    Slider stabilitySlider;
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
    Text stabilityValueText;
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
    Text trait1cValueText1;
    [SerializeField]
    Text trait1cValueText2;
    [SerializeField]
    Text trait2aValueText;
    [SerializeField]
    Text trait2bValueText;
    [SerializeField]
    Text trait2cValueText1;
    [SerializeField]
    Text trait2cValueText2;
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
    [SerializeField]
    AnimationCurve recoilCurve = AnimationCurve.EaseInOut(0, 0, 0.5f, 0.5f);
    [SerializeField]
    GameObject aoeObject;

    [HideInInspector]
    public enum gunType { Auto = 0, SemiAuto = 1, Lazer = 2 }
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
    public bool AoEOn = false;
    [HideInInspector]
    public bool bulletDropOn = false;
    [HideInInspector]
    public float currentEnergy;
    [HideInInspector]
    public float recoilAngle;
    [HideInInspector]
    public float yMin = -90;
    [HideInInspector]
    public float yMax = 90;
    [HideInInspector]
    public bool isCoolingDown;
    [HideInInspector]
    public float recoilCurveTime = 0;
    [HideInInspector]
    public float previousRecoilTime = 0;
    [HideInInspector]
    public bool recoilSpreadOn = false;

    float fireRate = 0.3f;
    float accuracy = 5;
    float stability = 10;
    float bulletVelocity = 5000;
    float size = 1;
    float magazineSize = 1;
    float reloadSpeed = 10;
    float energy = 100;
    float cooldownSpeed = 10;
    float energyBoost = 1.25f;
    float numberOfAdditionalBullets = 1;
    float nextFireTime;
    float currentMagazineCount;
    bool isReloading;
    bool isReducingEnergy;
    float maximumEnergy = 100;
    float cooldownPauseTime = 0.5f;
    bool energyBoostOn = false;
    bool bulletSpreadOn = false;
    bool repeaterOn = false;
    bool shotgunOn = false;
    float previousDropdown1;
    float previousDropdown2;
    Vector3 kickbackSmoothDampVelocity;
    float recoilSmoothDampVelocity;
    float gunKickback = 0.05f;
    Transform kickbackTransform;
    float previousRecoilAngle;
    bool canRepeat = true;
    List<Coroutine> cooldownCoroutines = new List<Coroutine>();
    //float recoilRef;

    void Awake()
    {
        isReloading = false;
        currentMagazineCount = magazineSize;
        canFire = true;
        currentEnergy = 0;
        isCoolingDown = false;
        isReducingEnergy = false;
        overheatSlider.maxValue = maximumEnergy;
        overheatSlider.value = currentEnergy;
        overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;
        previousDropdown1 = 0;
        previousDropdown2 = 0;
        kickbackTransform = GetComponentInParent<PlayerAnimation>().transform;

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

        if (stabilitySlider != null && stabilityValueText != null)
        {
            stabilitySlider.value = FindHalfwayPoint(stabilitySlider.minValue, stabilitySlider.maxValue);
            ChangeStability();
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

        if (trait1cSlider != null && trait1cValueText2 != null)
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

        if (trait2cSlider != null && trait2cValueText1 != null)
        {
            trait2cSlider.value = FindHalfwayPoint(trait2cSlider.minValue, trait2cSlider.maxValue);
            ChangeTrait2c();
        }
    }

    void Update()
    {
        if (typeOfGun == gunType.Auto || typeOfGun == gunType.Lazer)
        {
            if (Input.GetButton(fireButton))
            {
                FireProjectile();
            }
        }
        else if (typeOfGun == gunType.SemiAuto)
        {
            if (Input.GetButtonDown(fireButton))
            {
                FireProjectile();
            }
        }

        if (isCoolingDown || isReducingEnergy)
        {
            Overheat();
        }

        kickbackTransform.localPosition = Vector3.SmoothDamp(kickbackTransform.localPosition, new Vector3(0, -0.53f, 0), ref kickbackSmoothDampVelocity, 0.1f);

        if (typeOfGun == gunType.SemiAuto || typeOfGun == gunType.Lazer)
        {
            recoilCurveTime += Time.deltaTime;
            previousRecoilAngle = recoilAngle;

            recoilAngle = previousRecoilTime + recoilCurve.Evaluate(recoilCurveTime);
            recoilAngle = Mathf.Clamp(recoilAngle, 0, 200);
            
            if (recoilAngle >= 200 && !recoilSpreadOn)
            {
                accuracy += 5;
                recoilSpreadOn = true;
            }
            //recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRef, 0.1f);
            //transform.localEulerAngles = transform.localEulerAngles + Vector3.right * recoilAngle;
        }

        if (recoilAngle != previousRecoilAngle)
        {
            yMin = -90;
            yMax = 90;
            yMin -= recoilAngle;
            yMax -= recoilAngle;

            if (recoilAngle >= 359)
            {
                recoilAngle = 0;
            }

            if (yMin <= -359 || yMax <= -359)
            {
                yMin = -90;
                yMax = 90;
                recoilAngle = 0;
            }
        }
    }

    void FireProjectile()
    {
        if (Time.time > nextFireTime && !isReloading && canFire && !isCoolingDown)
        {
            if (currentMagazineCount > 0 && currentEnergy < maximumEnergy)
            {
                ChangeFireRate();

                nextFireTime = 5 / fireRate + Time.time;

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

                if (typeOfGun == gunType.Auto || typeOfGun == gunType.Lazer)
                {
                    float xRand = Random.Range(0, accuracy);
                    float zRand = Random.Range(0, 359);

                    transform.localEulerAngles = new Vector3(xRand, transform.localEulerAngles.y, transform.localEulerAngles.z);
                    transform.Rotate(transform.parent.forward, zRand, Space.World);
                }

                Rigidbody bullet = new Rigidbody();

                if (typeOfGun == gunType.Auto || typeOfGun == gunType.SemiAuto)
                {
                    bullet = Instantiate(projectile, transform.position, transform.rotation, null) as Rigidbody;
                    bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * size, bullet.transform.localScale.y * size, bullet.transform.localScale.z * size);
                }

                if (energyBoostOn)
                {
                    ChangeDamage();
                    damage += Mathf.Round(currentEnergy * (energyBoost - 1) * 0.01f * damage);
                }

                if (typeOfGun == gunType.Lazer)
                {
                    CastRay(transform.position, transform.forward);
                    
                    if (shotgunOn)
                    {
                        for (int i = 0; i < numberOfAdditionalBullets; i++)
                        {
                            float xRand = Random.Range(0, accuracy);
                            float zRand = Random.Range(0, 359);

                            transform.localEulerAngles = new Vector3(xRand, transform.localEulerAngles.y, transform.localEulerAngles.z);
                            transform.Rotate(transform.parent.forward, zRand, Space.World);

                            CastRay(transform.position, transform.forward);
                        }
                    }
                }

                if (bulletSpreadOn || repeaterOn)
                {
                    List<Rigidbody> bullets = new List<Rigidbody>();
                    Rigidbody bullet1 = projectile;
                    Rigidbody bullet2 = projectile;
                    Rigidbody bullet3 = projectile;
                    Rigidbody bullet4 = projectile;
                    Rigidbody bullet5 = projectile;
                    Rigidbody bullet6 = projectile;
                    Rigidbody bullet7 = projectile;
                    Rigidbody bullet8 = projectile;

                    bullets.Add(bullet1);
                    bullets.Add(bullet2);
                    bullets.Add(bullet3);
                    bullets.Add(bullet4);
                    bullets.Add(bullet5);
                    bullets.Add(bullet6);
                    bullets.Add(bullet7);
                    bullets.Add(bullet8);

                    if (bulletSpreadOn)
                    {   
                        for (int i = 1; i <= numberOfAdditionalBullets; i++)
                        {
                            bullets[i - 1] = Instantiate(bullet, transform.position, transform.rotation, null) as Rigidbody;
                        }
                    }

                    if (repeaterOn)
                    {
                        if (canRepeat)
                        {
                            float pauseTime = 0.03f;
                            float waitTime = numberOfAdditionalBullets * pauseTime;

                            for (int i = 1; i <= numberOfAdditionalBullets; i++)
                            {
                                bullets[i - 1] = Instantiate(bullet, transform.position, transform.rotation, null) as Rigidbody;
                                StartCoroutine(RepeaterBullets(waitTime - i * pauseTime, bullets[i - 1]));
                            }
                        }
                    }

                    ChangeDamage();
                    ChangeFireRate();
                    ChangeRange();
                    ChangeEnergy();
                    ChangeCooldownSpeed();
                    ChangeBulletVelocity();

                    float plusOrMinus = -1;

                    for (int i = 1; i <= numberOfAdditionalBullets; i++)
                    {
                        bullets[i - 1].GetComponent<ProjectileBehavior>().projectileDamage = damage;
                        bullets[i - 1].GetComponent<ProjectileBehavior>().projectileRange = distance;
                        bullets[i - 1].GetComponent<ProjectileBehavior>().projectileDamageFallOff = range;
                        bullets[i - 1].GetComponent<ProjectileBehavior>().projectileEndPoint = rangeEndPoint;


                        if (bulletSpreadOn)
                        {
                            plusOrMinus *= -1;
                            bullets[i - 1].AddRelativeForce(0, plusOrMinus * 30 * i, 0);
                            bullets[i - 1].AddForce(transform.forward * bulletVelocity);
                        }
                    }
                }

                if (typeOfGun == gunType.Auto || typeOfGun == gunType.SemiAuto)
                {
                    bullet.GetComponent<ProjectileBehavior>().projectileDamage = damage;
                    bullet.GetComponent<ProjectileBehavior>().projectileRange = distance;
                    bullet.GetComponent<ProjectileBehavior>().projectileDamageFallOff = range;
                    bullet.GetComponent<ProjectileBehavior>().projectileEndPoint = rangeEndPoint;

                    if (AoEOn)
                    {
                        bullet.GetComponent<ProjectileBehavior>().projectileAoESize = AoESize;
                        bullet.GetComponent<ProjectileBehavior>().projectileAoEOn = AoEOn;
                    }
                }

                if (bulletDropOn)
                {
                    bullet.GetComponent<ProjectileBehavior>().projectileDrop = bulletDrop;
                    bullet.GetComponent<ProjectileBehavior>().projectileDropOn = bulletDropOn;
                }

                if (typeOfGun == gunType.Auto || typeOfGun == gunType.SemiAuto)
                {
                    bullet.AddForce(transform.forward * bulletVelocity);
                }

                if (typeOfGun == gunType.SemiAuto|| typeOfGun == gunType.Lazer)
                {
                    Keyframe recoilHeightKey = new Keyframe(recoilCurve.keys[1].time, stability);
                    recoilCurve.MoveKey(1, recoilHeightKey);
                    Keyframe recoilLengthKey = new Keyframe(stability / 10, recoilCurve.keys[2].value);
                    recoilCurve.MoveKey(2, recoilLengthKey);
                    previousRecoilTime = recoilAngle;
                    recoilCurveTime = 0;
                }

                kickbackTransform = GetComponentInParent<PlayerAnimation>().transform;
                kickbackTransform.localPosition -= Vector3.forward * gunKickback;

                if (usesOverheat)
                {
                    float energyAmount = maximumEnergy / energy;
                    currentEnergy += energyAmount;
                    overheatSlider.value = currentEnergy;

                    if (currentEnergy >= maximumEnergy)
                    {
                        currentEnergy = maximumEnergy;

                        for (int i = 0; i < cooldownCoroutines.Count; i++)
                        {
                            StopCoroutine(cooldownCoroutines[i]);
                        }
                        cooldownCoroutines.Add(StartCoroutine(Cooldown(true)));
                    }
                    else if (currentEnergy < maximumEnergy)
                    {
                        isReducingEnergy = false;

                        for (int i = 0; i < cooldownCoroutines.Count; i++)
                        {
                            StopCoroutine(cooldownCoroutines[i]);
                        }
                        cooldownCoroutines.Add(StartCoroutine(Cooldown(false)));
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

        if (Input.GetButtonDown(reloadButton) && !isReloading && currentMagazineCount < magazineSize && usesReload)
        {
            StartCoroutine(Reload());
        }
    }

    void CastRay(Vector3 start, Vector3 direction)
    {
        Ray lazerRay = new Ray(start, direction);
        RaycastHit hit;
        if (Physics.Raycast(lazerRay, out hit, Vector3.Distance(transform.position, rangeEndPoint)) && hit.collider.tag != "Damager")
        {       
            LineRenderer lazerBeam = Instantiate(lazerLineRenderer);
            lazerBeam.SetPosition(0, transform.position);
            lazerBeam.SetPosition(1, hit.point);   

            if (hit.collider.GetComponent<Damagable>())
            {
                float fallOffStartDistance = range * 0.01f * distance;
                float damageFallOffPeriod = distance - fallOffStartDistance;
                float fallOffDistanceTraveled = 0;
                if (Vector3.Distance(lazerRay.origin, hit.point) >= fallOffStartDistance)
                {
                    fallOffDistanceTraveled = Vector3.Distance(lazerRay.GetPoint(fallOffStartDistance), hit.point);
                }
                float damageFallOffPercent = fallOffDistanceTraveled / damageFallOffPeriod;
                float lazerDamage = Mathf.Round(damage - damageFallOffPercent * 0.75f * damage);

                if (!AoEOn)
                {
                    hit.collider.GetComponent<Damagable>().TakeDamage(lazerDamage);
                }
                else if (AoEOn && aoeObject != null)
                {
                    GameObject aoe = Instantiate(aoeObject, hit.point, transform.rotation, null) as GameObject;
                    aoe.GetComponent<AoEDamage>().AoEDamageValue = lazerDamage;
                    aoe.GetComponent<AoEDamage>().AoESize = AoESize;
                }
            }
        }
        else
        {
            LineRenderer lazerBeam = Instantiate(lazerLineRenderer);
            lazerBeam.SetPosition(0, transform.position);
            lazerBeam.SetPosition(1, lazerRay.GetPoint(distance));

            if (AoEOn)
            {
                GameObject aoe = Instantiate(aoeObject, lazerRay.GetPoint(distance), transform.rotation, null) as GameObject;
                aoe.GetComponent<AoEDamage>().AoEDamageValue = damage;
                aoe.GetComponent<AoEDamage>().AoESize = AoESize;
            }
        }
    }

    void Overheat()
    {
        currentEnergy -= maximumEnergy / (cooldownSpeed / Time.deltaTime);
        overheatSlider.value = currentEnergy;
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
            isReducingEnergy = true;

        float amountToCoolDown = currentEnergy / maximumEnergy * cooldownSpeed;
        yield return new WaitForSeconds(amountToCoolDown);

        currentEnergy = 0;
        overheatSlider.value = currentEnergy;

        if (fullHeat)
        {
            overheatSlider.transform.GetChild(1).GetComponentInChildren<Image>().color = Color.white;
            isCoolingDown = false;
        }

        if (!fullHeat)
            isReducingEnergy = false;
    }

    IEnumerator RepeaterBullets(float waitTime, Rigidbody bullet)
    {
        yield return new WaitForSeconds(waitTime);
        ChangeBulletVelocity();
        bullet.AddForce(transform.forward * bulletVelocity);
    }

    public void ChangeDamage()
    {
        if (typeOfGun == gunType.Auto)
            damage = Mathf.Round(Mathf.Lerp(5, 50, damageSlider.normalizedValue));
        if (typeOfGun == gunType.SemiAuto)
            damage = Mathf.Round(Mathf.Lerp(10, 100, damageSlider.normalizedValue));
        if (typeOfGun == gunType.Lazer)
            damage = Mathf.Round(Mathf.Lerp(7, 70, damageSlider.normalizedValue));

        if (repeaterOn)
        {
            damage = Mathf.Round(damage / numberOfAdditionalBullets);
        }

        damageValueText.text = damage.ToString();
    }

    public void ChangeFireRate()
    {
        if (typeOfGun == gunType.Auto)
            fireRate = Mathf.Round(Mathf.Lerp(10, 100, fireRateSlider.normalizedValue));
        if (typeOfGun == gunType.SemiAuto)
            fireRate = Mathf.Round(Mathf.Lerp(5, 50, fireRateSlider.normalizedValue));
        if (typeOfGun == gunType.Lazer)
            fireRate = Mathf.Round(Mathf.Lerp(7, 70, fireRateSlider.normalizedValue));

        if (bulletSpreadOn)
        {
            fireRate = Mathf.Round(fireRate / numberOfAdditionalBullets);
        }

        fireRateValueText.text = fireRate.ToString();
    }

    public void ChangeRange()
    {
        range = Mathf.Round(Mathf.Lerp(1, 100, rangeSlider.normalizedValue));

        if (shotgunOn)
        {
            range = Mathf.Round(fireRate / numberOfAdditionalBullets);
        }

        rangeValueText.text = range.ToString();
    }

    public void ChangeAccuracy()
    {
        if (typeOfGun == gunType.Auto || typeOfGun == gunType.Lazer)
        {
            accuracy = Mathf.Lerp(5, 1, accuracySlider.normalizedValue);
            accuracyValueText.text = (Mathf.Round(100 * accuracy) / 100).ToString();
        }
    }

    public void ChangeStability()
    {
        if (typeOfGun == gunType.SemiAuto)
            stability = Mathf.Lerp(7, 2, stabilitySlider.normalizedValue);
        if (typeOfGun == gunType.Lazer)
            stability = Mathf.Lerp(3, 1, stabilitySlider.normalizedValue);

        stabilityValueText.text = (Mathf.Round(100 * stability) / 100).ToString();
    }

    public void ChangeEnergy()
    {
        energy = Mathf.Round(Mathf.Lerp(1, 100, energySlider.normalizedValue));
        energyValueText.text = energy.ToString();
    }

    public void ChangeCooldownSpeed()
    {
        cooldownSpeed = Mathf.Lerp(6, 0.5f, cooldownSpeedSlider.normalizedValue);
        cooldownSpeedValueText.text = (Mathf.Round(100 * cooldownSpeed) / 100).ToString();
    }

    public void ChangeBulletVelocity()
    {
        bulletVelocity = Mathf.Round(Mathf.Lerp(2000, 6000, bulletVelocitySlider.normalizedValue));
        bulletVelocityValueText.text = bulletVelocity.ToString();
    }

    public void ChangeDistance()
    {
        if (shotgunOn)
        {
            distance /= numberOfAdditionalBullets;
        }
    }

    public void ChangeMagazineSize()
    {
        magazineSize = Mathf.Lerp(1, 50, magazineSizeSlider.normalizedValue);
        magazineSizeValueText.text = magazineSize.ToString();
    }

    public void ChangeReloadSpeed()
    {
        reloadSpeed = Mathf.Lerp(10, 0.5f, reloadSpeedSlider.normalizedValue);
        reloadSpeedValueText.text = reloadSpeed.ToString();
    }

    public void ChangeZoomDistance()
    {
        zoomDistance = Mathf.Round(Mathf.Lerp(55, 10, zoomDistanceSlider.normalizedValue));
        zoomDistanceValueText.text = zoomDistance.ToString();
    }

    public void ChangeMeleeDamage()
    {
        meleeDamage = Mathf.Round(Mathf.Lerp(50, 300, meleeDamageSlider.normalizedValue));
        meleeDamageValueText.text = meleeDamage.ToString();
    }

    public void ChangeTrait1a()
    {
        AoESize = Mathf.Lerp(1, 5, trait1aSlider.normalizedValue);
        trait1aValueText.text = (Mathf.Round(100 * AoESize) / 100).ToString();
    }

    public void ChangeTrait1b()
    {
        energyBoost = Mathf.Lerp(1.25f, 1.75f, trait1bSlider.normalizedValue);
        trait1bValueText.text = (Mathf.Round(100 * energyBoost) / 100).ToString();
    }

    public void ChangeTrait1c()
    {
        if (typeOfGun == gunType.Auto)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 8, trait1cSlider.normalizedValue));
            trait1cValueText1.text = numberOfAdditionalBullets.ToString();
            trait1cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeFireRate();
        }
        else if (typeOfGun == gunType.SemiAuto)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 4, trait1cSlider.normalizedValue));
            trait1cValueText1.text = numberOfAdditionalBullets.ToString();
            trait1cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeDamage();
        }
        else if (typeOfGun == gunType.Lazer)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 8, trait1cSlider.normalizedValue));
            trait1cValueText1.text = numberOfAdditionalBullets.ToString();
            trait1cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeRange();
        }
    }

    public void ChangeTrait2a()
    {
        AoESize = Mathf.Lerp(1, 5, trait2aSlider.normalizedValue);
        trait2aValueText.text = (Mathf.Round(100 * AoESize) / 100).ToString();
    }

    public void ChangeTrait2b()
    {
        energyBoost = Mathf.Lerp(1.25f, 1.75f, trait2bSlider.normalizedValue);
        trait2bValueText.text = (Mathf.Round(100 * energyBoost) / 100).ToString();
    }

    public void ChangeTrait2c()
    {
        if (typeOfGun == gunType.Auto)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 8, trait2cSlider.normalizedValue));
            trait2cValueText1.text = numberOfAdditionalBullets.ToString();
            trait2cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeFireRate();
        }
        else if (typeOfGun == gunType.SemiAuto)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 4, trait1cSlider.normalizedValue));
            trait1cValueText1.text = numberOfAdditionalBullets.ToString();
            trait1cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeDamage();
        }
        else if (typeOfGun == gunType.Lazer)
        {
            numberOfAdditionalBullets = Mathf.Round(Mathf.Lerp(1, 8, trait1cSlider.normalizedValue));
            trait1cValueText1.text = numberOfAdditionalBullets.ToString();
            trait1cValueText2.text = "1/" + numberOfAdditionalBullets;
            ChangeRange();
        }
    }

    public void ChangeTrait1Dropdown()
    {
        if (trait1Dropdown.value == 0)
        {
            AoEOn = false;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait1Dropdown.value == 1)
        {
            AoEOn = true;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait1Dropdown.value == 2)
        {
            AoEOn = false;
            energyBoostOn = true;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait1Dropdown.value == 3)
        {
            AoEOn = false;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = true;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = true;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = true;
        }

        if (previousDropdown2 == 1)
        {
            AoEOn = true;
        }
        else if (previousDropdown2 == 2)
        {
            energyBoostOn = true;
        }
        else if (previousDropdown2 == 3)
        {
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = true;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = true;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = true;
        }

        if (trait1Dropdown.value == 0)
        {
            previousDropdown1 = 0;
        }
        else if (trait1Dropdown.value == 1)
        {
            previousDropdown1 = 1;
        }
        else if (trait1Dropdown.value == 2)
        {
            previousDropdown1 = 2;
        }
        else if (trait1Dropdown.value == 3)
        {
            previousDropdown1 = 3;
        }
    }

    public void ChangeTrait2Dropdown()
    {
        if (trait2Dropdown.value == 0)
        {
            AoEOn = false;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait2Dropdown.value == 1)
        {
            AoEOn = true;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait2Dropdown.value == 2)
        {
            AoEOn = false;
            energyBoostOn = true;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = false;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = false;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = false;
        }
        else if (trait2Dropdown.value == 3)
        {
            AoEOn = false;
            energyBoostOn = false;
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = true;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = true;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = true;
        }

        if (previousDropdown1 == 1)
        {
            AoEOn = true;
        }
        else if (previousDropdown1 == 2)
        {
            energyBoostOn = true;
        }
        else if (previousDropdown1 == 3)
        {
            if (typeOfGun == gunType.Auto)
                bulletSpreadOn = true;
            if (typeOfGun == gunType.SemiAuto)
                repeaterOn = true;
            if (typeOfGun == gunType.Lazer)
                shotgunOn = true;
        }

        if (trait2Dropdown.value == 0)
        {
            previousDropdown2 = 0;
        }
        else if (trait2Dropdown.value == 1)
        {
            previousDropdown2 = 1;
        }
        else if (trait2Dropdown.value == 2)
        {
            previousDropdown2 = 2;
        }
        else if (trait2Dropdown.value == 3)
        {
            previousDropdown2 = 3;
        }
    }

    float FindHalfwayPoint(float min, float max)
    {
        return (min + max) / 3;
    }
}
