using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

    public AudioSource music;

    bool activated;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
      
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            if (other.tag == "Player")
            {
                music.Play();
                activated = true;
            }
        }
    }
}
