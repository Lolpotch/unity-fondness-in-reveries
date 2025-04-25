using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class CloseCurtain : MonoBehaviour
{
    [SerializeField] private GameObject curentMechanic; 
    public GameObject blackPanelIn, blackPanelOut;
    public RectTransform background;
    public RectTransform guideRight;
    public RectTransform guideLeft;
    public RectTransform space;
    public Slider sliderLeft, sliderRight;

    public float delayLength = 3f;

    public Image[] fadeIn1;
    bool activeOnce = false;
    bool activeSpaceOnce = false;

    bool isRightSwiped = false;
    bool isLeftSwiped = false;
    bool isMechanicBegin = false;

    void Start()
    {
        blackPanelOut.SetActive(false);
        
        guideRight.gameObject.SetActive(false);
        guideLeft.gameObject.SetActive(false);

        sliderRight.gameObject.SetActive(false);
        sliderLeft.gameObject.SetActive(false);

        sliderLeft.value = 0f;
        sliderRight.value = 0f;

        isRightSwiped = false;
        isLeftSwiped = false;
    }

    void Update()
    {
        // Fade in first time
        if (blackPanelIn.activeSelf && !activeOnce)
        {
            activeOnce = true;
            //FadeManager.Instance.FadeIn(fadeIn1, 1f);
            blackPanelIn.GetComponent<FadeImage>().FadeInOut(.7f);
        }

        if (DialogueTrigger.Instance.isCloseCurtain_5Played)
        {
            if (!isMechanicBegin)
            {
                guideRight.gameObject.SetActive(true);
                guideLeft.gameObject.SetActive(false);

                sliderRight.gameObject.SetActive(true);
                sliderLeft.gameObject.SetActive(false);    

                isMechanicBegin = true;
            }
            
            // Detect when the left mouse button is released
            if (!isRightSwiped || !isLeftSwiped)
            {
                if (!isRightSwiped && sliderRight.value > .9f)
                {
                    Debug.Log("Swiped Left");
                    
                    isRightSwiped = true;
                    isLeftSwiped = false;

                    sliderRight.value = 1f;

                    sliderRight.gameObject.SetActive(false);
                    sliderLeft.gameObject.SetActive(true);

                    guideRight.gameObject.SetActive(false);
                    guideLeft.gameObject.SetActive(true);
                }
                else if (isRightSwiped && !isLeftSwiped && sliderLeft.value > .9f)
                {
                    Debug.Log("Swiped Right");

                    isRightSwiped = true;
                    isLeftSwiped = true;

                    sliderLeft.value = 1f;

                    sliderRight.gameObject.SetActive(false);
                    sliderLeft.gameObject.SetActive(false);

                    guideLeft.gameObject.SetActive(false);
                    guideRight.gameObject.SetActive(false);
                }

                // Check if both swipes are completed
                if (isRightSwiped && isLeftSwiped)
                {
                    Debug.Log("Both swipes completed");

                    MechanicsManager.Instance.isCloseCurtainPlayed = true;
                }
            }

            if (MechanicsManager.Instance.isCloseCurtainPlayed && !DialogueManager.instance.isRunningConversation)
            {
                if (!activeSpaceOnce)
                {
                    space.gameObject.SetActive(true);
                    space.GetComponent<FadeImage>().FadeIn(1f);

                    activeSpaceOnce = true;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space) && space.gameObject.activeInHierarchy)
                    {
                        // SetMechanic(false);
                        blackPanelOut.SetActive(true);
                        blackPanelOut.GetComponent<FadeImage>().FadeInOut(.7f);
                        // background.GetComponent<FadeImage>().FadeOut(1f);
                        // space.GetComponent<FadeImage>().FadeOut(1f);
                    }
                }
            }
        }
    }

    public void SetMechanic(bool value)
    {
        curentMechanic.SetActive(value);
        MechanicsManager.Instance.isOpenMechanic = value;    
    }
}