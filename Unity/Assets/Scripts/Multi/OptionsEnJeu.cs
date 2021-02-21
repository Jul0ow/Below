using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsEnJeu : MonoBehaviour
{
    public GameObject principalOptions;
    public GameObject soundOptions;
    public AudioMixer master;
    public GameObject graphicOptions;
    public Dropdown ResolutionDropdown;
    private Resolution[] resolutions;
    private bool menuOpen = false;
    public Slider soundSlide;
    private float soundValue;

        public void closeOptions()
    {
        menuOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        principalOptions.SetActive(false);
        graphicOptions.SetActive(false);
        soundOptions.SetActive(false);
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

    public void graphics()
    {
        principalOptions.SetActive(false);
        graphicOptions.SetActive(true);
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
        master.GetFloat("Master", out soundValue);
        soundSlide.SetValueWithoutNotify(soundValue);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuOpen)
        {
            menuOpen = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            principalOptions.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuOpen)
        {
            menuOpen = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            principalOptions.SetActive(false);
            graphicOptions.SetActive(false);
            soundOptions.SetActive(false);
        }
    }
}
