using UnityEngine;

public class SpawnpointManager : MonoBehaviour
{
    public static SpawnpointManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Menghindari duplikasi instance
            return;
        } else instance = this;
        DontDestroyOnLoad(gameObject); // Jika perlu instance bertahan antar scene
    }

    public DoorName currentDoor;
    public DoorName enterDoor;
}

public enum DoorName
{
    MakDoor,
    LeaveMakDoor,
    PakDoor,
    LeavePakDoor,
    none
}