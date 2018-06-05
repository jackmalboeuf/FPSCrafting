using UnityEngine;
using System.Collections;

public class SetActiveStateOnStart : MonoBehaviour
{
    [SerializeField]
    bool isActive;

    void Start()
    {
        gameObject.SetActive(isActive);
    }
}
