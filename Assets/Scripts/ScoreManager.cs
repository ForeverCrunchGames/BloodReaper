using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    PlayerMOD player;

    public GameObject textObj;
    public GameObject finalStatsObj;
    public GameObject abilityObj;

    public bool isAbilityLearned;

    int scoreState;
    float counter;

    // Use this for initialization
	void Start () 
    {
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
        SceneManager.LoadScene("Main menu");
        scoreState = 0;
        player.isScoreScreen = false;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        scoreState = 0;
        player.isScoreScreen = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        scoreState = 0;
        player.isScoreScreen = false;
    }
}
