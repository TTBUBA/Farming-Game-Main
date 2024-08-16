using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class notte_giorno : MonoBehaviour
{
    public Light2D Luce;
    public GameObject[] LucePali;
    public TimeManager TimeManager;
    public float transitionDuration = 2f; // Durata della transizione in secondi

    private bool isTransitioning = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("e") && !isTransitioning)
        {
            StartCoroutine(ChangeLightIntensity(Luce.intensity, 0.4f));
            foreach (GameObject lucepali in LucePali)
            {
                lucepali.SetActive(true);
            }
            Debug.Log("cliccato");
        }
        else if (Input.GetButtonDown("t") && !isTransitioning)
        {
            StartCoroutine(ChangeLightIntensity(Luce.intensity, 1f));
            foreach (GameObject lucepali in LucePali)
            {
                lucepali.SetActive(false);
            }
            Debug.Log("cliccato");
        }

        CycleNight_day();
    }

    public void CycleNight_day()
    {
        int currentHour = TimeManager.Hour;
        float CurrentMinutes = TimeManager.Minutes;

        // Controllo Orario per Attivazione della luce globale e della disattivazione delle luce
        if (currentHour >= 19 || currentHour < 8)
        {
            if (!isTransitioning)
            {
                StartCoroutine(ChangeLightIntensity(Luce.intensity, 0.2f));
                foreach (GameObject lucepali in LucePali)
                {
                    lucepali.SetActive(true);
                }
            }
        }
        else
        {
            if (!isTransitioning)
            {
                StartCoroutine(ChangeLightIntensity(Luce.intensity, 1f));
                foreach (GameObject lucepali in LucePali)
                {
                    lucepali.SetActive(false);
                }
            }
        }


    }

    private IEnumerator ChangeLightIntensity(float startIntensity, float endIntensity)
    {
        isTransitioning = true;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            Luce.intensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Luce.intensity = endIntensity;
        isTransitioning = false;
    }


}