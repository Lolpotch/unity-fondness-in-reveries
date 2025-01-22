using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private MechanicsManager mechanicsManager;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private string sceneName;
    [SerializeField] private bool playerInTrigger = false;
    public bool isInteractable = true;
    private BoxCollider2D myCollider2D;

    private void Start() 
    {
        mechanicsManager = FindObjectOfType<MechanicsManager>();
        myCollider2D = GetComponent<BoxCollider2D>();
        myCollider2D.enabled = isInteractable;
    }

    private void Update() 
    {
        if (playerInTrigger && !PlayerMovement.Instance.isMakMoving && !mechanicsManager.isOpenMechanic && Input.GetKeyDown(KeyCode.E))
        {
            ChangeScene(sceneName);
        }
    }

    void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (!mechanicsManager.isOpenMechanic)
            {
                eToInteract.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            eToInteract.SetActive(false);
        }
    }
}