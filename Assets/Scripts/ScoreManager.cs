using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    PlayerMOD player;
    LevelLogic levelLogic;
    LevelOptions levelOptions;

    public GameObject textObj;
    public GameObject allChristals;

    public Animator animator;

    public AudioSource finalMusic;

    public Text score;
    public Text deaths;
    public Text time;

    public Image scoreSlider;
    public Image deathsSlider;
    public Image timeSlider;

    public GameObject cristalScore;
    public GameObject cristalDeaths;
    public GameObject cristalTime;

    int videoNumber;
    public GameObject video1;
    public GameObject video2;

    int christalCount;
    int scoreState;
    float counter;

    int videostate;

    // Use this for initialization
	void Start () 
    {
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();
        levelOptions = GameObject.FindGameObjectWithTag("LevelOptions").GetComponent<LevelOptions>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        textObj.SetActive(true);
        allChristals.SetActive(false);

        player.isLifeDecreasing = false;

        videoNumber = levelOptions.videoNumber;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (scoreState == 0) //text
        {
            counter += Time.deltaTime;

            if (counter >= 4)
            {
                counter = 0;
                scoreState = 1;
                textObj.SetActive(false);
                animator.SetTrigger("In1");
            }
        }
        else if (scoreState == 1)
        {
            if (videoNumber == 1)
            {
                video1.SetActive(true);
                scoreState = 2;
            }
            else if (videoNumber == 2)
            {
                video2.SetActive(true);
                scoreState = 2;
            }
            else if (videoNumber == 0)
            {
                counter += Time.deltaTime;
                if (counter >= 2)
                {
                    scoreState = 3;
                    counter = 0;
                    finalMusic.Play();
                }
            }
        }
        else if (scoreState == 2)
        {
            if (videoNumber == 1)
            {
                if (video1.activeSelf == false)
                {
                    scoreState = 3;
                }
            }
            else if (videoNumber == 2)
            {
                if (video2.activeSelf == false)
                {
                    scoreState = 3;
                }
            }
        }
        else if (scoreState == 3)
        {
            animator.SetTrigger("In2");

            finalMusic.Play();

            scoreState = 4;

            deaths.text = "" + player.deadCounter;
            time.text = "" + (int)player.time;

            if (player.deadCounter == 0)
            {
                player.deadCounter = 1;
            }

            if (player.isCollectionableCollected)
            {
                scoreSlider.fillAmount = 1;
            }
            else
            {
                scoreSlider.fillAmount = 0;
            }

            deathsSlider.fillAmount = levelOptions.levelMaxDeaths / player.deadCounter;
            timeSlider.fillAmount = levelOptions.levelMaxTime / player.time;

            if (scoreSlider.fillAmount >= 1)
            {
                cristalScore.SetActive(true);
                christalCount++;
            }
            else
                cristalScore.SetActive(false);

            if (deathsSlider.fillAmount >= 1)
            {
                cristalDeaths.SetActive(true);
                christalCount++;
            }
            else
                cristalDeaths.SetActive(false);

            if (timeSlider.fillAmount >= 1)
            {
                cristalTime.SetActive(true);
                christalCount++;
            }
            else
                cristalTime.SetActive(false);

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
            else if (levelOptions.levelNumber == 3)
            {
                levelLogic.isLvl3Done = true;
                levelLogic.lvl3Christals = christalCount;
            }
        }
        else if (scoreState == 4)
        {
            counter += Time.deltaTime;

            if (counter >= 2)
            {
                if (cristalScore.activeSelf == true && cristalDeaths.activeSelf == true && cristalTime.activeSelf == true)
                {
                    allChristals.SetActive(true);
                    finalMusic.Stop();
                }
            }

            if (counter >= 3)
            {
                animator.SetTrigger("In3");
                counter = 0;
                scoreState = 5;
            }
        }
        else if (scoreState == 5)
        {
            
        }
	}

    public void GoMenu()
    {
        levelLogic.LoadScene(1);
    }

    public void NextLevel()
    {
        levelLogic._loader.Load(levelLogic.nextScene);
    }

    public void Restart()
    {
        levelLogic.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
