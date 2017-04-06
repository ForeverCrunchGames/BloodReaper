using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    PlayerMOD player;
    LevelLogic levelLogic;

    public GameObject textObj;
    public GameObject finalStatsObj;
    public GameObject abilityObj;

    public bool isAbilityLearned;

    int scoreState;
    float counter;

    // Use this for initialization
	void Start () 
    {
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        textObj.SetActive(true);
        finalStatsObj.SetActive(false);
        abilityObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (scoreState == 0) //text
        {
            counter += Time.deltaTime;

            if (counter >= 2)
            {
                counter = 0;
                scoreState = 1;
                textObj.SetActive(false);
            }
        }
        else if (scoreState == 1) //Ability
        {
            if (isAbilityLearned)
            {
                abilityObj.SetActive(true);
                Cursor.visible = true;

                counter += Time.deltaTime;

                if (counter >= 2)
                {
                    counter = 0;
                    scoreState = 2;
                    abilityObj.SetActive(false);
                }
            }
            else
            {
                scoreState = 2;
            }
        }
        else if (scoreState == 2)
        {
            finalStatsObj.SetActive(true);
        }
	}

    public void GoMenu()
    {
        levelLogic.LoadScene(1);
    }

    public void NextLevel()
    {
        levelLogic.NextScene();
    }

    public void Restart()
    {
        levelLogic.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
