 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverpoweredTrigger : MonoBehaviour {

    //public AudioSource trankis;
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
            //trankis.Stop();
            doom.Play();

//            player.maxJumpHeight = 4;
//            player.gravity = -(2 * player.maxJumpHeight) / Mathf.Pow (player.timeToJumpApex, 2);
//            player.maxJumpVelocity = Mathf.Abs(player.gravity) * player.timeToJumpApex;
//            player.minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (player.gravity) * player.minJumpHeight);


            Destroy(gameObject);
        }
    }
}
