﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static System.Random;

public class EnnemyList : MonoBehaviour
{
    private List<string> MonsterList = new List<string>();

    private List<string> EliteList = new List<string>();

    public void Creat()
    {
        MonsterList.Add("Lice");
        MonsterList.Add("Buried Lice");
        MonsterList.Add("Splitter");
        
        EliteList.Add("QueenLice");
    }

    public string pickennemy()
    {
        System.Random random = new System.Random();
        int x = random.Next(MonsterList.Count);
        return MonsterList[x];
    }
    
    public string pickelite()
    {
        System.Random random = new System.Random();
        int x = random.Next(EliteList.Count);
        return EliteList[x];
    }

}
