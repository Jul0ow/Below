using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewtowrkController : Photon.Pun.MonoBehaviourPunCallbacks
{
   public Text txtStatus = null;
   public GameObject buttonStart = null;
   public byte MaxPlayers = 5;

   private void Start()
   {
      PhotonNetwork.ConnectUsingSettings();
      buttonStart.SetActive(false);
      Status("Connecting to server");
   }

   public override void OnConnectedToMaster()
   {
      base.OnConnectedToMaster();
      buttonStart.SetActive(true);
      Status("Connected to " + PhotonNetwork.ServerAddress);
      
   }

   public void ButtonStart_Click()
   {
      string roomName = "Room1";
      Photon.Realtime.RoomOptions opts = new Photon.Realtime.RoomOptions();
      opts.IsOpen = true;
      opts.IsVisible = true;
      opts.MaxPlayers = MaxPlayers;

      PhotonNetwork.JoinOrCreateRoom(roomName, opts,Photon.Realtime.TypedLobby.Default);
      buttonStart.SetActive(false);
      Status("Joining " + roomName);
   }

   public override void OnJoinedRoom()
   {
      base.OnJoinedRoom();
      SceneManager.LoadScene("Level");
   }
    void Status(string msg)
   {
      Debug.Log(msg);
      txtStatus.text = msg;
   }

}
