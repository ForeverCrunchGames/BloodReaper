 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverpoweredTrigger : MonoBehaviour {

    public AudioSource trankis;
    public AudioSource doom;

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
            player.lifeBar.SetActive(true);
            sword.SetActive(false);
            trankis.Stop();
            doom.Play();
            enabled = !enabled;
        }
    }
}
