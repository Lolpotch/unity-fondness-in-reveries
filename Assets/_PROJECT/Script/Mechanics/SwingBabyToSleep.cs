using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class SwingBabyToSleep : MonoBehaviour
{
    [SerializeField] private GameObject curentMechanic;
    [SerializeField] private GameObject blackPanelIn;
    [SerializeField] private GameObject blackPanelOut;
    public RectTransform background;
    public RectTransform baby;
    public RectTransform basket;
    public RectTransform basketWithBaby;
    public RectTransform guideLeft;
    public RectTransform guideRight;
    public RectTransform babyPanel;
    public RectTransform space;

    public Image[] fadeIn1;

    public float speed = 200f;
    public float pointLeft = -10f;
    public float pointRight = 10f;
    public int swingCountRequired = 4;
    public float delayLength = 1.5f;

    Button basketButton;
    Outline basketOutline;
    Image guideLeftImg;
    Image guideRightImg;

    int swingCount = 0;
    bool isSwingDialogueTriggered = false;
    // bool isBabySleepDialogueTriggered = false;
    bool movingLeft = true;
    bool activeOnce = false;
    bool activeSpaceOnce = false;

    void Start()
    {
        blackPanelOut.SetActive(false);
        guideLeft.gameObject.SetActive(true);
        guideRight.gameObject.SetActive(true);

        guideLeftImg = guideLeft.GetComponent<Image>();
        guideRightImg = guideRight.GetComponent<Image>();
        
        Color colorLeft = guideLeftImg.color;
        Color colorRight = guideRightImg.color;
        colorLeft.a = 0f;
        colorRight.a = 0f;
        
        // both guide color to 0a
        guideLeftImg.color = colorLeft; 
        guideRightImg.color = colorRight;
        
        babyPanel.gameObject.SetActive(false);
        basket.gameObject.SetActive(true);
        basketWithBaby.gameObject.SetActive(false);
        space.gameObject.SetActive(false);

        basketButton = basket.GetComponent<Button>();
        basketOutline = basket.GetComponent<Outline>();

        basketOutline.enabled = false;
        basketButton.interactable = false;

        MechanicsManager.Instance.isSwingComplete = false;
        MechanicsManager.Instance.isPutBabySleep = false;
    }
    void Update()
    {
        if (blackPanelIn.activeSelf && !activeOnce)
        {
            activeOnce = true;
            // FadeManager.Instance.FadeIn(fadeIn1, 1f);
            // background.GetComponent<FadeImage>().FadeIn(1f);
            // baby.GetComponent<FadeImage>().FadeIn(1f);
            // guideLeft.GetComponent<FadeImage>().FadeIn(1f);
            blackPanelIn.GetComponent<FadeImage>().FadeInOut(.7f);
        }

        if (!MechanicsManager.Instance.isSwingComplete)
        {
            if (movingLeft && Input.GetKey(KeyCode.Q))
            {
                MoveBaby(pointLeft);
            }
            else if (!movingLeft && Input.GetKey(KeyCode.E))
            {
                MoveBaby(pointRight);
            }
        }

        if (DialogueTrigger.Instance.isSwingingBaby_3Played && !isSwingDialogueTriggered)
        {
            isSwingDialogueTriggered = true;
            basketOutline.enabled = true;
            basketButton.interactable = true;
        }

        // if (DialogueTrigger.Instance.isPutBabySleep_4Played && !isBabySleepDialogueTriggered)
        // {
        //     isBabySleepDialogueTriggered = true;

        //     StartCoroutine(DisableMechanic());
        // }
        if (MechanicsManager.Instance.isSwingingBabyToSleepPlayed && DialogueTrigger.Instance.isPutBabySleep_4Played && !DialogueManager.instance.isRunningConversation)
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
                }
            }
        }
    }

    public void SetMechanic(bool value)
    {
        curentMechanic.SetActive(value);
        MechanicsManager.Instance.isOpenMechanic = value;    
    }
    
    public void PlaySleepBabyAnim()
    {
        basketOutline.enabled = false;
        basketButton.interactable = false;

        StartCoroutine(PlayBabyPanel());
    }

    private IEnumerator PlayBabyPanel()
    {
        babyPanel.gameObject.SetActive(true);
        basketWithBaby.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(delayLength);
        
        babyPanel.gameObject.SetActive(false);
        basket.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(delayLength);

        MechanicsManager.Instance.isPutBabySleep = true;
        MechanicsManager.Instance.isSwingingBabyToSleepPlayed = true;
    }

    void MoveBaby(float target)
    {
        Vector3 babyPosition = baby.localPosition;

        if (movingLeft)
        {
            babyPosition.x = Mathf.MoveTowards(babyPosition.x, target, speed * Time.deltaTime);
        }
        else
        {
            babyPosition.x = Mathf.MoveTowards(babyPosition.x, target, speed * Time.deltaTime);
        }

        baby.localPosition = babyPosition;

        if (Mathf.Abs(baby.localPosition.x - target) < 0.1f)
        {
            ToggleGuides();
        }
    }

    void ToggleGuides()
    {
        movingLeft = !movingLeft;
        swingCount++;

        // guideLeft.gameObject.SetActive(movingLeft); // INI BAKAL GA BENER
        // guideRight.gameObject.SetActive(!movingLeft); // INI BAKAL GA BENER

        // Image[] guideLefts = {guideLeft.GetComponent<Image>()};
        // Image[] guideRights = {guideRight.GetComponent<Image>()};


        if (swingCount >= swingCountRequired)
        {
            MechanicsManager.Instance.isSwingComplete = true;
            
            // guideLeft.gameObject.SetActive(false);
            // guideRight.gameObject.SetActive(false);

            guideRight.GetComponent<FadeImage>().FadeOut(.7f);

            // if (movingLeft)
            // {guideLeft.GetComponent<FadeImage>().FadeOut(.7f);}
            // else
            // {guideRight.GetComponent<FadeImage>().FadeOut(.7f);}
        }
        else
        {
            if (movingLeft)
            {
                // FadeManager.Instance.FadeIn(guideLefts, 1f);
                // FadeManager.Instance.FadeOut(guideRights, 1f);

                guideLeft.GetComponent<FadeImage>().FadeIn(.7f);
                guideRight.GetComponent<FadeImage>().FadeOut(.7f);
            }
            else
            {
                // FadeManager.Instance.FadeOut(guideLefts, 1f);
                // FadeManager.Instance.FadeIn(guideRights, 1f);

                guideLeft.GetComponent<FadeImage>().FadeOut(.7f);
                guideRight.GetComponent<FadeImage>().FadeIn(.7f);
            }
        }
        
        
    }

    // private IEnumerator DisableMechanic()
    // {
    //     yield return new WaitForSeconds(delayLength);

    //     GetComponent<DisableMechanic>().DisableThisMechanic();
    // }
}