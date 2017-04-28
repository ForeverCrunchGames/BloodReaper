using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeIndicator : MonoBehaviour {

    PlayerMOD player;

    MainCamera cam;

    public SinusMovmentLifeBar lifebarMov;

    Image thisImage;

    AudioSource beat;

    float delay = 0.4f;
    float counter;
    float wantedValue;
    float currentValue = 1;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();

        thisImage = GetComponent<Image>();

        beat = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!lifebarMov)
        {
            return;
        }

        if (currentValue < 0)
        {
            currentValue = 0;
        }

        wantedValue = (player.currentLife / player.maxLife);

        currentValue = Mathf.Lerp(currentValue, wantedValue, 5 * Time.deltaTime);

        thisImage.color = new Color(1, 1, 1, 0.75f - currentValue);

        lifebarMov.distanceY = 4 * (1 - currentValue);
        lifebarMov.velocityY = 20 * (1 - currentValue);

        beat.volume = 1 - currentValue;

        counter += Time.deltaTime;

        if (counter >= delay + currentValue * 1.5f)
        {
            beat.Play();
            counter = 0;
        }

        if (currentValue < 0.1)
        {
            cam.isShaking = true;
            cam.shakePower = 0.1f;
        }

        if (player.state == PlayerMOD.States.DEAD)
        {
            currentValue = 1;
            cam.isShaking = false;
        }
    }
}
