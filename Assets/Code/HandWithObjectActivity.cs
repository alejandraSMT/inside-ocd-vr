using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWithObjectActivity : MonoBehaviour
{
    public static HandWithObjectActivity Instance { get; private set; }
    public GameObject pencil;
    public GameObject bottle;
    public GameObject paper;
    public GameObject ticket;
    public GameObject coffee;
    // Start is called before the first frame update
    private void Awake()
    {
        pencil.SetActive(false);
        bottle.SetActive(false);
        paper.SetActive(false);
        ticket.SetActive(false);
        coffee.SetActive(false);
    }

    // Update is called once per frame
    public void Update(){
        if(GameManager.Instance.BackpackClick == true && GameManager.Instance.ItemBackpack == 1){
            pencil.SetActive(true);
        }else if(GameManager.Instance.BackpackClick == true && GameManager.Instance.ItemBackpack == 2){
            bottle.SetActive(true);
        }else if(GameManager.Instance.BackpackClick == true && GameManager.Instance.ItemBackpack == 3){
            paper.SetActive(true);
        }else if(GameManager.Instance.BackpackClick == true && GameManager.Instance.ItemBackpack == 4){
            ticket.SetActive(true);
        }else if(GameManager.Instance.BackpackClick == true && GameManager.Instance.ItemBackpack == 5){
            coffee.SetActive(true);
        }else{

        }
    }
}
