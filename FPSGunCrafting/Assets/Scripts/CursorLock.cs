using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour
{
    [SerializeField]
    ShootProjectile projectileSpawn;

    bool isCursorLocked;

    void Start()
    {
        ToggleCursorState();
    }

    void Update()
    {
        CheckInput();
        CheckIfCursorShouldBeLocked();
    }

    void ToggleCursorState()
    {
        isCursorLocked = !isCursorLocked;
    }

    void CheckInput()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Menu"))
        {
            ToggleCursorState();
        }
    }

    void CheckIfCursorShouldBeLocked()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<CameraMouseLook>().enabled = true;
            transform.GetChild(0).GetComponent<CameraMouseLook>().enabled = true;
            if (projectileSpawn != null)
                projectileSpawn.canFire = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<CameraMouseLook>().enabled = false;
            transform.GetChild(0).GetComponent<CameraMouseLook>().enabled = false;
            if (projectileSpawn != null)
                projectileSpawn.canFire = false;
        }
    }
}
