using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    //[SerializeField] GameObject pauseMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void MainMenu()
    {
        //Poprawa
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/Menus/MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
