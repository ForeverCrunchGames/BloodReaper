using UnityEngine;
using System.Collections;

public class DoorTriggerMod : MonoBehaviour 
{

	public DoorScriptMod door;
    public PlayerMOD player;

    public int score = 100;

	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            door.DoorOpens();
            gameObject.SetActive(false);
            Debug.Log("Collision");
            player.AddScore(score);
        }
	}
}
