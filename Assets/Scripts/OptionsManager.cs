using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Slider sliderVolume;
    public Slider sliderGamma;
    public Animator popup;

    public Color ambientDarkest;
    public Color ambientLightest;

	// Use this for initialization
	void Start () 
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void SetResolution()
    {
        if (resolutionDropdown.value == 0)
        {
            //720p
            Screen.SetResolution(1280, 720, Screen.fullScreen);
            Debug.Log ("Set game to 720p");
            //PlayerPrefs.Save ();
        }
        else if (resolutionDropdown.value == 1)
        {
            //1200p
            Screen.SetResolution(1600, 1200, Screen.fullScreen);
            Debug.Log ("Set game to 1200p");
            //PlayerPrefs.Save ();
        }
        else if (resolutionDropdown.value == 2)
        {
            //1080p
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
            Debug.Log ("Set game to 1080p");
            //PlayerPrefs.Save ();
        }
    }

    public void SetQuality()
    {
        if (qualityDropdown.value == 0)
        {
            QualitySettings.SetQualityLevel(0);
            Debug.Log("Quality: Fast");
            //PlayerPrefs.Save ();
        }
        else if (qualityDropdown.value == 1)
        {
            QualitySettings.SetQualityLevel(1);
            Debug.Log("Quality: Good");
            //PlayerPrefs.Save ();
        }
        else if (qualityDropdown.value == 2)
        {
            QualitySettings.SetQualityLevel(2);
            Debug.Log("Quality: Fantastic");
            //PlayerPrefs.Save ();
        }
        else if (qualityDropdown.value == 3)
        {
            QualitySettings.SetQualityLevel(3);
            Debug.Log ("Quality: Ultra");
            //PlayerPrefs.Save ();
        }
    }

    void SetAntialiasing()
    {
        QualitySettings.antiAliasing = 0;
        PlayerPrefs.Save ();
        Debug.Log ("0 AA");
    }

    void ToogleTripleBuffering()
    {
        
    }

    void SetRefreshRate()
    {
        
    }

    void ToogleAnisotropicFiltering()
    {
        
    }

    public void ToogleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log ("Toggled Fullscreen");
        //PlayerPrefs.SetInt ("fullscreen", ((isFullscreen) ? 1 : 0));
        //PlayerPrefs.Save ();
    }

    public void Back()
    {
        popup.SetTrigger("popupinverse");

        StartCoroutine(SetInactive());
    }

    public void Volume()
    {
        AudioListener.volume = sliderVolume.value;
    }

    public void Gamma()
    {
        RenderSettings.ambientLight = Color.Lerp(ambientDarkest, ambientLightest, sliderGamma.value);
    }

    IEnumerator SetInactive() {
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
    }
}
