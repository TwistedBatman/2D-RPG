using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // It will keep playing even when changing scenes
        }
/*        else if (SceneManager.GetActiveScene().name == "CreditsScene")
        {
            Destroy(GameObject.FindWithTag("AudioManager"));
            instance = this;
            DontDestroyOnLoad(gameObject);
        }*/
        else
            Destroy(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.group;
            s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //PlaySound("", 0f);
    }
    public void PlaySound(string name, float delay)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // It's like foreach, it searches in the array for the sound
        if (s == null) // If the audio is not found 
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.PlayDelayed(delay);
    }

/*    public void StopSoundAtSpecificTime(string name, float delay)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
                s.source.SetScheduledEndTime(delay);
        }
    }*/

    public void StopSound(string name)
    {
        if (name == "AllSFX")
        {
            foreach (Sound s in sounds)
            {
                if (s == null) // If the audio is not found 
                {
                    Debug.LogWarning("Sound: " + name + " not found!");
                    return;
                }
                if (!(s.name == "Theme"))
                    s.source.Stop();
            }
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
    }

    public void PauseSFX()
    {
        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            if (!(s.name == "Theme"))
                s.source.Pause();
        }
    }

    public void UnPauseSFX()
    {
        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.UnPause();
        }
    }
}
