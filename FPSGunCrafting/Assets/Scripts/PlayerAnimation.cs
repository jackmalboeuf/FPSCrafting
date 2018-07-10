using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject scopeOverlay;
    [SerializeField]
    string alternateFireButton;
    [SerializeField]
    GameObject weaponCamera;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    ShootProjectile projectileSpawn;
    [SerializeField]
    GameObject playerMovement;
    [SerializeField]
    float scopedSensitivity;
    [SerializeField]
    Dropdown altAttackDropdown;
    [SerializeField]
    GameObject meleeCollision;
    [SerializeField]
    Transform meleeHandle;
    [SerializeField]
    GameObject reticle;
    [SerializeField]
    ActivateOnButtonPress menuManager;

    [HideInInspector]
    public bool isScoped = false;

    float scopedFOV;
    float scopeTime = 0.15f;
    float unscopedFOV;
    bool isMeleeing = false;

    private void Update()
    {
        if (Input.GetButtonDown(alternateFireButton) && !menuManager.tuningMenu.activeSelf)
        {
            if (altAttackDropdown.value == 0)
            {
                isScoped = !isScoped;
                GetComponent<Animator>().SetBool("Scoped", isScoped);

                if (isScoped && scopeOverlay != null)
                    StartCoroutine(OnScope());
                else if (!isScoped && scopeOverlay != null)
                    OnUnscope();
            }
            else if (altAttackDropdown.value == 1)
            {
                if (!isMeleeing)
                    StartCoroutine(OnMelee());
            }
        }
    }

    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(scopeTime);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);
        menuManager.canShowMenu = false;

        scopedFOV = projectileSpawn.zoomDistance;
        reticle.SetActive(false);

        playerMovement.GetComponent<CameraMouseLook>().sensitivityX = scopedSensitivity;
        playerMovement.transform.GetChild(0).GetComponent<CameraMouseLook>().sensitivityY = scopedSensitivity;

        unscopedFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void OnUnscope()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);
        reticle.SetActive(true);
        menuManager.canShowMenu = true;

        playerMovement.GetComponent<CameraMouseLook>().sensitivityX = 3;
        playerMovement.transform.GetChild(0).GetComponent<CameraMouseLook>().sensitivityY = 3;
        mainCamera.fieldOfView = unscopedFOV;
    }

    IEnumerator OnMelee()
    {
        isMeleeing = true;
        projectileSpawn.canFire = false;
        GetComponent<Animator>().SetBool("IsMeleeing", isMeleeing);
        yield return new WaitForSeconds(0.08f);
        GameObject meleeCollider = Instantiate(meleeCollision, meleeHandle) as GameObject;
        meleeCollider.GetComponent<MeleeColliderDamage>().meleeDamage = projectileSpawn.meleeDamage;
        yield return new WaitForSeconds(0.07f);
        Destroy(meleeCollider);
        yield return new WaitForSeconds(0.3f);
        isMeleeing = false;
        projectileSpawn.canFire = true;
        GetComponent<Animator>().SetBool("IsMeleeing", isMeleeing);
    }
}
