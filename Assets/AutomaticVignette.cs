using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class AutomaticVignette : MonoBehaviour
{
    public Material vignetteMaterial;
    [HideInInspector]
    public float radius;
    private float smoothSpeed = 0.1f;
    private float targetRadius;
    private float decreaseAmount = 0.13f;
    private float duration = 1f; // Duraci�n de la transici�n
    private float pulseSpeed = 2f;  // Velocidad del pulso
    private float minRadiusLimit = 0.4f; // L�mite m�nimo del radius

    // Haptics
    public HapticImpulsePlayer leftHapticPlayer;
    public HapticImpulsePlayer rightHapticPlayer;
    private float hapticIntensity = 0f;
    private bool isHapticsActive = false;
    private float shakeFrequency = 0.05f; // Intervalo de tiempo entre sacudidas
    private Coroutine shakeCoroutine;

    // Virtual hand objects
    public Transform leftHandTransform;   // Asigna las manos virtuales en el Inspector
    public Transform rightHandTransform;
    private Vector3 leftHandOriginalPosition;
    private Vector3 rightHandOriginalPosition;
    private Quaternion leftHandOriginalRotation;
    private Quaternion rightHandOriginalRotation;

    [SerializeField]
    private bool m_HandsShake = true;

    // Intensity of visual shake
    private float shakeAmount = 0.02f;  // Ajustar para mayor o menor movimiento

    void Start()
    {
        if (vignetteMaterial != null)
        {
            radius = 0.8f;
        }

        // Guardar las posiciones y rotaciones originales de las manos virtuales
        if (m_HandsShake)
        {
            leftHandOriginalPosition = leftHandTransform.localPosition;
            rightHandOriginalPosition = rightHandTransform.localPosition;
            leftHandOriginalRotation = leftHandTransform.localRotation;
            rightHandOriginalRotation = rightHandTransform.localRotation;
        }

        // Comenzar los efectos autom�ticamente
        StartCoroutine(AutoStartEffects());
    }

    private IEnumerator AutoStartEffects()
    {
        while (true)
        {
            UpdateVignetteEffect(); // Ejecutar el efecto de vi�eta y shake
            yield return new WaitForSeconds(duration); // Esperar la duraci�n antes de reiniciar el ciclo
        }
    }

    void Update()
    {
        vignetteMaterial.SetFloat("_Radius", radius);

        // Oscilar el radius entre -0.1 y +0.1 del valor actual cada 4 segundos
        float oscillationTime = 4f; // Cada 4 segundos
        float minRadius = radius - 0.1f;
        float maxRadius = radius + 0.1f;

        // Aplicar una oscilaci�n suave entre el valor m�nimo y m�ximo usando Mathf.PingPong
        float oscillation = Mathf.PingPong(Time.time / oscillationTime, 1f); // Normalizado entre 0 y 1
        float pulsingRadius = Mathf.Lerp(minRadius, maxRadius, oscillation);

        // Aplicar el nuevo radius oscilante al material
        vignetteMaterial.SetFloat("_Radius", pulsingRadius);
    }

    public void ResetVignette()
    {
        radius = 0.8f;
        if (m_HandsShake)
        {
            StopHaptics();
            ResetHandPositions();
        }
    }

    public void UpdateVignetteEffect()
    {
        targetRadius = Mathf.Max(radius - decreaseAmount, minRadiusLimit); // Aplicar el l�mite de 0.4
        StartCoroutine(ChangeRadius(radius, targetRadius, duration));

        // Iniciar el efecto de shake h�ptico y visual
        if (m_HandsShake)
        {
            StartHapticsAndShake();
        }
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

    private void StartHapticsAndShake()
    {
        if (shakeCoroutine == null)
        {
            hapticIntensity = 0.2f; // Intensidad inicial
            isHapticsActive = true;
            shakeCoroutine = StartCoroutine(ShakeHapticsAndVisuals());
        }
    }

    private void StopHaptics()
    {
        isHapticsActive = false;

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }

        // Detener las sacudidas y restaurar posiciones originales
        ResetHandPositions();
        leftHapticPlayer.SendHapticImpulse(0f, 0.1f);
        rightHapticPlayer.SendHapticImpulse(0f, 0.1f);
    }

    private IEnumerator ShakeHapticsAndVisuals()
    {
        while (isHapticsActive)
        {
            // Aumentar la intensidad gradualmente
            hapticIntensity = Mathf.Clamp(hapticIntensity + Time.deltaTime * 0.05f, 0.2f, 1f);

            // Aplicar el "shake" como una serie de pulsos h�pticos r�pidos
            leftHapticPlayer.SendHapticImpulse(hapticIntensity, shakeFrequency);
            rightHapticPlayer.SendHapticImpulse(hapticIntensity, shakeFrequency);

            // Aplicar sacudida visual a las manos virtuales
            ShakeVirtualHands();

            // Esperar antes de aplicar el siguiente impulso
            yield return new WaitForSeconds(shakeFrequency);
        }
    }

    private void ShakeVirtualHands()
    {
        // Generar movimientos aleatorios para las manos dentro de un rango peque�o
        Vector3 leftShakeOffset = Random.insideUnitSphere * shakeAmount;
        Vector3 rightShakeOffset = Random.insideUnitSphere * shakeAmount;

        // Aplicar la sacudida a la posici�n de las manos virtuales
        leftHandTransform.localPosition = leftHandOriginalPosition + leftShakeOffset;
        rightHandTransform.localPosition = rightHandOriginalPosition + rightShakeOffset;

        // Tambi�n puedes aplicar rotaci�n para un efecto m�s dram�tico
        Quaternion leftShakeRotation = Quaternion.Euler(Random.insideUnitSphere * shakeAmount * 10f);
        Quaternion rightShakeRotation = Quaternion.Euler(Random.insideUnitSphere * shakeAmount * 10f);

        leftHandTransform.localRotation = leftHandOriginalRotation * leftShakeRotation;
        rightHandTransform.localRotation = rightHandOriginalRotation * rightShakeRotation;
    }

    private void ResetHandPositions()
    {
        // Restaurar la posici�n y rotaci�n original de las manos virtuales
        leftHandTransform.localPosition = leftHandOriginalPosition;
        rightHandTransform.localPosition = rightHandOriginalPosition;
        leftHandTransform.localRotation = leftHandOriginalRotation;
        rightHandTransform.localRotation = rightHandOriginalRotation;
    }
}
