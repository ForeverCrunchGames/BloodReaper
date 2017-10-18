using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHeal : MonoBehaviour {

    public SpawnerLogic Spawner;

    public AudioSource heal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Spawner.isDestroyed)
        {
            heal.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Spawner.isPlayerInside = false;
            heal.Stop();
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
