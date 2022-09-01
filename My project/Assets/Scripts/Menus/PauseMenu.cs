using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] int mainMenuId = 0;

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(mainMenuId, new Vector3(0, 0, -10), transitionAnimator));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
