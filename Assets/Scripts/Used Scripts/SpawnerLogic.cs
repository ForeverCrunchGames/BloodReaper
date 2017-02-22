using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour 
{

    public PlayerMOD Player;

    public GameObject hitPoint;
    public GameObject explosionParticles;
    public GameObject healParticles;
    public GameObject splashEffect;
    public bool isDestroyed = false;
    public bool isDestroyedState = true;
    public bool isPlayerInside = false;
    public float regenSpeed = 5;
    public int score = 100;

    [Header("Spawn Config.")]
    public float spawnRate = 2;
    public Transform spawnPoint;
    public int maxEnemies = 5;
    public int spawnDirection; //0 left, 1 right, 2 intercalate
    public GameObject enemyPrefab;
    public GameObject[] enemiesArray;
    public int counter = -1;


	// Use this for initialization
	void Start () 
    {
        explosionParticles.SetActive(false);
        healParticles.SetActive(false);
        splashEffect.SetActive(true);
        hitPoint.SetActive(true);

        InvokeRepeating("Spawn", spawnRate, spawnRate);
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
                splashEffect.SetActive(false);
                Player.AddScore(score);
                isDestroyedState = false;
                CancelInvoke();
            }

            if (isPlayerInside == true)
            {
                healParticles.SetActive(true);
                Player.currentLife += regenSpeed * Time.deltaTime;
                Player.spawn = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            }
            else
            {
                healParticles.SetActive(false);
            }
        }
        else
        {
            
        }
	}

    void Spawn()
    {
        counter = counter + 1;

        if (counter < maxEnemies)
        {
            enemiesArray[counter] = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else if (counter >= maxEnemies)
            counter = maxEnemies;

    }
}
