using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
	public LevelLogic levelLogic;
    public Image blackScreen;
    private float _targetAlpha, _timeDuration;
    private bool _doTransition, _loading, _waitForLoadState;
    private int _newScene;

    private AsyncOperation async = null; // When assigned, load is in progress.

	void Start()
	{
		levelLogic = GetComponentInParent<LevelLogic> ();
	}
	// Update is called once per frame
	void Update ()
    {
	    if(_doTransition)
        {
            if (_timeDuration > 0) _timeDuration -= Time.deltaTime;
            else
            {
                _doTransition = false;
                Loading(_newScene);
            }
        }
        else if(_loading)
        {
            if(async.isDone)
            {
                async = null;
                _loading = false;

                UpdateActiveScene();
                FadeIn();
            }
        }
	}

    public void Load(int scene)
    {
        Debug.Log("Loading new scene: " + scene);


        _newScene = scene;
        _targetAlpha = 1;
        _timeDuration = 1;
        _doTransition = true;
        FadeTransition();
    }
    void FadeIn()
    {
        _targetAlpha = 0;
        _timeDuration = 1;
        FadeTransition();
    }
    void FadeTransition()
    {
        blackScreen.CrossFadeAlpha(_targetAlpha, _timeDuration, true);
    }
    void Loading(int scene)
    {
        if (scene == -1) Application.Quit();
        else
        {
            //if (scene == LevelLogic.instance.menuScene) SceneManager.LoadScene(scene);
            //else
            {
                _loading = true;

                /*Load data*/

				if(!levelLogic.IsLastScene())
                {
					SceneManager.UnloadScene(levelLogic.currentScene);
                }
                async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

            }
        }
    }
    void UpdateActiveScene()
    {
        Scene scene = SceneManager.GetSceneAt(1);
        SceneManager.SetActiveScene(scene);

		levelLogic.UpdateSceneState();
    }
}