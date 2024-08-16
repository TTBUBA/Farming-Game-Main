using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Options : MonoBehaviour
{
    public AudioSource AudioBackGround;
    public Slider slider;
    public TMP_Dropdown dropdownResolution;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        dropdownResolution.ClearOptions();
        
        List<string> options = new List<string>();

        int CurrentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Debug.Log("Risoluzione disponibile: " + resolutions[i].width + "x" + resolutions[i].height);
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolutionIndex = i;
            }
        }

        dropdownResolution.AddOptions(options);
        dropdownResolution.value = CurrentResolutionIndex;
        dropdownResolution.RefreshShownValue();
    }

    private void Awake()
    {
        slider.value = AudioBackGround.volume;
        slider.onValueChanged.AddListener(Volumechange);
    }

    public void SetResolution(int IndexResolution)
    {
        if (IndexResolution < 0 || IndexResolution >= resolutions.Length) return;

        Resolution resolution = resolutions[IndexResolution];
        Debug.Log("Cambiando risoluzione a: " + resolution.width + "x" + resolution.height);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Volumechange(float volume)
    {
        AudioBackGround.volume = volume;
    }

    public void Quality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void Fullscreen(bool IsFullscreen)
    {
        Screen.fullScreen = IsFullscreen;
    }
}
