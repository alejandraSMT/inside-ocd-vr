using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaterSink : MonoBehaviour
{
    public GameObject currentWaterTube;   // El tubo de agua
    public GameObject WaterSphere;        // Esfera de agua u otro elemento relevante

    bool comenzo = false;         // Control de estado del agua

    // Inicializa el estado de los objetos
    public void Awake()
    {
        currentWaterTube.SetActive(false); // Comienza con el tubo de agua desactivado
        WaterSphere.SetActive(false);     // Comienza con la esfera de agua desactivada
    }

    public void Update()
    {
    
    }

    // Esta función se llama cuando se recibe un input para alternar el agua
    public void StartWater()
    {
        if (!comenzo)
        {
            
            currentWaterTube.SetActive(true);
            WaterSphere.SetActive(true);

            Debug.Log("El flujo de agua ha comenzado");
            comenzo = true;
            
        }else{
            currentWaterTube.SetActive(false);
            WaterSphere.SetActive(false);

            Debug.Log("El flujo de agua ha acabado");
            Debug.Log("El flujo de agua ha acabado");
            comenzo = false;
        }


        // Cambiar el estado de la variable
    }

    // Corrutina opcional para esperar un tiempo antes de desactivar
    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
}
