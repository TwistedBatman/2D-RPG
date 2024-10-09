using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuButtons : MonoBehaviour
{
    AudioManager audioManager;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        MusicMixer.SetFloat("MusicVolume", Mathf.Log(PlayerPrefs.GetFloat("MusicSlider")) * 20);
        SFXMixer.SetFloat("SFXVolume", Mathf.Log(PlayerPrefs.GetFloat("SFXSlider")) * 20);
        audioManager.PlaySound("Music", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioManager.PlaySound("ButtonClick", 0f); ;
        SceneManager.LoadScene("Game");
    }
    
    public void SettingsMenu()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    } 
    
    public void Credits()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        creditsMenu.SetActive(true);

    } 
    
    public void ExitGame()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        //DataPersistanceManager.instance.SaveGame();
        Application.Quit();
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
    }

    public void ButtonClickAudio()
    {

    }
}
