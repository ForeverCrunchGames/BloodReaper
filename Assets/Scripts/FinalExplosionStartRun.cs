using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalExplosionStartRun : MonoBehaviour {

    public FinalExplosionLogic explosionLogic;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (explosionLogic.explosionStates == 1)
            {
                explosionLogic.preliminarTime = 0;
                explosionLogic.explosionRange = 30;
            }
        }
    }
}
