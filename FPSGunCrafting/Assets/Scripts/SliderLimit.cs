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

    [HideInInspector]
    public List<Slider> sliders = new List<Slider>();
    [HideInInspector]
    public float sliderTotalValue;

    float sliderCurrentTotal;
    Slider lastSlider;
    bool shouldChangeIndex = false;

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

        UpdateLimitText();
        shouldChangeIndex = true;
    }

    public void UpdateLimitText()
    {
        sliderTotalValue = sliders.Count * 10;
        AddCurrentTotal();
        GetComponent<Text>().text = "Points used: " + sliderCurrentTotal.ToString() + " \t\t\t " + "Total points: " + sliderTotalValue.ToString();
    }

    void AddCurrentTotal()
    {
        sliderCurrentTotal = 0;

        for (int i = 0; i < sliders.Count; i++)
        {
            sliderCurrentTotal += sliders[i].value;
        }
    }

    void LimitSliders()
    {
        if (sliderCurrentTotal > sliderTotalValue)
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

        UpdateLimitText();
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

    public void OnCooldownSpeedValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(cooldownSpeedSlider);
            sliders.Insert(0, cooldownSpeedSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnBulletVelocityValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(bulletVelocitySlider);
            sliders.Insert(0, bulletVelocitySlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnZoomDistanceValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(zoomDistanceSlider);
            sliders.Insert(0, zoomDistanceSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnMeleeDamageValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(meleeDamageSlider);
            sliders.Insert(0, meleeDamageSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait1aValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait1aSlider);
            sliders.Insert(0, trait1aSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait1bValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait1bSlider);
            sliders.Insert(0, trait1bSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait1cValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait1cSlider);
            sliders.Insert(0, trait1cSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait2aValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait2aSlider);
            sliders.Insert(0, trait2aSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait2bValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait2bSlider);
            sliders.Insert(0, trait2bSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }

    public void OnTrait2cValueChanged()
    {
        if (shouldChangeIndex)
        {
            sliders.Remove(trait2cSlider);
            sliders.Insert(0, trait2cSlider);
        }

        AddCurrentTotal();
        LimitSliders();
    }
}
