using UnityEngine.Audio;
using UnityEngine;


// A class created in order to change the audio settings of each sound
[System.Serializable] // In order to show on inspector
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup group;

    [Range(0f, 1f)] // Make it a slider
    public float volume;
/*    [Range(.1f, 3f)]
    public float pitch;*/

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}