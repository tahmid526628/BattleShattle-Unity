using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;

    public Image imgFill; // we need this for set the gradient color after update health bar

    public void SetHealth(float health)
    {
        slider.value = health;

        //update color
        imgFill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        //color fix
        imgFill.color = gradient.Evaluate(1f);
    }
}
