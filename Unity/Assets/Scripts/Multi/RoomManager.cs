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
    public GameObject player;

    public GameObject redTemoin;
    public GameObject blueTemoin;
    //public static bool firstLoad = false;

    public void Start()
    {
        
    }
    public enum Team
    {
        Red,
        Blue,
    }

    private Team myTeam;
    void Awake()
    { //Make sure that there is only one roomManager
        //firstLoad = true;
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
        blueTemoin.SetActive(false);
        redTemoin.SetActive(true);
    }

    public void ChooseBlue()
    {
        myTeam = Team.Blue;
        redTemoin.SetActive(false);
        blueTemoin.SetActive(true);
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
            Destroy(gameObject);
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

        if (scene.buildIndex == 3)
        {
            GameObject manager = Instantiate(player, new Vector3(-777,33,-495), Quaternion.identity);
            manager.GetComponent<PlayerManager>().CreateControllerSolo();
            foreach (var x in scene.GetRootGameObjects())
            foreach (var summoner in x.GetComponentsInChildren<SummonEnnemy>())
                summoner.Summon();
        }
    }
    
}
