using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{

    [SerializeField] private float amp;
    [SerializeField] private float freq;
    Vector3 initPos;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        initPos = canvas.transform.position;
    }

    private void Update()
    {
        canvas.transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, initPos.z);
    }
}
