using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControllerScript : MonoBehaviour
{

    public Transform mouseCenter;
    public float maxXDistance = 0.3f;
    public float maxZDistance = 0.3f;

    private void OnTriggerStay(Collider other) {

        if (other.gameObject.CompareTag("Hand")){
            Vector3 newPosition = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);

            newPosition.x = Mathf.Clamp(newPosition.x, mouseCenter.position.x - maxXDistance, mouseCenter.position.x + maxXDistance);
            newPosition.z = Mathf.Clamp(newPosition.z, mouseCenter.position.z - maxZDistance, mouseCenter.position.z + maxZDistance);

            transform.position = newPosition;
        }
    }
}
