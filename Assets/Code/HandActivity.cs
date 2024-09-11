using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandActivity : MonoBehaviour
{
    public GameObject bacterie1;
    public GameObject bacterie2;
    public GameObject bacterie3;
    public GameObject bacterie4;
    public GameObject bacterie5;
    public GameObject bubbles;

    private bool cleaningInProgress = false;  // Para controlar si ya se está limpiando
    private float fadeDuration = 1f;          // Duración de cada paso de desvanecimiento
    private int washCount = 0;                // Contador de lavados
    private int maxWashes = 5;                // Número máximo de lavados

    public void Awake()
    {
        // Asegúrate de que todas las bacterias estén completamente visibles
        ResetBacterias();
        bubbles.SetActive(false);  // Comienza con las burbujas desactivadas
    }

    // Método que se llama cuando se empieza a limpiar las manos
    public void CleanHands()
    {
        if (!cleaningInProgress && washCount < maxWashes)  // Si no estamos limpiando y no hemos alcanzado el límite
        {
            cleaningInProgress = true;
            Debug.Log($"Lavado de manos iniciado... ({washCount + 1}/{maxWashes})");
            bubbles.SetActive(true);
            bubbles.GetComponent<ParticleSystem>().Play();
            StartCoroutine(FadeBacteriasOverTime());  // Inicia la corrutina para desvanecer bacterias gradualmente
        }
        else if (washCount >= maxWashes)
        {
            Debug.Log("Se completaron los 5 lavados. ¡Manos completamente limpias!");
        }
    }

    // Corrutina para desvanecer bacterias poco a poco
    private IEnumerator FadeBacteriasOverTime()
    {
        yield return StartCoroutine(FadeBacteria(bacterie1));  // Desvanece bacterie1
        yield return StartCoroutine(FadeBacteria(bacterie2));  // Desvanece bacterie2
        yield return StartCoroutine(FadeBacteria(bacterie3));  // Desvanece bacterie3
        yield return StartCoroutine(FadeBacteria(bacterie4));  // Desvanece bacterie4
        yield return StartCoroutine(FadeBacteria(bacterie5));  // Desvanece bacterie5

        // Una vez eliminadas todas las bacterias, detén las burbujas
        bubbles.SetActive(false);
        cleaningInProgress = false;  // Reinicia el estado de limpieza

        // Incrementa el contador de lavados
        washCount++;

        if (washCount < maxWashes)
        {
            ResetBacterias();  // Reinicia las bacterias para el siguiente intento
            Debug.Log("Listo para el siguiente lavado.");
        }
        else
        {
            Debug.Log("Se completaron los 5 lavados. ¡Manos completamente limpias!");
        }
    }

    // Corrutina para desvanecer una bacteria poco a poco
    private IEnumerator FadeBacteria(GameObject bacteria)
    {
        Renderer renderer = bacteria.GetComponent<Renderer>();  // Obtiene el renderer del objeto
        Color color = renderer.material.color;  // Obtiene el color actual
        float alpha = color.a;  // Obtiene la opacidad actual (alfa)

        while (alpha > 0)  // Mientras la opacidad sea mayor que 0
        {
            alpha -= Time.deltaTime / fadeDuration;  // Reduce la opacidad con el tiempo
            alpha = Mathf.Clamp(alpha, 0f, 1f);  // Asegúrate de que la opacidad no baje de 0
            SetBacteriaAlpha(bacteria, alpha);  // Aplica el nuevo valor de opacidad

            yield return null;  // Espera un frame antes de continuar
        }
    }

    // Método para establecer la opacidad (alfa) de una bacteria
    private void SetBacteriaAlpha(GameObject bacteria, float alpha)
    {
        Renderer renderer = bacteria.GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = alpha;  // Cambia el valor del canal alfa (opacidad)
        renderer.material.color = color;  // Aplica el nuevo color
    }

    // Reinicia la opacidad de todas las bacterias para el siguiente intento
    private void ResetBacterias()
    {
        SetBacteriaAlpha(bacterie1, 1f);
        SetBacteriaAlpha(bacterie2, 1f);
        SetBacteriaAlpha(bacterie3, 1f);
        SetBacteriaAlpha(bacterie4, 1f);
        SetBacteriaAlpha(bacterie5, 1f);
    }
}
