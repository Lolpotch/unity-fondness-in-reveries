using UnityEngine;
using UnityEngine.UI;

public class SwingBabyToSleep : MonoBehaviour
{
    public RectTransform baby;
    public RectTransform basket;
    public RectTransform guideLeft;
    public RectTransform guideRight;

    public float speed = 200f;
    public float pointLeft = -10f;
    public float pointRight = 10f;
    public float swingCountRequired = 4;

    Button basketButton;
    Outline basketOutline;

    int swingCount = 0;
    bool isSwingDialogueTriggered = false;
    bool movingLeft = true;

    void Start()
    {
        guideLeft.gameObject.SetActive(true);
        guideRight.gameObject.SetActive(false);

        basketButton = basket.GetComponent<Button>();
        basketOutline = basket.GetComponent<Outline>();

        basketOutline.enabled = false;
        basketButton.interactable = false;

        MechanicsManager.Instance.isSwingComplete = false;
        MechanicsManager.Instance.isPutBabySleep = false;
    }
    void Update()
    {
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

        if (DialogueTrigger.Instance.isSwingingBabyPlayed && !isSwingDialogueTriggered)
        {
            isSwingDialogueTriggered = true;

            basketOutline.enabled = true;
            basketButton.interactable = true;
        }
    }
    
    public void PlaySleepBabyAnim()
    {
        basketOutline.enabled = false;
        basketButton.interactable = false;

        print("SLEEP BABY ANIMATION START HERE");
        // Kode buat play animasi
        MechanicsManager.Instance.isPutBabySleep = true;
        // Abis itu matiin mekanik ini
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

        guideLeft.gameObject.SetActive(movingLeft);
        guideRight.gameObject.SetActive(!movingLeft);
        
        if (swingCount >= swingCountRequired)
        {
            MechanicsManager.Instance.isSwingComplete = true;
            
            guideLeft.gameObject.SetActive(false);
            guideRight.gameObject.SetActive(false);
        }
    }
}
