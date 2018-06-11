using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownItemDisable : MonoBehaviour
{
    [SerializeField]
    DropdownBehavior dropBehavior;

    private void Start()
    {
        Toggle tog = GetComponent<Toggle>();

        if (transform.GetSiblingIndex() - 1 == dropBehavior.GetComponent<Dropdown>().value && transform.GetSiblingIndex() != 1)
        {
            tog.interactable = false;
        }
    }
}
