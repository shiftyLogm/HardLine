using UnityEngine;
using UnityEngine.UI;

public class GeralMusic : MonoBehaviour
{
    public string tagVolume;
    private GameObject[] geralMusic;
    public Slider sliderMusic;


    void Start()
    { 
        geralMusic = GameObject.FindGameObjectsWithTag(tagVolume);
        if (geralMusic != null)
        {
            foreach (GameObject musicObject in geralMusic)
            {
                AudioSource audio = musicObject.GetComponent<AudioSource>();
                audio.volume = 5f;
                audio.loop = true;
                audio.playOnAwake = true;
                audio.pitch = .85f;
            }
        }

        sliderMusic.minValue = 0;
        sliderMusic.maxValue = 1; 
    }

    public void setCurrentVolume()
    {
        if (geralMusic != null)
        { 
            foreach (GameObject music in geralMusic)
            {
                AudioSource audio = music.GetComponent<AudioSource>();
                audio.volume = sliderMusic.value;
            }
        }
    }
}