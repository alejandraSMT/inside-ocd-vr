using System.Collections;
using UnityEngine;

public class BacterieActivity : MonoBehaviour
{
    public GameObject bacterie1;
    public GameObject bacterie2;
    public GameObject bacterie3;
    public GameObject bacterie4;
    public GameObject bacterie5;
    public GameObject bacterie6;

    public GameObject bubbles;
    public GameObject hand;
    public float fadeSpeed = 0.5f;
    public bool cleaningInProgress = false;  // Para controlar si ya se está limpiando
    public int washCount = 0;                // Contador de lavados
    public int maxWashes = 5;                // Número máximo de lavados
    public Material[] bacterieMaterials;     // Materiales para las bacterias

    public void Awake()
    {
        Debug.Log($"Bienvenido al juego de lavado de manos....");
        bubbles.SetActive(false);
        // Inicializa los materiales de las bacterias
        bacterieMaterials = new Material[]
        {
            bacterie1.GetComponent<MeshRenderer>().material,
            bacterie2.GetComponent<MeshRenderer>().material,
            bacterie3.GetComponent<MeshRenderer>().material,
            bacterie4.GetComponent<MeshRenderer>().material,
            bacterie5.GetComponent<MeshRenderer>().material,
            bacterie6.GetComponent<MeshRenderer>().material
        };

        // Asegúrate de que todas las bacterias estén completamente visibles
        foreach (var bacterie in new GameObject[] { bacterie1, bacterie2, bacterie3, bacterie4, bacterie5, bacterie6 })
        {
            bacterie.SetActive(true);
        }
    }

    // Método que se llama cuando se empieza a limpiar las manos
    public void Update(){
        if(GameManager.Instance.RightHandFinished == true && GameManager.Instance.LeftHandFinished == true){
                washCount++;
                if(hand.name == "RightHand"){
                        GameManager.Instance.RightHandFinished = false;
                }else if(hand.name == "LeftHand"){
                        GameManager.Instance.LeftHandFinished = false;
                }
                StartCoroutine(ResetCleaning());
        }

        if(washCount>0 && washCount < maxWashes){
            Debug.Log("Lavado #"+ washCount+ " terminado." + "Empiece el lavado #"+ (washCount+1));
        }
        else if(washCount == maxWashes){
            foreach (var bacterie in new GameObject[] { bacterie1, bacterie2, bacterie3, bacterie4, bacterie5, bacterie6 })
            {
                bacterie.SetActive(false);
                
            }
            bubbles.SetActive(false);
            bubbles.GetComponent<ParticleSystem>().Clear();
            Debug.Log($"Ahora estas completamente limpio :)");
        }
    }
    public void CleanHands()
    {
        if (!cleaningInProgress && GameManager.Instance.WaterActivate == true && GameManager.Instance.SoapActivate == true)  // Si no estamos limpiando y no hemos alcanzado el límite
        {
           
            if(washCount < maxWashes){
                //Debug.Log("Lavado #"+ washCount+ " terminado." + "Empiece el lavado #"+ (washCount+1));
                StartCoroutine(FadeOutBacteria());
                bubbles.SetActive(true);
                bubbles.GetComponent<ParticleSystem>().Play();
            }
            
           
        }
    }

    public IEnumerator FadeOutBacteria()
    {
        // Reduce el valor del Alpha en un 10% para cada material
        foreach (var material in bacterieMaterials)
        {
            Color color = material.color;
            if (color.a > 0)
                {
                    // Conversión de alpha de 0-1 a 0-255
                    float alpha255 = color.a * 255;
                    Debug.Log($"Antes de reducir: {alpha255}");

                   
                    alpha255 -= 0.10f * 255; // Ajusta el decremento en el rango de 0 a 255
                    

                    // Conversión de alpha de 0-255 a 0-1
                    color.a = alpha255 / 255;
                    material.color = color;

                    // Log el valor del alpha después de la reducción
                    Debug.Log($"Después de reducir: {alpha255}");
                }
            if(color.a == 0){
                    if(hand.name == "RightHand"){
                        GameManager.Instance.RightHandFinished = true;
                        Debug.Log($"Lavado terminado en Mano Derecha");
                    }
                    else if(hand.name == "LeftHand"){
                        GameManager.Instance.LeftHandFinished = true;
                        Debug.Log($"Lavado terminado en Mano Izquierda");
                    }
                }
            
        }

        // Espera un segundo antes de volver a activar las bacterias
        yield return new WaitForSecondsRealtime(1f);
    }

    public IEnumerator ResetCleaning()
    {
        // Espera un segundo antes de volver a activar las bacterias
        yield return new WaitForSecondsRealtime(1f);

        
            // Reactiva todas las bacterias
            foreach (var material in bacterieMaterials)
            {
                Color color = material.color;
                color.a = 1;
                material.color = color;
                    
                   

                   
            }
            
        }

     
    
}
