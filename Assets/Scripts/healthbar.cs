using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
 public Slider healthbarSlider;

    public void givefullhealth(float health)
    {
        healthbarSlider.maxValue = health;
        healthbarSlider.value = health;
    }

    public void sethealth(float health)
    {
        healthbarSlider.value = health;
    }
}
