using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreDamageTrigger : MonoBehaviour {

    public CoreLogic coreLogic;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (coreLogic.isDestroyed)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Attack")
        {
            coreLogic.Damage();
        }
    }
}
