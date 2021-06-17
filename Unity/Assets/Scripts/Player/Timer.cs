using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI text;
    

    // Update is called once per frame
    void Update()
    {
        text.text = Convert.ToString(Convert.ToInt32(Time.time));
    }
}
