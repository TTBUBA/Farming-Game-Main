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
        if (collider.gameObject == this.gameObject || collider.CompareTag("Axe"))
        {
            return;
        }

        currentCollisionTag = collider.gameObject.tag;
        TrackerDevice();

        // Imposta IsCollision solo se ShowCurrentUI() restituisce true
        IsCollision = CheckCollisionTag();
        
        ShowUiButton();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == this.gameObject || collider.CompareTag("Player"))
        {
            return;
        }

        ResetUI();
        IsCollision = false;
        currentCollisionTag = null;
    }

    private void TrackerDevice()
    {
        if (Playerinput != null)
        {
            var CurrentDevice = Playerinput.currentControlScheme;

            foreach (var device in Playerinput.devices)
            {
                if (device is Keyboard)
                {
                    IsUsingKeyboard = true;
                }
                else if (device is Gamepad)
                {
                    IsUsingKeyboard = false;
                }
            }
        }
    }

    private void ShowUiButton()
    {
        if (IsCollision == true)
        {
            Ui_Button_Log.SetActive(true);

            if (IsUsingKeyboard)
            {
                Ui_Log_Keyboard.SetActive(true);
                Ui_Log_Controller.SetActive(false);
            }
            else
            {
                Ui_Log_Controller.SetActive(true);
                Ui_Log_Keyboard.SetActive(false);
            }
        }
    }

    private bool CheckCollisionTag()
    {
        switch (currentCollisionTag)
        {
            case "statistics_cow":
            case "statistics_chicken":  
            case "statistics_Sheep":
            case "statistics_pig":
            case "Box_Magazzino":
            case "Box_Silo": 
            case "Mill":
            case "Order_Tab":
                return true;
            default:
                return false;
        }
    }

    private void ShowCurrentUI()
    {
        switch (currentCollisionTag)
        {
            case "statistics_cow":
                Ui_Cow.SetActive(true);
                break;
            case "statistics_chicken":
                Ui_Chicken.SetActive(true);
                break;
            case "statistics_Sheep":
                Ui_Sheep.SetActive(true);
                break;
            case "statistics_pig":
                Ui_Pig.SetActive(true);
                break;
            case "Box_Magazzino":
                Ui_Magazzino.SetActive(true);
                break;
            case "Box_Silo":
                Ui_Silo.SetActive(true);
                break;
            case "Mill":
                Ui_Mill.SetActive(true);
                break;
            case "Order_Tab":
                Ui_Order.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void ResetUI()
    {
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

        currentCollisionTag = null;
        IsCollision = false;
    }

    private void OnEnable()
    {
        Button_Log_KeyBoard.action.Enable();
        Button_Exit_KeyBoard.action.Enable();

        Button_Log_Controller.action.Enable();
        Button_Exit_Controller.action.Enable();

        Button_Log_KeyBoard.action.started += Button_Log;
        Button_Exit_KeyBoard.action.started += Button_Exit;

        Button_Log_Controller.action.started += Button_Log;
        Button_Exit_Controller.action.started += Button_Exit;
    }

    private void OnDisable()
    {
        Button_Log_KeyBoard.action.Disable();
        Button_Exit_KeyBoard.action.Disable();

        Button_Log_Controller.action.Disable();
        Button_Exit_Controller.action.Disable();

        Button_Log_KeyBoard.action.started -= Button_Log;
        Button_Exit_KeyBoard.action.started -= Button_Exit;

        Button_Log_Controller.action.started -= Button_Log;
        Button_Exit_Controller.action.started -= Button_Exit;
    }

    private void Button_Log(InputAction.CallbackContext context)
    {
        Ui_Button_Log.SetActive(false);
        Ui_Button_Exit.SetActive(true);
        ShowCurrentUI();

        if (IsUsingKeyboard)
        {
            Ui_Exit_Keyboard.SetActive(true);
            Ui_Exit_Controller.SetActive(false);
        }
        else
        {
            Ui_Exit_Controller.SetActive(true);
            Ui_Exit_Keyboard.SetActive(false);
        }
    }

    private void Button_Exit(InputAction.CallbackContext context)
    {
        Ui_Button_Log.SetActive(true);
        Ui_Button_Exit.SetActive(false);
        ResetUI();
    }

    public void ShowUicurrent()
    {
        ShowCurrentUI();
    }

    public void DisactiveCurrentUi()
    {
        ResetUI();
    }
}
