using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else Instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    [SerializeField] private GameObject notifMoveSide;
    [SerializeField] private GameObject notifTDLCollected;

    public IEnumerator NotifMoveSide()
    {
        notifMoveSide.SetActive(true);
        yield return new WaitUntil (() => PlayerMovement.Instance.isMakMoving);
        yield return new WaitForSeconds(2f);
        notifMoveSide.SetActive(false);
        StopCoroutine(NotifMoveSide());
    }

    public IEnumerator NotifTDLCollected()
    {
        notifTDLCollected.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notifTDLCollected.SetActive(false);
        StopCoroutine(NotifTDLCollected());
    }
}
