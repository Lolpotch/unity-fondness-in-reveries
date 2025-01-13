using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoList : MonoBehaviour
{
    private GameObject fToOpen;
    [SerializeField] private GameObject tdl;

    private void Update() 
    {
        fToOpen = GameObject.Find("F ToOpen");
        if (MechanicsManager.Instance.isTDLCollected && Input.GetKeyDown(KeyCode.F))
        {
            if (fToOpen != null)
            {
                fToOpen.SetActive(false);
            }

            bool isActive = tdl.activeSelf;
            tdl.SetActive(!isActive); // Toggle status
            MechanicsManager.Instance.isOpenMechanic = !isActive;
        }
    }
}
