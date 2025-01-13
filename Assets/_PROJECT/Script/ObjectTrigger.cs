using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public MechanicName mechanicName;
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject mechanics;
    [SerializeField] private bool playerInTrigger = false;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
    }

    private void Update() 
    {
        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKeyDown(KeyCode.E))
        {
            RunMechanic();
        }
    }

    private void RunMechanic()
    {
        switch(mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                OpenMechanics();
                mechanicsManager.isSwingingBabyToSleepOpened = true;
                break;

            case MechanicName.closeCurtain:
                OpenMechanics();
                mechanicsManager.isCloseCurtainOpened = true;
                break;

            case MechanicName.turnOffLamp:
                OpenMechanics();
                mechanicsManager.isTurnOffLampOpened = true;
                break;

            case MechanicName.interactPhoto1:
                OpenMechanics();
                mechanicsManager.isInteractPhoto_1Opened = true;
                break;

            case MechanicName.interactPhoto2:
                OpenMechanics();
                mechanicsManager.isInteractPhoto_2Opened = true;
                break;

            case MechanicName.interactPhoto3:
                OpenMechanics();
                mechanicsManager.isInteractPhoto_3Opened = true;
                break;

            case MechanicName.interactPhoto4:
                OpenMechanics();
                mechanicsManager.isInteractPhoto_4Opened = true;
                break;

            case MechanicName.toDoList:
                OpenMechanics();
                mechanicsManager.isTDLCollected = true;
                break;

            case MechanicName.makingMilk:
                OpenMechanics();
                mechanicsManager.isMakingMilkOpened = true;
                break;

            case MechanicName.none:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (!mechanicsManager.isOpenMechanic)
            {
                eToInteract.SetActive(true);
            }
            mechanicsManager.currentTriggerMechanic = mechanicName.ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            eToInteract.SetActive(false);
        }
        mechanicsManager.currentTriggerMechanic = MechanicName.none.ToString();
    }

    private void OpenMechanics()
    {
        eToInteract.SetActive(false);
        mechanics.SetActive(true);
        mechanicsManager.isOpenMechanic = true;
    }
}
