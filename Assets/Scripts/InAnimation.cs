using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAnimation : MonoBehaviour {

    PlayerMOD player;

    public Animator animatoor;
    public GameObject ship;
    float counter;

	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();

        if (player.isIntroAnim)
        {
            ship.SetActive(true);
        }
        else
        {
            animatoor.enabled = !animatoor.enabled;
            ship.SetActive(false);
            player.isScripted = false;
            this.enabled = !this.enabled;
        }
	}
	
	void Update () 
    {
        counter += Time.deltaTime;

        if (counter >= 5)
        {
            animatoor.enabled = !animatoor.enabled;
            ship.SetActive(false);
            player.isScripted = false;
            this.enabled = !this.enabled;
        }
	}
}
