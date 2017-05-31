using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDetector : MonoBehaviour {

    public AudioSource boom;
    public GameObject particleEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("detected");
            boom.Play();
            Instantiate(particleEffect);
        }
    }
}
