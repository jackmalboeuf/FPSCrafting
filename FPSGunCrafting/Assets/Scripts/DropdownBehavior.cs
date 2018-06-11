using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownBehavior : MonoBehaviour
{
    [SerializeField]
    Dropdown dropdownMenu;
    [SerializeField]
    Dropdown otherTraitDropdown;
    [SerializeField]
    bool isTraitDropdown;
    [SerializeField]
    List<GameObject> components = new List<GameObject>();

    List<UnityEngine.UI.Dropdown.OptionData> OtherDropdownOptions = new List<UnityEngine.UI.Dropdown.OptionData>();

    private void Start()
    {
        if (otherTraitDropdown != null && isTraitDropdown)
        {
            foreach (var item in otherTraitDropdown.options)
            {
                OtherDropdownOptions.Add(item);
            }
        }
    }

    public void OnValueChanged()
    {
        foreach (GameObject component in components)
        {
            component.SetActive(false);
        }

        components[dropdownMenu.value].SetActive(true);
    }
}
