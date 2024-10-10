using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendButton : MonoBehaviour
{

    public GameObject check;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(ClassroomGameManager.Instance.countChecked < 4)
        {
            ClassroomGameManager.Instance.isChecked = false;
            check.SetActive(false);

            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.gray;
            button.colors = colorBlock;
        }
        else
        {
            ColorBlock colorBlock = button.colors;
            colorBlock.normalColor = Color.black;
            button.colors = colorBlock;
            Debug.Log("PUEDE TERMIANR EL JUEGO UWU");
        }
    }
}
