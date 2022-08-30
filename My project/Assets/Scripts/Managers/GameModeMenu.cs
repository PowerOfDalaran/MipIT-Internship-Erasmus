using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeMenu : MonoBehaviour
{   
    [SerializeField] Animator transitionAnimator;
    MusicManager musicManager;

    void Awake()
    {
        musicManager = GameObject.Find("MusicManager")?.GetComponent<MusicManager>();
    }

    public void PlayMission1(int sceneId = 1)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
        musicManager.ChangeBackgroundMusic(musicManager.gameBackgroundClip);
    }

    public void Return(int sceneId = 0)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }
}
