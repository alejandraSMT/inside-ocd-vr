using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceToPutActivity : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject placeObject;
    public GameObject ObjectToPut;
   
    
    public void Awake(){
        ObjectToPut.SetActive(false);
        
    }
    
    public void ActivityInPlace()
    {
       
        if(GameManager.Instance.ItemBackpack == 1 && GameManager.Instance.BackpackClick == true){
            //Lapiz y estante 
           GameManager.Instance.ItemBackpack = 2;
            GameManager.Instance.pencil.SetActive(false);
            ObjectToPut.SetActive(true);
            GameManager.Instance.BackpackClick = false;
        }else if(GameManager.Instance.ItemBackpack == 2 && GameManager.Instance.BackpackClick == true){
            //gasesosa y baul
            GameManager.Instance.ItemBackpack = 3;
            GameManager.Instance.bottle.SetActive(false);
            GameManager.Instance.BackpackClick = false;
             ObjectToPut.SetActive(true);
        }else if(GameManager.Instance.ItemBackpack == 3 && GameManager.Instance.BackpackClick == true){
            //papel y caja
            GameManager.Instance.ItemBackpack = 4;
            GameManager.Instance.paper.SetActive(false);
            GameManager.Instance.BackpackClick = false;
             ObjectToPut.SetActive(true);
        }else if(GameManager.Instance.ItemBackpack == 4 && GameManager.Instance.BackpackClick == true){
            //ticket y cajon
            GameManager.Instance.ItemBackpack = 5;
            GameManager.Instance.ticket.SetActive(false);
            GameManager.Instance.BackpackClick = false;
             ObjectToPut.SetActive(true);
        }else if(GameManager.Instance.ItemBackpack == 5 && GameManager.Instance.BackpackClick == true){
            //cafe y estante
            GameManager.Instance.ItemBackpack = 6;
            GameManager.Instance.coffee.SetActive(false);
            GameManager.Instance.BackpackClick = false;
            ObjectToPut.SetActive(true);
        }else if(GameManager.Instance.ItemBackpack == 6 && GameManager.Instance.BackpackClick == true){
            //cafe y estante
             GameManager.Instance.chocolate.SetActive(false);
             GameManager.Instance.BackpackClick = false;
             ObjectToPut.SetActive(true);
        }
    }

}
