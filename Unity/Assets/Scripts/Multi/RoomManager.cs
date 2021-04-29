using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public GameObject Summoner;
    private enum Team
    {
        Red,
        Blue,
    }

    private Team myTeam;
    void Awake()
    { //Make sure that there is only one roomManager
        if (Instance) //check if another RoomManager exists
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void ChooseRed()
    {
        myTeam = Team.Red;
    }

    public void ChooseBlue()
    {
        myTeam = Team.Blue;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadScene)
    {
        if (scene.buildIndex == 1) // we are in game scene (because scene game ==1)
        {
            if (myTeam == Team.Blue)
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), new Vector3(-13.10f,0.16f,-1027),
                    Quaternion.identity);
            }
            else
            {
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero,
                    Quaternion.identity); 
            }
            foreach (var x in scene.GetRootGameObjects())
                foreach (var summoner in x.GetComponentsInChildren<SummonEnnemy>())
                    summoner.Summon();
        }
    }
    
}
