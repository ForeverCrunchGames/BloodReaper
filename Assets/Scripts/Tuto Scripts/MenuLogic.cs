using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuLogic : MonoBehaviour
{

	private AudioSource clic;
    public  PlayerMOD player;

	public void Start ()
	{
        clic = GetComponent<AudioSource>();
    }

	void Update ()
	{
        
	}

	public void LoadScene(int numScene)
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        //player.isOptionsMenu = false;
	}

    public void  GoMenu()
    {
        SceneManager.LoadScene("Main menu");
    }

	public void PlaySound()
    {
		clic.Play ();
	}
        
}
