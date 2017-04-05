using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour {

    public PlatformControllerMOD target1;
    public PlatformControllerMOD target2;

    // Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (target1 == true)
            {
                target1.enabled = !target1.enabled;
            }

            if (target2 == true)
            {
                target2.enabled = !target2.enabled;
            }

            Destroy(gameObject);
        }
    }
}
