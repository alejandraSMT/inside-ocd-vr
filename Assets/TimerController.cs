using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    // Variables del temporizador
    public float countdownTime = 15f; // Comienza en 10 segundos
    private float elapsedTimeAfterZero = 0f; // Tiempo transcurrido despu�s de llegar a 0
    private bool countdownReachedZero = false; // Verifica si el tiempo lleg� a 0
    private bool isTimerRunning = true; // Verifica si el temporizador est� activo

    // TextMesh para mostrar los tiempos
    public TextMeshProUGUI countdownTextMesh; // TextMesh para countdownTime
    public TextMeshProUGUI elapsedTextMesh; // TextMesh para elapsedTimeAfterZero

    void Update()
    {
        if (isTimerRunning)
        {
            if (countdownTime > 0)
            {
                // Disminuir el temporizador hasta llegar a 0
                countdownTime -= Time.deltaTime;
                if (countdownTime <= 0)
                {
                    countdownTime = 0;
                    countdownReachedZero = true;
                }
            }
            else if (countdownReachedZero)
            {
                // Contar el tiempo desde que lleg� a 0
                elapsedTimeAfterZero += Time.deltaTime;
            }
        }

        // Actualizar el texto del TextMesh
        UpdateTextMeshes();
    }

    // Funci�n para detener el temporizador desde otro lugar
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Tiempo total transcurrido despu�s de llegar a 0: " + elapsedTimeAfterZero + " segundos.");
    }

    // Funci�n para reiniciar el temporizador, si la necesitas
    public void ResetTimer()
    {
        countdownTime = 15f;
        elapsedTimeAfterZero = 0f;
        countdownReachedZero = false;
        isTimerRunning = true;
    }

    // Funci�n para actualizar los textos en los TextMesh
    private void UpdateTextMeshes()
    {
        if (countdownTextMesh != null)
        {
            if (countdownTime > 0) {
                var minutes = Mathf.FloorToInt(countdownTime / 60);
                var seconds = Mathf.FloorToInt(countdownTime - minutes * 60);

                string gameTimeClockDisplay = string.Format("{0:0}:{1:00}", minutes, seconds);
                countdownTextMesh.text = gameTimeClockDisplay;
            }
            else
            {
                countdownTextMesh.text = "Atrasado";
            }
        }

        if (elapsedTextMesh != null && countdownReachedZero)
        {
            var minutes = Mathf.FloorToInt(elapsedTimeAfterZero / 60);
            var seconds = Mathf.FloorToInt(elapsedTimeAfterZero - minutes * 60);

            string gameTimeClockDisplay = string.Format("{0:0}:{1:00}", minutes, seconds);
            elapsedTextMesh.text = gameTimeClockDisplay;
        }
    }
}
