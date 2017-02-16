using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {
	public float distance = 1f;
	public SimplePlayer1 Player;
	public float speed=2f;
	bool walljumping;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,distance);



        if (Input.GetButtonDown("Jump") && !Player.grounded && hit.collider != null)
        {
            Player.outsideForce = true;

            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
        }
        else if (Player.grounded && hit.collider == null && !Input.GetButton ("Jump"))
        {
            Player.outsideForce = false;
        }
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
	}
}
