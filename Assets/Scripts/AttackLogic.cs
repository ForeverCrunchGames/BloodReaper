using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour {

    public AudioSource hit01;
    public AudioSource hit02;
    public AudioSource hit03;
    public GameObject lifeFlash;
    public AudioSource gainLife;
    public ParticleSystem swordLife;

    public int enemyMeleeLifeRestore;

    int random;

    PlayerMOD player;
    MainCamera cam;
    LifeIndicator lifeIndicator;
    public ScoreSystem score;
    public int state;
    public float wait = 0.1f;
    public float counter;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        lifeIndicator = GameObject.FindGameObjectWithTag("LifeIndicator").GetComponent<LifeIndicator>();
    }

    void Update()
    {
        if (state == 1)
        {
            counter += Time.unscaledDeltaTime;

            lifeIndicator.LifeGlowing();

            if (counter >= wait)
            {
                state = 0;
                Time.timeScale = 1;
                counter = 0;
                lifeFlash.SetActive(false);
            }


        }
        else
        {
            Time.timeScale = 1;
            lifeFlash.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyMelee")
        {
            if (state == 0)
            {
                other.GetComponentInParent<EnemyMeleeLogic>().SetDamage(1);

                //Blood sounds
                random = Random.Range(0, 3);
                if (random == 0)
                {
                    hit01.Play();
                }
                else if (random == 1)
                {
                    hit02.Play();
                }
                else if (random == 2)
                {
                    hit03.Play();
                }

                player.isRage = true;
                player.rageCounter = 0;

                player.currentLife += enemyMeleeLifeRestore;
                gainLife.Play();
                swordLife.Play();

                cam.isShaking = true;
                cam.shakeTime = 0.3f;
                cam.shakePower = 0.35f;

                lifeFlash.SetActive(true);
                state = 1;
                Time.timeScale = 0.2f;
            }
            else
            {
                //other.GetComponentInParent<EnemyMeleeLogic>().SetDamage(1);
                //player.currentLife += enemyMeleeLifeRestore;
                lifeFlash.SetActive(false);

//                random = Random.Range(0, 3);
//                if (random == 0)
//                {
//                    hit01.Play();
//                }
//                else if (random == 1)
//                {
//                    hit02.Play();
//                }
//                else if (random == 2)
//                {
//                    hit03.Play();
//                }
            }
                
          
            Debug.Log ("Attacked");
        }

        if (other.tag == "SpawnerHitPoint")
        {
            other.GetComponentInParent<SpawnerLogic>().isDestroyed = true;
            Debug.Log ("Spawn Destroyed");
        }
    }
        
}
