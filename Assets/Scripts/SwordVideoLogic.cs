using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordVideoLogic : MonoBehaviour {

    public PlayerMOD player;
    public MovieTexture movie;
    public OverpoweredTrigger trigger;

	void Start () 
    {
        movie.Play();
        Time.timeScale = 0;
	}
	
	void Update () 
    {
        if (movie.isPlaying == false)
        {
            Time.timeScale = 1;
            trigger.state = 1;
            Destroy(this.gameObject);
        }
	}
}
