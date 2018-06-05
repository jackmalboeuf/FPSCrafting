using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    float waitTime;

    void Start()
    {
        Destroy(gameObject, waitTime);
    }
}
