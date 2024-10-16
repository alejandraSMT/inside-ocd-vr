using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class ComputerScript : MonoBehaviour
{

    public GameObject mouse;
    public float sensitivity;
    public GameObject check;

    public Button sendButton;

    private bool triggerPulled = false;
    public InputActionReference triggerInputActionReference;

    private bool alreadyClicked = false;

    [SerializeField]
    private GameObject inExamScene;
    [SerializeField]
    private GameObject finishScreen;

    [SerializeField]
    private TimerController contoller;

    [SerializeField]
    private AudioSource src;

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
        src.Play();
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
        }
        else
        {
            ClassroomGameManager.Instance.canFinish = true;
        }
    }

    public void SendExam()
    {
        src.Play();
        if (ClassroomGameManager.Instance.canFinish && !ClassroomGameManager.Instance.finished)
        {
            alreadyClicked = true;
            ClassroomGameManager.Instance.finished = true;
            contoller.GetComponent<TimerController>().StopTimer();
            inExamScene.SetActive(false);
            finishScreen.SetActive(true);
        }
    }
}
