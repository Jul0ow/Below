using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private Menu[] menus;
    public GameObject titleMenu;
    public GameObject NewgameMenu;
    public GameObject MultiplayerMenu;
    public GameObject optionsMenu;
    public GameObject Findroom;
    public GameObject CreateRoom;
    public GameObject Errormenu;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Screen.fullScreen = true;
    }

    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(Menu menu)
    { 
        for (int i = 0; i < menus.Length; i++)
        { 
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void options()
    {
        titleMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Newgame2Menu()
    {
        NewgameMenu.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void NewMulti2Multi()
    {
        NewgameMenu.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }

    public void NewSolo2Solo()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void Title2New()
    {
        NewgameMenu.SetActive(true);
        titleMenu.SetActive(false);
    }

    public void Multi2New()
    {
        MultiplayerMenu.SetActive(false);
        NewgameMenu.SetActive(true);
    }

    public void Multi2Create()
    {
        MultiplayerMenu.SetActive(false);
        CreateRoom.SetActive(true);
    }

    public void Multi2Find()
    {
        Findroom.SetActive(true);
        MultiplayerMenu.SetActive(false);
    }

    public void Find2mult()
    {
        Findroom.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }

    public void create2Mult()
    {
        CreateRoom.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }

    public void Error2Mult()
    {
        Errormenu.SetActive(false);
        MultiplayerMenu.SetActive(true);
    }
    
}
