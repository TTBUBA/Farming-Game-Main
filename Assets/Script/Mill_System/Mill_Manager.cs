using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class SackData
{
    public string NameSack;          // Nome del sacchetto
    public float PreparationTime;    // Tempo di preparazione in minuti
    public Sprite SackSprite;        // Immagine del sacchetto
    public Sprite Vegetable1Sprite;  // Immagine del primo ortaggio
    public Sprite Vegetable2Sprite;  // Immagine del secondo ortaggio
    public int quantity = 0;         // Quantità di sacchetti raccolti
    public Text QuantityText;        // Riferimento al testo che mostra la quantità

    public string Vegetable1Name;    // Nome del primo ortaggio richiesto
    public string Vegetable2Name;    // Nome del secondo ortaggio richiesto

    public int RequiredVegetable1;   // Quantità richiesta del primo ortaggio
    public int RequiredVegetable2;   // Quantità richiesta del secondo ortaggio
}

public class Mill_Manager : MonoBehaviour
{
    // Riferimenti agli elementi UI nel canvas
    public Image selectedSackImage;  // Immagine del sacchetto selezionato
    public Text Sack_Animal_Name;    // Testo del nome del sacchetto
    public Text Minuti_Preparazione; // Testo del tempo di preparazione
    public Image BarTime;            // Barra che mostra il progresso del tempo di preparazione
    public Image Vegetable1Image;    // Immagine del primo ortaggio
    public Image Vegetable2Image;    // Immagine del secondo ortaggio

    public Text Vegetable1QuantityText;  // Testo della quantità del primo ortaggio
    public Text Vegetable2QuantityText;  // Testo della quantità del secondo ortaggio

    public GameObject Elica;             // Oggetto dell'elica nel mulino
    public float speedElica;             // Velocità di rotazione dell'elica
    public GameObject UiMulino;          // Interfaccia del mulino
    public GameObject button;            // Bottone per accedere al mulino
    public SackData[] sackDataArray;     // Array contenente i dati di tutti i sacchetti
    public Button[] sackButtons;         // Array contenente tutti i bottoni associati ai sacchetti

    public int selectedSackIndex;        // Indice del sacchetto selezionato

    private Coroutine preparationTimerCoroutine; // Riferimento alla Coroutine del timer di preparazione

    public TrakingLocal trakingRaccolto; // Riferimento alla classe che gestisce le quantità degli ortaggi

    [Header("Ui Controller")]
    public InputActionReference Controller_Right;        // Input per scorrere a destra
    public InputActionReference Controller_Left;         // Input per scorrere a sinistra
    public InputActionReference CollectionSack_Controller; // Input per raccogliere il sacchetto

    // Start is called before the first frame update
    void Start()
    {
        ButtonSackChicken(); // Seleziona il sacchetto di pollo all'inizio
        speedElica = 0;      // Inizialmente l'elica non gira
    }

    public void Update()
    {
        // Fa ruotare l'elica in base alla velocità corrente
        Elica.transform.Rotate(0, 0, speedElica * Time.deltaTime);
    }

    // Metodi per selezionare sacchetti specifici in base all'indice
    public void ButtonSackChicken() { OnselectSack(0); }
    public void ButtonSackCow() { OnselectSack(1); }
    public void ButtonSackGoat() { OnselectSack(2); }
    public void ButtonSackPig() { OnselectSack(3); }
    public void ButtonSackSheep() { OnselectSack(4); }

    // Chiude l'interfaccia del mulino
    public void ButtonQuit() { UiMulino.SetActive(false); }

    // Apre l'interfaccia del mulino
    public void ButtonAccess_Mill()
    {
        UiMulino.SetActive(true);
        button.SetActive(false); // Nasconde il bottone di accesso al mulino
    }

