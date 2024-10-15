using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackpackCollision : MonoBehaviour
{
    private GameObject[] alreadySaved = new GameObject[7];
    public GameObject[] interactables = new GameObject[7];
    [HideInInspector] public bool nextScene = false;

    private int orderPriority = 1;

    private void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    private void returnToPost()
    {
        foreach (var item in interactables)
        {
            item.transform.position = item.GetComponent<OnGrab>().currentPos;
            item.transform.rotation = item.GetComponent<OnGrab>().currentRotation;
            item.gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Interactable"))
        {
            var grabbed = collision.gameObject.GetComponent<OnGrab>().grabbed;
            var priority = collision.gameObject.GetComponent<OnGrab>().priority;
            if (!grabbed)
            {
                if(orderPriority == priority){
                    alreadySaved[orderPriority - 1] = collision.gameObject;
                    orderPriority++;
                    collision.gameObject.SetActive(false);
                }else{
                    orderPriority = 1;
                    returnToPost();
                    alreadySaved = new GameObject[7];
                }
            }
        }
    }

    private void Update() {
        if (orderPriority > 7)
        {
            nextScene = true;
        }
    }

    IEnumerator LoadGameSceneAsync()
    {
        while (!nextScene)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Bus");
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
