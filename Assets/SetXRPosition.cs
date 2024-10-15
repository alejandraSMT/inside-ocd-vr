using UnityEngine;
using Unity.XR.CoreUtils;
using UnityEngine.XR;
using System.Collections;

public class SetXRPosition : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 0, 0);  // Posición deseada
    public Vector3 targetRotation = new Vector3(0, 0, 0);  // Rotación deseada

    [SerializeField]
    private XROrigin xrOrigin;
    private Transform mainCameraTransform;

    void Start()
    {
        // Encuentra el XR Origin
        xrOrigin = GetComponent<XROrigin>();

        if (xrOrigin != null)
        {
            // Establece la posición y rotación del XR Origin
            xrOrigin.transform.position = targetPosition;
            xrOrigin.transform.eulerAngles = targetRotation;

            // Encuentra la cámara principal del XR Origin
            mainCameraTransform = xrOrigin.Camera.transform;

            if (mainCameraTransform != null)
            {
                // Forzar la posición de la cámara en el mismo lugar que el XR Origin
                mainCameraTransform.position = xrOrigin.transform.position;

                // Bloquear la posición de la cámara y permitir solo rotación horizontal
                StartCoroutine(LockCameraPosition());
            }
        }
        else
        {
            Debug.LogError("XROrigin no encontrado en el objeto.");
        }
    }

    IEnumerator LockCameraPosition()
    {
        // Asegura que la posición de la cámara permanezca en el punto inicial
        while (true)
        {
            // Bloquea la posición, solo se permite rotar sobre el eje Y (horizontalmente)
            mainCameraTransform.position = xrOrigin.transform.position;

            // Limita la rotación de la cámara solo en el eje Y
            Vector3 currentRotation = mainCameraTransform.eulerAngles;
            mainCameraTransform.eulerAngles = new Vector3(0, currentRotation.y, 0);

            yield return null; // Se ejecuta en cada frame
        }
    }
}
