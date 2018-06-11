using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    Animator FPSAnimator;
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
    //[SerializeField]
    float scopedFOV;
    [SerializeField]
    GameObject playerMovement;
    [SerializeField]
    float scopedSensitivity;

    bool isScoped = false;
    float scopeTime = 0.15f;
    float unscopedFOV;

    private void Update()
    {
        if (Input.GetButtonDown(alternateFireButton))
        {
            isScoped = !isScoped;
            FPSAnimator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine(OnScope());
            else
                OnUnscope();
        }
    }

    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(scopeTime);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        scopedFOV = projectileSpawn.zoomDistance;

        playerMovement.GetComponent<CameraMouseLook>().sensitivityX = scopedSensitivity;
        playerMovement.transform.GetChild(0).GetComponent<CameraMouseLook>().sensitivityY = scopedSensitivity;

        unscopedFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void OnUnscope()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = unscopedFOV;
    }
}
