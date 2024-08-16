using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Fps_Counter : MonoBehaviour
{
    // Campo pubblico per il testo UI che mostrerà gli FPS
    public Text TextFps;

    // Variabile per memorizzare il tempo trascorso tra i frame
    private float deltaTime;

    // Campo per l'intervallo di aggiornamento in secondi
    public float updateInterval = 0.2f;

    // Timer per tenere traccia del tempo trascorso
    private float timer;



    public void Start()
    {
        // test fps limitati sui 144 fissi
        Application.targetFrameRate = 144;
    }
    void Update()
    {
        //deltaTime viene aggiornato  con un filtro esponenziale per rendere il valore più stabile
        /* 
        
        un filtro esponenziale è un metodo per smussare i valori di deltaTime per ottenere una
        lettura più stabile degli FPS. Questo metodo riduce le fluttuazioni rapide che possono verificarsi
         causa delle variazioni significative del tempo trascorso tra i frame.

        */
        
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

       

        // Incrementa il timer con il tempo trascorso dall'ultimo frame
        timer += Time.deltaTime;

        // Se il timer supera l'intervallo di aggiornamento
        if (timer >= updateInterval)
        {
            // Calcola gli FPS come l'inverso di deltaTime
            float fps = 1.0f / deltaTime;

            // Aggiorna il testo UI per mostrare gli FPS
            TextFps.text = String.Format("{0:0} fps", fps);

            // Resetta il timer
            timer = 0f;
        }
    }
}
