﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExplosionLogic : MonoBehaviour {

    public bool isExplosion;
    public bool isRunnerStarted;
    public bool isRunnerRestarted;
    public GameObject explosionBall;
    public float explosionRange;
    public float explosionVelocity;
    public float preExplosionVelocity;

    public float preliminarTime;

    public GameObject preExplosionEffect;
    public GameObject preExplosionEffect2;

    public int explosionStates;

    PlayerMOD player;
    MainCamera cam;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        explosionRange = 0;
        preExplosionEffect.SetActive(false);
        preExplosionEffect2.SetActive(false);
        explosionBall.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isExplosion)
        {
            if (player.state == PlayerMOD.States.DEAD)
            {
                isRunnerRestarted = true;
            }

            if (explosionStates == 0)
            {
                cam.isShaking = true;
                cam.shakeTime = 4f;
                cam.shakePower = 0.5f;
                preExplosionEffect.SetActive(true);
                explosionBall.SetActive(true);
                explosionStates = 1;
            }
            else if (explosionStates == 1)
            {
                preliminarTime -= Time.deltaTime;

                explosionRange += preExplosionVelocity * Time.deltaTime;
                explosionBall.transform.localScale = new Vector3(explosionRange, explosionRange, explosionRange);

                if (preliminarTime <= 0)
                {
                    explosionStates = 2;
                    preExplosionEffect2.SetActive(true);
                    cam.isShaking = true;
                    cam.shakeTime = 1f;
                    cam.shakePower = 1f;

                }
            }
            else if (explosionStates == 2)
            {
                cam.isShaking = true;
                cam.shakeTime = 1f;
                cam.shakePower = 0.15f;

                explosionRange += explosionVelocity * Time.deltaTime;
                explosionBall.transform.localScale = new Vector3(explosionRange, explosionRange, explosionRange);
            }
        }

        if (isRunnerRestarted)
        {
            Reset();
            isRunnerRestarted = false;
        }
	}

    public void Reset()
    {
        explosionRange = 0;
        explosionStates = 0;
        preExplosionEffect.SetActive(true);
        preExplosionEffect2.SetActive(false);
        explosionBall.SetActive(true);
        preliminarTime = 3;
    }
}