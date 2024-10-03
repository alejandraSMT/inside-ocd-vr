using UnityEngine;

public class ComputerScript : MonoBehaviour
{

    public GameObject mouse;
    public float sensitivity;


    void Update()
    {
        transform.localPosition = new Vector3(mouse.transform.localPosition.x * sensitivity, -mouse.transform.localPosition.z * sensitivity, transform.localPosition.z);   
    }
}
