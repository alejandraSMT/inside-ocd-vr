using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpackBackpack : MonoBehaviour
{

    
    //public GameObject tbd;
    // Start is called before the first frame update

     public void ClickOnBackpack()
    {
    
            GameManager.Instance.BackpackClick = true;
    
    }
}
