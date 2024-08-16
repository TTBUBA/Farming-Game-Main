using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class notte_giorno : MonoBehaviour
{
    public Light2D Luce;
    // public GameObject LuceAbitazioni;
    public GameObject[] LucePali;
    public TimeManager TimeManager;
    public float transitionDuration = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("e"))
        {
            Luce.intensity = 0.4f;
            // LuceAbitazioni.SetActive(true);

            foreach(GameObject lucepali in LucePali)
            {
                lucepali.SetActive(true);
            }
            Debug.Log("cliccato");
        }
        else if(Input.GetButtonDown("t"))
        {
         
            Luce.intensity = 1f;
            // LuceAbitazioni.SetActive(false);

            foreach (GameObject lucepali in LucePali)
            {
                lucepali.SetActive(true);
            }
            Debug.Log("cliccato");
        }

        CycleNight_day();
        
    }

    public void CycleNight_day()
    {
        int currentHour = TimeManager.Hour;

        if (currentHour >= 19 || currentHour < 8)
        {
         
            
            StartCoroutine(ChangeLightIntensity(0.2f, transitionDuration));

            foreach (GameObject lucepali in LucePali)
            {
                lucepali.SetActive(true);
            }
        }
        else
        {
            
            
            StartCoroutine(ChangeLightIntensity(1f, transitionDuration));

            foreach (GameObject lucepali in LucePali)
            {
                lucepali.SetActive(false);
            }
        }
    }

    public  IEnumerator ChangeLightIntensity(float targetIntensity , float Duration)
    {
        float startIntensity = Luce.intensity;
        float TimeElapsed = 0f;

        while (TimeElapsed < Duration)
        {
            Luce.intensity = Mathf.Lerp(startIntensity, targetIntensity, TimeElapsed / Duration);
            TimeElapsed += Time.deltaTime;
            
            yield return null;
        }

        Luce.intensity = targetIntensity;
    }
}
