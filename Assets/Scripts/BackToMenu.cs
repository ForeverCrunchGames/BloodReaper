using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {

    LevelLogic levelLogic;

	// Use this for initialization
	void Start () 
    {
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void GoMenu()
    {
        levelLogic.LoadScene(1);
    }
}
