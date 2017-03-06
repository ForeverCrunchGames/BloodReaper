using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialStone : MonoBehaviour {

	//EXPLANATION: Detects if "player" enters the collider, inits an animation and show a gameobject, the same if the player exits the collider.

    private Animator animator;
    private bool isPlayerIn; 
    private int state;

    void Start () 
    {
        animator = GetComponent<Animator>();
        state = 0;
	}
	
	void Update () 
    {
        if (isPlayerIn)
        {
            if (state == 1)
            {
                animator.SetTrigger("open");
                state = 0;
            }
        }
        else
        {
            if (state == 1)
            {
                animator.SetTrigger("close");
                state = 0;
            }
        }
	}


    //Triggers
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = true;
            state = 1;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerIn = false;
            state = 1;

        }
    }
}
