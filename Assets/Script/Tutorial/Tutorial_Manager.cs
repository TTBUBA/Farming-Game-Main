using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Manager : MonoBehaviour
{
    private Move_Player move_Player;  // Riferimento al componente Move_Player

    public Animator camera_Animator;  // Riferimento all'animatore della camera
    public GameObject Ui_Tutorial;
    [Header("Oggetti da disattivare")]
    public GameObject Invetario;
    public GameObject bar_Up;
    public GameObject calendario;
    public GameObject Bax_Money;

    [Header("Presentazione")]
    public GameObject Background;
    public GameObject Prima_Parte;
    public GameObject Seconda_Parte;


    [Header("Tutorial")]
    public GameObject Background_Tutorial;
    public GameObject Primo_Tutorial;
    public GameObject Seconda_Tutorial;
    

    private bool primaparte = true;
    private bool secondaparte = false;
    private bool tutorialActive = false;
    private bool Fine = false;
    private Queue<IEnumerator> tutorialQueue;
    private IEnumerator currentTutorial; // Variabile per memorizzare il tutorial corrente

    void Start()
    {
        // Disattiva gli oggetti inizialmente
        Invetario.SetActive(false);
        bar_Up.SetActive(false);
        calendario.SetActive(false);
        Bax_Money.SetActive(false);

        // Trova il componente Move_Player e imposta la velocità a 0
        move_Player = FindAnyObjectByType<Move_Player>();
        move_Player.speed = 0;

        // Inizializza la coda dei tutorial e aggiungi i tutorial in ordine
        tutorialQueue = new Queue<IEnumerator>();
        tutorialQueue.Enqueue(StartRecintiTutorial());
        tutorialQueue.Enqueue(StartTerrenoTutorial());
        tutorialQueue.Enqueue(Finetutorial());
    }

    void Update()
    {
        if (Input.GetButtonDown("space"))
        {
            if (primaparte)
            {
                // Se siamo nella prima parte, passa alla seconda parte
                primaparte = false;
                Prima_Parte.SetActive(false);
                Seconda_Parte.SetActive(true);
                secondaparte = true;
            }
            else if (secondaparte)
            {
                // Se siamo nella seconda parte, attiva gli elementi UI e inizia i tutorial
                secondaparte = false;
                Seconda_Parte.SetActive(false);
                Background.SetActive(false);



                ProcessNextTutorial();
            }
            else if (tutorialActive)
            {
                // Se un tutorial è attivo e premi "space", interrompi il tutorial corrente e avvia il prossimo
                StopCoroutine(currentTutorial);
                tutorialActive = false;
                if (currentTutorial == StartRecintiTutorial())
                {
                    Primo_Tutorial.SetActive(false);
                }

                ProcessNextTutorial();
            }
            else if (!tutorialActive && tutorialQueue.Count > 0)
            {
                // Se nessun tutorial è attivo e ci sono tutorial nella coda, processa il prossimo
                ProcessNextTutorial();
            }
        }

        if (Fine == true)
        {
            move_Player.speed = 5;
            camera_Animator.enabled = false;
        }
    }

    void ProcessNextTutorial()
    {
        if (tutorialQueue.Count > 0)
        {
            // Se ci sono tutorial nella coda, avvia il prossimo
            currentTutorial = tutorialQueue.Dequeue();
            StartCoroutine(currentTutorial);
        }
        else
        {
            // Se la coda è vuota, la velocità del giocatore ritorna come prima
            move_Player.speed = 5;
        }
    }

    IEnumerator StartRecintiTutorial()
    {
        tutorialActive = true;
        camera_Animator.SetBool("Recinti", true);

        yield return new WaitForSeconds(1.0f);
        Background_Tutorial.SetActive(true);
        Primo_Tutorial.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        camera_Animator.SetBool("Recinti", false);
        tutorialActive = false;
    }

    IEnumerator StartTerrenoTutorial()
    {
        tutorialActive = true;
        camera_Animator.SetBool("Terreno", true);
        yield return new WaitForSeconds(1.0f);
        Background_Tutorial.SetActive(true);
        Seconda_Tutorial.SetActive(true);

        yield return new WaitForSeconds(2.0f);
        camera_Animator.SetBool("Terreno", false);
        tutorialActive = false;
    }


    IEnumerator Finetutorial()
    {
        tutorialActive = true;
        yield return new WaitForSeconds(0.2f);
        Ui_Tutorial.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        camera_Animator.SetBool("Fine", true);
        yield return new WaitForSeconds(0.2f);
        Invetario.SetActive(true);
        bar_Up.SetActive(true);
        calendario.SetActive(true);
        Bax_Money.SetActive(true);
        Fine = true;
         tutorialActive = false;
    }
}
