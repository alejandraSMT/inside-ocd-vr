using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BusManager : MonoBehaviour
{
    private bool nextScene = false;
    [SerializeField]
    private AutomaticVignette vignette;
    void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
        StartCoroutine(StartTimer()); // Inicia el temporizador al comienzo
    }

    private void Update()
    {
        Debug.Log("RADIUS EN UPDATE" + vignette.radius);
        /*while  (vignette.radius <= 0.4f)
        {
            vignette.UpdateVignetteEffect();
        }*/
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(40); // Espera 40 segundos
        nextScene = true; // Activa la escena
    }

    // Update is called once per frame
    IEnumerator LoadGameSceneAsync()
    {
        while (!nextScene)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BathroomScene");
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
