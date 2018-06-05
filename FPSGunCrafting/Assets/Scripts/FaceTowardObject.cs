using UnityEngine;
using System.Collections;

public class FaceTowardObject : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }
}
