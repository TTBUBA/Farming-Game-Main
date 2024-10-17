using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollision : MonoBehaviour
{
    
    [Header("UI Button_Log_Exit")]
    public GameObject Ui_Button_Log; // Ui Bottone di log
    public GameObject Ui_Button_Exit; // Ui Bottone di uscita

    [Header("UI Statistics - Animal")]
    public GameObject Ui_Chicken; // UI per le statistiche del pollo
    public GameObject Ui_Cow; // UI per le statistiche della mucca
    public GameObject Ui_Sheep; // UI per le statistiche della pecora
    public GameObject Ui_Pig; // UI per le statistiche del maiale

    [Header("UI Statistics - Magazzini")]
    public GameObject Ui_Silo; // UI per il silo
    public GameObject Ui_Magazzino; // UI per il magazzino

    [Header("UI Showcase")]
    public GameObject Ui_Order; // UI per mostrare gli ordini

    [Header("UI Mill")]
    public GameObject Ui_Mill; // UI per il mulino

    [Header("KeyBoard Controls")]
    public InputActionReference Button_Log_KeyBoard; // Riferimento al tasto di log sulla tastiera
    public InputActionReference Button_Exit_KeyBoard; // Riferimento al tasto di uscita sulla tastiera

    [Header("Controller Controls")]
    public InputActionReference Button_Log_Controller; // Riferimento al tasto di log sul controller
    public InputActionReference Button_Exit_Controller; // Riferimento al tasto di uscita sul controller

    [Header("UI-Controller")]
    public GameObject Ui_Log_Keyboard; // UI per log su tastiera
    public GameObject Ui_Exit_Keyboard; // UI per uscita su tastiera

    [Header("UI-Keyboard")]
    public GameObject Ui_Log_Controller; // UI per log su controller
    public GameObject Ui_Exit_Controller; // UI per uscita su controller

    private string currentCollisionTag; // Tag dell'oggetto con cui il giocatore collide
    private PlayerInput Playerinput; // Riferimento al componente PlayerInput

    public bool IsUsingKeyboard; // Indica se si sta usando la tastiera
    public bool IsCollision = false; // Indica se c'è una collisione attiva

    private void Start()
    {
        // Inizializza il componente PlayerInput
        Playerinput = GetComponent<PlayerInput>();
        IsCollision = false; 
    }

    private void Update()
    {
        TrackerDevice(); // Controlla quale dispositivo di input è in uso
        ShowUiButton(); // Mostra o nasconde il bottone dell'interfaccia utente
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Se il giocatore collide con sé stesso o con un oggetto specifico (Ascia), non fare nulla
        if (collider.gameObject == this.gameObject || collider.CompareTag("Axe"))
        {
            return;
        }

        // Salva il tag dell'oggetto con cui si è in collisione
        currentCollisionTag = collider.gameObject.tag;

        TrackerDevice(); // Controlla quale dispositivo è in uso
        ShowUiButton(); // Mostra il bottone dell'interfaccia utente
        IsCollision = true; 
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // Se il giocatore esce dalla collisione con se stesso o con un altro giocatore, non fa nulla
        if (collider.gameObject == this.gameObject || collider.CompareTag("Player"))
        {
            return;
        }

        ResetUI();  // Quando esci dalla collisione, resetta l'interfaccia utente
        IsCollision = false; 
    }

    private void TrackerDevice()
    {
        // Controlla quale dispositivo (tastiera o controller) sta usando il giocatore
        if (Playerinput != null)
        {
            var CurrentDevice = Playerinput.currentControlScheme;

            foreach (var device in Playerinput.devices)
            {
                // Se il dispositivo è una tastiera, imposta IsUsingKeyboard su true
                if (device is Keyboard)
                {
                    IsUsingKeyboard = true;
                }
                // Se il dispositivo è un controller, imposta IsUsingKeyboard su false
                else if (device is Gamepad)
                {
                    IsUsingKeyboard = false;
                }
            }
        }
    }

    private void ShowUiButton()
    {
        // Mostra il bottone solo se il giocatore è in collisione
        if (IsCollision == true)
        {
            Ui_Button_Log.SetActive(true); // Attiva il bottone di log

            // Mostra le icone in base al dispositivo usato
            if (IsUsingKeyboard)
            {
                Ui_Log_Keyboard.SetActive(true); // Mostra l'icona per tastiera
                Ui_Log_Controller.SetActive(false); // Nascondi l'icona per controller
            }
            else
            {
                Ui_Log_Controller.SetActive(true); // Mostra l'icona per controller
                Ui_Log_Keyboard.SetActive(false); // Nascondi l'icona per tastiera
            }
        }
    }

    private void ShowCurrentUI()
    {
        // Mostra la UI corrispondente al tag dell'oggetto con cui si è in collisione
        switch (currentCollisionTag)
        {
            case "statistics_cow":
                Ui_Cow.SetActive(true); // Mostra la UI per la mucca
                break;
            case "statistics_chicken":
                Ui_Chicken.SetActive(true); // Mostra la UI per il pollo
                break;
            case "statistics_Sheep":
                Ui_Sheep.SetActive(true); // Mostra la UI per la pecora
                break;
            case "statistics_pig":
                Ui_Pig.SetActive(true); // Mostra la UI per il maiale
                break;
            case "Box_Magazzino":
                Ui_Magazzino.SetActive(true); // Mostra la UI per il magazzino
                break;
            case "Box_Silo":
                Ui_Silo.SetActive(true); // Mostra la UI per il silo
                break;
            case "Mill":
                Ui_Mill.SetActive(true); // Mostra la UI per il mulino
                break;
            case "Order_Tab":
                Ui_Order.SetActive(true); // Mostra la UI per gli ordini
                break;
        }
    }

    private void ResetUI()
    {
        // Disattiva tutte le UI
        Ui_Button_Log.SetActive(false);
        Ui_Button_Exit.SetActive(false);

        Ui_Mill.SetActive(false);
        Ui_Silo.SetActive(false);
        Ui_Magazzino.SetActive(false);
        Ui_Order.SetActive(false);
        Ui_Chicken.SetActive(false);
        Ui_Cow.SetActive(false);
        Ui_Pig.SetActive(false);
        Ui_Sheep.SetActive(false);

        Ui_Log_Controller.SetActive(false);
        Ui_Exit_Controller.SetActive(false);

        Ui_Log_Keyboard.SetActive(false);
        Ui_Exit_Keyboard.SetActive(false);

        currentCollisionTag = null; // Resetta il tag della collisione
        IsCollision = false; // Imposta IsCollision su false
    }

    //============INPUT SYSTEM============//
    private void OnEnable()
    {
        // Abilita le azioni di input quando lo script è attivo
        Button_Log_KeyBoard.action.Enable();
        Button_Exit_KeyBoard.action.Enable();

        Button_Log_Controller.action.Enable();
        Button_Exit_Controller.action.Enable();

        // Collega le funzioni ai pulsanti di log e uscita
        Button_Log_KeyBoard.action.started += Button_Log;
        Button_Exit_KeyBoard.action.started += Button_Exit;

        Button_Log_Controller.action.started += Button_Log;
        Button_Exit_Controller.action.started += Button_Exit;
    }

    private void OnDisable()
    {
        // Disabilita le azioni di input quando lo script non è attivo
        Button_Log_KeyBoard.action.Disable();
        Button_Exit_KeyBoard.action.Disable();

        Button_Log_Controller.action.Disable();
        Button_Exit_Controller.action.Disable();

        // Scollega le funzioni dai pulsanti di log e uscita
        Button_Log_KeyBoard.action.started -= Button_Log;
        Button_Exit_KeyBoard.action.started -= Button_Exit;

        Button_Log_Controller.action.started -= Button_Log;
        Button_Exit_Controller.action.started -= Button_Exit;
    }

    private void Button_Log(InputAction.CallbackContext context)
    {
        // Azione da eseguire quando si preme il bottone di log
        Ui_Button_Log.SetActive(false); // Nasconde il bottone di log
        Ui_Button_Exit.SetActive(true); // Mostra il bottone di uscita
        ShowCurrentUI(); // Mostra la UI attuale

        // Mostra le icone in base al dispositivo usato
        if (IsUsingKeyboard)
        {
            Ui_Exit_Keyboard.SetActive(true); // Mostra l'icona di uscita per tastiera
            Ui_Exit_Controller.SetActive(false); // Nasconde l'icona di uscita per controller
        }
        else
        {
            Ui_Exit_Controller.SetActive(true); // Mostra l'icona di uscita per controller
            Ui_Exit_Keyboard.SetActive(false); // Nascondi l'icona di uscita per tastiera
        }
    }

    private void Button_Exit(InputAction.CallbackContext context)
    {
        // Azione da eseguire quando si preme il bottone di uscita
        Ui_Button_Log.SetActive(true); // Mostra il bottone di log
        Ui_Button_Exit.SetActive(false); // Nascondi il bottone di uscita
        ResetUI(); // Resetta l'interfaccia utente
    }

    // Metodo pubblico per mostrare la UI attuale
    public void ShowUicurrent()
    {
        ShowCurrentUI();
    }

    // Metodo pubblico per disattivare l'interfaccia utente attuale
    public void DisactiveCurrentUi()
    {
        ResetUI();
    }
}
