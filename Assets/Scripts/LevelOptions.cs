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
    public bool isAbilityLearned;

    [Header("Level Options")]
    public int levelSpawners;
    public int levelMaxDeaths;
    public int levelMaxTime;

    [Header("Saving Options")]
    public int levelNumber;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();

        if (isSword == true)
        {
            player.isPlayerOverpowered = true;
            player.swordMesh.SetActive(true);
            player.isLifeDecreasing = true;
        }
        else
        {
            player.isLifeDecreasing = false;
        }

        if (isWallSlide == true)
        {
            player.isPlayerHaveWallSlide = true;
        }

        if (isAngularSlide == true)
        {
            player.isPlayerHaveAngularSlide = true;
        }

        if (isAbilityLearned == true)
            player.isAbilityLearned = true;
        else
        {
            player.isAbilityLearned = false;
        }

        player.levelSpawners = levelSpawners;

	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
