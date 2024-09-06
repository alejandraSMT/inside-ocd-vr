using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGrab : MonoBehaviour
{
    public bool grabbed;
    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
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
