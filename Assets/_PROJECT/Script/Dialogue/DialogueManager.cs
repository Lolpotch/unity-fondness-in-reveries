using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace DIALOGUE
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject); // Menghindari duplikasi instance
                return;
            } else instance = this;
            DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        public GameObject dialogueContainer;
        public TextMeshProUGUI dialogueText;
        private ConversationManager conversationManager;
        private DialogueArchitect architect;
        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onDialogue_Next;

        // private bool isInitialized = false;
        private bool checkStart;

        // referensi kalo mau klik layar next
        public void OnDialogue_Next()
        {
            onDialogue_Next?.Invoke();
        }

        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
        
        public void StartConversation()
        {
            checkStart = true;
            if (checkStart == true)
            {
                List<string> lines = FileManager.ReadTextAsset(currentDialogue, false);
                Say(lines);
                spaceForNext.SetActive(true);
                dialogueShadow.SetActive(true);
            }
            checkStart = false;
        }

        private void Update() 
        {
            // DialogueTrigger.Instance.ChooseDialogue();

            if(Input.GetKeyDown(KeyCode.Space) && isRunningConversation) 
            {
                StartCoroutine(PlayspaceForNext());
                OnDialogue_Next();
            }
            
            SetCurrentDialogue(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }

        public bool isRunningConversation; 
        [SerializeField] private DialogueTrigger triggerAct_1;
        [SerializeField] private TextAsset currentDialogue;

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
            // if (isInitialized)
            //     return;

            GameObject mainCanvas = GameObject.Find("Canvas - Main");
            RectTransform dialogueGroup = mainCanvas.transform.Find("[8] - Dialogue") as RectTransform;

            dialogueContainer = dialogueGroup.Find("DialogueContainer").gameObject;
            dialogueText = dialogueContainer.transform.Find("DialogueText").GetComponent<TextMeshProUGUI>();
            dialogueShadow = dialogueContainer.transform.Find("DialogueShadow").gameObject;
            spaceForNext = dialogueContainer.transform.Find("Space ForNext").gameObject;
            spaceForNextImage = spaceForNext.GetComponent<Image>();

            // isInitialized = true;
            architect = new DialogueArchitect(dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        private void SetCurrentDialogue(Scene scene, LoadSceneMode mode)
        {
            // Check specific scenes for dialogue trigger
            if (scene.name == "Act-1_Scene1_KamarIbu" || scene.name == "Act-1_Scene2_RuangTamu")
            {
                triggerAct_1 = GameObject.Find("DialogTrigger Act-1")?.GetComponent<DialogueTrigger>();
                currentDialogue = triggerAct_1.currentDialogue;
            }
        }

        [Header("--- On Press Space ---")]
        [SerializeField] private GameObject spaceForNext;
        [SerializeField] private GameObject dialogueShadow;
        [SerializeField] private Image spaceForNextImage; 
        [SerializeField] private Sprite[] spaceForNextFrames;
        [SerializeField] private Sprite spaceForNextNormal;
        private bool isPlayingAnimation = false; // Pindah jadi field class

        private IEnumerator PlayspaceForNext() {
            if (!isPlayingAnimation) {
                isPlayingAnimation = true;
                for (int i = 0; i < spaceForNextFrames.Length; i++) {
                    spaceForNextImage.sprite = spaceForNextFrames[i];
                    yield return new WaitForSeconds(0.083f); // 12 FPS
                }
                isPlayingAnimation = false;
                spaceForNextImage.sprite = spaceForNextNormal;
            }
        }
    }
}