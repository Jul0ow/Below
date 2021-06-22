using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class TimerSolo : MonoBehaviour
{

    public TextMeshProUGUI text;
    

    // Update is called once per frame
    void Update()
    {
        int time = Convert.ToInt32(UnityEngine.Time.timeSinceLevelLoad);
        string minutes = Convert.ToString(time / 60);
        string secondes = Convert.ToString(time % 60);
        text.text = minutes + ":" + secondes;
    }
}
