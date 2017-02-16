using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

    public EnemyMeleeLogic enemy;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
        
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.isPlayerDetected = true;
            enemy.currentVelocity = enemy.rageVelocity;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.isPlayerDetected = false;
            enemy.currentVelocity = enemy.velocity;
        }
    }
}
