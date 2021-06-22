using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEditor;


public class EndGame : MonoBehaviourPunCallbacks
{
    public GameObject overlay;

    public bool solo = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        if (solo)
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
        else
        {
            PhotonNetwork.LeaveRoom();
        }
        
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
