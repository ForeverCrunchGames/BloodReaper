using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroVideoLogic : MonoBehaviour {

    public MovieTexture movie;

	void Start () 
    {
        movie.Play();
        Time.timeScale = 0;
	}
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }

        if (movie.isPlaying == false)
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
	}
}
