using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionableLogic : MonoBehaviour {

    PlayerMOD Player;
    ScoreSystem score;

    public GameObject explosionPrefab;

    // Use this for initialization
	void Start () 
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        score = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Instantiate(explosionPrefab, transform.position, transform.localRotation); 
            Player.isCollectionableCollected = true;
            score.AddScoreCollectable();
            Destroy(gameObject);
        }
    }
}
