using UnityEngine;
using System.Collections;

public class DoorScriptMod : MonoBehaviour {
	
    public Animator anim;
    public GameObject doorCollider;
    public GameObject key;
    public GameObject effect;
    public float elimColliderDelay;
    public float elimEffectDelay;

    [HideInInspector]
    public bool isKeyCollected = false;

    float counter;
    bool isDoorOpened = false;

    void Start()
    {
        key.SetActive(false);
        doorCollider.SetActive(true);
    }

    void Update()
    {
        if (isDoorOpened == true)
        {
            counter += Time.deltaTime;

            if (counter >= elimEffectDelay)
            {
                Destroy(effect);
            }

            if (counter >= elimColliderDelay)
            {
                Destroy(doorCollider);
                Destroy(key);
            }
        }
    }

	public void DoorOpens()
	{
        isDoorOpened = true;
        key.SetActive(true);
        anim.SetTrigger("OpenDoor");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isKeyCollected)
            {
                DoorOpens();
            }
        }
    }
}
