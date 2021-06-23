using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        int time = Convert.ToInt32(UnityEngine.Time.timeSinceLevelLoad);
        string minutes = Convert.ToString(time / 60);
        string secondes = Convert.ToString(time % 60);
        text.text = "Your time is " + minutes + ":" + secondes;
    }
}
