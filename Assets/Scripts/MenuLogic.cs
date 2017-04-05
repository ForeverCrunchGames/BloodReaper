﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuLogic : MonoBehaviour
{

	private AudioSource clic;
    public GameObject options;
    PlayerMOD player;

	public void Start ()
	{
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        clic = GetComponent<AudioSource>();
        options.SetActive(false);
    }

	public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

    public void  GoMenu()
    {
        player.ExitPause();
        Cursor.visible = true;
        SceneManager.LoadScene("Main menu");
    }

	public void PlaySound()
    {
		clic.Play ();
	}

    public void Options()
    {
        options.SetActive(true);
    }
        
}