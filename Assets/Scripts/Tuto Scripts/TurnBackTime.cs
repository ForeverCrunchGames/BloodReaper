using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnBackTime : MonoBehaviour {
	public float precision;
	public SimplePlayer0 sp0;
	float counter;
	float counter1;
	public float rate=.5f;

	Animator anim;
	public List<Vector3> posicoes;
	public List<Vector3> quats;
	int i;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.StartRecording(0);


	}
	
	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;

		if (counter <= 0) {
			counter=precision;
			posicoes.Add(transform.position);

				}
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			anim.StopRecording();
			anim.StartPlayback();
			anim.playbackTime=anim.recorderStartTime;
		}

	if (Input.GetKey (KeyCode.Backspace)) {
			sp0.enabled=false;
			if(anim.playbackTime<anim.recorderStopTime)
				anim.playbackTime+=Time.deltaTime;
			GetComponent<Rigidbody2D>().isKinematic=true;
			counter1-=Time.deltaTime;
			if (counter1 <= 0) {
				counter1=precision;
			//	transform.position =posicoes[i];
				i++;
			}
			transform.position = posicoes[i];



			//anim.speed=-.5f;
				}
		//if(anim.StartPlayback)
	}
}

