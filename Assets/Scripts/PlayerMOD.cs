using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof (Controller2DMOD))]
public class PlayerMOD : MonoBehaviour {

    Controller2DMOD controller;

    public enum States { IDLE, RUN, JUMP, WALL_SLIDE, WALL_JUMP, ANGULAR_SLIDE, ATTACK, STRONG_ATTACK, PROTECT, DAMAGE, DEAD, SCORE, GODMODE }
    public States state;

    public enum ScreenStates { GAME_RUNNING, GAME_PAUSED, GAME_INTRO, GAME_END, GODMODE, SCRIPTED }
    public ScreenStates screenState;

    [Header("Stats")]
    public Vector3 spawn;
    public int deadCounter;
    public bool isTimePaused;
    public float time;
    public float maxLife;
    public bool isLifeDecreasing;
    public bool isInmune;
    public float inmuneTime;
    private Color color;
    public float lifeDecreasingVelocity;
    public float currentLife;
    private float storeLife;
    private float secondaryLife;
    public float secondaryLifeSmooth;
    public int playerScore;
    public bool isCollectionableCollected;
    public bool isFacingRight;
    private float timer;
    private float deadTimer;
    public bool isScripted;
    public int jumpFrameDelay;

    [Header("Attacks")]
    public bool enableIntro;
    public float damage;
    public float cooldown;
    private bool isCooldown;
    private float cooldownCounter;
    public bool isAttacking;
    public float attackTime;
    public bool isPlayerOverpowered;
    public bool isPlayerHaveWallSlide;
    Vector2 directionalInput;
    bool wallSliding;
    int wallDirX;
    public bool isPlayerHaveAngularSlide;
    int SlidingState;
    public bool isAbilityLearned;
    public bool isRage;
    public float rageTime;
    public float rageVelocity;
    public float rageCounter;

    [Header("States")]
    public bool isLevelEnded;
    public bool isOptionsMenu;
    public bool isIntroEnded;
    public bool isGodModeOn;
    int scoreState;
    float scoreCounter;
    public bool isScoreScreen;

    [Header("Animation Controllers")]
    public bool introFallAnim;
    public float animRunSensibility;
    public bool isDeadAnim;
    public int deadState;
    private float jumpCounter;
    public float jumpTimeAnim;

    [Header("Player Physics")]
    public float maxJumpHeight;
    public float minJumpHeight;
    public float timeToJumpApex;
    public float accelerationTimeAirborne;
    public float accelerationTimeGrounded;
    public float moveSpeed;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public float wallSlideSpeedMax;
    public float wallStickTime;
    public float timeToWallUnstick;
    public float gravity;
    public float maxJumpVelocity;
    public float minJumpVelocity;
    public Vector3 velocity;
    float velocityXSmoothing;

    [Header("Definitions")]
    public Text deadCounterText;
    public Text timeText;
    public GameObject scoreUI;
    public GameObject playerUI;
    public GameObject optionsUI;
    public GameObject hitUI;
    public GameObject deadCounterUI;
    //public GameObject playerGraphics;
    public SkinnedMeshRenderer playerMeshRenderer;
    public SkinnedMeshRenderer swordMeshRenderer;
    public Image lifeUI;
    public Image lifeSecondaryUI;
    public GameObject UIgodMode;
    public GameObject collectionable;
    public Renderer graphics;
    public GameObject attackBounds;
    public Transform graphicsTransform;

    public GameObject splatterPrefab;

    public GameObject wallSlideParticles;
    public GameObject slideParticles;
    public GameObject DieParticles;
    public ParticleSystem rageParticles;
    public GameObject swordMesh;
    public GameObject lifeBar;
    public GameObject TimeScoreUI;
    public GameObject newAbility;
    public GameObject FlyingShip;

    public Texture2D LidricWell;
    public Texture2D LidricBad;

    public Animator Player;
    public Animator deadCounterAnim;
    public Animator playerManagerAnim;

    public BlurOptimized blurEffect;
    public AudioLowPassFilter lowpassFilter; 

