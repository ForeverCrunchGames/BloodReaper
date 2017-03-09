using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeLogic : MonoBehaviour {

    public RecieveAttack recieveAttack;

    // Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (recieveAttack.DamageRecived == true)
        {
            //Start Break Animation/pysics

            //White a time

            //Dissolve

            //Destroy barricade
            Destroy(gameObject);
        }
	}
}
