using UnityEngine;

public class PauseMenu : MonoBehaviour
{    
    public static bool GameIsPaused = false;

    [SerializeField] Animator transitionAnimator;
    MusicManager musicManager;

    void Awake()
    {
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    //Setting time scale to 1 and unpausing the game itself
    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    //Setting time scale to 0 and pausing the game itself
    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //Setting time scale to 1, covering the screen, initating change of the scene and change of the music
    public void MainMenu(int sceneId = 0)
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        musicManager.ChangeBackgroundMusic(musicManager.menuBackgroundClip);

        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
