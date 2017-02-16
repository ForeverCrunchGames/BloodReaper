using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHeal : MonoBehaviour {

    public SpawnerLogic Spawner;


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Spawner.isPlayerInside = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Spawner.isPlayerInside = true;
        }
    }
}
