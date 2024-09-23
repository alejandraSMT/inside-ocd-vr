using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SoapDispenser : MonoBehaviour
{

    public GameObject currentBubbles;
    [SerializeField] private GameObject indicatorUI;
    
    // Prefab de burbujas
    //crear un bool  si comenzo
    bool comenzo = false;
    float timmer = 2f;
    //llamar update
    public void Awake(){
        //currentBubbles.GetComponent<ParticleSystem>().Pause();
        currentBubbles.SetActive(false);
    }

    public void Update(){
        if(comenzo){
            if(timmer > 0){
                timmer -= Time.deltaTime;
            }else{
                timmer = 2f;
                currentBubbles.GetComponent<ParticleSystem>().Clear();
                currentBubbles.GetComponent<ParticleSystem>().Pause();
                comenzo = false;
            }
        }
    }
    // Asigna esta función a una acción de Input
    public void StartBubble()
    {
        if(!comenzo){
            //intelicence autocompletat unity
            // Instanciar las burbujas en la posición del dispensador

            GameObject aux =  currentBubbles;
            currentBubbles.SetActive(true);
            aux.GetComponent<ParticleSystem>().Play();
            Debug.Log("Se agarro jabon");
            GameManager.Instance.SoapActivate = true;  
            if(indicatorUI != null)
            {
                Destroy(indicatorUI);
            }
            //StartCoroutine(Wait());
            //aux.GetComponent<ParticleSystem>().Pause();  
            //Debug.Log("se puso Pause");   
            comenzo=true;
        }
       
            
    }

    private IEnumerator Wait(){
        yield return new WaitForSecondsRealtime(1f);
    }
}
