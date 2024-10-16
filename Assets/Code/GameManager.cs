using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string NextScene;
    [HideInInspector]
    public bool nextScene = false;
    public static GameManager Instance { get; private set; }
    public bool WaterActivate = false;
    public bool SoapActivate = false;

    public bool RightHandFinished = false;
    public bool LeftHandFinished = false;

    public bool BackpackClick = false;
    public int ItemBackpack = 1;
    public GameObject pencil;
    public GameObject bottle;
    public GameObject paper;
    public GameObject ticket;
    public GameObject coffee;
    public GameObject chocolate;
    public int washCount = 0;
    public int maxWashes = 5; 

    public GameObject rightHand;
    public GameObject leftHand;

    public bool gameFinish;

    public VignetteController vignette;

    private void Start()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

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
        gameFinish = false;
    }

    private void Update(){
        if(WaterActivate == true && SoapActivate == true && !gameFinish){
            Debug.Log("Ya se puede lavar las manos");
        }else{
            Debug.Log("Aun NO se puede lavar las manos");
        }
        
    }

    public void ResetCleaning()
    {
        if (vignette != null) { 
            vignette.UpdateVignetteEffect();
        }
        Material[] rightHandList = rightHand.GetComponent<BacterieActivity>().bacterieMaterials;

        foreach (var material in rightHandList)
            {
                Color color = material.color;
                color.a = 1;
                material.color = color;
            }
        
        Material[] leftHandList = leftHand.GetComponent<BacterieActivity>().bacterieMaterials;

        foreach (var material in leftHandList)
            {
                Color color = material.color;
                color.a = 1;
                material.color = color;
            }

        RightHandFinished = false;
        LeftHandFinished = false;

    }

    public IEnumerator VerifyCleanedHands()
    {
        yield return new WaitForSeconds(1);
        if(RightHandFinished == true && LeftHandFinished == true){
                Debug.Log("Lavado #"+ washCount+ " terminado. " + "Empiece el lavado #"+ (washCount+1));
                ResetCleaning();
                washCount++;
            }
    }

    IEnumerator LoadGameSceneAsync()
    {
        while (!nextScene)
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