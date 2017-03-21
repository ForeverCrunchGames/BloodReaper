using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour 
{
    PlayerMOD Player;
    ScoreSystem score;

    public GameObject hitPoint;
    public GameObject explosionParticles;
    public GameObject healParticles;
    public GameObject splashEffect;

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
    int spawnDirection; //0 left, 1 right, 2 intercalate
    int _direction = -1;
    public int counter = -1;


    // Use this for initialization
    void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        score = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();

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
                score.AddScoreSpawner();
                isDestroyedState = false;
                CancelInvoke();
            }

            if (isPlayerInside == true)
            {
                healParticles.SetActive(true);
                Player.currentLife += lifeRegenSpeed * Time.deltaTime;
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
            //Instanciate Prefab
            GameObject go = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            go.GetComponent<EnemyMeleeLogic> ().Init (this);
            go.transform.parent = transform;

            //Spawn direction
            if (spawnDirection == 0)
            {
            }
            else if (spawnDirection == 1)
            {
            }
            else
            {
                _direction *= -1;

                if (_direction == -1)
                {
                }       
            }
        }
        else if (counter >= maxEnemies)
            counter = maxEnemies;

    }

    public void LostOne()
    {
        counter--;
    }
}
