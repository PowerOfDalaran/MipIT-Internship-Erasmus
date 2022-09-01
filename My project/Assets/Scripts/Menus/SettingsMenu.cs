using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Slider generalVolumeSlider;
    [SerializeField] Slider soundEffectsVolumeSlider;

    [SerializeField] Animator transitionAnimator;

    MusicManager musicManager;

    void Awake()
    {
        //Accesign music manager and setting sliders to proper values
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        generalVolumeSlider.value = musicManager.GeneralVolume;
        soundEffectsVolumeSlider.value = musicManager.SoundEffectsVolume;
    }
    

    //Changing general sounds volume
    public void GeneralVolumeChanged(float sliderValue)
    {
        musicManager.GeneralVolume = sliderValue;
    }

    //Changing single sound effects volume (like firing weapons etc.)
    public void SoundEffectVolumeChanged(float sliderValue)
    {
        musicManager.SoundEffectsVolume = sliderValue;
    }

    //Covering the screen and initating change of the scene
    public void Return(int sceneId = 0)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }
}
