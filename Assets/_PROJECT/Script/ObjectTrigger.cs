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
    private BoxCollider2D myCollider2D;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myCollider2D.enabled = false;

        mechanicsManager.currentMechanic = MechanicName.none;
    }

    private void Update() 
    {
        EnableMechanic();

        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKeyDown(KeyCode.E))
        {
            RunMechanic();
        }

        if (mechanicName == MechanicName.photoMemoryAct1 && playerInTrigger && mechanicsManager.isRepairSwingPlayed && Input.GetKeyDown(KeyCode.E))
        {
            RunMechanic();
        }
    }

    private void EnableMechanic()
    {
        switch(mechanicName)
        {
            case MechanicName.swingingBabyToSleep:
                if (!mechanicsManager.isSwingingBabyToSleepPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.closeCurtain:
                if (mechanicsManager.isSwingingBabyToSleepPlayed && !mechanicsManager.isCloseCurtainPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.turnOffLamp:
                if (mechanicsManager.isCloseCurtainPlayed && !mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.interactPhoto1:
                if (mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; }
                break;

            case MechanicName.interactPhoto2:
                if (mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; }
                break;
            
            case MechanicName.interactPhoto3:
                if (mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; }
                break;
            
            case MechanicName.interactPhoto4:
                if (mechanicsManager.isTurnOffLampPlayed)
                { myCollider2D.enabled = true; }
                break;

            case MechanicName.cameraPolaroid:
                if (mechanicsManager.isTurnOffLampPlayed && !mechanicsManager.isCameraCollected)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.toDoList:
                if (mechanicsManager.isCameraCollected && !mechanicsManager.isTDLCollected)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.makingMilk:
                if (mechanicsManager.isTDLCollected && !mechanicsManager.isMakingMilkPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isCarryingArrelToBath)
                { myCollider2D.enabled = true; } 
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isCarryingArrelToBath && !mechanicsManager.isGetBackBaby)
                { myCollider2D.enabled = true; } 
                else { myCollider2D.enabled = false; }
                break;

            case MechanicName.getWater:
                if (mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isGetWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.boilWater:
                if (mechanicsManager.isGetWaterPlayed && !mechanicsManager.isBoilWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.pourWater:
                if (mechanicsManager.isBoilWaterPlayed && !mechanicsManager.isPourWaterPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.bathingBaby:
                if (!mechanicsManager.isBathingBabyPlayed && mechanicsManager.isCarryingArrelToBath)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.repairSwing:
                if (mechanicsManager.isBathingBabyPlayed && !mechanicsManager.isRepairSwingPlayed)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;

            case MechanicName.photoMemoryAct1:
                if (mechanicsManager.isRepairSwingPlayed && !mechanicsManager.isPhotoTaken)
                { myCollider2D.enabled = true; } else { myCollider2D.enabled = false; }
                break;
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

            case MechanicName.cameraPolaroid:
                eToInteract.SetActive(false);
                mechanicsManager.isCameraCollected = true;
                gameObject.SetActive(false);
                break;

            case MechanicName.toDoList:
                OpenMechanics();
                mechanicsManager.isOpenMechanic = false;
                mechanicsManager.isTDLCollected = true;
                gameObject.SetActive(false);
                break;

            case MechanicName.makingMilk:
                OpenMechanics();
                mechanicsManager.isMakingMilkOpened = true;
                break;

            case MechanicName.givingMilk:
                if (mechanicsManager.isMakingMilkPlayed && !mechanicsManager.isGivingMilkPlayed && !mechanicsManager.isCarryingArrelToBath)
                {
                    OpenMechanics();
                    mechanicsManager.isGivingMilkOpened = true;
                }
                else if (mechanicsManager.isPourWaterPlayed && !mechanicsManager.isCarryingArrelToBath && !mechanicsManager.isGetBackBaby)
                {
                    OpenMechanics();
                    mechanicsManager.isGetWaterOpened = true;
                }
                break;

            case MechanicName.getWater:
                OpenMechanics();
                mechanicsManager.isGetWaterOpened = true;
                break;

            case MechanicName.boilWater:
                OpenMechanics();
                mechanicsManager.isBoilWaterOpened = true;
                break;

            case MechanicName.pourWater:
                OpenMechanics();
                mechanicsManager.isPourWaterOpened = true;
                break;

            case MechanicName.bathingBaby:
                OpenMechanics();
                mechanicsManager.isBathingBabyOpened = true;
                break;

            case MechanicName.repairSwing:
                OpenMechanics();
                mechanicsManager.isRepairSwingOpened = true;
                break;

            case MechanicName.photoMemoryAct1:
                OpenMechanics();
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
            mechanicsManager.currentMechanic = mechanicName;
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
        mechanicsManager.currentMechanic = MechanicName.none;
    }

    private void OpenMechanics()
    {
        eToInteract.SetActive(false);
        mechanics.SetActive(true);
        mechanicsManager.isOpenMechanic = mechanics.activeSelf;
    }
}
