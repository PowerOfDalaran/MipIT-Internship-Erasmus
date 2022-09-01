using UnityEngine;

public class GameModeMenu : MonoBehaviour
{   
    [SerializeField] Animator transitionAnimator;

    MusicManager musicManager;

    void Awake()
    {
        //Accessing the music manager
        musicManager = GameObject.Find("MusicManager")?.GetComponent<MusicManager>();
    }

    //Covering the screen, initating change of the scene and switching the background music
    public void PlayMission1(int sceneId = 1)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));

        musicManager.ChangeBackgroundMusic(musicManager.gameBackgroundClip);
    }

    //Covering the screen and initating change of the scene
    public void Return(int sceneId = 0)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }
}
