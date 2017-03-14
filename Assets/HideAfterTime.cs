using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAfterTime : MonoBehaviour {

    public float time;
    private float _currentTime = 0;

	void Update () 
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= time)
        {
            gameObject.SetActive(false);
            _currentTime = 0;
        }
	}
}
