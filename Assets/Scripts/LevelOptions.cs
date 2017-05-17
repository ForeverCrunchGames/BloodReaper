using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOptions : MonoBehaviour 
{
    PlayerMOD player;

    [Header("Player Options")]
    public bool isSword;
    public bool isWallSlide;
    public bool isAngularSlide;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();

        if (isSword == true)
        {
            player.isPlayerOverpowered = true;
            player.swordMesh.SetActive(true);
        }

        if (isWallSlide == true)
        {
            player.isPlayerHaveWallSlide = true;
        }

        if (isAngularSlide == true)
        {
            player.isPlayerHaveAngularSlide = true;
        }

	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
