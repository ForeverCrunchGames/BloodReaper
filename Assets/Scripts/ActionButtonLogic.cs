using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonLogic : MonoBehaviour {

    public enum States {ACTIVE, PRESSED, COOLDOWN, DISABLED}
    public States state;

    PlayerMOD player;

    public  Image cooldownRenderer;

    public Color PressedColor;
    public Color CooldownColor;
    public Color DisabledColor;

    private float counter;
    public float pressedTime;

    public bool isDisabled;

    private Image thisRenderer;

    // Use this for initialization
	void Start () 
    {
        thisRenderer = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        thisRenderer.color = Color.white;
	}
	
	void Update () 
    {
        switch (state)
        {
            case States.ACTIVE:
                {
                    //Quan s'inizialitza resposta sonora + popup.

                    if (Input.GetButtonDown ("Fire1"))
                    {
                        thisRenderer.color = PressedColor;
                        state = States.PRESSED;
                    }

                    break;  
                }
            case States.PRESSED:
                {
                    if (counter >= pressedTime)
                    {
                        counter = player.cooldown;
                        thisRenderer.color = CooldownColor;
                        state = States.COOLDOWN;
                    }
                    else
                    {
                        counter += Time.deltaTime;
                    }

                    break;  
                }
            case States.COOLDOWN:
                {

                    if (counter > 0)
                    {
                        counter -= Time.deltaTime;
                        cooldownRenderer.fillAmount = counter / player.cooldown; 
                    }
                    else
                    {
                        thisRenderer.color = Color.white;
                        state = States.ACTIVE;

                    }

                    //Si es pitxe mentres esta en cooldown ementre soroll de error i petit moviment

                    break;  
                }
            case States.DISABLED:
                {
                    thisRenderer.color = DisabledColor;
                    break;  
                }
            default:
                break;
        }
	}
}
