using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
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
    public int quantity = 0;
    public Text QuantityText;



    public string Vegetable1Name;    // Nome del primo ortaggio
    public string Vegetable2Name;    // Nome del secondo ortaggio

    public int RequiredVegetable1;   // Quantità richiesta del primo ortaggio
    public int RequiredVegetable2;   // Quantità richiesta del secondo ortaggio
}

public class Mill_Manager : MonoBehaviour
{
    // Riferimenti agli elementi UI nel canvas
    public Image selectedSackImage;  // Immagine del sacchetto selezionato
    public Text Sack_Animal_Name;    // Testo del nome del sacchetto
    public Text Minuti_Preparazione; // Testo del tempo di preparazione
    public Image BarTime;
    public Image Vegetable1Image;    // Immagine del primo ortaggio
    public Image Vegetable2Image;    // Immagine del secondo ortaggio

    public Text Vegetable1QuantityText;  // Testo della quantità del primo ortaggio
    public Text Vegetable2QuantityText;  // Testo della quantità del secondo ortaggio

    public GameObject Elica;
    public float speedElica;
    public GameObject UiMulino;
    public GameObject button;
    // Array contenente i dati di tutti i sacchetti
    public SackData[] sackDataArray;
    // Array contenente tutti i bottoni 
    public Button[] sackButtons;

    public int selectedSackIndex;   // Indice del sacchetto selezionato

    private Coroutine preparationTimerCoroutine;

    public TrakingLocal trakingRaccolto;

    [Header("Ui Controller")]
    public InputActionReference Controller_Right;
    public InputActionReference Controller_Left;
    public InputActionReference CollectSackAction;

    public PlayerInput Playerinput;
    // Start is called before the first frame update
    void Start()
    {
        ButtonSackChicken();
        speedElica = 0;

        
    }

