using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform spawn = null;
        switch (SpawnpointManager.instance.enterDoor)
        {
            case DoorName.MakDoor:
            spawn = GameObject.Find("Spawn MakDoor").transform;
            break;

            case DoorName.LeaveMakDoor:
            spawn = GameObject.Find("Spawn LeaveMakDoor").transform;
            break;

            case DoorName.PakDoor:
            spawn = GameObject.Find("Spawn PakDoor").transform;
            break;

            case DoorName.LeavePakDoor:
            spawn = GameObject.Find("Spawn LeavePakDoor").transform;
            break;
        }

        if (spawn != null)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            
            Vector3 playerPos = player.position;
            playerPos.x = spawn.position.x;
            
            player.position = playerPos;
        }

        SpawnpointManager.instance.enterDoor = DoorName.none;
    }
}
