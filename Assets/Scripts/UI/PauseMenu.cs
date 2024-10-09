using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject pauseBackground;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Time.timeScale = 0f; // Start of the game so the player can read the story 
        Debug.Log("Pause for story.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pauseBackground.SetActive(!pauseBackground.activeSelf);
            if (pauseBackground.activeSelf)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        pauseBackground.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Options()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void BackButton()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ToMainMenu()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        SceneManager.LoadScene("Menu");
    }

    public void ToDesktop()
    {
        audioManager.PlaySound("ButtonClick", 0f);
        Application.Quit();
    }
}
