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
    private bool finishGame = false;
    public bool cleaningInProgress = false;  // Para controlar si ya se está limpiando
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

    private void Update() {
        if(GameManager.Instance.washCount == GameManager.Instance.maxWashes && !finishGame){
            foreach (var bacterie in new GameObject[] { bacterie1, bacterie2, bacterie3, bacterie4, bacterie5, bacterie6 })
            {
                bacterie.SetActive(false);
                
            }
            bubbles.GetComponent<ParticleSystem>().Clear();
            bubbles.SetActive(false);
            Debug.Log($"Ahora estas completamente limpio :)");
            finishGame = true;
            GameManager.Instance.gameFinish = true;
            GameManager.Instance.nextScene = true;
        }
    }

    // Método que se llama cuando se empieza a limpiar las manos
    public void CleanHands()
    {
        if (!cleaningInProgress && GameManager.Instance.WaterActivate == true && GameManager.Instance.SoapActivate == true)  // Si no estamos limpiando y no hemos alcanzado el límite
        {
            if(GameManager.Instance.washCount < GameManager.Instance.maxWashes){
                bubbles.SetActive(true);
                bubbles.GetComponent<ParticleSystem>().Play();
                FadeOutBacteria();
            }
            StartCoroutine(GameManager.Instance.VerifyCleanedHands());
        }
    }

    public void FadeOutBacteria()
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
                   
                    alpha255 -= 0.20f * 255; // Ajusta el decremento en el rango de 0 a 255

                    // Conversión de alpha de 0-255 a 0-1
                    color.a = alpha255 / 255;
                    material.color = color;

                    // Log el valor del alpha después de la reducción
                    Debug.Log($"Después de reducir: {alpha255}");
                }
            if(color.a == 0){
                    if(hand.name == "RightHand"){
                        GameManager.Instance.RightHandFinished = true;
                        //bubbles.GetComponent<ParticleSystem>().Stop();
                        Debug.Log($"Lavado terminado en Mano Derecha");
                    }
                    else if(hand.name == "LeftHand"){
                        GameManager.Instance.LeftHandFinished = true;
                        //bubbles.GetComponent<ParticleSystem>().Stop();
                        Debug.Log($"Lavado terminado en Mano Izquierda");
                    }
                }
            
        }
    }
    
}
