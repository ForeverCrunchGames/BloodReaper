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

    MainCamera mainCamera;


    public int states;

	// Use this for initialization
	void Start () 
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        coreCurrentLifes = coreLifes;
        coreWell.SetActive(true);
        coreDestroyed.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (coreCurrentLifes <= 0)
        {
            DestroyCore();
            states = 1;
        }

        if (isLastCore && isDestroyed)
        {
            
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
            mainCamera.shakeTime = 0.3f;
            mainCamera.shakePower = 0.2f;
        }

    }

    public void DestroyCore()
    {
        if (states == 1)
        {
            boxCollider.enabled = false;
            coreWell.SetActive(false);
            coreDestroyed.SetActive(true);
            isDestroyed = true;
            //mainCamera.isShaking = true;
            //mainCamera.shakeTime = 1f;
            //mainCamera.shakePower = 0.3f;
            states = 2;
        }
        else if (states == 2)
        {
        }
    }

    public void FinalExplosion()
    {
        
    }
}
