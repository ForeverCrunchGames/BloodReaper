using System.Collections;
using UnityEngine;

public class InstaDeath : MonoBehaviour {

    PlayerMOD player;
    EnemyMeleeLogic melee;

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
                if (player.isGodModeOn == false)
                {
                player.SetDead();

                Debug.Log("Insta Death!");
                }
            }
        }
    }
}
