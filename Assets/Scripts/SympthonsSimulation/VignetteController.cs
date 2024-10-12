using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteController : MonoBehaviour
{
    public Material vignetteMaterial;
    [HideInInspector] private float radius;
    [HideInInspector] private float smoothness;

    [HideInInspector] private float smoothSpeed = 0.1f;

    private float targetRadius;
    private float decreaseAmount = 0.13f;
    private float duration = 1f; // Duración de la transición
    private float pulseSpeed = 2f;  // Velocidad del pulso

    void Start()
    {
        if (vignetteMaterial != null)
        {
            radius = 0.8f;
            smoothness = vignetteMaterial.GetFloat("_Smoothness");
        }
    }

    void Update()
    {
        vignetteMaterial.SetFloat("_Radius", radius);


        float minRadius = radius - 0.1f;
        float maxRadius = radius - 0.05f;
        float pulsingRadius = Mathf.Lerp(minRadius, maxRadius, Mathf.PingPong(Time.time * pulseSpeed, 1f));
        vignetteMaterial.SetFloat("_Radius", pulsingRadius);
    }

    public void ResetVignette()
    {
        radius = 0.8f;
    }

    public void UpdateVignetteEffect()
    {
        targetRadius = radius - decreaseAmount;
        StartCoroutine(ChangeRadius(radius, targetRadius, duration));
    }

    private IEnumerator ChangeRadius(float start, float end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            radius = Mathf.Lerp(start, end, elapsedTime / duration);
            vignetteMaterial.SetFloat("_Radius", radius);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        radius = end;
    }
}
