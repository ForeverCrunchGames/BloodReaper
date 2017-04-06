using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLogic : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject sceneLoaderGO;
	[Header("Scripts")]
	public SceneLoader _loader;

	[Header("Scene state")]
	public int backScene; 
	public int currentScene;
	public int nextScene;
	private int _managerScene;
	private int _sceneCountInBuildSettings;

    bool introScene;

    public int mainMenuState;
    public bool isLvl1Done;
    public bool isLvl2Done;

	void Awake()
	{
		GameObject go = Instantiate(sceneLoaderGO, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
		go.name = "SceneLoaderCanvas";
		_loader = go.GetComponent<SceneLoader> ();	
	}
	// Use this for initialization
	void Start ()
	{
        UpdateSceneState();

        if (SceneManager.sceneCount == 1)
        {
            NextScene();
        }
    }
	void Update()
	{

	}
	public void UpdateSceneState()
	{
		_sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;

		_managerScene = _sceneCountInBuildSettings - 1;
		currentScene = SceneManager.GetActiveScene().buildIndex;

		if (currentScene <= 0) backScene = _managerScene - 1;
		else backScene = currentScene - 1;

		if (currentScene >= _managerScene - 1) nextScene = 0;
		else nextScene = currentScene + 1;
	}

	public void NextScene()
	{
		_loader.Load(nextScene);
	}
	public void BackScene()
	{
		_loader.Load(backScene);
	}
	public void ResetScene()
	{
		_loader.Load(currentScene);
	}

	public bool IsLastScene()
	{
		if (currentScene == _managerScene) return true;
		else return false;
	}
	public int GetSceneCount()
	{
		_sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
		return _sceneCountInBuildSettings;
	}

    public void LoadScene(int i)
    {
        _loader.Load(i);
    }
}