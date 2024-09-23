using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorBathroom : MonoBehaviour
{
    [SerializeField] private float amp;
    [SerializeField] private float freq;
    Vector3 initPos;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject indicatorUI;
    [SerializeField] private GameObject sinkIndicatorUI;
    [SerializeField] private GameObject soapDispenserIndicatorUI;

    private void Start()
    {
        initPos = canvas.transform.position;
    }

    private void Update()
    {
        canvas.transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Character"))
        {
            indicatorUI.SetActive(false);
            sinkIndicatorUI.SetActive(true);
            soapDispenserIndicatorUI.SetActive(true);
        }
    }
}
