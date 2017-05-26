using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    PlayerMOD player;
    LevelLogic levelLogic;
    //ScoreSystem scoreSystem;
    LevelOptions levelOptions;

    public GameObject textObj;
    public GameObject finalStatsObj;
    public GameObject abilityObj;
    public GameObject allChristals;

    public Text score;
    public Text deaths;
    public Text time;

    public Slider scoreSlider;
    public Slider deathsSlider;
    public Slider timeSlider;

    public Button cristalScore;
    public Button cristalDeaths;
    public Button cristalTime;

    int christalCount;
    int scoreState;
    float counter;

    // Use this for initialization
	void Start () 
    {
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();
        //scoreSystem = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScoreSystem>();
        levelOptions = GameObject.FindGameObjectWithTag("LevelOptions").GetComponent<LevelOptions>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        textObj.SetActive(true);
        finalStatsObj.SetActive(false);
        abilityObj.SetActive(false);
        allChristals.SetActive(false);
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
            if (levelOptions.isAbilityLearned)
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

                deaths.text = "" + player.deadCounter;
                time.text = "" + (int)player.time;

                finalStatsObj.SetActive(true);

                if (player.deadCounter == 0)
                {
                    player.deadCounter = 1;
                }

                if (player.isCollectionableCollected)
                {
                    scoreSlider.value = 1;
                }
                else
                {
                    scoreSlider.value = 0;
                }

                deathsSlider.value = levelOptions.levelMaxDeaths / player.deadCounter;
                timeSlider.value = levelOptions.levelMaxTime / player.time;

                if (scoreSlider.value >= 1)
                {
                    cristalScore.interactable = true;
                    christalCount++;
                }
                else cristalScore.interactable = false;

                if (deathsSlider.value >= 1)
                {
                    cristalDeaths.interactable = true;
                    christalCount++;
                }
                else cristalDeaths.interactable = false;

                if (timeSlider.value >= 1)
                {
                    cristalTime.interactable = true;
                    christalCount++;
                }
                else cristalTime.interactable = false;

                if (cristalScore.interactable == true && cristalDeaths.interactable == true && cristalTime.interactable == true)
                {
                    allChristals.SetActive(true);
                }

                if (player.isCollectionableCollected)
                    score.text = "YES!";
                else
                {
                    score.text = "NO";
                }

                if (levelOptions.levelNumber == 1)
                {
                    levelLogic.isLvl1Done = true;
                    levelLogic.lvl1Christals = christalCount;
                }
                else if (levelOptions.levelNumber == 2)
                {
                    levelLogic.isLvl2Done = true;
                    levelLogic.lvl2Christals = christalCount;
                }


            }
        }
        else if (scoreState == 2)
        {
            
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
