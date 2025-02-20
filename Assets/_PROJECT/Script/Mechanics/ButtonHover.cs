using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    TurnOffLamp turnOffLamp;
    void Start()
    {
        turnOffLamp = FindObjectOfType<TurnOffLamp>();
    }

    public void OnPointerEnter()
    {
        turnOffLamp.OnPointerEnterButton();
    }

    public void OnPointerExit()
    {
        turnOffLamp.OnPointerExitButton();
    }
}