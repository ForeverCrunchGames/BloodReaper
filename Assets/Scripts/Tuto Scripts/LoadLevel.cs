using UnityEngine;
using System.Collections;
//INCLUDE SCENEMANAGEMENT
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour 
{	
	public int backLevel, nextLevel, level;
	// Update is called once per frame

	void Start()
	{
		// Consultamos el número de escenas en las buildSettings
		int levelCounts = SceneManager.sceneCountInBuildSettings;
		// Consultamos el indice del nivel actual
		level = SceneManager.GetActiveScene().buildIndex;
		// Calculamos el nivel anterior
		backLevel = level - 1;
		if (backLevel < 0) backLevel = levelCounts - 1;
		// Calculamos el siguiente nivel
		nextLevel = level + 1;
		if(nextLevel > levelCounts - 1) nextLevel = 0;

	}
	void Update () 
	{
		//Para cambiar de nivel por teclado
		if (Input.GetKeyDown (KeyCode.N)) 
		{
			LoadNextLevel ();
		}
		if (Input.GetKeyDown (KeyCode.B)) 
		{
			LoadBackLevel ();
		}
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			ResetLevel ();
		}

	}

	public void LoadNextLevel()
	{
		SceneManager.LoadScene (nextLevel);
	}
	public void LoadBackLevel()
	{
		SceneManager.LoadScene (backLevel);
	}
	public void ResetLevel()
	{
		SceneManager.LoadScene (level);
	}
}
