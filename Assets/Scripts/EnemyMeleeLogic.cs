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
    public GameObject enemyBounds;
    public GameObject enemyExplosion;
    public GameObject enemySight;

    public float enemieVelocity = 1f;
    public float currentVelocity;
    public float rageVelocity = 5f;
    public float direction = 1;

    Rigidbody2D rb;

    [Header("Distances")]
    public Transform target;
    public float distanceFromTarget;
    public float huntRange;
    public float attackRange;

    public float damage = 10;

    public bool isPlayerDetected;

    public LayerMask obstacleMask;

    public bool isObstacle;
    public bool isFloor;
    public bool isGrounded;
    public float obstacleDisDet = 1; //Obstacle Distance Detection
    public float floorDisDet = 1; 
    public float groundDisDet = 1; 
    public float directionDelay;

    public SpawnerLogic _spawner;

    public Collider2D attack;

    // Use this for initialization
    void Start () 
    {
        Physics2D.queriesStartInColliders = true;
        currentVelocity = enemieVelocity;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        rb = GetComponent<Rigidbody2D>();
        enemyExplosion.SetActive(false);
    }

    public void Init(SpawnerLogic spawner){
        _spawner = spawner;
    }

    // Update is called once per frame
    void Update () 
    {
        rb.velocity = new Vector2 (currentVelocity, rb.velocity.y);

        isObstacle = Physics2D.Raycast (transform.position, new Vector2(direction, 0), obstacleDisDet, obstacleMask);
        isFloor = Physics2D.Raycast (new Vector2(transform.position.x + 1 * direction, transform.position.y),  Vector2.down, floorDisDet, obstacleMask);
        //isGrounded = Physics2D.Raycast (transform.position, Vector2.down, obstacleDisDet, obstacleMask);

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
                    //UpdateDead();
                    break;  
                }
            default:
                break;
        }
    }


    void UpdatePatrol()
    {
        if (isPlayerDetected == true)
        {
            SetHunt();
        }

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
            if (rb.velocity.x > 0)
            {
                direction *= -1;
                currentVelocity *= -1;
                enemyGraphics.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (target.position.x >= transform.position.x)
        {
            if (rb.velocity.x < 0)
            {
                direction *= -1;
                currentVelocity *= -1;
                enemyGraphics.transform.localScale = new Vector3(transform.localScale.x * 1, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (rb.velocity.x == 0)
        {
            currentVelocity = rageVelocity;
        }

        if (distanceFromTarget > huntRange)
        {
            SetIdle();
        }
    }
    void UpdateAttack()
    {

    }
    void UpdateDamage()
    {
    }

    void SetIdle()
    {
        currentVelocity = enemieVelocity;
        enemySight.SetActive(true);
        state = States.PATROL;
    }
    void SetHunt()
    {
        currentVelocity = rageVelocity;
        enemySight.SetActive(false);
        state = States.HUNT;
    }   
    void SetAttack()
    {
        state = States.ATTACK;
    }   
    public void SetDamage(int damage)
    {
        state = States.DAMAGE;
        enemyGraphics.SetActive(false);
        enemyBounds.SetActive(false);
        enemyExplosion.SetActive(true);
        _spawner.LostOne ();
        Destroy(gameObject, 0.5f);
    }   
    void SetDead()
    {
        state = States.DEAD;
    }     
    public void ChangeDirection()
    {
        direction *= -1;
        currentVelocity *= -1;
        enemyGraphics.transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Hazzard")
        {
            if (state != States.DAMAGE)
            {
                SetDamage(1000);
            }
        }
    }
}
