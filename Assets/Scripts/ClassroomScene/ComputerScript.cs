using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ComputerScript : MonoBehaviour
{

    public GameObject mouse;
    public float sensitivity;
    public GameObject check;

    public Button sendButton;

    private bool triggerPulled = false;
    public InputActionReference triggerInputActionReference;

    void Update()
    {
        transform.localPosition = new Vector3(mouse.transform.localPosition.x * sensitivity, -mouse.transform.localPosition.z * sensitivity, transform.localPosition.z);   
    }

    private void OnTriggerStay(Collider other)
    {
        if (triggerInputActionReference.action.ReadValue<float>() > 0.5f)
        {
            if (!triggerPulled)
            {
                triggerPulled = true;
                if(other.GetComponent<Button>() != null)
                {
                    other.GetComponent<Button>().onClick.Invoke();
                }
            }
        }
        else
        {
            triggerPulled = false;
        }
    }

    public void CheckboxClicked()
    {
        if (ClassroomGameManager.Instance.countChecked < 4)
        {
            if(!ClassroomGameManager.Instance.isChecked)
            {
                ClassroomGameManager.Instance.isChecked = true;
                check.SetActive(true);
                ClassroomGameManager.Instance.updateCountChecked();

                ColorBlock colorBlock = sendButton.colors;
                colorBlock.normalColor = Color.black;
                sendButton.colors = colorBlock;
            }
            /*if (ClassroomGameManager.Instance.isChecked)
            {
                ClassroomGameManager.Instance.isChecked = false;
                check.SetActive(false);
            }
            else
            {
                ClassroomGameManager.Instance.isChecked = true;
                check.SetActive(true);
                ClassroomGameManager.Instance.updateCountChecked();
            }*/
        }
        else
        {
            Debug.Log("TERMINÓ EL JUEGO");
        }
    }

    public void SendExam()
    {
        Debug.Log("CLICK al final");
    }
}
