using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour {

    public EnemyMeleeLogic enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (enemy.player.isInmune == false)
            {
                enemy.player.RecieveDamage(enemy.damage);
            }
        }
    }
}
