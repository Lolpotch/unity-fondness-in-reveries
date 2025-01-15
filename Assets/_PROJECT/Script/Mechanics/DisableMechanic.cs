using UnityEngine;

public class DisableMechanic : MonoBehaviour
{
    public RectTransform parent;
    public MechanicName mechanicName;
    public bool isDisableByKey = true;

    void Start()
    {
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isDisableByKey)
        {
            DisableThisMechanic();
        }
    }

    public void DisableThisMechanic()
    {
        switch (mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                MechanicsManager.Instance.isSwingingBabyToSleepPlayed = true;
                break;

            case MechanicName.closeCurtain:
                MechanicsManager.Instance.isCloseCurtainPlayed = true;
                break;

            case MechanicName.turnOffLamp:
                MechanicsManager.Instance.isTurnOffLampPlayed = true;
                break;
            
            case MechanicName.interactPhoto1:
                MechanicsManager.Instance.isInteractPhoto_1Played = true;
                break;
            
            case MechanicName.interactPhoto2:
                MechanicsManager.Instance.isInteractPhoto_2Played = true;
                break;

            case MechanicName.interactPhoto3:
                MechanicsManager.Instance.isInteractPhoto_3Played = true;
                break;

            case MechanicName.interactPhoto4:
                MechanicsManager.Instance.isInteractPhoto_4Played = true;
                break;
        }
        
        MechanicsManager.Instance.isOpenMechanic = false;

        parent.gameObject.SetActive(false);
    }
}