    public AudioSource hit;
    public AudioSource sword;
    public AudioSource sword1;
    public AudioSource sword2;
    public AudioSource sword3;
    public AudioSource avraeScream;
    public AudioSource win;
    public AudioSource button;

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
        isLevelEnded = false;
        playerUI.SetActive(true);
        scoreUI.SetActive(false);
        optionsUI.SetActive(false);
        slideParticles.SetActive(false);
        TimeScoreUI.SetActive(true);
        newAbility.SetActive(false);
        lifeBar.SetActive(true);
        swordMesh.SetActive(false);
        playerManagerAnim.enabled = false;
        FlyingShip.SetActive(false);
        UIgodMode.SetActive(false);
        deadCounterUI.SetActive(false);
        deadCounter = 0;
        graphics.material.SetTexture("_MainTex",LidricWell);

        if (isPlayerOverpowered)
        {
            graphics.material.SetTexture("_MainTex",LidricBad);
            swordMesh.SetActive(true);
            isLifeDecreasing = true;
        }

        if (enableIntro == true)
        {
            screenState = ScreenStates.GAME_INTRO;
            playerManagerAnim.enabled = true;
            FlyingShip.SetActive(true);
            lifeBar.SetActive(false);
            TimeScoreUI.SetActive(false);
        }

