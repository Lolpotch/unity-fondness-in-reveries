using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsManager : MonoBehaviour
{
    public static MechanicsManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    [HideInInspector] public bool isSwingComplete = false;
    [HideInInspector] public bool isPutBabySleep = false;

    public bool isGameStart;
    public bool isOpenMechanic;
    public string currentTriggerMechanic;

    public bool isSwingingBabyToSleepOpened;
    public bool isSwingingBabyToSleepPlayed;
    public bool isCloseCurtainOpened;
    public bool isCloseCurtainPlayed;
    public bool isTurnOffLampOpened;
    public bool isTurnOffLampPlayed;
    public bool isInteractPhoto_1Opened;
    public bool isInteractPhoto_1Played;
    public bool isInteractPhoto_2Opened;
    public bool isInteractPhoto_2Played;
    public bool isInteractPhoto_3Opened;
    public bool isInteractPhoto_3Played;
    public bool isInteractPhoto_4Opened;
    public bool isInteractPhoto_4Played;
    public bool isInteractPhoto1Opened;
    public bool isInteractPhoto1Played;

    public bool isTDLCollected;
    public bool isMakingMilkOpened;
    public bool isMakingMilkPlayed;
}

public enum MechanicName {  // isi dengan mekanik lain
    swingingBabyToSleep, 
    closeCurtain,
    turnOffLamp,
    interactPhoto1,
    interactPhoto2,
    interactPhoto3,
    interactPhoto4,
    toDoList,
    makingMilk,
    givingMilk,
    bathingBaby,
    repairSwing,
    photoMemoryAct1,
    diaryBook,
    none
}