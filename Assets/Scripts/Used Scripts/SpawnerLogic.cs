using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour {

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

	// Use this for initialization
	void Start () 
    {
        explosionParticles.SetActive(false);
        healParticles.SetActive(false);
        splashEffect.SetActive(true);
        hitPoint.SetActive(true);
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
	}
}
