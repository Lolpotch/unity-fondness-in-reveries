using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi Instance
            return;
        } else Instance = this;
    }

    [Header("-- Dialogue --")]
    public TextAsset currentDialogue;

    [SerializeField] private TextAsset startHumming;
    [SerializeField] private TextAsset moveSideScroll;
    [SerializeField] private TextAsset swingingBaby;
    [SerializeField] private TextAsset putSleepBaby;
    [SerializeField] private TextAsset closeCurtain;
    [SerializeField] private TextAsset turnOffLamp;
    [SerializeField] private TextAsset interactPhoto1;
    [SerializeField] private TextAsset interactPhoto2;
    [SerializeField] private TextAsset interactPhoto3;
    [SerializeField] private TextAsset interactPhoto4;
    [SerializeField] private TextAsset toDoList;
    [SerializeField] private TextAsset makingMilk;
    [SerializeField] private TextAsset givingMilk;
    [SerializeField] private TextAsset bathingBaby;
    [SerializeField] private TextAsset repairSwing;
    [SerializeField] private TextAsset photoMemoryAct1;
    [SerializeField] private TextAsset diaryBook;
    
    //DialogueManager.instance.StartConversation();
    private void Start()
    {
        if (!isStartHummingPlayed) {
            StartCoroutine(PlayStartHumming());
        } else {
            StopCoroutine(PlayStartHumming());
        }

        if (!isToDoListPlayed) {
            StartCoroutine(PlayToDoList());
        } else {
            StopCoroutine(PlayToDoList());
        }

        if (!isMakingMilkPlayed) {
            StartCoroutine(PlayMakingMilk());
        } else {
            StopCoroutine(PlayMakingMilk());
        }

        // Variabel di bawah butuh w buat public untuk kapan trigger mechanic pas dialog
        // Jadi w assign ulang variabelnya di sini biar ga kecelakaan variabelnya berubah di inspector
        isSwingingBabyPlayed = false;
        isCloseCurtainPlayed = false;
        isTurnOffLampPlayed = false;
    }
    
    public void ChooseDialogue()
    {
        if (MechanicsManager.Instance.currentTriggerMechanic != null)
        {
            switch (MechanicsManager.Instance.currentTriggerMechanic)
            {
                case "none":
                    currentDialogue = null;
                    break;
            }
        }
    }

    [SerializeField] private bool isStartHummingPlayed;
    private IEnumerator PlayStartHumming()
    {
        yield return new WaitForSeconds(3f);
        currentDialogue = startHumming;
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isStartHummingPlayed = true;

        if (isStartHummingPlayed && !isMoveSideScrollPlayed) {
            StartCoroutine(PlayMoveSideScroll());
        } else {
            StopCoroutine(PlayMoveSideScroll());
        }
    }

    [SerializeField] private bool isMoveSideScrollPlayed;
    private IEnumerator PlayMoveSideScroll()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(NotificationManager.Instance.NotifMoveSide());
        MechanicsManager.Instance.isGameStart = true;
        yield return new WaitUntil (() => PlayerMovement.Instance.isMakMoving);
        currentDialogue = moveSideScroll;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isMoveSideScrollPlayed = true;

        if (isMoveSideScrollPlayed && !isSwingingBabyPlayed) {
            StartCoroutine(PlaySwingingBaby());
        } else {
            StopCoroutine(PlaySwingingBaby());
        }
    }

    [SerializeField] public bool isSwingingBabyPlayed;
    private IEnumerator PlaySwingingBaby()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isSwingComplete);
        currentDialogue = swingingBaby;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isSwingingBabyPlayed = true;

        if (isSwingingBabyPlayed && !isPutBabySleepPlayed) {
            StartCoroutine(PlayBabySleep());
        } else {
            StopCoroutine(PlayBabySleep());
        }
    }

    [SerializeField] private bool isPutBabySleepPlayed = false;
    private IEnumerator PlayBabySleep()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isPutBabySleep);
        currentDialogue = putSleepBaby;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPutBabySleepPlayed = true;

        if (isPutBabySleepPlayed && !isCloseCurtainPlayed) {
            StartCoroutine(PlayCloseCurtain());
        } else {
            StopCoroutine(PlayCloseCurtain());
        }
    }

    [SerializeField] public bool isCloseCurtainPlayed = false;
    private IEnumerator PlayCloseCurtain()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isCloseCurtainOpened);
        currentDialogue = closeCurtain;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isCloseCurtainPlayed = true;

        if (isCloseCurtainPlayed && !isTurnOffLampPlayed) {
            StartCoroutine(PlayTurnOffLamp());
        } else {
            StopCoroutine(PlayTurnOffLamp());
        }
    }

    [SerializeField] public bool isTurnOffLampPlayed = false;
    private IEnumerator PlayTurnOffLamp()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isTurnOffLampOpened);
        currentDialogue = turnOffLamp;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isTurnOffLampPlayed = true;

        if (isTurnOffLampPlayed && !isPlayInteractPhoto1Played) {
            StartCoroutine(PlayInteractPhoto1());
        } else {
            StopCoroutine(PlayInteractPhoto1());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto1Played= false;
    private IEnumerator PlayInteractPhoto1()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_1Opened);
        currentDialogue = interactPhoto1;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPlayInteractPhoto1Played = true;

        if (isPlayInteractPhoto1Played && !isPlayInteractPhoto2Played) {
            StartCoroutine(PlayInteractPhoto2());
        } else {
            StopCoroutine(PlayInteractPhoto2());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto2Played= false;
    private IEnumerator PlayInteractPhoto2()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_2Opened);
        currentDialogue = interactPhoto2;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPlayInteractPhoto2Played = true;

        if (isPlayInteractPhoto2Played && !isPlayInteractPhoto3Played) {
            StartCoroutine(PlayInteractPhoto3());
        } else {
            StopCoroutine(PlayInteractPhoto3());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto3Played= false;
    private IEnumerator PlayInteractPhoto3()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_3Opened);
        currentDialogue = interactPhoto3;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPlayInteractPhoto3Played = true;

        if (isPlayInteractPhoto3Played && !isPlayInteractPhoto4Played) {
            StartCoroutine(PlayInteractPhoto4());
        } else {
            StopCoroutine(PlayInteractPhoto4());
        }
    }

    [SerializeField] public bool isPlayInteractPhoto4Played= false;
    private IEnumerator PlayInteractPhoto4()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isInteractPhoto_4Opened);
        currentDialogue = interactPhoto4;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isPlayInteractPhoto4Played = true;
    }

    [SerializeField] private bool isToDoListPlayed;
    private IEnumerator PlayToDoList()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isTDLCollected);
        StartCoroutine(NotificationManager.Instance.NotifTDLCollected());
        currentDialogue = toDoList;
        yield return new WaitForSeconds(0.5f);
        DialogueManager.instance.StartConversation();
        
        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isToDoListPlayed = true;
    }

    [SerializeField] private bool isMakingMilkPlayed;
    private IEnumerator PlayMakingMilk()
    {
        yield return new WaitUntil (() => MechanicsManager.Instance.isMakingMilkPlayed);
        currentDialogue = makingMilk;
        yield return new WaitForSeconds(1f);
        DialogueManager.instance.StartConversation();

        yield return new WaitUntil (() => !DialogueManager.instance.isRunningConversation);
        currentDialogue = null;
        isMakingMilkPlayed = true;
    }
}
