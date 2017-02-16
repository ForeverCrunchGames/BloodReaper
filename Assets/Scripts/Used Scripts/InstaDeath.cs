using System.Collections;
using UnityEngine;

public class InstaDeath : MonoBehaviour {

    public bool ignoreTrigger;

    public PlayerMOD Player;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (ignoreTrigger)
            return;

        if (other.tag == "Player")
        {
            Player.SetDead();

            Debug.Log("Insta Death");
        }

    }
}
