using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static System.Random;
using Random = UnityEngine.Random;

public class EnnemyList : MonoBehaviour
{
    
    List<string> MonsterList;

    List<string> EliteList;
    
    
    

    public void Creat()
    {
        MonsterList = new List<string>();
        EliteList = new List<string>();
        
        MonsterList.Add("Lice");
        MonsterList.Add("Buried Lice");
        MonsterList.Add("Splitter");
        MonsterList.Add("Gout");
        MonsterList.Add("Kamikaze Splitter");
        MonsterList.Add("Knight Mare");
        
        EliteList.Add("QueenLice");
        EliteList.Add("Goutausorus");
        EliteList.Add("Mauricio");
        EliteList.Add("BigBouquetin");
    }

    public string pickennemy()
    {
        int x = Random.Range(0, MonsterList.Count);
        return MonsterList[x];
    }
    
    public string pickennemy(int x)
    {
        return MonsterList[x];
    }
    
    public string pickelite()
    {
        int x = Random.Range(0, EliteList.Count);
        return EliteList[x];
    }
    
    public string pickelite(int x)
    {
        return EliteList[x];
    }

}
