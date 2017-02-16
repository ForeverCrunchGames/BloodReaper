using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PlayerMOD))]
public class EnemyMeleeLogic : MonoBehaviour 
{
    public enum States { PATROL, HUNT, ATTACK, DAMAGE, DEAD }
    public States state;

    public PlayerMOD player;

    public GameObject enemyGraphics;

    public float velocity = 1f;
    public float currentVelocity;
    public float rageVelocity = 5f;
    public float direction = 1;

    Rigidbody2D rb;

	public Transform sightStart;
	public Transform sightEnd;

    [Header("Distances")]
    public Transform target;
    public float distanceFromTarget;
    public float huntRange;
    public float attackRange;

    public float damage = 10;

    public bool isPlayerDetected;

    public LayerMask obstacleMask;

	//public Transform weakness;

    public bool isObstacle;
	public bool isFloor;
    public bool isGrounded;
    public float obstacleDisDet = 1; //Obstacle Distance Detection
    public float floorDisDet = 1; 
    public float groundDisDet = 1; 
    public float directionDelay;

	//Animator anim;

	// Use this for initialization
	void Start () 
    {
		//anim = GetComponent<Animator> ();
		Physics2D.queriesStartInColliders = true;
        currentVelocity = velocity;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		rb.velocity = new Vector2 (currentVelocity, rb.velocity.y);

        isObstacle = Physics2D.Raycast (transform.position, new Vector2(direction, 0), obstacleDisDet, obstacleMask);
        isFloor = Physics2D.Raycast (new Vector2(transform.position.x + 1 * direction, transform.position.y),  Vector2.down, obstacleDisDet, obstacleMask);
        isGrounded = Physics2D.Raycast (transform.position, Vector2.down, obstacleDisDet, obstacleMask);

        switch (state)
        {
            case States.PATROL:
                {
                    UpdatePatrol();
                    break;  
                }
            case States.HUNT:
                {
                    UpdateHunt();
                    break;  
                }
            case States.ATTACK:
                {
                    UpdateAttack();
                    break;  
                }
            case States.DAMAGE:
                {
                    UpdateDamage();
                    break;  
                }
            case States.DEAD:
                {
                    UpdateDead();
                    break;  
                }
            default:
                break;
        }
    }
        

    void UpdatePatrol()
    {

        if (isObstacle)
        {
            directionDelay += Time.deltaTime;

            if (directionDelay >= 0.2)
            {
                ChangeDirection();
                directionDelay = 0;
            }
        }

        if (!isFloor)
        {
            directionDelay += Time.deltaTime;

            if (directionDelay >= 0.2)
            {
                ChangeDirection();
                directionDelay = 0;
            }
        }
    }

    void UpdateHunt()
    {
        distanceFromTarget = Vector3.Distance(transform.position, target.position);

        if (target.position.x < transform.position.x)
        {
            if (currentVelocity > 0)
            {
                this.transform.Rotate(new Vector3(0,180,0));
                currentVelocity *= -1;
            }
        }
        else
        {
            if (currentVelocity <= 0)
            {
                this.transform.Rotate(new Vector3(0,180,0));
                currentVelocity *= -1;
            }
        }

        if (distanceFromTarget > huntRange)
        {
            SetIdle();
        }
        if (distanceFromTarget < attackRange)
        {
            SetAttack();
        }
    }
    void UpdateAttack()
    {

    }
    void UpdateDamage()
    {
        gameObject.SetActive(false);
    }
    void UpdateDead()
    {
        this.gameObject.SetActive(false);
    }

    void SetIdle()
    {
        currentVelocity = velocity;
        state = States.PATROL;
    }
    void SetHunt()
    {
        currentVelocity = rageVelocity;
        state = States.HUNT;
    }   
    void SetAttack()
    {
        state = States.ATTACK;
    }   
    public void SetDamage(int hit)
    {
        state = States.DAMAGE;
    }   
    void SetDead()
    {
        state = States.DEAD;
    }     

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player.isInmune == false)
            {
                player.RecieveDamage(damage);
            }
        }
    }

	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
	}

    void ChangeDirection()
    {
        direction *= -1;
        currentVelocity *= -1;
        enemyGraphics.transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

//	void OnCollisionEnter2D(Collision2D col)
//	{
//		if (col.gameObject.tag == "Player") 
//        {
//			float height = col.contacts[0].point.y - weakness.position.y;
//
//			if(height>0)
//			{
//				Dies();
//				col.rigidbody.AddForce(new Vector2(0,300));
//			}
//		}	
//	}

//	void Dies()
//	{
//		//anim.SetBool ("stomped", true);
//        Destroy (this.gameObject, 0.5f);
//		//gameObject.tag = "neutralized";
//
//	}
  
}
