using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.PlayerLoop.PreUpdate;

public class ClassroomGameManager : MonoBehaviour
{

    public static ClassroomGameManager Instance { get; private set; }
    public int countChecked = 0;
    public bool isChecked = false;

    [SerializeField]
    private string NextScene;

    [HideInInspector] public bool canUpdate;
    [HideInInspector] public bool canFinish = false;
    [HideInInspector] public bool finished = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    public void updateCountChecked() {
        countChecked += 1;
        canUpdate = true;
        Debug.Log("COUNT CHECKED: " + countChecked);

        if (countChecked == 4) {
            canFinish = true;
            Debug.Log("FINISH GAME");
        }
    }

    IEnumerator LoadGameSceneAsync()
    {
        while (!finished)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextScene);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(10);
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
