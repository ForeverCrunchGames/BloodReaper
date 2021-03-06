﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour 
{
    PlayerMOD Player;
    //ScoreSystem score;
    LifeIndicator lifeIndicator;

    public GameObject hitPoint;
    public GameObject explosionParticles;
    public ParticleSystem healParticles;

    public bool isDestroyed = false;
    [HideInInspector]
    public bool isDestroyedState = true;
    [HideInInspector]
    public bool isPlayerInside = false;

    [Header("Spawner Config.")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnRate = 2;
    public float lifeRegenSpeed = 10;
    public int maxEnemies = 5;
    public int spawnDirection; //0 left, 1 right, 2 intercalate, 3 random
    //int _direction = -1;
    public int counter = -1;


    // Use this for initialization
    void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        //score = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
        lifeIndicator = GameObject.FindGameObjectWithTag("LifeIndicator").GetComponent<LifeIndicator>();

        explosionParticles.SetActive(false);
        //healParticles.SetActive(false);
        healParticles.Stop();
        hitPoint.SetActive(true);
        counter = maxEnemies;

        InvokeRepeating("Spawn", spawnRate, spawnRate);

        if (isDestroyed)
        {
            if (isDestroyedState)
            {
                hitPoint.SetActive(false);
                isDestroyedState = false;
                CancelInvoke();
            }
        }
    }

    // Update is called once per frame
    void Update () 
    {
        if (isDestroyed)
        {
            if (isDestroyedState)
            {
                explosionParticles.SetActive(true);
                hitPoint.SetActive(false);
                //score.AddScoreSpawner();
                isDestroyedState = false;
                CancelInvoke();
            }

            if (isPlayerInside == true)
            {
                healParticles.Play();
                Player.currentLife += lifeRegenSpeed * Time.deltaTime;
                lifeIndicator.LifeGlowing();
                Player.spawn = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            }
            else
            {
                healParticles.Stop();
            }
        }
    }
        
    void Spawn()
    {

        if (counter >= maxEnemies)
        {
            counter = maxEnemies;
        }

        if (counter <= 0)
        {
            counter = 0;
        }
        else
        {
            //Instanciate Prefab
            GameObject go = Instantiate(enemyPrefab, new Vector3(spawnPoint.position.x,spawnPoint.position.y,spawnPoint.position.z + Random.Range(-3, 3)), spawnPoint.rotation);
            go.GetComponent<EnemyMeleeLogic> ().Init (this);
            go.transform.parent = transform;
            counter--;

            //Spawn direction
            if (spawnDirection == 0)
            {
                go.GetComponent<EnemyMeleeLogic> ().changeDirection = true;
            }
            else if (spawnDirection == 1)
            {
            }
            else if (spawnDirection == 2)
            {   
                if (counter % 2 == 0)
                {
                    go.GetComponent<EnemyMeleeLogic> ().changeDirection = true;
                }
            }
        }

    }

    public void LostOne()
    {
        counter++;
    }
}
