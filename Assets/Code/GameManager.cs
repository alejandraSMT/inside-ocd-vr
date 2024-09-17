using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    public bool WaterActivate = false;
    public bool SoapActivate = false;

    public bool RightHandFinished = false;
    public bool LeftHandFinished = false;

    public int washCount = 0;
    public int maxWashes = 5; 

    public GameObject rightHand;
    public GameObject leftHand;
   
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

    private void Update(){
        if(WaterActivate == true && SoapActivate == true){

        
        Debug.Log("Ya se puede lavar las manos");
        }else{
            Debug.Log("Aun NO se puede lavar las manos");
        }
        
        
    }

    public void ResetCleaning()
    {
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

    public void VerifyCleanedHands()
    {
        if(RightHandFinished == true && LeftHandFinished == true){
                Debug.Log("Lavado #"+ washCount+ " terminado. " + "Empiece el lavado #"+ (washCount+1));
                /*GameManager.Instance.RightHandFinished = false;
                GameManager.Instance.LeftHandFinished = false;*/
                //bubbles.GetComponent<ParticleSystem>().Stop();
                ResetCleaning();
                washCount++;
            }
    }
    
}