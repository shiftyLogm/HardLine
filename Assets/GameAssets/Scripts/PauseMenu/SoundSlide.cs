using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlide : MonoBehaviour
{
    [SerializeField] private AudioSource[] sounds;
    [SerializeField] private AudioSource mainMusic;
    [SerializeField] private AudioSource[] effects;

    [SerializeField] private Slider sliderGeneral;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderEffects;

    void Start()
    {
        sliderGeneral.minValue = 0;
        sliderGeneral.maxValue = 1;

    }

    public void setVolumeGeneral()
    {
        foreach(var sound in sounds)
        {
            sound.volume = sliderGeneral.value;
        }
    }

    public void setVolumeMusic()
    {
        mainMusic.volume = sliderMusic.value;
    }

    public void setVolumeEffects()
    {
        foreach (var sound in effects)
        {
            sound.volume = sliderEffects.value;
        }
    }

}
