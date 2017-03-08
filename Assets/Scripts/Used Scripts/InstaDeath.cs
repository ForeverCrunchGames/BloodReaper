using System.Collections;
using UnityEngine;

public class InstaDeath : MonoBehaviour {

    PlayerMOD player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player.state != PlayerMOD.States.DEAD)
            {
                player.SetDead();

                Debug.Log("Death by falling to Abysm");
            }
        }

    }
}
