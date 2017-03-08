using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2DMOD))]
public class PlayerMOD : MonoBehaviour {

    public enum States { IDLE, RUN, JUMP, WALL_SLIDE, WALL_JUMP, ANGULAR_SLIDE, ATTACK, STRONG_ATTACK, PROTECT, DAMAGE, DEAD, PAUSE, SCORE }
    public States state;

    [Header("Physics")]
    public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	public float timeToWallUnstick;

    [Header("Stats")]
    public Vector3 spawn;

    public int deadCounter;
    public Text deadCounterText;

    public float maxLife = 100;
    public bool isLifeDecreasing = true;
    public float lifeDecreasingVelocity = 1; //Life unit decreasing per second
    public float currentLife;
    private float storeLife;

    public bool isPlayerOverpowered = true;

    public int playerScore;
    public GameObject scoreUI;
    public GameObject playerUI;
    public GameObject optionsUI;
    public bool pause;
    public Image lifeUI;
    public Text collectionableUI;
    public Text playerScoreUI;
    public bool isCollectionableCollected = false;
    public Renderer graphics;
    public bool isInmune;
    public float inmuneTime = 1;
    private Color color;
    public bool isLevelEnded;
    public bool isOptionsMenu;

    [Header("Attack")]
    public float damage;
    public bool isAttacking;
    public float attackTime;
    public GameObject attackBounds;

    public bool isStrongAttack = false;
    public float StrongAttackChargeTime = 1f;
    public bool isStrongAttackCharged = false;

    private float timer;
    private float timerIntro;

    [Header("Graphs")]
    public Transform graphicsTransform;
    public bool isFacingRight;

    [Header("Animations")]
    public Animator Player;
    public float animRunSensibility;

	float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

    Controller2DMOD controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

    int SlidingState = 0;

    public GameObject wallSlideParticles;
    public GameObject slideParticles;
    public GameObject runParticles;
    public GameObject DieParticles;
    public GameObject DieParticles2;

    //public Animator Intro;
    public bool isIntroEnded;
    public bool isGodModeOn;
    public GameObject UIgodMode;

    public AudioSource hit;
    public AudioSource sword;
    public AudioSource avraeScream;

	void Start() 
    {   
        //Calls
		controller = GetComponent<Controller2DMOD> ();

        //Player Init Stats
        spawn = transform.position;
        currentLife = maxLife;
        color = graphics.material.color;
        wallSlideParticles.SetActive(false);
        DieParticles.SetActive(false);
        DieParticles2.SetActive(false);
        pause = false;
        isLevelEnded = false;
        playerUI.SetActive(true);
        scoreUI.SetActive(false);
        optionsUI.SetActive(false);

        //Anims
        animRunSensibility = 5f;

        //Gravity start calculation
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() 
    {
		//AlwaysUpdate
        //////////////////////////////
        if (!pause)
        {
            if (!isLevelEnded)
            {
                Time.timeScale = 1;

                if (Input.GetKeyDown(KeyCode.F10))
                {
                    isGodModeOn = !isGodModeOn;
                }

                CalculateVelocity();

                if (isPlayerOverpowered == true)
                {
                    HandleWallSliding();
                }
                AttackLogic();

                AngularSliding();

                if (!isGodModeOn)
                {
                    LifeLogic();
                    UIgodMode.SetActive(false);

                }
                else
                {
                    UIgodMode.SetActive(true);

                }

                collectionableUI.text = ("Collectionable: " + isCollectionableCollected);
                playerScoreUI.text = ("Score: " + playerScore);
                deadCounterText.text = ("" + deadCounter);

                if (wallSliding)
                {
                    Player.SetBool("isWallSliding", true);
                    wallSlideParticles.SetActive(true);

                }
                else
                {
                    Player.SetBool("isWallSliding", false);
                    wallSlideParticles.SetActive(false);
                }

                //Flip
                if (controller.currentSlopeAngle == controller.maxSlopeAngle)
                {
                    if (!isFacingRight && velocity.x < 0)
                        Flip();
                    else if (isFacingRight && velocity.x > 0)
                        Flip();
                }

                //Pause
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    SetPause();
                }

                //Movment
                if (!isGodModeOn)
                {
                    controller.Move(velocity * Time.deltaTime, directionalInput);

                    if (controller.collisions.above || controller.collisions.below)
                    {
                        if (controller.collisions.slidingDownMaxSlope)
                        {
                            velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
                        }
                        else
                        {
                            velocity.y = 0;
                        }
                    }
                }
                else
                {
                    //GodMode Fly

                    float sensibility = 0.5f;
                    float sensibilityHigh = 2;

                    float currentSensibility;

                    if (Input.GetButton("Fire3"))
                    {
                        currentSensibility = sensibilityHigh;
                    }
                    else
                    {
                        currentSensibility = sensibility;
                    }

                    if (directionalInput.x < -0.5)
                    {
                        transform.Translate(-currentSensibility, 0, 0);
                    }
                    else if (directionalInput.x > 0.5)
                    {
                        transform.Translate(currentSensibility, 0, 0);
                    }

                    if (directionalInput.y < -0.5)
                    {
                        transform.Translate(0, -currentSensibility, 0);
                    }
                    else if (directionalInput.y > 0.5)
                    {
                        transform.Translate(0, currentSensibility, 0);
                    }
                }

                //Animation Controllers
                if (velocity.y > 0)
                {
                    Player.SetBool("VelocityUp", true);
                }
                else
                {
                    Player.SetBool("VelocityUp", false);
                }

                if (controller.collisions.below)
                {
                    Player.SetBool("isJumping", false);
                }
                else
                {
                    Player.SetBool("isJumping", true);
                }

                if (!isIntroEnded)
                {
                    timerIntro += Time.deltaTime;

                    if (timerIntro >= 2.7f)
                    {
                        timerIntro = 0;
                        //Intro.enabled = !Intro.enabled;
                        isIntroEnded = true;

                    }
                }
               
            }
            else if (isLevelEnded)
            {
                SetScore();
            }
        }
            
        //////////////////////////////

        switch (state)
        {
            case States.IDLE:
                {
                    UpdateIdle();
                    break;  
                }
            case States.RUN:
                {
                    UpdateRun();
                    break;  
                }
            case States.JUMP:
                {
                    UpdateJump();
                    break;  
                }
            case States.ATTACK:
                {
                    UpdateAttack();
                    break;  
                }
            case States.STRONG_ATTACK:
                {
                    UpdateStrongAttack();
                    break;  
                }
            case States.DAMAGE:
                {
                    //UpdateDamage();
                    break;  
                }
            case States.DEAD:
                {
                    UpdateDead();
                    break;  
                }
            case States.PAUSE:
                {
                    UpdatePause();
                    break;  
                }
            case States.SCORE:
                {
                    UpdateScore();
                    break;  
                }
            default:
                break;
        }
                
	}