    public void Update()
    {
        Elica.transform.Rotate(0, 0, speedElica * Time.deltaTime);

        DeviceTracker();
    }
    public void ButtonSackChicken()
    {
        OnselectSack(0);
    }
    public void ButtonSackCow()
    {
        OnselectSack(1);

    }
    public void ButtonSackGoat()
    {
        OnselectSack(2);
    }
    public void ButtonSackPig()
    {
        OnselectSack(3);
    }
    public void ButtonSackSheep()
    {
        OnselectSack(4);


    }
    public void ButtonQuit()
    {
        UiMulino.SetActive(false);
    }
    public void ButtonAccess_Mill()
    {
        UiMulino.SetActive(true);
        button.SetActive(false);
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
                button.transform.DOKill();
                button.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.OutBounce);
            }

            // Ingrandisci il pulsante selezionato
            sackButtons[SackIndex].transform.DOScale(Vector2.one * 1.2f, 0.2f).SetEase(Ease.OutBounce);
            UpdateUI(SackIndex);
        }
    }


    public void CollectionSack(int SackIndex)
    {

        SackData selectedSack = sackDataArray[SackIndex];

        if (preparationTimerCoroutine != null)
        {
            StopCoroutine(preparationTimerCoroutine);
        }
        // velocita del elica che gira sul mulino
        speedElica = 80;

        trakingRaccolto.SubtractVegetableQuantity(selectedSack.Vegetable1Name, selectedSack.RequiredVegetable1);
        trakingRaccolto.SubtractVegetableQuantity(selectedSack.Vegetable2Name, selectedSack.RequiredVegetable2);


        preparationTimerCoroutine = StartCoroutine(Timer(sackDataArray[SackIndex].PreparationTime * 60, SackIndex));
        SetSackButtonsInteractable(false);

    }


    public IEnumerator Timer(float preparationTimeInSeconds, int SackIndex)
    {
        float elapsedTime = 0;
        BarTime.fillAmount = 0;

        while (elapsedTime < preparationTimeInSeconds)
        {
            elapsedTime += Time.deltaTime;

            // Calcola il riempimento della barra in base al tempo trascorso
            BarTime.fillAmount = elapsedTime / preparationTimeInSeconds;

            yield return null;
            // Debug.Log(preparationTimeInSeconds);
        }

        while (speedElica > 0)
        {
            // velocita del elica che gira sul mulino
            speedElica -= 3;
            yield return new WaitForSeconds(0.1f);
        }
        // velocita del elica che gira sul mulino
        speedElica = 0;


        BarTime.fillAmount = 1;

        SetSackButtonsInteractable(true);

        RewordSack(SackIndex);

    }


    public void SetSackButtonsInteractable(bool interactable)
    {
        foreach (Button button in sackButtons)
        {
            button.interactable = interactable;
        }
    }

    public void RewordSack(int SackIndex)
    {

        SackData selectedSack = sackDataArray[SackIndex];
        // Debug.Log("premio ricevuto" + selectedSack.NameSack);
        selectedSack.quantity += 1;
        selectedSack.QuantityText.text = selectedSack.quantity.ToString();

    }

    // Metodo per aggiornare l'interfaccia utente con i dati del sacchetto selezionato
    public void UpdateUI(int SackIndex)
    {
        SackData selectedSack = sackDataArray[selectedSackIndex]; // Ottiene i dati del sacchetto selezionato
        selectedSackImage.sprite = selectedSack.SackSprite;       // Aggiorna l'immagine del sacchetto
        Sack_Animal_Name.text = selectedSack.NameSack;            // Aggiorna il nome del sacchetto
        Minuti_Preparazione.text = selectedSack.PreparationTime.ToString() + " Min"; // Aggiorna il tempo di preparazione
        Vegetable1Image.sprite = selectedSack.Vegetable1Sprite;   // Aggiorna l'immagine del primo ortaggio
        Vegetable2Image.sprite = selectedSack.Vegetable2Sprite;   // Aggiorna l'immagine del secondo ortaggio

        UpdateVegetableQuantities(selectedSack);

        AnimateSelectedImageSack();
        AnimateSelectedSack(sackButtons[SackIndex]);
    }

    public void UpdateVegetableQuantities(SackData selectedSack)
    {
        Vegetable1QuantityText.text = GetVegetableQuantity(selectedSack.Vegetable1Name).ToString() + "/" + selectedSack.RequiredVegetable1.ToString();

        Vegetable2QuantityText.text = GetVegetableQuantity(selectedSack.Vegetable2Name).ToString() + "/" + selectedSack.RequiredVegetable2.ToString(); ;
    }

    public int GetVegetableQuantity(string vegetableName)
    {

        switch (vegetableName)
        {
            case "Carota":
                return trakingRaccolto.RaccoltoCarote;
            case "Patate":
                return trakingRaccolto.RaccoltoPatate;
            case "Grano":
                return trakingRaccolto.RaccoltoGrano;
            case "Cavolo":
                return trakingRaccolto.Raccoltocavolo;
            default:
                return 0;
        }
    }

    //Input controller
    private void OnEnable()
    {
        

        Controller_Right.action.started += ScrollForwardController;
        Controller_Left.action.started += ScrollBackwardController;
        CollectSackAction.action.started += OnCollectSackController;

        CollectSackAction.action.Enable();
        Controller_Right.action.Enable();
        Controller_Left.action.Enable();
    }

    private void OnDisable()
    {

        

        Controller_Right.action.started -= ScrollForwardController;
        Controller_Left.action.started -= ScrollBackwardController;
        CollectSackAction.action.started -= OnCollectSackController;

        CollectSackAction.action.Disable();
        Controller_Right.action.Disable();
        Controller_Left.action.Disable();
    }

    private void DeviceTracker()
    {
        if(Playerinput != null)
        {
            var currentDevice = Playerinput.currentControlScheme;

            foreach(var device in Playerinput.devices)
            {
                if(device is Gamepad)
                {
                    
                }
                else if(device is Keyboard)
                {
                    
                }
            }
        }
            
       
    }
    private void ScrollForward()
    {
        selectedSackIndex = (selectedSackIndex + 1) % sackDataArray.Length;
        UpdateUI(selectedSackIndex);

        foreach (var button in sackButtons)
        {

            button.transform.localScale = Vector2.one;

        }

    }
    private void ScrollBackward()
    {
        selectedSackIndex = (selectedSackIndex - 1 + sackDataArray.Length) % sackDataArray.Length;
        UpdateUI(selectedSackIndex);

        foreach (var button in sackButtons)
        {

            button.transform.localScale = Vector2.one;

        }

    }
    private void ScrollForwardController(InputAction.CallbackContext Obj)
    {
        ScrollForward();
    }

    private void ScrollBackwardController(InputAction.CallbackContext Obj)
    {
        ScrollBackward();
    }

    private void OnCollectSackController(InputAction.CallbackContext Obj)
    {
        CollectionSack(selectedSackIndex);
    }

    private void AnimateSelectedImageSack()
    {
        selectedSackImage.transform.DOScale(new Vector2(1.1f, 1.1f), 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            selectedSackImage.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.OutBounce);
        });
    }
    private void AnimateSelectedSack(Button button)
    {

        button.transform.DOScale(new Vector2(1.15f, 1.15f), 0.2f).SetEase(Ease.InBounce);

    }


}
