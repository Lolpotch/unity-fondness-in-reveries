using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPhoto : MonoBehaviour
{
    public MechanicName mechanicName;
    public RectTransform disableMechanicButton;

    [SerializeField] bool isButtonAppeared = false;
    // Start is called before the first frame update
    void Start()
    {
        isButtonAppeared = false;
        disableMechanicButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isButtonAppeared)
        {
            switch (mechanicName)
            {
                case MechanicName.interactPhoto1:
                    if (DialogueTrigger.Instance.isPlayInteractPhoto1_8Played)
                    {
                        disableMechanicButton.gameObject.SetActive(true);
                        isButtonAppeared = true;
                    }
                    break;

                case MechanicName.interactPhoto2:
                    if (DialogueTrigger.Instance.isPlayInteractPhoto2_9Played)
                    {
                        disableMechanicButton.gameObject.SetActive(true);
                        isButtonAppeared = true;
                    }
                    break;
                
                case MechanicName.interactPhoto3:
                    if (DialogueTrigger.Instance.isPlayInteractPhoto3_10Played)
                    {
                        disableMechanicButton.gameObject.SetActive(true);
                        isButtonAppeared = true;
                    }
                    break;

                case MechanicName.interactPhoto4:
                    if (DialogueTrigger.Instance.isPlayInteractPhoto4_11Played)
                    {
                        disableMechanicButton.gameObject.SetActive(true);
                        isButtonAppeared = true;
                    }
                    break;
            }
        }
    }


}
