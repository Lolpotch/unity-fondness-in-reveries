using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffLamp : MonoBehaviour
{
    public RectTransform buttonOn;
    public RectTransform buttonOnOutline;
    public RectTransform buttonOff;
    public float delayLength = 3f;

    Button buttonOnBtn;
    //Outline buttonOnOutline;
    void Start()
    {
        buttonOnBtn = buttonOn.GetComponent<Button>();
        //buttonOnOutline = buttonOn.GetComponent<Outline>();

        buttonOnBtn.interactable = false;
        //buttonOnOutline.enabled = false;

        buttonOn.gameObject.SetActive(true);
        buttonOnOutline.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(false);
    }

    void Update()
    {
        buttonOnBtn.interactable = DialogueTrigger.Instance.isTurnOffLamp_6Played;
    }
    
    public void TurnOffLampButton()
    {
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);

        // MechanicsManager.Instance.isTurnOffLampPlayed = true;
        StartCoroutine(DisableMechanic());
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

    private IEnumerator DisableMechanic()
    {
        yield return new WaitForSeconds(delayLength);

        GetComponent<DisableMechanic>().DisableThisMechanic();
    }
}
