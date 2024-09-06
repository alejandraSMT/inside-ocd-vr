using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackCollision : MonoBehaviour
{
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Interactable"))
        {
            var grabbed = collision.gameObject.GetComponent<OnGrab>().grabbed;
            Debug.Log("ESTÁ AGARRADO?" + grabbed);
            if(!grabbed)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Interactable"))
        {
            var grabbed = collision.gameObject.GetComponent<OnGrab>().grabbed;
            if (!grabbed)
            {
                collision.gameObject.SetActive(false);
            }
        }
    }
}
