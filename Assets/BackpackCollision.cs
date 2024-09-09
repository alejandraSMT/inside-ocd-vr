using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackCollision : MonoBehaviour
{
    private GameObject[] alreadySaved = new GameObject[7];
    public GameObject[] interactables = new GameObject[7];

    private int orderPriority = 1;

    private void returnToPost()
    {
        foreach (var item in interactables)
        {
            item.transform.position = item.GetComponent<OnGrab>().currentPos;
            item.transform.rotation = item.GetComponent<OnGrab>().currentRotation;
            item.gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Interactable"))
        {
            var grabbed = collision.gameObject.GetComponent<OnGrab>().grabbed;
            var priority = collision.gameObject.GetComponent<OnGrab>().priority;
            if (!grabbed && (orderPriority == priority))
            {
                alreadySaved[orderPriority - 1] = collision.gameObject;
                orderPriority++;
                collision.gameObject.SetActive(false);
            }
            else
            {
                orderPriority = 1;
                /*collision.transform.position = collision.gameObject.GetComponent<OnGrab>().currentPos;
                collision.transform.rotation = collision.gameObject.GetComponent<OnGrab>().currentRotation;

                foreach (var item in alreadySaved)
                {
                    item.transform.position = item.GetComponent<OnGrab>().currentPos;
                    item.transform.rotation = item.GetComponent<OnGrab>().currentRotation;
                    item.gameObject.SetActive(true);
                }*/

                returnToPost();
                
                alreadySaved = new GameObject[7];
            }
        }
    }
}
