using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordVideoLogic : MonoBehaviour {

    public PlayerMOD player;
    public MovieTexture movie;
    public OverpoweredTrigger trigger;

    int states;

	void Start () 
    {
        movie.Play();
        Time.timeScale = 0;
	}
	
	void Update () 
    {
        if (states == 0)
        {
            movie.Play();
            states = 1;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            trigger.state = 1;
            gameObject.SetActive(false);
            movie.Stop();
            states = 0;
        }

        if (movie.isPlaying == false)
        {
            Time.timeScale = 1;
            trigger.state = 1;
            gameObject.SetActive(false);
            movie.Stop();
            states = 0;
        }
	}
}
