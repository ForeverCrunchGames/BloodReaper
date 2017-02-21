using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour 
{

    public GameObject startText;

    public GameObject titleScreen;
    public GameObject menu1;
    public GameObject menu2;
    public GameObject menu3;
    public GameObject menuTesting;

    float textCounter;
    public float startTextInterval = 1;
    public int State;

	void Start () 
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        State = 1;
        titleScreen.SetActive(true);

        menu1.SetActive(false);
        menu2.SetActive(false);
        menu3.SetActive(false);
        menuTesting.SetActive(false);
	}
	
	void Update () 
    {
        if (State == 1) //Title
        {
            if (Input.anyKey)
            {
                SetMenu1();
            }

            //Text poping
            /////////////
            textCounter += Time.deltaTime;

            if (textCounter >= startTextInterval * 2)
            {
                startText.SetActive(true);
                textCounter = 0;
            }
            else if (textCounter >= startTextInterval)
            {
                startText.SetActive(false);
            }
            /////////////
        }
        else if (State == 2) //Menu 1
        {
            
        }
        else if (State == 3) //Menu 2 (New Game or Select Level)
        {
            
        }        
        else if (State == 4) //Menu 3 (Select Level)
        {

        }
        else if (State == 5) //NewGame
        {

        }
        else if (State == 6) //Testing
        {

        }
	}

    public void SetMenu1 ()
    {
        State = 2;
        titleScreen.SetActive(false);
        menu1.SetActive(true);
    }
    public void BackToMenu1 ()
    {
        State = 2;
        menu1.SetActive(true);
        menu2.SetActive(false);
    } 
    public void SetMenu2 ()
    {
        State = 3;
        menu1.SetActive(false);
        menu2.SetActive(true);
    }
    public void BackToMenu2 ()
    {
        State = 3;
        menu2.SetActive(true);
        menu3.SetActive(false);
    } 
    public void SetMenu3 ()
    {
        State = 4;
        menu2.SetActive(false);
        menu3.SetActive(true);
    }
    public void SetTesting ()
    {
        State = 6;
        menu1.SetActive(false);
        menuTesting.SetActive(true);
    }
    public void BackTesting ()
    {
        State = 2;
        menu1.SetActive(true);
        menuTesting.SetActive(false);
    }

    public void GoLevel1_1 ()
    {
        SceneManager.LoadScene("Level 1.1", LoadSceneMode.Single);
    }
    public void GoLevel2_2 ()
    {
        SceneManager.LoadScene("Level 2.2", LoadSceneMode.Single);
    }
    public void GoPlayground ()
    {
        SceneManager.LoadScene("Playground", LoadSceneMode.Single);
    }    
    public void GoCombatArena ()
    {
        SceneManager.LoadScene("CombatArena", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
