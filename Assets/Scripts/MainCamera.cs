using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainCamera : MonoBehaviour
{

    PlayerMOD player;

    [Header("Target")]
	public Transform target;

    [Space]

    [Header("Camera Seetings")]
	public float height = 5.0f;
    public float maxZoom = 30;
    public float minZoom = 15;
    public float velocityToZoom = 5;
    [Space]
	public float heightDamping = 1;
    public float widthDamping = 1;
    public float zoomDamping = 1;

    ////////////////////////
    float currentAngle;
    float wantedAngle;

    float currentTargetX;
    float currentTargetY;

    float currentZoom = 15;
    float wantedZoom;
    [Space]

    [Header("Shake")] 
    public bool isShaking;
    public float shakeTime;
    float shakeCounter;
    public float shakePower;
   

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
    }

	void LateUpdate() 
	{
		if (!target)
			return;

        var wantedHeight = currentTargetY + height;
        var wantedWidth = currentTargetX;

        var currentHeight = transform.position.y;
		var currentWidth = transform.position.x;

        var wantedAngle = currentTargetY;

        if (Mathf.Abs(player.velocity.x) > velocityToZoom)
        {
            wantedZoom = maxZoom;
        }
        else
        {
            wantedZoom = minZoom;
        }

        currentZoom = Mathf.Lerp(currentZoom, wantedZoom, zoomDamping * Time.deltaTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentWidth = Mathf.Lerp(currentWidth, wantedWidth, widthDamping * Time.deltaTime);

        currentTargetX = Mathf.Lerp(currentTargetX, target.position.x, 3 * Time.deltaTime);
        currentTargetY = Mathf.Lerp(currentTargetY, target.position.y, 3 * Time.deltaTime);

        currentAngle = Mathf.Lerp(currentAngle, wantedAngle, 3 * Time.deltaTime);

        transform.position = new Vector3(currentWidth, currentHeight, -currentZoom);

        transform.LookAt(new Vector3(currentTargetX, currentTargetY, 0));

        if (isShaking)
        {
            if (shakeTime > shakeCounter)
            {
                transform.localPosition = new Vector3(currentWidth, currentHeight, -currentZoom) + Random.insideUnitSphere * shakePower;

                shakeCounter += Time.deltaTime;
            }
            else
            {
                shakeCounter = 0f;
                isShaking = false;
            }
        }
	}
}