    //SATES UPDATES
    //////////////////////////////////
    void SetIdle()
    {
        state = States.IDLE;
    }
    void UpdateIdle()
    {
        if (!((velocity.x <= animRunSensibility && velocity.x >= -animRunSensibility) || (controller.collisions.left == true) || (controller.collisions.right == true)))
        {
            SetRun();
        }
    }
    //------------------------------
    void SetRun()
    {
        state = States.RUN;
        Player.SetBool("isRunning", true);
    }
    void UpdateRun()
    {
        if ((velocity.x <= animRunSensibility && velocity.x >= -animRunSensibility) || (controller.collisions.left == true) || (controller.collisions.right == true))
        {
            Player.SetBool("isRunning", false);
            SetIdle();
        }
    }
    //------------------------------
    void SetJump()
    {
        state = States.JUMP;
        Player.SetBool("isJumping", true);
    }
    void UpdateJump()
    {
        
    }
    //------------------------------
    void UpdateAttack()
    {
    }
    //------------------------------
    void UpdateStrongAttack()
    {
    }
    //------------------------------
    public void SetDead()
    {
        state = States.DEAD;
        transform.position = spawn;
        currentLife = maxLife;
        DieParticles.SetActive(true);
        DieParticles2.SetActive(true);
        hit.Play();
        deadCounter += 1;
        avraeScream.Play();
    }
    void UpdateDead()
    {

        timer += Time.deltaTime;

        if (timer >= 1)
        {
            timer = 0;
            DieParticles.SetActive(false);
            DieParticles2.SetActive(false);
            SetIdle();
        }
    }
    //------------------------------
    void SetPause()
    {
        state = States.PAUSE;
        optionsUI.SetActive(true);
        //Time.timeScale = 0;
        pause = true;
        pauseTimer = 0;

        Debug.Log("Set Pause");
    }
    float pauseTimer;
    void UpdatePause()
    {
        pauseTimer += Time.deltaTime;

        if (pauseTimer > 0.05f)
        {
            Time.timeScale = 0;
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ExitPause();//ExitPause
            }
        }


