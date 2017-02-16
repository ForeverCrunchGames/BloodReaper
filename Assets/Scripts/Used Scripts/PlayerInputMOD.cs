using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerMOD))]
public class PlayerInputMOD : MonoBehaviour {

	PlayerMOD player;

	void Start () {
        player = GetComponent<PlayerMOD> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown ("Jump")) {
			player.OnJumpInputDown ();
		}
        if (Input.GetButtonUp ("Jump")) {
			player.OnJumpInputUp ();
		}

        if (Input.GetButtonDown ("Fire1")) {
            player.Attack ();
        }

//        if (Input.GetButtonDown ("Fire2")) {
//            player.StrongAttack ();
//        }

        if (Input.GetButtonDown ("Fire3")) {
            player.Defense ();
        }
	}
}
