using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI Statistics - Animal")]
    public GameObject Button_Log_Box; // Bottone per visualizzare le statistiche degli animali
    public GameObject Button_Exit_Box; // Bottone per chiudere le statistiche degli animali
    public GameObject Ui_Chicken; // Interfaccia utente per le statistiche del pollo
    public GameObject Ui_Cow; // Interfaccia utente per le statistiche della mucca
    public GameObject Ui_Sheep; // Interfaccia utente per le statistiche della pecora
    public GameObject Ui_Pig; // Interfaccia utente per le statistiche del maiale

    [Header("UI Statistics - Magazzini")]
    public GameObject Button_Log; // Bottone per visualizzare le statistiche del magazzino o silo
    public GameObject Button_Exit; // Bottone per chiudere le statistiche del magazzino o silo
    public GameObject Ui_Silo; // Interfaccia utente per le statistiche del silo
    public GameObject Ui_Magazzino; // Interfaccia utente per le statistiche del magazzino

    [Header("UI Mill")]
    public GameObject Button_Log_Mill;
    public GameObject Button_Exit_Mill;
    public GameObject Ui_Mill;


    [Header("Gamepad Controls")]
    public InputActionReference Button_Log_Magazzini_Gamepad; // Azione del gamepad per visualizzare le statistiche del magazzino/silo
    public InputActionReference Button_Exit_Magazzini_Gamepad; // Azione del gamepad per chiudere le statistiche del magazzino/silo
    public InputActionReference Button_Log_Box_Animal_Gamepad; // Azione del gamepad per visualizzare le statistiche degli animali
    public InputActionReference Button_Exit_Box_Animal_Gamepad; // Azione del gamepad per chiudere le statistiche degli animali
    public InputActionReference Button_Log_Mill_Gamepad;
    public InputActionReference Button_Exit_Mill_Gamepad;


    [Header("Player Movement")]
    public Move_Player MovePlayer;

    [Header("Mill")]
    public Mill_Manager MillManager;

    [Header("UI Icons")]
    public GameObject Icon_Log_GamePad; // Icona per indicare il pulsante di log quando si utilizza il gamepad
    public GameObject Icon_Exit_GamePad; // Icona per indicare il pulsante di uscita quando si utilizza il gamepad
    public GameObject Icon_Log_Keyboard; // Icona per indicare il pulsante di log quando si utilizza la tastiera

    private string currentCollisionTag; // Tag dell'oggetto con cui il giocatore è in collisione

    private PlayerInput Playerinput;


    private void Start()
    {
        Playerinput = GetComponent<PlayerInput>();
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;
        
        // Se il tag è associato a un animale, gestisce la collisione con l'animale
        if (IsAnimalTag(tag))
        {
            HandleAnimalCollision(tag);
        }
        // Se il tag è associato a un magazzino o silo, gestisce la collisione con il magazzino
        else if (IsMagazzinoTag(tag))
        {
            HandleMagazzinoCollision(tag);
        }
        else if (IsMillTag(tag))
        {
            HandleMillCollision(tag);
        }

    }


 
    // Gestisce l'uscita del giocatore da un'area con un trigger collider
    private void OnTriggerExit2D(Collider2D collider)
    {
        ResetUI(); // Resetta l'interfaccia utente quando il giocatore esce dalla collisione
        ResetTag(tag);
    }

    // Gestisce la logica quando il giocatore collide con un oggetto animale
    private void HandleAnimalCollision(string tag)
    {
        currentCollisionTag = tag; // Salva il tag corrente per future interazioni
        ShowAppropriateIcons(); // Mostra le icone appropriate a seconda del dispositivo di input

        // Attiva il bottone di log per gli animali e abilita le azioni del gamepad per gli animali
        Button_Log_Box.SetActive(true);
        Button_Log_Box_Animal_Gamepad.action.Enable();
        Button_Exit_Box_Animal_Gamepad.action.Enable();
    }

    private void ResetTag(string tag)
    {
        currentCollisionTag = null;
    }

    // Gestisce la logica quando il giocatore collide con un magazzino o silo
    private void HandleMagazzinoCollision(string tag)
    {
        currentCollisionTag = tag; // Salva il tag corrente per future interazioni
        ShowAppropriateIcons(); // Mostra le icone appropriate a seconda del dispositivo di input

        // Attiva il bottone di log per i magazzini e abilita le azioni del gamepad per i magazzini
        Button_Log.SetActive(true);
        Button_Log_Magazzini_Gamepad.action.Enable();
        Button_Exit_Magazzini_Gamepad.action.Enable();
    }

    private void HandleMillCollision(string tag)
    {
        currentCollisionTag = tag; // Salva il tag corrente per future interazioni
        ShowAppropriateIcons(); // Mostra le icone appropriate a seconda del dispositivo di input

        // Attiva il bottone di log per i magazzini e abilita le azioni del gamepad per i magazzini
        Button_Log.SetActive(true);
        Button_Log_Mill_Gamepad.action.Enable();
        Button_Exit_Mill_Gamepad.action.Enable();
    }

    // Mostra le icone appropriate a seconda se si utilizza un gamepad o una tastiera
    private void ShowAppropriateIcons()
    {
        if(Playerinput != null)
        {
            var currentDevice = Playerinput.currentControlScheme;

            foreach(var Device in Playerinput.devices)
            {
                if (Device is Gamepad)
                {
                    Icon_Log_GamePad.SetActive(true);

                    Icon_Log_Keyboard.SetActive(false);
                }
                else if (Device is Keyboard)
                {
                    Icon_Log_Keyboard.SetActive(true);

                    Icon_Log_GamePad.SetActive(false);
                }
            }

        }
    }

    // Resetta l'interfaccia utente quando il giocatore esce da una zona di collisione
    private void ResetUI()
    {
        Button_Log_Box.SetActive(false);
        Button_Log.SetActive(false);
        Button_Exit_Box.SetActive(false);
        Button_Exit.SetActive(false);
   

        Icon_Log_GamePad.SetActive(false);
        Icon_Exit_GamePad.SetActive(false);
        Icon_Log_Keyboard.SetActive(false);

        Button_Log_Mill.SetActive(false);
        Button_Exit_Mill.SetActive(false);


        Button_Log_Box_Animal_Gamepad.action.Disable();
        Button_Exit_Box_Animal_Gamepad.action.Disable();
        Button_Log_Magazzini_Gamepad.action.Disable();
        Button_Exit_Magazzini_Gamepad.action.Disable();
        Button_Log_Mill_Gamepad.action.Disable();
        Button_Exit_Mill_Gamepad.action.Disable();

        currentCollisionTag = null;
    }

    // Gestisce il click sul bottone di log per gli animali (usato per attivare la UI specifica dell'animale)
    public void OnButtonLogBoxClick()
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
        }

        Button_Log_Box.SetActive(false); // Nasconde il bottone di log dopo l'uso
        Button_Exit_Box.SetActive(true); // Mostra il bottone di uscita
    }

    // Gestisce il click sul bottone di uscita per gli animali (usato per disattivare la UI specifica dell'animale)
    public void OnButtonExitBoxClick()
    {
        switch (currentCollisionTag)
        {
            case "statistics_cow":
                Ui_Cow.SetActive(false);
                break;
            case "statistics_chicken":
                Ui_Chicken.SetActive(false);
                break;
            case "statistics_Sheep":
                Ui_Sheep.SetActive(false);
                break;
            case "statistics_pig":
                Ui_Pig.SetActive(false);
                break;
        }

        Button_Exit_Box.SetActive(false); // Nasconde il bottone di uscita dopo l'uso
        Icon_Log_Keyboard.SetActive(false); // Nasconde il bottone della tastiera di uscita dopo l'uso
    }

    public void OnbuttonLogMill()
    {
        switch (currentCollisionTag)
        {
            case "Mill":
                Ui_Mill.SetActive(true);
                break;

        }

        Button_Log_Mill.SetActive(false); 
                                         
        Button_Exit_Mill.SetActive(true); // Mostra il bottone di uscita
    }

    public void OnbuttonExitMill()
    {
        switch (currentCollisionTag)
        {
            case "Mill":
                Ui_Mill.SetActive(false);
                break;

        }

        Button_Exit_Mill.SetActive(false);
    }

    // Gestisce il caricamento della UI del magazzino o del silo
    public void ButtonLoadMagazzini()
    {
        switch (currentCollisionTag)
        {
            case "Box_Magazzino":
                Ui_Magazzino.SetActive(true);
                break;
            case "Box_Silo":
                Ui_Silo.SetActive(true);
                break;
        }

        Button_Log_Mill.SetActive(false);
    }

    // Gestisce la chiusura della UI del magazzino o del silo
    public void ButtonExitMagazzini()
    {
        switch (currentCollisionTag)
        {
            case "Box_Magazzino":
                Ui_Magazzino.SetActive(false);
                break;
            case "Box_Silo":
                Ui_Silo.SetActive(false);
                break;
        }

        Button_Exit.SetActive(false); // Nasconde il bottone di uscita dopo l'uso
        Icon_Log_Keyboard.SetActive(false); // Nasconde l'icona della tastiera
    }

    // Abilita le azioni del gamepad quando lo script è attivo
    private void OnEnable()
    {
        Button_Log_Magazzini_Gamepad.action.started += LogMagazziniGamePad;
        Button_Exit_Magazzini_Gamepad.action.started += ExitMagazziniGamePad;
        Button_Log_Box_Animal_Gamepad.action.started += LogBoxAnimal;
        Button_Exit_Box_Animal_Gamepad.action.started += ExitBoxAnimal;
        Button_Log_Mill_Gamepad.action.started += LogMill;
        Button_Exit_Mill_Gamepad.action.started += ExiMill;

        ResetUI();
    }

    // Disabilita le azioni del gamepad quando lo script è disattivato
    private void OnDisable()
    {
        Button_Log_Magazzini_Gamepad.action.started -= LogMagazziniGamePad;
        Button_Exit_Magazzini_Gamepad.action.started -= ExitMagazziniGamePad;
        Button_Log_Box_Animal_Gamepad.action.started -= LogBoxAnimal;
        Button_Exit_Box_Animal_Gamepad.action.started -= ExitBoxAnimal;
        Button_Log_Mill_Gamepad.action.started -= LogMill;
        Button_Exit_Mill_Gamepad.action.started -= ExiMill;

        ResetUI();
    }



    //=======CONTROLLER SYSTEM======//

    // Gestisce l'azione del gamepad per visualizzare la UI del magazzino/silo
    private void LogMagazziniGamePad(InputAction.CallbackContext context)
    {
        if (currentCollisionTag != null && IsMagazzinoTag(currentCollisionTag))
        {
             ButtonLoadMagazzini();
             Icon_Log_GamePad.SetActive(false);
             Icon_Exit_GamePad.SetActive(true);
        }
  
    }

    // Gestisce l'azione del gamepad per chiudere la UI del magazzino/silo
    private void ExitMagazziniGamePad(InputAction.CallbackContext context)
    {
        if (currentCollisionTag != null && IsMagazzinoTag(currentCollisionTag))
        {
            ButtonExitMagazzini();
            Icon_Exit_GamePad.SetActive(false);
        }

    }

    // Gestisce l'azione del gamepad per visualizzare la UI degli animali
    private void LogBoxAnimal(InputAction.CallbackContext context)
    {
        if (currentCollisionTag != null && IsAnimalTag(currentCollisionTag))
        {
            OnButtonLogBoxClick();
            Icon_Log_GamePad.SetActive(false);
            Icon_Exit_GamePad.SetActive(true);
        }
        
    }

    // Gestisce l'azione del gamepad per chiudere la UI degli animali
    private void ExitBoxAnimal(InputAction.CallbackContext context)
    {
        if(currentCollisionTag != null && IsAnimalTag(currentCollisionTag))
        {
            OnButtonExitBoxClick();
            Icon_Exit_GamePad.SetActive(false);
        }
        
    }

    // Gestisce l'azione del gamepad per visualizzare la UI del mulino
    private void LogMill(InputAction.CallbackContext context)
    {
        if(currentCollisionTag != null && IsMillTag(currentCollisionTag))
        {
            OnbuttonLogMill();
            Icon_Exit_GamePad.SetActive(true);
            MillManager.MillIsActive = true;
        }
    }

    // Gestisce l'azione del gamepad per chiudere la UI del mulino
    private void ExiMill(InputAction.CallbackContext context)
    {
        if (currentCollisionTag != null && IsMillTag(currentCollisionTag))
        {
            OnbuttonExitMill();
            Icon_Exit_GamePad.SetActive(false);
            MillManager.MillIsActive = false;
        }
    }

    // Controlla se il tag corrisponde a un animale
    private bool IsAnimalTag(string tag)
    {
        return tag == "statistics_cow" ||
               tag == "statistics_chicken" ||
               tag == "statistics_Sheep" ||
               tag == "statistics_pig";
    }

    // Controlla se il tag corrisponde a un magazzino o a un silo
    private bool IsMagazzinoTag(string tag)
    {
        return tag == "Box_Magazzino" || tag == "Box_Silo";
    }

    private bool IsMillTag(string tag)
    {
        return tag == "Mill";
    }

}