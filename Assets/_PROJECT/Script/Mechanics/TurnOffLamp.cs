using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DIALOGUE;

public class TurnOffLamp : MonoBehaviour
{
    [SerializeField] private GameObject currentMechanic;
    public GameObject blackPanelIn, blackPanelOut;
    public RectTransform background;
    public RectTransform buttonOn;
    public RectTransform buttonOnOutline;
    public RectTransform buttonOff;
    public RectTransform bayanganNyala;
    public RectTransform gelapMati;
    public Transform dustParticleMap;
    public RectTransform space;
    public Transform dustParticleMech;
    public float delayLength = 3f;

    public Image[] fadeIn1;
    bool activeOnce = false;
    bool activeSpaceOnce = false;

    Button buttonOnBtn;
    //Outline buttonOnOutline;
    void Start()
    {
        buttonOnBtn = buttonOn.GetComponent<Button>();
        //buttonOnOutline = buttonOn.GetComponent<Outline>();

        buttonOnBtn.interactable = false;
        //buttonOnOutline.enabled = false;

        blackPanelOut.SetActive(false);
        buttonOn.gameObject.SetActive(true);
        buttonOnOutline.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(false);
        bayanganNyala.gameObject.SetActive(true);
        gelapMati.gameObject.SetActive(false);
        dustParticleMap.gameObject.SetActive(false);
    }

    void Update()
    {
        // Fade in first time
        if (blackPanelIn.activeSelf && !activeOnce)
        {
            activeOnce = true;
            // FadeManager.Instance.FadeIn(fadeIn1, 1f);
            // background.GetComponent<FadeImage>().FadeIn(1f);
            // bayanganNyala.GetComponent<FadeImage>().FadeIn(1f);
            blackPanelIn.GetComponent<FadeImage>().FadeInOut(.7f);
        }

        buttonOnBtn.interactable = DialogueTrigger.Instance.isTurnOffLamp_6Played;
        dustParticleMap.gameObject.SetActive(MechanicsManager.Instance.isTurnOffLampPlayed && !MechanicsManager.Instance.isOpenMechanic);

        if (MechanicsManager.Instance.isTurnOffLampPlayed && !DialogueManager.instance.isRunningConversation)
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
                    // gelapMati.GetComponent<FadeImage>().FadeOut(1f);
                    // buttonOff.GetComponent<FadeImage>().FadeOut(1f);
                    // space.GetComponent<FadeImage>().FadeOut(1f);
                }
            }
        }
    }


    public void SetMechanic(bool value)
    {
        currentMechanic.SetActive(value);
        MechanicsManager.Instance.isOpenMechanic = value;    
    }
    
    public void TurnOffLampButton()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
        bayanganNyala.gameObject.SetActive(false);
        gelapMati.gameObject.SetActive(true);
        dustParticleMech.gameObject.SetActive(false);

        MechanicsManager.Instance.isTurnOffLampPlayed = true;
        // Abis itu matiin mekanik ini
    }

    public void OnPointerEnterButton()
    {
        if (buttonOnBtn.interactable)
        {
            buttonOnOutline.gameObject.SetActive(true);
        }
    }

    public void OnPointerExitButton()
    {
        buttonOnOutline.gameObject.SetActive(false);
    }
}