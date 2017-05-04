using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {

    [Header("References")]
    public Text scoreUI;
    public Text scorePopUpUI;
    public GameObject PopUp;

    [Space]

    [Header("Score Table")]
    public int scoreEnemyMelee = 100;
    public int scoreBarricade = 100;
    public int scoreSpawner = 100;
    public int scoreCollectable = 100;

    [Space]

    [Header("Timers")]
    public float timePopUpActive = 1f;
    public float timeInterpolation = 1f;

    float scoreTotal = 0;
    float scoreCurrent = 0;
    float scorePopCurrent = 0;

    bool isAddScore;

    private int state;


    void Start () 
    {
        PopUp.SetActive(false);
        scoreUI.text = "" + (int)scoreCurrent;
	}
	
	void Update () 
    {
        scoreUI.text = "" + (int)scoreCurrent;
        scorePopUpUI.text = "+" + (int)scorePopCurrent;


        if (isAddScore)
        {
            if (state == 0)
            {
                PopUp.SetActive(true);
                StartCoroutine(WaitPopUp());
                state = 1;
            }
            else if (state == 1)
            {
               //Waiting Coroutine
            }
            else if (state == 2)
            {
                scorePopCurrent = Mathf.Lerp(scorePopCurrent, 0f, timeInterpolation);
                scoreCurrent = Mathf.Lerp(scoreCurrent, scoreTotal, timeInterpolation);

                if (scorePopCurrent < 1)
                {
                    scoreCurrent = scoreTotal;
                    scorePopCurrent = 0;
                    PopUp.SetActive(false);
                    isAddScore = false;
                    state = 0;
                }
            }
        }
	}

    public IEnumerator WaitPopUp()
    {
        yield return new WaitForSeconds(timePopUpActive);
        state = 2;

    }
     
    //SCORE TABLE
    /////////////////////////////////////////////////

    public void AddScoreEnemyMelee()
    {
        scoreTotal += (float)scoreEnemyMelee;
        scorePopCurrent += (float)scoreEnemyMelee;

        isAddScore = true;
    }

    public void AddScoreBarricade()
    {
        scoreTotal += (float)scoreBarricade;
        scorePopCurrent += (float)scoreBarricade;

        isAddScore = true;
    }

    public void AddScoreSpawner()
    {
        scoreTotal += (float)scoreSpawner;
        scorePopCurrent += (float)scoreSpawner;

        isAddScore = true;
    }

    public void AddScoreCollectable()
    {
        scoreTotal += (float)scoreCollectable;
        scorePopCurrent += (float)scoreCollectable;

        isAddScore = true;
    }
        
}
