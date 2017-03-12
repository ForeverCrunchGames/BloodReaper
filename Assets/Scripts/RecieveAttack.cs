using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveAttack : MonoBehaviour {

    public bool DamageRecived;

    // Use this for initialization
	void Start () 
    {
        DamageRecived = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Attack")
        {
            DamageRecived = true;
        }
    }
 
}
