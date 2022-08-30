using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
    {
        //Enumerator loading new scene, setting camera position and closing previous scene
        public static IEnumerator MoveToScene(int nextSceneId, Vector3 cameraPosition, Animator transition)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentId = currentScene.buildIndex;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(nextSceneId, LoadSceneMode.Additive);
            nextScene.allowSceneActivation = false;

            yield return new WaitForSeconds(2f);
            if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                while (nextScene.progress < 0.9f)
                {
                    Debug.Log("Loading...");
                    yield return null;
                }

                nextScene.allowSceneActivation = true;

                Scene nextThisScene = SceneManager.GetSceneByBuildIndex(nextSceneId);

                while (!nextScene.isDone)
                {
                    Debug.Log("Almost there...");
                    yield return null;
                }

                SceneManager.SetActiveScene(nextThisScene);

                SceneManager.UnloadScene(currentId);

                Camera.main.gameObject.transform.position = cameraPosition;
            }
        }
}
