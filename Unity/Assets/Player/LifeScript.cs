using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{
    public Rigidbody rb;
    public int MaxHP = 100;
    private int HP;
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Start()
    {
        HP = MaxHP;
        SetMaxHealth(MaxHP);
    }
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            HP -= 1;
        }
        if (Input.GetKeyDown("l"))
        {
            HP -= 10;
        }
        if (rb.position.y < -10f)
        {
            HP = 0;
        }
        SetHealth(HP);
        if (HP <= 0)
        {
            /* Do Something */
        }
    }
}
