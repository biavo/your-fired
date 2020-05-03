using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

//taken from this video: https://www.youtube.com/watch?v=yQgVKR6PMqo
public class AudioSettings : MonoBehaviour
{

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;

    float MusicVolume = 1f;
    float SFXVolume = 1f;
    public Slider MusicSlider;
    public Slider SFXSlider;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

    void Start()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void setMusicVolumeLevel()
    {
        var vol = MusicSlider.value;
        MusicVolume = vol;
        Music.setVolume(MusicVolume);
        //Music.
    }

    public void setSFXVolumeLevel()
    {
        var vol = SFXSlider.value;
        SFXVolume = vol;
        SFX.setVolume(SFXVolume);
    }


}
