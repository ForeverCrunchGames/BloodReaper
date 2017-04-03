 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverpoweredTrigger : MonoBehaviour {

    PlayerMOD player;
    public GameObject sword;

    // Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.isPlayerOverpowered = true;
            player.isLifeDecreasing = true;
            sword.SetActive(false);
            enabled = !enabled;
        }
    }
}
