using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    GameObject playerObj;
    PlayerMOD player;


    public GameObject handle;
    public Animator anim;

    // Use this for initialization
	void Start () 
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.transform.parent = handle.transform;
            anim.SetTrigger("Exit");
            anim.SetBool("TrapDoor_Close", true);
            anim.SetBool("Bridge_Open", true);
            anim.SetBool("Wings_Open", true);
            anim.SetFloat("HelicesVelocity", 3);
            player.isScripted = true;
            player.SetScore();
            Cursor.visible = true;
        }
    }
}
