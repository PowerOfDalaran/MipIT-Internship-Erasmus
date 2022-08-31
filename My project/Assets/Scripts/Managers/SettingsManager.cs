using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] Slider generalVolumeSlider;
    [SerializeField] Slider soundEffectsVolumeSlider;
    MusicManager musicManager;

    void Awake()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        generalVolumeSlider.value = musicManager.GeneralVolume;
        soundEffectsVolumeSlider.value = musicManager.SoundEffectsVolume;
    }
    
    public void GeneralVolumeChanged(float sliderValue)
    {
        musicManager.GeneralVolume = sliderValue;
    }
    public void SoundEffectVolumeChanged(float sliderValue)
    {
        musicManager.SoundEffectsVolume = sliderValue;
    }

    public void Return(int sceneId = 0)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }
}
