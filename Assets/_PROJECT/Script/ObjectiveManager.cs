using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }
    
    [SerializeField] private GameObject objectiveContainer;
    [SerializeField] private GameObject objective;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private string[] objectiveContent;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject mainCanvas = GameObject.Find("Canvas - Main");
        RectTransform objectivesGroup = mainCanvas.transform.Find("[5] - Objectives") as RectTransform;

        objectiveContainer = objectivesGroup.transform.Find("ObjectiveContainer").gameObject;
        objective = objectiveContainer.transform.Find("Objective").gameObject;
        GameObject objectiveTextObject = objectiveContainer.transform.Find("ObjectiveText").gameObject;
        objectiveText = objectiveTextObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!MechanicsManager.Instance.isOpenMechanic)
        {
            if (MechanicsManager.Instance.isSwingingBabyToSleepPlayed)
            {
                objectiveContainer.SetActive(true);
                objectiveText.text = objectiveContent[0];
            }

            if (MechanicsManager.Instance.isCloseCurtainOpened)
            {
                objectiveText.text = objectiveContent[1];
            }
        } 
        else
        {
            objectiveContainer.SetActive(false);
        }
    }
}
