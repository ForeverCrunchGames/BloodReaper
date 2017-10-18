using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionableUI : MonoBehaviour {

    PlayerMOD player;


	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player.isCollectionableCollected)
        {
            
        }
	}
}
