using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    public Rigidbody rb;
    public int MaxHP;
    public int HP;
    public Slider slider;
    public GameObject owner;

    public virtual void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public virtual void SetHealth(int health)
    {
        slider.value = health;
    }
    
    protected virtual void Update()
    {
        SetHealth(HP);
    }
}
