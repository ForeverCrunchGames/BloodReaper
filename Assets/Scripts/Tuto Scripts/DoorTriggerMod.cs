using UnityEngine;
using System.Collections;

public class DoorTriggerMod : MonoBehaviour 
{

	public DoorScriptMod door;
    public int color; //1 blue, 2 magenta, 3 yellow;
    InventorySystem _inventory;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventorySystem>();
    }
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            KeyCollected();
        }
	}

    void KeyCollected()
    {
        door.isKeyCollected = true;
        _inventory.ItemColleted(color);
        Destroy(gameObject);
    }
}
