using UnityEngine;
using System.Collections;

public class CameraMouseLook : MonoBehaviour
{
    enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

    [SerializeField]
    RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 3F;
    public float sensitivityY = 3F;
    [SerializeField]
    float minimumX = -360F;
    [SerializeField]
    float maximumX = 360F;
    [SerializeField]
    float minimumY = -90F;
    [SerializeField]
    float maximumY = 90F;
    public ShootProjectile projectileSpawn;

    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;
    CursorLockMode cursorMode;

    void Update()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            if (projectileSpawn.typeOfGun == ShootProjectile.gunType.SemiAuto|| projectileSpawn.typeOfGun == ShootProjectile.gunType.Lazer)
            {
                minimumY = projectileSpawn.yMin;
                maximumY = projectileSpawn.yMax;
            }
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            Quaternion recoilQuaternion = Quaternion.AngleAxis(-projectileSpawn.recoilAngle, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion * recoilQuaternion;
        }
    }

    void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalRotation = transform.localRotation;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetRotation(Transform other)
    {

    }
}
