using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PlayerMOD))]
public class EnemyMeleeLogic : MonoBehaviour 
{
    public enum States { PATROL, HUNT, DAMAGE, DEAD }
    public States state;

    public PlayerMOD player;

    public Animator enemyAnim;

    public GameObject explosionPrefab;
    public GameObject splatterPrefab;
    public GameObject splatterPrefabDecal;

    public Transform spawnPoint;

    public GameObject enemyGraphics;
    public GameObject enemyBounds;
    public GameObject enemySight;

    public float enemieVelocity = 1f;
    public float currentVelocity;
    public float rageVelocity = 5f;
    public float direction = 1;

    Rigidbody2D rb;

    [Header("Distances")]
    public Transform target;
    public float distanceFromTarget;
    public float distanceFromTargetY;
    public float huntRange;
    public float attackRange;

    public float damage = 10;

    public bool isPlayerDetected;

    public LayerMask obstacleMask;

    public bool isObstacle;
    public bool isFloor;
    public bool isJumpable;
    public bool isEnemie;
    public bool isGrounded;
    public float obstacleDisDet = 1; //Obstacle Distance Detection
    public float floorDisDet = 1; 
    public float currentDirectionDelay;
    public float directionDelay;
    public float jumpDelay;
    public float jumpTimer;
    public bool isJumpActive;

    public SpawnerLogic _spawner;

    public Collider2D attack;

    public bool changeDirection;

    // Use this for initialization
    void Start () 
    {
        Physics2D.queriesStartInColliders = true;
        currentVelocity = enemieVelocity;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        rb = GetComponent<Rigidbody2D>();

        directionDelay = 0.3f;
        currentDirectionDelay = directionDelay;

        jumpTimer = jumpDelay;
        isJumpActive = true;
        jumpDelay = 0.4f;
        isGrounded = true;
        huntRange = 5;
    }

    public void Init(SpawnerLogic spawner){
        _spawner = spawner;
    }

    // Update is called once per frame
    void Update () 
    {
        if (changeDirection == true)
        {
            ChangeDirection();
            changeDirection = false;
        }

        isObstacle = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(direction, 0), obstacleDisDet, obstacleMask);
        isFloor = Physics2D.Raycast (new Vector2(transform.position.x + 1 * direction, transform.position.y),  Vector2.down, floorDisDet, obstacleMask);
        isGrounded = Physics2D.Raycast (new Vector2(transform.position.x + 0.5f * direction, transform.position.y),  Vector2.down, 1.5f, obstacleMask);
        isJumpable = Physics2D.Raycast (new Vector2(transform.position.x, transform.position.y + 2), new Vector2(direction, 0), obstacleDisDet, obstacleMask);

        //Blood effect direction
        if (player.isFacingRight == false)
        {
            spawnPoint.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (player.isFacingRight == true)
        {
            spawnPoint.transform.rotation = new Quaternion(0, 0, 180, 0);
        }

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
            case States.DAMAGE:
                {
                    UpdateDamage();
                    break;  
                }
            default:
                break;
        }
    }


    void UpdatePatrol()
    {
        rb.velocity = new Vector2 (currentVelocity, rb.velocity.y);

        if (isPlayerDetected == true && isGrounded)
        {
            SetHunt();
        }

        if (isObstacle && isJumpActive)
        {
            if (!isJumpable && isGrounded)
            {
                rb.AddForce(new Vector2(0, 700));
                isJumpActive = false;
                jumpTimer = 0;
            }
            else
            {
                ChangeDirection();
            }

        }
        else if (isJumpActive == false)
        {
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpDelay)
            {
                isJumpActive = true;
            }
        }
            
        if (!isFloor)
        {
            currentDirectionDelay -= Time.deltaTime;

            if (currentDirectionDelay <= 0)
            {
                ChangeDirection();
                currentDirectionDelay = directionDelay;
            }
        }

        //Bug patch
        /*
        if (direction == -1 && currentVelocity > 0)
        {
            ChangeDirection();
        }

        if (direction == 1 && currentVelocity < 0)
        {
            ChangeDirection();
        }
        */
    }

    void UpdateHunt()
    {
        rb.velocity = new Vector2 (rageVelocity, rb.velocity.y);

        distanceFromTarget = Vector3.Distance(transform.position, target.position);
        distanceFromTargetY = Mathf.Abs(transform.position.y - target.position.y);


        if (isObstacle && isJumpActive)
        {
            if (!isJumpable && isGrounded)
            {
                rb.velocity = new Vector2 (currentVelocity, rb.velocity.y);
                rb.AddForce(new Vector2(0, 750));
                isJumpActive = false;
                jumpTimer = 0;
            }
        }
        else if (isJumpActive == false)
        {
            rb.velocity = new Vector2 (currentVelocity, rb.velocity.y);
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpDelay)
            {
                isJumpActive = true;
            }
        }

        if (target.position.x > transform.position.x)
        {
            if (direction == -1 && isGrounded)
            {
                ChangeDirectionRage();
            }
        }
        else if (target.position.x < transform.position.x)
        {
            if (direction == 1 && isGrounded)
            {
                ChangeDirectionRage();
            }
        }

        //Bug patch
        /*
        if (direction == -1 && currentVelocity > 0)
        {
            ChangeDirectionRage();
        }

        if (direction == 1 && currentVelocity < 0)
        {
            ChangeDirectionRage();
        }
        */

        if (distanceFromTarget > huntRange)
        {
            if (distanceFromTargetY > huntRange / 2)
            {
                SetIdle();
            }
        }

    }
    void UpdateDamage()
    {
        Destroy(gameObject);
    }

    void SetIdle()
    {
        state = States.PATROL;

        enemyAnim.SetBool("isRunning", false);
    }
    void SetHunt()
    {
        state = States.HUNT;

        enemyAnim.SetBool("isRunning", true);

        if (direction == -1)
        {
            rageVelocity *= -1;
        }
    }

    public void SetDamage(int damage)
    {
        state = States.DAMAGE;
        Instantiate(explosionPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(splatterPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(splatterPrefabDecal, spawnPoint.position, transform.rotation);

        _spawner.LostOne ();
    } 

    public void ChangeDirection()
    {
        direction *= -1;
        currentVelocity *= -1;
        enemyGraphics.transform.localScale = new Vector3(transform.localScale.x * direction, transform.localScale.y, transform.localScale.z);
    }

    public void ChangeDirectionRage()
    {
        direction *= -1;
        rageVelocity *= -1;
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
