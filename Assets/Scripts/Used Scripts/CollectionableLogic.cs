using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionableLogic : MonoBehaviour {

    public PlayerMOD Player;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.isCollectionableCollected = true;
            gameObject.SetActive(false);
        }
    }
}
