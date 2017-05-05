using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    PlayerMOD player;
    GameObject playerObj;
    MainCamera cam;

    public AudioSource shipOut;

    public GameObject handle;
    public Animator anim;
    public Animator anim2;


    int state;

    // Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        playerObj = GameObject.FindGameObjectWithTag("PlayerGraphics");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state == 1)
        {
            player.SetScore();
            player.isLifeDecreasing = false;
            player.transform.parent = handle.transform;
            anim2.SetTrigger("exit");
            anim.SetBool("TrapDoor_Close", true);
            anim.SetBool("Bridge_Open", true);
            anim.SetBool("Wings_Open", true);
            anim.SetFloat("HelicesVelocity", 3);
            player.isScripted = true;
            Cursor.visible = true;
            cam.minZoom = 200;
            cam.maxZoom = 200;
            cam.velocityToZoom = 2;
            state = 2;
            playerObj.SetActive(false);
            shipOut.Play();
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
