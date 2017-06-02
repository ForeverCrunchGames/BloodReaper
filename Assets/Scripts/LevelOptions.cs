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
    public bool isIntroDisabled;

    [Header("Level Options")]
    public int levelSpawners;
    public int levelMaxDeaths;
    public int levelMaxTime;

    [Header("Saving Options")]
    public int levelNumber;

    [Header("Tiles Color")]
    public int tileColor;
    public Material tilesMaterial;
    public Texture2D tileColor0;
    public Texture2D tileColor1;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();

        if (isIntroDisabled == true)
        {
            player.isIntroEnded = true;
        }

        if (tileColor == 0)
        {
            tilesMaterial.SetTexture("_MainTex", tileColor0);
        }
        else if (tileColor == 1)
        {
            tilesMaterial.SetTexture("_MainTex", tileColor1);
        }

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
