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

    public bool isTimePaused = false;
    private float time;
    public Text timeUI;

    public float maxLife = 100;
    public bool isLifeDecreasing = true;
    public float lifeDecreasingVelocity = 1; //Life unit decreasing per second
    public float currentLife;
    private float storeLife;
    private float secondaryLife;
    public float secondaryLifeSmooth = 2;

    public bool isPlayerOverpowered = true;

    public int playerScore;
    public GameObject scoreUI;
    public GameObject playerUI;
    public GameObject optionsUI;
    public GameObject hitUI;
    public bool pause;
    public Image lifeUI;
    public Image lifeSecondaryUI;
    //public Text playerScoreUI;
    public bool isCollectionableCollected = false;
    public GameObject collectionable;
    public Renderer graphics;
    public bool isInmune;
    public float inmuneTime = 1;
    private Color color;
    public bool isLevelEnded;
    public bool isOptionsMenu;

    [Header("Attack")]
    public float damage;
    public float cooldown;
    private bool isCooldown;
    private float cooldownCounter;
    public bool isAttacking;
    public float attackTime;
    public GameObject attackBounds;

    public bool isStrongAttack = false;
    public float StrongAttackChargeTime = 1f;
    public bool isStrongAttackCharged = false;

    private float timer;
    private float deadTimer;
    private float timerIntro;

    [Header("Graphs")]
    public Transform graphicsTransform;
    public bool isFacingRight;

    [Header("Animations")]
    public Animator Player;
    public float animRunSensibility;

    public Animator deadCounterAnim;

    public bool isIntroAnim;
    public bool isDeadAnim;
    public int deadState;

	float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

    private float jumpCounter;
    public float jumpTimeAnim;

    Controller2DMOD controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;

    int SlidingState = 0;

    public GameObject wallSlideParticles;
    public GameObject slideParticles;
    public GameObject runParticles;
    public GameObject DieParticles;
    public GameObject swordMesh;

    //public Animator Intro;
    public bool isIntroEnded;
    public bool isGodModeOn;
    public GameObject UIgodMode;

    public AudioSource hit;
    public AudioSource sword;
    public AudioSource avraeScream;

    public Transform upperBody;


    public bool isScripted;
    public bool isPlayerHaveWallSlide;
    public bool isPlayerHaveAngularSlide;
    public bool isAbilityLearned;

    public GameObject lifeBar;
    public GameObject TimeScoreUI;
    public GameObject newAbility;

    int scoreState;
    float scoreCounter;
    public bool isScoreScreen;

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
        pause = false;
        isLevelEnded = false;
        playerUI.SetActive(true);
        scoreUI.SetActive(false);
        optionsUI.SetActive(false);
        slideParticles.SetActive(false);
        TimeScoreUI.SetActive(false);
        newAbility.SetActive(false);
        lifeBar.SetActive(false);

        //Gravity start calculation
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

        //Cursor
        Cursor.visible = false;
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
                    if (isPlayerHaveWallSlide)
                    {
                        HandleWallSliding();
                    }

                    swordMesh.SetActive(true);

                    AttackLogic();

                    if (isPlayerHaveAngularSlide)
                    {
                        AngularSliding();
                    }
                }
                else
                {
                    swordMesh.SetActive(false);
                    lifeBar.SetActive(false);
                }

                if (!isGodModeOn)
                {
                    LifeLogic();
                    UIgodMode.SetActive(false);

                }
                else
                {
                    UIgodMode.SetActive(true);
                }

                //Collectionable
                if (isCollectionableCollected)
                {
                    collectionable.SetActive(true);
                }

                //Time
                if (!isTimePaused)
                {
                    if (isIntroEnded)
                    {
                        time += Time.deltaTime;
                    }
                }

                //UI TEXTS
                timeUI.text = ("" + (int)time);  
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

                if (isScoreScreen)
                {
                    pause = false;
                }

                //Movment
                if (!isGodModeOn)
                {
                    if (!isScripted)
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
                }
                else if (isGodModeOn) 
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

                if (Input.GetButtonDown("Jump"))
                {
                    SetJump();
                }

                if (velocity.y > 0)
                {
                    Player.SetBool("VelocityUp", true);
                }
                else
                {
                    Player.SetBool("VelocityUp", false);
                }

                //Detect floor
                if (controller.collisions.below)
                {
                    Player.SetBool("isJumping", false);
                    jumpCounter = 0;
                }
                else
                {
                    jumpCounter += Time.deltaTime;

                    if (jumpTimeAnim < jumpCounter)
                    {
                        Player.SetBool("isJumping", true);
                    }   
                }


                //INTRO
                if (!isIntroEnded)
                {
                    timerIntro += Time.deltaTime;

                    if (timerIntro >= 5)
                    {
                        timerIntro = 0;
                        isIntroEnded = true;
                        TimeScoreUI.SetActive(true);

                        if (isLifeDecreasing)
                        {
                            lifeBar.SetActive(true);
                        }
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
        Player.SetBool("isRunning", false);
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
            SetIdle();
        }
            
    }
    //------------------------------
    void SetJump()
    {
        state = States.JUMP;
        Player.SetTrigger("JumpPush");
    }
    void UpdateJump()
    {
        if (Player.GetBool("isJumping") == false)
        {
            SetIdle();
        }
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
    }
    void UpdateDead()
    {
        if (deadState == 0)
        {
            hit.Play();
            Player.SetTrigger("SetDead");
            isDeadAnim = true;
            isScripted = true;

            deadState = 1;
        }
        else if (deadState == 1)
        {
            //deadAnimTime
            deadTimer += Time.deltaTime;

            if (deadTimer >= 0.8f)
            {
                transform.position = spawn;
                currentLife = maxLife;
                deadCounter += 1;

                avraeScream.Play();
                deadCounterAnim.SetTrigger("dead");

                deadTimer = 0;
                deadState = 2;
            }
        }
        else if (deadState == 2)
        {
            deadTimer += Time.deltaTime;

            if (deadTimer >= 2f)
            {
                isDeadAnim = false;
                deadTimer = 0;
                deadState = 0;
                isScripted = false;

                SetIdle();
            }
        }
    }
    //------------------------------
    void SetPause()
    {
        state = States.PAUSE;
        optionsUI.SetActive(true);
        pause = true;
        pauseTimer = 0;
        Cursor.visible = true;

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
        Cursor.visible = false;

        Debug.Log("Exit Pause");
    }
    //------------------------------
    public void SetScore()
    {
        state = States.SCORE;
        scoreUI.SetActive(true);
        playerUI.SetActive(false);
        isScoreScreen = true;
    }
    void UpdateScore()
    {
//        if (scoreState == 0)
//        {
//            if (isAbilityLearned)
//            {
//                newAbility.SetActive(true);
//                Cursor.visible = true;
//
//                scoreCounter += Time.deltaTime;
//
//                if (scoreCounter > 1)
//                {
//                    if (Input.anyKey)
//                    {
//                        scoreCounter = 0;
//                        scoreState = 1;
//                    }
//                }
//            }
//            else
//            {
//                scoreState = 1;
//            }
//        }
//        else if (scoreState == 1)
//        {
//            scoreUI.SetActive(true);
//        }
//
//
//        if (Input.anyKey)
//            {
//                SceneManager.LoadScene ("Main menu");
//            }
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
        lifeUI.fillAmount = currentLife/maxLife;
        lifeSecondaryUI.fillAmount = secondaryLife/maxLife;

        //Life descreasing
        if (isLifeDecreasing)
        {
            currentLife -= lifeDecreasingVelocity * Time.deltaTime;
        }

        //ClampMaxHealth
        if (currentLife >= maxLife)
        {
            currentLife = maxLife;
        }
        //ClampSecondaryLife
        if (secondaryLife < currentLife)
        {
            secondaryLife = currentLife;
        }

        //Inmunity
        if (isInmune)
        {
            hitUI.SetActive(true);

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

        //Secondary Life Smooth Follow
        secondaryLife = Mathf.Lerp(secondaryLife, currentLife, secondaryLifeSmooth * Time.deltaTime);
    }

    public void RecieveDamage (float damage)
    {
        currentLife -= damage;
        isInmune = true;
        hit.Play();
    }

    public void Respawn()
    {
        SetDead();
        optionsUI.SetActive(false);
        Time.timeScale = 1;
        pause = false;
        pauseTimer = 0;
        Cursor.visible = false;

        Debug.Log("Exit Pause");
    }

    public void StrongAttack()
    {

    }
    public void StrongAttackLogic()
    {
    }

    public void Defense()
    {
    }

    public void Attack()
    {
        if (isCooldown == true)
        {
            
            int random;

            //Random attack
            random = Random.Range(0, 3);

            if (random == 0)
            {
                Player.SetTrigger("AtackBasic");
                //Player.Play("Attack01")
            }
            else if (random == 1)
            {
                Player.SetTrigger("AtackBasic2");
            }
            else if (random == 2)
            {
                Player.SetTrigger("AttackBasic3");
            }

            Debug.Log("Basic Attack");
            isAttacking = true;
            sword.Play();
        }

    }
    public void AttackLogic()
    {
        if (cooldownCounter >= cooldown)
        {
            isCooldown = true;

            if (isAttacking)
            {
                attackBounds.SetActive(true);

                timer += Time.deltaTime;

                if (timer >= attackTime)
                {
                    isAttacking = false;
                    cooldownCounter = 0;
                    timer = 0;
                }
            }
            else
            {
                attackBounds.SetActive(false);
            }
        }
        else
        {
            cooldownCounter += Time.deltaTime;
            isCooldown = false;
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    void Flip()
    {
        Vector3 newScale = graphicsTransform.localScale;
        newScale.x *= -1;
        graphicsTransform.localScale = newScale;

        isFacingRight = !isFacingRight;
    }
}
