using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuLogic : MonoBehaviour
{
    LevelLogic levelLogic;
  public AudioSource pauseButton; 
	private AudioSource clic;
    public GameObject options;
    PlayerMOD player;

    int activeScreen;

	public void Start ()
	{
        levelLogic = GameObject.FindGameObjectWithTag("LevelMnanager").GetComponent<LevelLogic>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMOD>();
        clic = GetComponent<AudioSource>();
        options.SetActive(false);
    }

	public void ResetScene()
    {
        levelLogic.LoadScene(SceneManager.GetActiveScene().buildIndex);
        player.ExitPause();
	}

    public void  GoMenu()
    {
        player.ExitPause();
        Cursor.visible = true;
        levelLogic.LoadScene(1);
    }

	public void PlaySound()
    {
		clic.Play ();
	}

    public void Options()
    {
        options.SetActive(true);
    }

    public void LoadScene(int i)
    {
        levelLogic.LoadScene(i);
        player.ExitPause();
    }

  public void pauseSound()
  {
    pauseButton.Play();
  }
        
}