        //Gravity start calculation
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);

        //Cursor
        Cursor.visible = false;
	}

	void Update() 
    {
        switch (screenState)
        {
            case ScreenStates.GAME_RUNNING:
                {
                    //PAUSE SWITCH
                    if (Input.GetKeyUp(KeyCode.Escape))
                    {
                        SetPause();
                    }

                    //GODMODE SWITCH
                    if (Input.GetKeyDown(KeyCode.F10))
                    {
                        SetGodMode();
                    }

                    CalculateVelocity();
                    LifeLogic();

                    //PLAYER POWERS LOGIC
                    if (isPlayerOverpowered == true)
                    {
                        AttackLogic();

//                        if (isRage)
//                        {
//                            moveSpeed = rageVelocity;
//                            rageCounter += Time.deltaTime;
//                            rageParticles.Play();
//
//                            if (rageCounter >= rageTime)
//                            {
//                                isRage = false;
//
//                                rageParticles.Stop();
//                                moveSpeed = 8;
//                            }
//                        }

                        if (isPlayerHaveWallSlide)
                        {
                            HandleWallSliding();

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
                        }

                        if (isPlayerHaveAngularSlide)
                        {
                            AngularSliding();
                        }
                    }

                    //UI TEXTS
                    timeText.text = ("" + (int)time);  
                    deadCounterText.text = ("" + deadCounter);

                    //COUNTDOWN
                    time += Time.deltaTime;

                    //PLAYER FLIP
                    if (controller.currentSlopeAngle == controller.maxSlopeAngle)
                    {
                        if (!isFacingRight && velocity.x < 0)
                            Flip();
                        else if (isFacingRight && velocity.x > 0)
                            Flip();
                    }

                    //PLAYER MOVMENT
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

                    //JUMP
                    if (Input.GetButtonDown("Jump"))
                    {
                        SetJump();
                    }

                    //CONTROLLERS
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

                    break;  
                }
            case ScreenStates.GODMODE:
                {
                    if (Input.GetKeyDown(KeyCode.F10))
                    {
                        SetGameRunning();
                        UIgodMode.SetActive(false);
                    }

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

                    break;
                }
            case ScreenStates.GAME_PAUSED:
                {
                    if (Input.GetKeyUp(KeyCode.Escape))
                    {
                        ExitPause();
                    }

                    break;
                }
            case ScreenStates.GAME_INTRO:
                {
                    if (introFallAnim == true)
                    {
                        Player.SetTrigger("IntroFall");
                    }

                    if (isIntroEnded)
                    {
                        playerManagerAnim.enabled = false;
                        FlyingShip.SetActive(false);
                        TimeScoreUI.SetActive(true);

                        if (isPlayerOverpowered)
                        {
                            lifeBar.SetActive(true);
                            deadCounterUI.SetActive(true);
                            isLifeDecreasing = true;
                            deadCounterUI.SetActive(true);
                            deadCounterAnim.SetTrigger("dead");
                            lifeBar.SetActive(true);
                            graphics.material.SetTexture("_MainTex",LidricBad);
                        }
                        SetGameRunning();
                    }

                    break;
                }
            case ScreenStates.GAME_END:
                {
                    break;
                }
            case ScreenStates.SCRIPTED:
                {
                    timeText.text = ("" + (int)time);  
                    deadCounterText.text = ("" + deadCounter);

                    break;
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
            case States.SCORE:
                {
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
    public void SetDead()
    {
        state = States.DEAD;
        screenState = ScreenStates.SCRIPTED;
    }
    void UpdateDead()
    {
        if (deadState == 0)
        {
            hit.Play();
            Player.SetTrigger("SetDead");
            isDeadAnim = true;
            graphics.material.color = Color.red;
            playerMeshRenderer.enabled = false;
            swordMeshRenderer.enabled = false;
            //playerGraphics.SetActive(false);
            Instantiate(splatterPrefab, transform.position, transform.rotation);

            deadState = 1;
        }
        else if (deadState == 1)
        {
            deadTimer += Time.deltaTime;

            if (deadTimer >= 0.8f)
            {
                playerMeshRenderer.enabled = true;
                swordMeshRenderer.enabled = true;

                transform.position = spawn;
                currentLife = maxLife;
                deadCounter += 1;

                avraeScream.Play();
                //playerGraphics.SetActive(true);
                deadCounterAnim.SetTrigger("dead");

                deadTimer = 0;
                deadState = 2;
            }
        }
        else if (deadState == 2)
        {
            deadTimer += Time.deltaTime;
            graphics.material.color = color;

            if (deadTimer >= 2f)
            {
                isDeadAnim = false;
                deadTimer = 0;
                deadState = 0;

                SetIdle();
                SetGameRunning();
            }
        }
    }

    public void SetScore()
    {
        state = States.SCORE;
        scoreUI.SetActive(true);
        playerUI.SetActive(false);
        isScoreScreen = true;
        win.Play();
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
            if (!isGodModeOn || !isInmune)
            {
                currentLife -= lifeDecreasingVelocity * Time.deltaTime;
            }
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
        ExitPause();
        SetDead();
        optionsUI.SetActive(false);
        Cursor.visible = false;
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
        if (screenState == ScreenStates.GAME_RUNNING)
        {
            if (isCooldown == true)
            {
            
                int random;

                //Random attack
                random = Random.Range(0, 3);

                if (random == 0)
                {
                    Player.SetTrigger("AtackBasic");
                    sword1.Play();

                }
                else if (random == 1)
                {
                    Player.SetTrigger("AtackBasic2");
                    sword2.Play();
                }
                else if (random == 2)
                {
                    Player.SetTrigger("AttackBasic3");
                    sword3.Play();
                }

                Debug.Log("Basic Attack");
                isAttacking = true;
                //sword.Play();
            }
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

    public void ButtonSound()
    {
        button.Play();
    }

    void Flip()
    {
        Vector3 newScale = graphicsTransform.localScale;
        newScale.x *= -1;
        graphicsTransform.localScale = newScale;

        isFacingRight = !isFacingRight;
    }

    public void SetPlayerOverpowered()
    {
        isPlayerOverpowered = true;
      
        swordMesh.SetActive(true);

        lifeBar.SetActive(true);
    }

    public void SetPlayerWallSlide()
    {
        isPlayerHaveWallSlide = true;
    }

    public void SetPlayerAngularSlide()
    {
        isPlayerHaveAngularSlide = true;
    }

    public void SetCollectionable()
    {
        isCollectionableCollected = true;
        collectionable.SetActive(true);
    }

    void SetGameRunning()
    {
        screenState = ScreenStates.GAME_RUNNING;
    }

    void SetGodMode()
    {
        screenState = ScreenStates.GODMODE;
        UIgodMode.SetActive(true);
    }

    public void SetPause()
    {
        screenState = ScreenStates.GAME_PAUSED;
        optionsUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        blurEffect.enabled = true;
        lowpassFilter.enabled = true;
        if (isPlayerOverpowered)
        {
            deadCounterAnim.SetBool("isPause", true);
        }
        if (isCollectionableCollected == true)
        {
            collectionable.SetActive(true);
        }

       
    }
    public void ExitPause()
    {
        SetGameRunning();
        optionsUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        blurEffect.enabled = false;
        lowpassFilter.enabled = false;
        if (isPlayerOverpowered)
        {
            deadCounterAnim.SetBool("isPause", false);
        }
        collectionable.SetActive(false);
    }
}
