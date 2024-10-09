using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer MusicMixer;
    public AudioMixer SFXMixer;
    
    public Slider MusicSlider;
    public Slider SFXSlider;

    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    float musicSliderValue;
    float SFXSliderValue;

    public Button musicButton;
    public Button SFXButton;

    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite SFXOnSprite;
    public Sprite SFXOffSprite;

    Resolution[] resolutions;    

    void Awake()
    {
        // Put to the slider the last value that was saved
        MusicSlider.value = PlayerPrefs.GetFloat("MusicSlider", 1); 
        SFXSlider.value = PlayerPrefs.GetFloat("SFXSlider", 1); 
    }

    void Start()
    {
        // Get the available resolutions from the player and filter out the duplicates that arise from the hz
        resolutions = Screen.resolutions.Select(resolution => new Resolution {width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionDropdown.ClearOptions(); // Clear the array

        // To add options to the dropdown they have to be strings
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        // Convert the resolutions to strings
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            // Check if the resolution [i] is the same with the current screen resolution
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
                //Debug.Log(currentResolutionIndex);
            }
        }
        resolutionDropdown.AddOptions(options); // Add the resolutions (converted to strings) to the dropdown
        resolutionDropdown.value = currentResolutionIndex; // Set the current resolution
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen; // If it's fullscreen then have the check mark on otherwise it's off       

        qualityDropdown.value = PlayerPrefs.GetInt("quality", 3);

        // Put the slider value to the audio mixer
        MusicMixer.SetFloat("MusicVolume", Mathf.Log(PlayerPrefs.GetFloat("MusicSlider")) * 20);
        SFXMixer.SetFloat("SFXVolume", Mathf.Log(PlayerPrefs.GetFloat("SFXSlider")) * 20);

        // Put the right music and SFX icons
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 0)
            musicButton.GetComponent<Image>().sprite = musicOnSprite;
        else
            musicButton.GetComponent<Image>().sprite = musicOffSprite;

        if (PlayerPrefs.GetInt("SFXMuted", 0) == 0)
            SFXButton.GetComponent<Image>().sprite = SFXOnSprite;
        else
            SFXButton.GetComponent<Image>().sprite = SFXOffSprite;

    }

    public void SetSFXVolume(float volume) // Called when SFX slider changes
    {
        // Put the slider value to the audio mixer
        SFXMixer.SetFloat("SFXVolume", Mathf.Log(volume) * 20);
        SFXButton.GetComponent<Image>().sprite = SFXOnSprite;
        PlayerPrefs.SetFloat("SFXSlider", SFXSlider.value);
    }

    public void SetMusicVolume(float volume) // Called when music slider changes
    {
        // Put the slider value to the audio mixer
        MusicMixer.SetFloat("MusicVolume", Mathf.Log(volume) * 20);
        musicButton.GetComponent<Image>().sprite = musicOnSprite;
        PlayerPrefs.SetFloat("MusicSlider", MusicSlider.value);
    }

    public void ToggleMusic() // Mute/unmute music
    {
        if (PlayerPrefs.GetInt("MusicMuted", 0) == 0) // If not muted
        {
            PlayerPrefs.SetInt("MusicMuted", 1); // Change to mute for the next time
            MusicMixer.SetFloat("MusicVolume", Mathf.Log(0.0001f) * 20); // Set the current volume to 0
            musicSliderValue = MusicSlider.value; // Get the slider value before muting
            MusicSlider.value = 0.0001f; // Current volume
            PlayerPrefs.SetFloat("PreviousMusicSlider", musicSliderValue); // Save the previous slider value
            musicButton.GetComponent<Image>().sprite = musicOffSprite; // Change sprite
        }
        else
        {
            PlayerPrefs.SetInt("MusicMuted", 0); // Change to unmute for the next time
            MusicSlider.value = PlayerPrefs.GetFloat("PreviousMusicSlider"); // Get the slider value before muting
            MusicMixer.SetFloat("MusicVolume", Mathf.Log(MusicSlider.value) * 20); // Set the current volume to the last saved slider value before muting
            musicButton.GetComponent<Image>().sprite = musicOnSprite; // Change sprite
        }
        PlayerPrefs.SetFloat("MusicSlider", MusicSlider.value); // Set the slider value to the current volume
    }

    // Same as ToggleMusic but for the SFX
    public void ToggleSFX() // Mute/unmute SFX
    {
        if (PlayerPrefs.GetInt("SFXMuted", 0) == 0)
        {
            PlayerPrefs.SetInt("SFXMuted", 1);
            SFXMixer.SetFloat("SFXVolume", Mathf.Log(0.0001f) * 20);
            SFXSliderValue = SFXSlider.value;
            SFXSlider.value = 0.0001f;
            PlayerPrefs.SetFloat("PreviousSFXSlider", SFXSliderValue);
            SFXButton.GetComponent<Image>().sprite = SFXOffSprite;
        }
        else
        {
            PlayerPrefs.SetInt("SFXMuted", 0);
            SFXSlider.value = PlayerPrefs.GetFloat("PreviousSFXSlider");
            SFXMixer.SetFloat("SFXVolume", Mathf.Log(SFXSlider.value) * 20);
            SFXButton.GetComponent<Image>().sprite = SFXOnSprite;
        }
        PlayerPrefs.SetFloat("SFXSlider", SFXSlider.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex]; // Find the resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void PlayButtonClick() // Is called from some buttons to make the click noise
    {
        FindObjectOfType<AudioManager>().PlaySound("ButtonClick", 0f);
    }
}
