using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEmiterLogic : MonoBehaviour {

    public GameObject DustParticleSystem;
    public AudioSource impactSound;
    //public float frequency;
    //public float initDelay;

    //float counter;

	// Use this for initialization
	void Start () 
    {
        //counter = counter + initDelay;
	}
	
	// Update is called once per frame
	void Update () 
    {
//        counter += Time.deltaTime;
//
//        if (counter > frequency)
//        {
//            counter = 0;
//            impactSound.Play();
//            Instantiate(DustParticleSystem, this.transform.position, this.transform.rotation);
//        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Hazzard")
        {
            impactSound.Play();
            Instantiate(DustParticleSystem, this.transform.position, this.transform.rotation);
        }
        //}
    }
}
