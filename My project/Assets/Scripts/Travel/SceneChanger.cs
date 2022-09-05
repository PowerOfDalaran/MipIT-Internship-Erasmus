using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
    {
        static bool notLoading = true;

        //Enumerator changing scenes
        public static IEnumerator MoveToScene(int nextSceneId, Vector3 cameraPosition, Animator transition)
        {
            //Checking if script wasn't called earlier - so that 2 scenes wouldn't start loading at the same time
            if(notLoading)
            {
                //Assigment of variables
                notLoading = false;
                Scene currentScene = SceneManager.GetActiveScene();
                int currentId = currentScene.buildIndex;

                //Start loading new scene and stop it from activating
                AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
                nextScene.allowSceneActivation = false;

                //Waiting for animation to cover the screen
                yield return new WaitForSeconds(2f);
                if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    while (nextScene.progress < 0.9f)
                    {
                        yield return null;
                    }

                    //Allowing the new scene to activate and waiting for it to finish loading
                    nextScene.allowSceneActivation = true;
                    Scene nextThisScene = SceneManager.GetSceneByBuildIndex(nextSceneId);

                    while (!nextScene.isDone)
                    {
                        yield return null;
                    }

                    //Activating new scene and unloading old one
                    SceneManager.SetActiveScene(nextThisScene);
                    SceneManager.UnloadScene(currentId);

                    //Setting position of the camera and allowing SceneChanger to work again
                    Camera.main.gameObject.transform.position = cameraPosition;  
                    notLoading = true;
                }
            }
        }
}
