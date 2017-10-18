using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLogic : MonoBehaviour {

    public bool isLastCore;
    public int coreLifes;
    public int coreCurrentLifes;
    public bool isDestroyed;
    public Collider2D boxCollider;
    public GameObject damageEffect;
    public GameObject coreWell;
    public GameObject coreDestroyed;
    public AudioSource hit;
    public AudioSource destroyed;

    MainCamera mainCamera;
    FinalExplosionLogic finalExplosion;


    public int states;

	// Use this for initialization
	void Start () 
    {
        if (isLastCore)
        {
            finalExplosion = GameObject.FindGameObjectWithTag("FinalExplosion").GetComponent<FinalExplosionLogic>();
        }

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        coreCurrentLifes = coreLifes;
        coreWell.SetActive(true);
        coreDestroyed.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (coreCurrentLifes < 0)
        {
            DestroyCore();

            if (isLastCore)
            {
                finalExplosion.isExplosion = true;
            }
        }
	}

    public void Damage()
    {
        if (states == 0)
        {
            coreCurrentLifes--;
            Instantiate(damageEffect, transform.position, transform.rotation);
            hit.Play();
            mainCamera.isShaking = true;
            mainCamera.shakeTime = 1f;
            mainCamera.shakePower = 0.25f;
        }

    }

    public void DestroyCore()
    {
        if (states == 0) //Init
        {
            boxCollider.enabled = false;
            coreWell.SetActive(false);
            coreDestroyed.SetActive(true);
            destroyed.Play();
            isDestroyed = true;
            states = 1;
        }
        else if (states == 1) //Preliminars
        {
            
        }
    }

    public void FinalExplosion()
    {
        
    }
}
