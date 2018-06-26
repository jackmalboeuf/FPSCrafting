using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderLimit : MonoBehaviour
{
    [SerializeField]
    Slider damageSlider;
    [SerializeField]
    Slider fireRateSlider;
    [SerializeField]
    Slider rangeSlider;
    [SerializeField]
    Slider accuracySlider;
    [SerializeField]
    Slider energySlider;
    [SerializeField]
    Slider cooldownSpeedSlider;
    [SerializeField]
    Slider bulletVelocitySlider;
    [SerializeField]
    Slider zoomDistanceSlider;
    [SerializeField]
    Slider meleeDamageSlider;
    [SerializeField]
    Slider trait1aSlider;
    [SerializeField]
    Slider trait1bSlider;
    [SerializeField]
    Slider trait1cSlider;
    [SerializeField]
    Slider trait2aSlider;
    [SerializeField]
    Slider trait2bSlider;
    [SerializeField]
    Slider trait2cSlider;

    public List<Slider> sliders = new List<Slider>();

    float sliderTotalValue = 200;
    float sliderCurrentTotal;
    Slider lastSlider;
    bool shouldChangeIndex = true;

    private void Start()
    {
        sliders.Add(damageSlider);
        sliders.Add(fireRateSlider);
        sliders.Add(rangeSlider);
        sliders.Add(accuracySlider);
        sliders.Add(energySlider);
        sliders.Add(cooldownSpeedSlider);
        sliders.Add(bulletVelocitySlider);
        sliders.Add(zoomDistanceSlider);
    }

    void LimitSliders()
    {
        if (sliderCurrentTotal >= sliderTotalValue)
        {
            for (int i = 0; i < sliders.Count; i++)
            {
                lastSlider = sliders[sliders.Count - (1 + i)];

                if (lastSlider.gameObject.activeSelf && lastSlider.value > lastSlider.minValue)
                {
                    shouldChangeIndex = false;
                    lastSlider.value--;
                    shouldChangeIndex = true;
                    break;
                }
            }
        }

        GetComponent<Text>().text = sliderCurrentTotal.ToString();
    }

    void AddCurrentTotal()
    {
        sliderCurrentTotal = 0;

        for (int i = 0; i < sliders.Count; i++)
        {
            sliderCurrentTotal += sliders[i].value;
        }
    }

    public void OnDamageValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(damageSlider);
            sliders.Insert(0, damageSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnFireRateValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(fireRateSlider);
            sliders.Insert(0, fireRateSlider);
        }
        
        AddCurrentTotal();
        LimitSliders();
    }

    public void OnRangeValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(rangeSlider);
            sliders.Insert(0, rangeSlider);
        }
        
        AddCurrentTotal();
        LimitSliders();
    }

    public void OnAccuracyValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(accuracySlider);
            sliders.Insert(0, accuracySlider);
        }
        
        AddCurrentTotal();
        LimitSliders();
    }

    public void OnEnergyValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(energySlider);
            sliders.Insert(0, energySlider);
        }
        
        AddCurrentTotal();
        LimitSliders();
    }
}
