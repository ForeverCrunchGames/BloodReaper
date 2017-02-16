using UnityEngine;
using System.Collections;

public class wallSlide : MonoBehaviour {

	public float distance = 0.4f;
	public SimplePlayer1 Player;
    public Rigidbody2D rb;
	public float speed= -3;
	public float gravScale =1f;

	public bool onlyDown;


	// Use this for initialization
	void Start () 
    {
		
    }

	// Update is called once per frame
	void Update () 
    {
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.right*transform.localScale.x,distance);

		if (onlyDown) 
        {
            if (!Player.grounded && hit.collider != null && rb.velocity.y < speed) 
            {
                rb.velocity = new Vector2 (0, speed);
            }
		} 
	    else 
        {
			if (!Player.grounded && hit.collider != null) 
            {
				rb.gravityScale = gravScale;
			}
			else
			{
				rb.gravityScale = 3;	
			}
		}

	}
	
}