    // Metodo per selezionare un sacchetto dato il suo indice
    public void OnselectSack(int SackIndex)
    {
        // Controlla se l'indice è valido
        if (SackIndex < sackDataArray.Length)
        {
            selectedSackIndex = SackIndex; // Aggiorna l'indice del sacchetto selezionato
            UpdateUI(SackIndex);           // Aggiorna l'interfaccia utente con i dati del sacchetto selezionato

            // Annulla le animazioni su tutti i bottoni e resetta la loro scala
            foreach (var button in sackButtons)
            {
                button.transform.DOKill(); // Interrompe qualsiasi animazione in corso
                button.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.OutBounce); // Reset della scala
            }

            // Ingrandisce il pulsante selezionato
            sackButtons[SackIndex].transform.DOScale(Vector2.one * 1.2f, 0.2f).SetEase(Ease.OutBounce);
        }
    }

    // Metodo per avviare la preparazione di un sacchetto
    public void CollectionSack(int SackIndex)
    {
        SackData selectedSack = sackDataArray[SackIndex]; // Ottiene il sacchetto selezionato

        // Se c'è un timer di preparazione in corso, lo ferma
        if (preparationTimerCoroutine != null)
        {
            StopCoroutine(preparationTimerCoroutine);
        }

        // Imposta la velocità dell'elica e sottrae le quantità richieste di ortaggi
        speedElica = 80;
        trakingRaccolto.SubtractVegetableQuantity(selectedSack.Vegetable1Name, selectedSack.RequiredVegetable1);
        trakingRaccolto.SubtractVegetableQuantity(selectedSack.Vegetable2Name, selectedSack.RequiredVegetable2);

        // Avvia il timer di preparazione
        preparationTimerCoroutine = StartCoroutine(Timer(sackDataArray[SackIndex].PreparationTime * 60, SackIndex));
        SetSackButtonsInteractable(false); // Disattiva i bottoni durante la preparazione
    }

    // Coroutine per gestire il conto alla rovescia della preparazione
    public IEnumerator Timer(float preparationTimeInSeconds, int SackIndex)
    {
        float elapsedTime = 0;
        BarTime.fillAmount = 0; // Resetta la barra del tempo

        while (elapsedTime < preparationTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;
            BarTime.fillAmount = elapsedTime / preparationTimeInSeconds; // Aggiorna la barra del tempo
            yield return null;
        }

        // Riduce gradualmente la velocità dell'elica
        while (speedElica > 0)
        {
            speedElica -= 3;
            yield return new WaitForSeconds(0.1f);
        }
        speedElica = 0;

        BarTime.fillAmount = 1; // Riempie completamente la barra del tempo
        SetSackButtonsInteractable(true); // Rende di nuovo attivi i bottoni

        RewordSack(SackIndex); // Aggiunge il sacchetto preparato all'inventario
    }

    // Rende i bottoni interattivi o non interattivi
    public void SetSackButtonsInteractable(bool interactable)
    {
        foreach (Button button in sackButtons)
        {
            button.interactable = interactable;
        }
    }

    // Aumenta la quantità del sacchetto selezionato dopo la preparazione
    public void RewordSack(int SackIndex)
    {
        SackData selectedSack = sackDataArray[SackIndex];
        selectedSack.quantity += 1;
        selectedSack.QuantityText.text = selectedSack.quantity.ToString(); // Aggiorna il testo della quantità
    }

    // Aggiorna l'interfaccia utente con i dati del sacchetto selezionato
    public void UpdateUI(int SackIndex)
    {
        SackData selectedSack = sackDataArray[selectedSackIndex]; // Ottiene i dati del sacchetto selezionato
        selectedSackImage.sprite = selectedSack.SackSprite;       // Aggiorna l'immagine del sacchetto
        Sack_Animal_Name.text = selectedSack.NameSack;            // Aggiorna il nome del sacchetto
        Minuti_Preparazione.text = selectedSack.PreparationTime.ToString() + " Min"; // Aggiorna il tempo di preparazione
        Vegetable1Image.sprite = selectedSack.Vegetable1Sprite;   // Aggiorna l'immagine del primo ortaggio
        Vegetable2Image.sprite = selectedSack.Vegetable2Sprite;   // Aggiorna l'immagine del secondo ortaggio

        UpdateVegetableQuantities(selectedSack); // Aggiorna le quantità degli ortaggi

        AnimateSelectedImageSack(); // Anima l'immagine del sacchetto selezionato
        AnimateSelectedSack(sackButtons[SackIndex]); // Anima il bottone del sacchetto selezionato
    }

    // Aggiorna i testi delle quantità degli ortaggi richiesti
    public void UpdateVegetableQuantities(SackData selectedSack)
    {
        Vegetable1QuantityText.text = GetVegetableQuantity(selectedSack.Vegetable1Name).ToString(); // Aggiorna il testo del primo ortaggio
        Vegetable2QuantityText.text = GetVegetableQuantity(selectedSack.Vegetable2Name).ToString(); // Aggiorna il testo del secondo ortaggio
    }

    // Restituisce la quantità disponibile di un ortaggio specifico
    public int GetVegetableQuantity(string vegetableName)
    {
        return trakingRaccolto.GetVegetableQuantity(vegetableName); // Ottiene la quantità dall'oggetto trakingRaccolto
    }

    // Attiva i listener per gli input del controller
    public void OnEnable()
    {
        Controller_Right.action.performed += ctx => ScrollForwardController(); // Listener per scorrere avanti
        Controller_Left.action.performed += ctx => ScrollBackwardController(); // Listener per scorrere indietro
        CollectionSack_Controller.action.performed += ctx => CollectionSackController(); // Listener per raccogliere
    }

    // Disattiva i listener per gli input del controller
    public void OnDisable()
    {
        Controller_Right.action.performed -= ctx => ScrollForwardController(); // Rimuove il listener per scorrere avanti
        Controller_Left.action.performed -= ctx => ScrollBackwardController(); // Rimuove il listener per scorrere indietro
        CollectionSack_Controller.action.performed -= ctx => CollectionSackController(); // Rimuove il listener per raccogliere
    }

    // Metodo per scorrere avanti i sacchetti tramite controller
    public void ScrollForwardController()
    {
        selectedSackIndex++; // Incrementa l'indice del sacchetto selezionato
        if (selectedSackIndex >= sackDataArray.Length)
        {
            selectedSackIndex = 0; // Resetta l'indice se supera la lunghezza dell'array
        }

        OnselectSack(selectedSackIndex); // Aggiorna la selezione del sacchetto
    }

    // Metodo per scorrere indietro i sacchetti tramite controller
    public void ScrollBackwardController()
    {
        selectedSackIndex--; // Decrementa l'indice del sacchetto selezionato
        if (selectedSackIndex < 0)
        {
            selectedSackIndex = sackDataArray.Length - 1; // Va all'ultimo sacchetto se l'indice è negativo
        }

        OnselectSack(selectedSackIndex); // Aggiorna la selezione del sacchetto
    }

    // Metodo per raccogliere il sacchetto selezionato tramite controller
    public void CollectionSackController()
    {
        CollectionSack(selectedSackIndex); // Avvia la raccolta del sacchetto selezionato
    }

    // Anima l'immagine del sacchetto selezionato
    public void AnimateSelectedImageSack()
    {
        selectedSackImage.transform.DOKill(); // Interrompe qualsiasi animazione in corso
        selectedSackImage.transform.DOScale(Vector2.one * 1.2f, 0.2f).SetEase(Ease.OutBounce) // Ingrandisce l'immagine
            .OnComplete(() =>
            {
                selectedSackImage.transform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutBounce); // Riporta l'immagine alla scala originale
            });
    }

    // Anima il bottone del sacchetto selezionato
    public void AnimateSelectedSack(Button button)
    {
        button.transform.DOKill(); // Interrompe qualsiasi animazione in corso
        button.transform.DOScale(Vector2.one * 1.2f, 0.2f).SetEase(Ease.OutBounce) // Ingrandisce il bottone
            .OnComplete(() =>
            {
                button.transform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutBounce); // Riporta il bottone alla scala originale
            });
    }
}