        Debug.Log("Update Pause " + pauseTimer);
    }
    public void ExitPause()
    {
        state = States.IDLE;
        optionsUI.SetActive(false);
        Time.timeScale = 1;
        pause = false;
        pauseTimer = 0;

        Debug.Log("Exit Pause");
    }
    //------------------------------
    void SetScore()
    {
        state = States.SCORE;
        scoreUI.SetActive(true);
        playerUI.SetActive(false);
    }
    void UpdateScore()
    {
        if (Input.anyKey)
            {
                SceneManager.LoadScene ("Main menu");
            }
    }
    //////////////////////////////////

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		if (controller.collisions.below) {
			if (controller.collisions.slidingDownMaxSlope) {
				if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
					velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
					velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
				}
			} else {
				velocity.y = maxJumpVelocity;
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
        
    void AngularSliding()
    {
        //Debug Stats
        //Debug.Log("CurrentSlopeAngle: " + controller.currentSlopeAngle);
        //Debug.Log("isDescendingSlope: " + controller.collisions.descendingSlope);
        //Debug.Log("SlopeAngle: " + controller.collisions.slopeAngle);

        if (directionalInput.y < -0.1)
        {
            if (SlidingState == 0)
            {
                if (controller.collisions.slopeAngle == 45 && controller.collisions.descendingSlope)
                {
                    controller.currentSlopeAngle = controller.minSlopeAngle;
                    Player.SetBool("isSliding", true);
                    isAttacking = true;
                    slideParticles.SetActive(true);
                }
                else if (controller.collisions.slopeAngle != 45 && controller.currentSlopeAngle == controller.minSlopeAngle)
                {
                    SlidingState = 1;
                }
            }
            else if (SlidingState == 1)
            {
                controller.currentSlopeAngle = controller.maxSlopeAngle;
                Player.SetBool("isSliding", false);
                slideParticles.SetActive(false);
                isAttacking = false;
            }
        }
        else
        {
            SlidingState = 0;
            controller.currentSlopeAngle = controller.maxSlopeAngle;
            Player.SetBool("isSliding", false);
            slideParticles.SetActive(false);
        }
    }


    void LifeLogic()
    {
        //Draw Life UI
        lifeUI.fillAmount = currentLife / 100;

        if (isLifeDecreasing)
        {
            currentLife -= lifeDecreasingVelocity * Time.deltaTime;
        }

        //ClampMaxHealth
        if (currentLife >= maxLife)
        {
            currentLife = maxLife;
        }

        //Inmunity
        if (isInmune)
        {
            graphics.material.color = Color.red; 

            timer += Time.deltaTime;

            if (timer >= inmuneTime)
            {
                graphics.material.color = color;
                timer = 0;
                isInmune = false;
            }
        }

        //Die
        if (!isInmune)
        {
            if (currentLife <= 0)
            {
                SetDead();
            }
        }
    }

    public void RecieveDamage (float damage)
    {
        currentLife -= damage;
        isInmune = true;
        hit.Play();
    }


    public void StrongAttack()
    {

    }
    public void StrongAttackLogic()
    {
//        if (Input.GetButtonDown("Fire2"))
//        {
//            isStrongAttack = true;
//            Player.SetBool("isAttackStrong", true);
//
//            if (isStrongAttackCharged)
//            {
//            }
//            else
//            {
//                timer = +Time.deltaTime; 
//
//                if (timer >= StrongAttackChargeTime)
//                {
//                    isStrongAttackCharged = true;
//                    timer = 0;
//                }
//            }
//        }
//        else
//        {
//            if (isStrongAttackCharged)
//            {
//                Player.SetTrigger("AttackStrong");
//                isStrongAttack = false;
//                isStrongAttackCharged = false;
//            }
//
//            isStrongAttack = false;
//            Player.SetBool("isAttackStrong", false);
//            timer = 0;
//        }
    }

    public void Defense()
    {
    }

    public void Attack()
    {
        Debug.Log("CATAPUM!");
        Player.SetTrigger("AtackBasic");
        isAttacking = true;
        sword.Play();

    }
    public void AttackLogic()
    {
        if (isAttacking)
        {
            attackBounds.SetActive(true);

            timer += Time.deltaTime;

            if (timer >= attackTime)
            {
                isAttacking = false;
                timer = 0;
            }
        }
        else
        {
            attackBounds.SetActive(false);
        }
    }

    public void AddScore(int score)
    {
        playerScore = playerScore + score;
    }

    void Flip()
    {
        Vector3 newScale = graphicsTransform.localScale;
        newScale.x *= -1;
        graphicsTransform.localScale = newScale;

        isFacingRight = !isFacingRight;
    }
}
