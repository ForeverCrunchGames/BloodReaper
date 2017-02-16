using UnityEngine;
using System.Collections;

public class DoorTriggerMod : MonoBehaviour 
{

	public DoorScriptMod door;
    public PlayerMOD player;

	public bool ignoreTrigger;
    public int score = 100;

	void OnTriggerEnter2D(Collider2D other)
    {

		if (ignoreTrigger)
						return;

		if (other.tag == "Player")
						door.DoorOpens ();
        gameObject.SetActive(false);
        Debug.Log("Collision");
        player.AddScore(score);

	}
    
	void OnDrawGizmos()
	{
		if (!ignoreTrigger) {
			BoxCollider2D box = GetComponent<BoxCollider2D>();

			Gizmos.DrawWireCube(transform.position, new Vector2(box.size.x,box.size.y));

				}


	}
}
