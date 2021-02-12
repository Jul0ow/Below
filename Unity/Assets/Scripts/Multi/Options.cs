using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public GameObject principalOptions;
    public GameObject soundOptions;
    public AudioMixer master;
    public void options()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public void returnmenu()
    {
        PhotonNetwork.LoadLevel(0);
        PhotonNetwork.Disconnect();
    }
    
    public void sound()
    {
        principalOptions.SetActive(false);
        soundOptions.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        master.SetFloat("Master", volume);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
