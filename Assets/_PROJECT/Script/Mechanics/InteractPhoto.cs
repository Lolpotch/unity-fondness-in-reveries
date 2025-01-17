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
            }
        }
    }


}
