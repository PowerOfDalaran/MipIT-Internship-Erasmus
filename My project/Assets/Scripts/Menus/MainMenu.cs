using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;

    //Covering the screen and initating change of the scene
    public void PlayGame(int sceneId = 3)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }

    //Covering the screen and initating change of the scene
    public void Options(int sceneId = 2)
    {
        transitionAnimator.SetTrigger("CoverTheScreen");
        StartCoroutine(SceneChanger.MoveToScene(sceneId, new Vector3(0, 0, -10), transitionAnimator));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
