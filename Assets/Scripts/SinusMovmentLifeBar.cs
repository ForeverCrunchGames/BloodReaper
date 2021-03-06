﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusMovmentLifeBar : MonoBehaviour {

    [Range(0f, 100f)]
    public float velocityX = 1f;
    [Range(0f, 100f)]
    public float distanceX = 5f;
    [Range(0f, 100f)]
    public float delayX = 0;
    [Range(0f, 100f)]
    public float velocityY = 1f;
    [Range(0f, 100f)]
    public float distanceY = 5f;
    [Range(0f, 10f)]
    public float delayY = 0;

    public GameObject LifeBar;
   
	// Use this for initialization
	void Start () 
    {
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = LifeBar.transform.position + new Vector3(distanceX * Mathf.Sin(Time.time * velocityX + delayX), distanceY * Mathf.Sin(Time.time * velocityY +  delayY), 0.0f);
	}
}
