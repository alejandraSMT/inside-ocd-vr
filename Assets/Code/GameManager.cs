using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    

    
}