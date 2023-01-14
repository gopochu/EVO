using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int MaxHP;

    private void Awake()
    {
        slider.maxValue = MaxHP;
        slider.value = MaxHP;
    }

    public void SetHealth(int hp)
    {
        slider.value = hp;
    }
}
