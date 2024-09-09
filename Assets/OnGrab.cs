using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrab : MonoBehaviour
{
    public bool grabbed;
    public int priority;
    public Vector3 currentPos;
    public Quaternion currentRotation;
    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        currentPos = transform.position;
        currentRotation = transform.rotation;
    }

    public void whenGrabActivated()
    {
        grabbed = true;
    }

    public void whenGrabDeactivated()
    {
        grabbed = false;
    }
}
