using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool nextScene = false;
    void Start()
    { 
        StartCoroutine(LoadGameSceneAsync());
    }

    public void onClickButton()
    {
        nextScene = true;
    }

    IEnumerator LoadGameSceneAsync()
    {
        while (!nextScene)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BedroomScene1");
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
