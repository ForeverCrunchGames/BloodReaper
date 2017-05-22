using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    PlayerMOD player;
    LevelLogic levelLogic;
    ScoreSystem scoreSystem;
    LevelOptions levelOptions;

    public GameObject textObj;
    public GameObject finalStatsObj;
    public GameObject abilityObj;

    public Text score;
    public Text deaths;
    public Text time;

    public Slider scoreSlider;
    public Slider deathsSlider;
    public Slider timeSlider;

    public Button cristalScore;
    public Button cristalDeaths;
    public Button cristalTime;

    public bool isAbilityLearned;

    int scoreState;
    float counter;

    // Use this for initialization
	void Start () 
    {
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();
        scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
        levelOptions = GameObject.FindGameObjectWithTag("LevelOptions").GetComponent<LevelOptions>();

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

            scoreSlider.value = (int)scoreSystem.scoreCurrent / player.levelSpawners;
            deathsSlider.value = levelOptions.levelMaxDeaths/ player.deadCounter;
            timeSlider.value = levelOptions.levelMaxTime/ player.time;

            if (scoreSlider.value >= 1)
            {
                cristalScore.interactable = true;
            }
            else cristalScore.interactable = false;

            if (deathsSlider.value >= 1)
            {
                cristalDeaths.interactable = true;
            }
            else cristalDeaths.interactable = false;

            if (timeSlider.value >= 1)
            {
                cristalTime.interactable = true;
            }
            else cristalTime.interactable = false;

            score.text = "" + (int)scoreSystem.scoreCurrent + "/" + player.levelSpawners;
            score.text = ("" + score);
            deaths.text = ("" + player.deadCounter);
            time.text = ("" + (int)player.time);
        }
	}

    public void GoMenu()
    {
        levelLogic.LoadScene(1);
    }

    public void NextLevel()
    {
        //levelLogic.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        levelLogic._loader.Load(levelLogic.nextScene);
    }

    public void Restart()
    {
        levelLogic.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
