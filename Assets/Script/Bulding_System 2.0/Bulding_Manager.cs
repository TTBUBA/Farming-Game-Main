using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bulding_Manager : MonoBehaviour
{
   
    [Header("Bulding System")]
    public int Prezzo_Edificio = 100;  // Prezzo per costruire l'edificio
    public Sprite[] growthStages_Bulding;  // Array di sprite per le diverse fasi di crescita dell'edificio
    public float time = 10f;  // Tempo totale per completare la costruzione
    public int currentStage = 0;  // Fase attuale della costruzione
    private SpriteRenderer spriteRenderer;  // Riferimento al componente SpriteRenderer
    public bool IsBulding = false;
    
    [Header("UI Bulding")]
    public Text text_Coin;  // Testo per visualizzare le monete
    public GameObject Shop_Edicio;  // Riferimento al negozio dell'edificio
    public GameObject Bar_Time;  // Barra del tempo per la costruzione
    public Image Bar;  // Immagine della barra del tempo
    private GameManger gameManager;  // Riferimento al GameManager

    [Header("Animazioni")]
    public Animator animator_Bulding;

    [Header("Audio")]
    public AudioSource Click_Sound;

    void Start()
    {
        // Inizializza il componente SpriteRenderer e imposta lo sprite iniziale
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = growthStages_Bulding[currentStage];

        // Imposta la fase corrente al numero di fasi di crescita
        currentStage = growthStages_Bulding.Length;

        // Nasconde la barra del tempo
        Bar_Time.SetActive(false);

        // Trova il riferimento al GameManager
        gameManager = FindObjectOfType<GameManger>();

        animator_Bulding = GetComponent<Animator>();

        IsBulding = false ;
    }
    public void Update()
    {
        if (IsBulding == true)
        {
            Shop_Edicio.SetActive(false);
            
        }

        if(Bar.fillAmount == 1)
        {
            Bar_Time.SetActive(false);
            Color color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
            animator_Bulding.SetBool("Attivo", true);
        }
    }

    public void Active_Buy_Costruzione()
    {

    }
    // Metodo chiamato quando il pulsante di costruzione viene premuto
    public void PurchaseBuilding()
    {
        Shop_Edicio.SetActive(false);
        // Controlla se il giocatore ha abbastanza monete per costruire l'edificio
        if (gameManager.Coin >= Prezzo_Edificio)
        {
            

            // Avvia la routine di crescita dell'edificio
            StartCoroutine(GrowBulding());

            // Sottrae il prezzo dell'edificio dalle monete del giocatore
            gameManager.Coin -= Prezzo_Edificio;

            // Aggiorna il testo delle monete
            text_Coin.text = gameManager.Coin.ToString();

            // Nasconde il negozio e mostra la barra del tempo
            
            Bar_Time.SetActive(true);

            // Imposta la barra del tempo a zero
            Bar.fillAmount = 0;

            // Quando clicci il pulsante fa partite laudio del click
            Click_Sound.Play();

            IsBulding = true;
        }
    }

    // Routine per gestire la crescita dell'edificio
    IEnumerator GrowBulding()
    {
        // Resetta la fase corrente a zero
        currentStage = 0;

        // Aspetta 3 secondi prima di iniziare la crescita
        yield return new WaitForSeconds(3f);

        // Cicla attraverso ogni fase di crescita
        for (int i = currentStage; i < growthStages_Bulding.Length; i++)
        {
            // Aspetta il tempo specificato tra ogni fase
            yield return new WaitForSeconds(time);

            // Aggiorna lo sprite dell'edificio alla fase corrente
            spriteRenderer.sprite = growthStages_Bulding[currentStage];

            // Passa alla fase successiva
            currentStage++;

            // Aggiorna la barra del tempo in base alla fase corrente
            Bar.fillAmount = (float)currentStage / growthStages_Bulding.Length;
        }

        // Imposta la barra del tempo al 100% al completamento della costruzione
        Bar.fillAmount = 1;
    }
}
