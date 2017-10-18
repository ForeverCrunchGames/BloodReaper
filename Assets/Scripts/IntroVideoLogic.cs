using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroVideoLogic : MonoBehaviour {

    public MovieTexture movie;

    int states;

	void Start () 
    {
        movie.Play();
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
	}

    void Init()
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
            gameObject.SetActive(false);
            movie.Stop();
            states = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }

        if (movie.isPlaying == false)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            movie.Stop();
            states = 0;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
	}

    public void Skip()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        movie.Stop();
        states = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
