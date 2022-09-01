using UnityEngine;

public class PauseMenu : MonoBehaviour
{    
    public static bool GameIsPaused = false;

    [SerializeField] Animator transitionAnimator;

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

    //Setting time scale to 1, covering the screen and initating change of the scene
    public void MainMenu(int sceneId = 0)
    {
        Time.timeScale = 1f;

        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
