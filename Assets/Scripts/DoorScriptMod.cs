using UnityEngine;
using System.Collections;

public class DoorScriptMod : MonoBehaviour {
	
    public Animator anim;
    public GameObject doorCollider;
    public GameObject key;
    public float elimColliderDelay;
    public int color; //1 blue, 2 magenta, 3 yellow;
    InventorySystem _inventory;

    [HideInInspector]
    public bool isKeyCollected = false;

    float counter;
    bool isDoorOpened = false;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
        key.SetActive(false);
        doorCollider.SetActive(true);
    }

    void Update()
    {
        if (isDoorOpened == true)
        {
            counter += Time.deltaTime;

            if (counter >= elimColliderDelay)
            {
                Destroy(doorCollider);
                enabled = !enabled;
            }
        }
    }

	public void DoorOpens()
	{
        isDoorOpened = true;

        _inventory.ItemUsed(color);

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
