using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    MainCamera cam;

    [Header("New Camera Seetings")]
    public float maxZoom = 30;
    public float minZoom = 15;
    [Space]
    public float heightDamping = 1;
    public float widthDamping = 1;
    public float zoomDamping = 1;

	void Start () 
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            cam.maxZoom = maxZoom;
            cam.minZoom = minZoom;
            cam.heightDamping = heightDamping;
            cam.widthDamping = widthDamping;
            cam.zoomDamping = zoomDamping;
        }
    }
}
