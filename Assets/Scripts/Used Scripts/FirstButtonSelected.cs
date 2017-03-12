using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstButtonSelected : MonoBehaviour {

    public EventSystem eventsystem;
    public GameObject selectedGameObject;

    void Start () 
    {
        eventsystem.firstSelectedGameObject = null;
        //GameObject obj = eventsystem.currentSelectedGameObject;
        //eventsystem.SetSelectedGameObject();
	}

    void OnEnable()
    {
        eventsystem.SetSelectedGameObject(selectedGameObject);
    }
	
	void Update () 
    {
		
	}
}
