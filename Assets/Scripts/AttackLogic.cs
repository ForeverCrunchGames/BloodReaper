using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour {

    public AudioSource hit01;
    public AudioSource hit02;
    public AudioSource hit03;

    int random;

    PlayerMOD player;
    MainCamera cam;
    public ScoreSystem score;
    public int state;
    public float wait = 0.01f;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyMelee")
        {
            if (state == 0)
            {
                other.GetComponentInParent<EnemyMeleeLogic>().SetDamage(1);

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

                cam.isShaking = true;
                cam.shakeTime = 0.2f;
                cam.shakePower = 0.1f;

                Time.timeScale = 0.0f;

                score.AddScoreEnemyMelee();
                player.currentLife += 5;
                state = 1;
                //StartCoroutine(Wait());
            } 
            else
            {
                state = 0;
                Time.timeScale = 1;
            }
          

           

            Debug.Log ("Attacked");
        }

        if (other.tag == "SpawnerHitPoint")
        {
            other.GetComponentInParent<SpawnerLogic>().isDestroyed = true;
            Debug.Log ("Spawn Destroyed");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(wait);
        state = 0;
    }
        
}
