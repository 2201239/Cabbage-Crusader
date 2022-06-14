using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealthEnemy(int health2)
    {
        slider.maxValue = health2;
        slider.value = health2;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealthEnemy(int health2)
    {
        slider.value = health2;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
