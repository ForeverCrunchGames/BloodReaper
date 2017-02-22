using UnityEngine;
using System.Collections;

public class DoorTriggerMod : MonoBehaviour 
{

	public DoorScriptMod door;

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
        Destroy(gameObject);
    }
}
