using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;
    
    [SerializeField] TMP_InputField roomNameInputField;

    [SerializeField]  TMP_Text errorText;
    [SerializeField]  TMP_Text roomNameText;
    [SerializeField]  Transform roomListContent;
    [SerializeField]  GameObject roomListItemPrefab;
    [SerializeField]  Transform playerListContent;
    [SerializeField]  GameObject PlayerListItemPrefab;
    [SerializeField] private GameObject startgameButton;
    public GameObject titlemenu;
    public GameObject RoomMenu;
    public GameObject findRoom;
    public GameObject createRoom;
    public GameObject error;
    public GameObject loading;
    

    void Awake()
    {
        Instance=this;
    }
    
    void Start()
    {
        Debug.Log("Connecting To Master");
        PhotonNetwork.ConnectUsingSettings();
        titlemenu.SetActive(true);
        loading.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
       
        Debug.Log("joined Lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000"); //donne un pseudo aléatoire de type "Player xxxx"
    }
    
    // Update is called once per frame

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        loading.SetActive(true);
        loading.SetActive(true);
        createRoom.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        RoomMenu.SetActive(true);
        loading.SetActive(false);
        findRoom.SetActive(false);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < players.Length ; i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startgameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startgameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1); // we use 1 as parameter because 1 is the build index of the game scene set in the nuild settings
    }
    

    public override void OnCreateRoomFailed(short returncode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        createRoom.SetActive(false);
        error.SetActive(true);
        loading.SetActive(false);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        loading.SetActive(true);
        titlemenu.SetActive(true);
        loading.SetActive(false);
        //MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        loading.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        RoomMenu.SetActive(false);
        loading.SetActive(false);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
