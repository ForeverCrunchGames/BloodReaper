using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour {

    PlayerMOD player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyMeleeLogic>().state = EnemyMeleeLogic.States.DAMAGE;
            player.currentLife += 5;
            Debug.Log ("Attacked");
        }

        if (other.tag == "SpawnerHitPoint")
        {
            other.GetComponentInParent<SpawnerLogic>().isDestroyed = true;
            Debug.Log ("Spawn Destroyed");
        }
    }
        
}
