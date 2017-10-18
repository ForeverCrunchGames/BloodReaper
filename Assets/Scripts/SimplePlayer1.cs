using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class SimplePlayer1 : MonoBehaviour {
	
    bool isDead;
	public GameObject restartText;
    private Rigidbody2D rb;
    public bool facingRight;
    public bool outsideForce;

	//Lives
	public int lives;
	public bool isImmune;
	public float immuneCounter;
	public float immuneTime;

	Animator anim;
	
	//SpeedVariables

    public float speed = 5f;
	public float maxSpeed = 8f;
    public float acceleration = 10f;
    float BaseSpeed;
	
	//JumpVariables
	public bool grounded;
	public Transform point1;
	public Transform point2;
	public LayerMask onlyGroundMask;
	public float jumpForce;

	public Vector3 initialPos;
	
	public float timer;
	bool canJump;
	public float maxTime = 0.1f;

	public GameObject jumpsParticles;

	void Start () {

		initialPos = new Vector3 (0, 2, 0);
		anim = GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {


		if (isImmune) {

			immuneCounter -= Time.deltaTime;

				}
		if (immuneCounter <= 0) {

			isImmune=false;
			immuneCounter=immuneTime;
			anim.SetBool("Immune",false);

		}


		if (lives <= 100) {
			
			if (isDead == true) 
			{
				restartText.SetActive (true);
				SceneManager.LoadScene ("Menu");
			}
		}
	


		if (!grounded && (Input.GetButtonDown ("Horizontal") || Input.GetButtonUp ("Horizontal")))
						BaseSpeed = 0;

		
		//MOVING CODE
		anim.SetFloat ("velocityY", rb.velocity.y);


        if (rb.velocity.x != 0)
            anim.SetBool("Moving", true);
        
        if (!outsideForce)
        {
            if (Input.GetButton("Horizontal"))
            {
                if (Input.GetAxis("Horizontal") > 0.1f)
                {
//                if ((rb.velocity.x < maxSpeed))
//                {
//                    rb.AddForce(new Vector2(acceleration, 0));
//                }
//                else
                    {
                        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed + BaseSpeed, rb.velocity.y);
                    }
                }

                if (Input.GetAxis("Horizontal") < -0.1f)
                {
//                if ((rb.velocity.x > -maxSpeed))
//                {
//                    rb.AddForce(new Vector2(-acceleration, 0));
//                }
//                else
                    {
                        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed + BaseSpeed, rb.velocity.y);
                    }
                }
            }
            else
            {
                anim.SetBool("Moving", false);
                rb.velocity = new Vector2(BaseSpeed, rb.velocity.y);
            }

			if (lives <= 0) {
			
				SceneManager.LoadScene ("Menu");

			}
        }

		//FLIP CODE
        if (!facingRight && rb.velocity.x < 0)
            Flip ();
        else if (facingRight && rb.velocity.x > 0)
            Flip ();


		//JUMPCODE
		grounded = Physics2D.OverlapArea (point1.position, point2.position, onlyGroundMask);
		
        if (grounded && Input.GetButtonDown("Jump")) 
        {
			timer = 0;
			canJump = true;
            rb.AddForce (new Vector2 (0, jumpForce * 4));

		} 
        else if (Input.GetButton ("Jump") && canJump && timer < maxTime) 
        {
			timer += Time.deltaTime;
            rb.AddForce (new Vector2 (0, jumpForce));
        } 
        else 
        {
				canJump = false;
				
        }
				anim.SetBool ("Grounded", grounded);




	}
		

	void OnTriggerStay2D(Collider2D other)
	{
				if (other.tag == "deadly" && !isDead && lives <=1 && !isImmune) {
						rb.velocity = Vector2.zero;
			lives=0;
						anim.SetBool ("Dies", true);
						rb.AddForce (new Vector2 (0, 500));
						isDead = true;
			
			
				} else if (other.tag == "deadly" && lives > 1 && !isImmune) {
			lives--;
			anim.SetBool("Immune",true);
			isImmune=true;
				}

	}


	
	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "deadly" && !isDead && lives <=1 && !isImmune) {
			rb.velocity = Vector2.zero;
			lives=0;
			anim.SetBool ("Dies", true);
			rb.AddForce (new Vector2 (0, 500));
			isDead = true;
			
			
		} else if (other.gameObject.tag == "deadly" && lives > 1 && !isImmune) {
			lives--;
			anim.SetBool("Immune",true);
			isImmune=true;
		}
		
	}





	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Platform")
            BaseSpeed = other.gameObject.GetComponent<Rigidbody2D>().velocity.x;
		else 
			BaseSpeed = 0;
		
	}

	void OnTriggerEnter2D (Collider2D other){
	

		if (other.tag == "Enemy") {

			lives = lives - 1;

			transform.position = new Vector3 (0, 2, 0);

		}

		if (other.tag == "Win") {

			SceneManager.LoadScene ("Menu");
	
		}
	}
	
	void OnCollisionExit2D(Collision2D other)
	{

	}

	void OnGUI()
    {
		GUILayout.Label ("  Lives: " + lives);

	}

    void Flip()
    {
        Vector3 newScale = this.transform.localScale;
        newScale.x *= -1;
        this.transform.localScale = newScale;

        facingRight = !facingRight;
    }

}
