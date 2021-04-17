﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] private Menu[] menus;
    public GameObject titleMenu;
    public GameObject NewgameMenu;
    public GameObject MultiplayerMenu;
    public GameObject optionsMenu;
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
        throw new NotImplementedException();
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
}
