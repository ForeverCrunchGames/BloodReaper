using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour 
{
    public DoorScriptMod door;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (door.isKeyCollected)
            {
                door.DoorOpens();
                enabled = false;
            }
        }
    }
}
