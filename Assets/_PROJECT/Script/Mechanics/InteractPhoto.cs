using UnityEngine;

public class InteractPhoto : MonoBehaviour
{

    public MechanicName mechanicName;
    
    void Start()
    {
        switch(mechanicName)
        {
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
    }
}
