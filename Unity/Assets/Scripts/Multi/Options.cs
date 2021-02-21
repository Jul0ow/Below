using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject principalOptions;
    public GameObject soundOptions;
    public AudioMixer master;
    public GameObject graphicOptions;
    public Dropdown ResolutionDropdown;
    public GameObject titleMenu;
    public GameObject engrenages;
    public GameObject ControlsOptions;


    private Resolution[] resolutions;

    

    public void returnmenu()
    {
        titleMenu.SetActive(true);
        principalOptions.SetActive(false);
        engrenages.SetActive(true);
    }

    public void returnsoundoption()
    {
        principalOptions.SetActive(true);
        soundOptions.SetActive(false);
    }
    
    public void returngraphicsoption()
    {
        principalOptions.SetActive(true);
        graphicOptions.SetActive(false);
    }
    public void returnControlsoption()
    {
        principalOptions.SetActive(true);
        ControlsOptions.SetActive(false);
    }

    public void graphics()
    {
        principalOptions.SetActive(false);
        graphicOptions.SetActive(true);
    }
    
    public void controls()
    {
        principalOptions.SetActive(false);
        ControlsOptions.SetActive(true);
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

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    void Start()
    {
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResIndex;
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}