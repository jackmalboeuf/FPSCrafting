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
    SliderLimit sLimit;
    [SerializeField]
    bool isTraitDropdown;
    [SerializeField]
    List<GameObject> components = new List<GameObject>();

    List<Dropdown.OptionData> OtherDropdownOptions = new List<Dropdown.OptionData>();

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
            sLimit.sliders.Remove(component.transform.GetComponentInChildren<Slider>());
        }

        components[dropdownMenu.value].SetActive(true);
        sLimit.sliders.Insert(0, components[dropdownMenu.value].transform.GetComponentInChildren<Slider>());
    }
}
