using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreUpdate;

public class ClassroomGameManager : MonoBehaviour
{

    public static ClassroomGameManager Instance { get; private set; }
    public int countChecked = 0;
    public bool isChecked = false;

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
    
    public void updateCountChecked() {
        countChecked += 1;
        canUpdate = true;
        Debug.Log("COUNT CHECKED: " + countChecked);

        if (countChecked == 4) {
            canFinish = true;
            Debug.Log("FINISH GAME");
        }
    }
}
