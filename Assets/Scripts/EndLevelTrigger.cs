using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    PlayerMOD player;
    MainCamera cam;

    public GameObject handle;
    public Animator anim;

    int state;

    // Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state == 1)
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

            //Ocultar player, emparentar a la nau, allunyar zoom, iniciar anim nau +  volar

            //Esperar

            //New abitie unlocked!

            //Stats
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (state == 0)
            {
                state = 1;
            }
        }
    }
}
