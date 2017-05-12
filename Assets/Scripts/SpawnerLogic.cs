using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour 
{
    PlayerMOD Player;
    //ScoreSystem score;

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

        explosionParticles.SetActive(false);
        //healParticles.SetActive(false);
        healParticles.Stop();
        hitPoint.SetActive(true);
        counter = maxEnemies;

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
                //score.AddScoreSpawner();
                isDestroyedState = false;
                CancelInvoke();
            }

            if (isPlayerInside == true)
            {
                //healParticles.SetActive(true);
                healParticles.Play();
                Player.currentLife += lifeRegenSpeed * Time.deltaTime;
                Player.spawn = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            }
            else
            {
                //healParticles.SetActive(false);
                healParticles.Stop();
            }
        }
        else
        {
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
            GameObject go = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
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
