using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomGameManager : MonoBehaviour
{

    public static ClassroomGameManager Instance { get; private set; }
    public int countChecked = 0;
    public bool isChecked = false;

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
    
    public void updateCountChecked() {
        countChecked += 1;
        Debug.Log("COUNT CHECKED: " + countChecked);
    }
}
