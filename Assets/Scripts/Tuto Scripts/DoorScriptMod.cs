using UnityEngine;
using System.Collections;

public class DoorScriptMod : MonoBehaviour {
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DoorOpens()
	{
		//anim.SetBool ("Opens", true);
        gameObject.SetActive(false);
		}

//	void CollEnable()
//	{
//		GetComponent<Collider2D>().enabled = true;
//	}
//
//	void CollDisable()
//	{
//		GetComponent<Collider2D>().enabled = false;
//	}



}
