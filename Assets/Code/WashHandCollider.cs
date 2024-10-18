using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashHandCollider : MonoBehaviour
{
    public GameObject hand;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bacteries"))
        {
            hand.GetComponent<BacterieActivity>().CleanHands();
        }
    }
}
