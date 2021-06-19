using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoloManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Spawnpoint;
    public Scene scene;

    protected void Start()
    {
        OnSceneLoaded(scene);
    }
    
    void OnSceneLoaded(Scene scene)
    {
        if (scene.buildIndex == 3) // we are in game scene (because scene game ==3)
        {
            Instantiate(player, Spawnpoint.transform.position, Quaternion.identity);
            foreach (var x in scene.GetRootGameObjects())
            foreach (var summoner in x.GetComponentsInChildren<SummonEnnemy>())
                summoner.Summon();
            Destroy(this);
        }
    }
}
