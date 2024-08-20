using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerCollision : MonoBehaviour
{
    [Header("UI Statistics_Animal")]
    public GameObject Button_Log_Box;
    public GameObject Button_Exit_Box;
    public GameObject Ui_Chicken;
    public GameObject Ui_cow;
    public GameObject Ui_Sheep;
    public GameObject Ui_pig;

    [Header("UI Statistics_Magazzini")]
    public GameObject Button_Log;
    public GameObject Button_Exit;
    public GameObject Ui_Silo;
    public GameObject Ui_Magazzino;

    [Header("Tasti Gamepad")]
    public InputActionReference Button_Log_Magazzini_Gamepad;
    public InputActionReference Button_Exit_Magazzini_Gamepad;

    public InputActionReference Button_Log_Box_Animal_Gamepad;
    public InputActionReference Button_Exit_Box_Animal_Gamepad;

    //Controllare Input Se sia tastiera o GamePad per attivare le icone
    public Move_Player MovePlayer;

    //Ui
    public GameObject Icon_Log_GamePad;
    public GameObject Icon_Exit_GamePad;
    public GameObject Icon_Log_KeyBord;


    private string currentCollisionTag;
    public void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        // Collider con box degli animali
        if (collider.gameObject.CompareTag("statistics_cow") ||
            collider.gameObject.CompareTag("statistics_chicken") ||
            collider.gameObject.CompareTag("statistics_Sheep") ||
            collider.gameObject.CompareTag("statistics_pig"))
        {
            currentCollisionTag = collider.gameObject.tag;
            Button_Log_Box.SetActive(true);
            Button_Log_Box_Animal_Gamepad.action.Enable(); // Abilita solo per animali
            Button_Exit_Box_Animal_Gamepad.action.Enable();
            Button_Log_Magazzini_Gamepad.action.Disable(); // Disabilita per magazzini
            Button_Exit_Magazzini_Gamepad.action.Disable();
        }

        // Collider che rappresenta i box del magazzino e del silo
        if (collider.gameObject.CompareTag("Box_Magazzino") ||
            collider.gameObject.CompareTag("Box_Silo"))
        {
            if (MovePlayer.controllerMovement != Vector2.zero)
            {
                Icon_Log_GamePad.SetActive(true);

                Icon_Log_KeyBord.SetActive(false);
            }
            else if(MovePlayer.keyboardMovement != Vector2.zero)
            {
                Icon_Log_KeyBord.SetActive(true);

                Icon_Log_GamePad.SetActive(false);
                Icon_Exit_GamePad.SetActive(false);
            }
            currentCollisionTag = collider.gameObject.tag;
            Button_Log.SetActive(true);
            Button_Log_Box_Animal_Gamepad.action.Disable(); // Disabilita per animali
            Button_Exit_Box_Animal_Gamepad.action.Disable();
            Button_Log_Magazzini_Gamepad.action.Enable(); // Abilita solo per magazzini
            Button_Exit_Magazzini_Gamepad.action.Enable();
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        
            Button_Log_Box.SetActive(false);
            Button_Log.SetActive(false);
            currentCollisionTag = null;

        Icon_Log_GamePad.SetActive(false);
        Icon_Exit_GamePad.SetActive(false);
        Icon_Log_KeyBord.SetActive(false);
    }

    public void ButtonLoadMagazzini()
     {
        if (currentCollisionTag == "Box_Magazzino")
        {
            Ui_Magazzino.SetActive(true);
        }
        else if(currentCollisionTag == "Box_Silo")
        {
            Ui_Silo.SetActive(true);
        }

        Button_Log.SetActive(false);
        Button_Exit.SetActive(true);
    }
    
    //Log Magazzino e silo tramite tastiera
    public void ButtonExitMagazzini()
    {
        if (currentCollisionTag == "Box_Magazzino")
        {
            Ui_Magazzino.SetActive(false);
        }
        else if (currentCollisionTag == "Box_Silo")
        {
            Ui_Silo.SetActive(false);
        }

        Button_Exit.SetActive(false);
        Icon_Log_KeyBord.SetActive(false);
    }

    //Log Recinti tramite tastiera
    public void OnButtonLogBoxClick()
    {
        if (currentCollisionTag == "statistics_cow")
        {
            Ui_cow.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_chicken")
        {
            Ui_Chicken.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_Sheep")
        {
            Ui_Sheep.SetActive(true);
        }
        else if (currentCollisionTag == "statistics_pig")
        {
            Ui_pig.SetActive(true);
        }
        


        Button_Log_Box.SetActive(false);
        Button_Exit_Box.SetActive(true);
    }

    //Uscita Recinti tramite tastiera
    public void OnButtonExitBoxClick()
    {
        if (currentCollisionTag == "statistics_cow")
        {
            Ui_cow.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_chicken")
        {
            Ui_Chicken.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_Sheep")
        {
            Ui_Sheep.SetActive(false);
        }
        else if (currentCollisionTag == "statistics_pig")
        {
            Ui_pig.SetActive(false);
        }
        

        // Optionally, hide the button after it is clicked
        Button_Exit_Box.SetActive(false);
    }

    //Codice GamePad//
    private void OnEnable()
    {
        Button_Log_Magazzini_Gamepad.action.started += LogMagazziniGamePad;
        Button_Exit_Magazzini_Gamepad.action.started += ExitGamePad;

        Button_Log_Box_Animal_Gamepad.action.started += LogBoxAnimal;
        Button_Exit_Box_Animal_Gamepad.action.started += ExitGamePad;


        Button_Exit_Box_Animal_Gamepad.action.Enable();
        Button_Exit_Magazzini_Gamepad.action.Enable();
    }

    private void OnDisable()
    {
        Button_Log_Magazzini_Gamepad.action.started -= LogMagazziniGamePad;
        Button_Exit_Magazzini_Gamepad.action.started -= ExitMagazziniGamePad;

        Button_Log_Box_Animal_Gamepad.action.started -= LogBoxAnimal;
        Button_Exit_Box_Animal_Gamepad.action.started -= ExitBoxAnimal;

        Button_Exit_Box_Animal_Gamepad.action.Disable();
        Button_Exit_Magazzini_Gamepad.action.Disable();

    }

    public void LogMagazziniGamePad(InputAction.CallbackContext Obj)
    {

        ButtonLoadMagazzini();

        Icon_Log_GamePad.SetActive(false);

        Icon_Exit_GamePad.SetActive(true);
    }

    public void ExitMagazziniGamePad(InputAction.CallbackContext Obj)
    {

        ButtonExitMagazzini();
        Icon_Exit_GamePad.SetActive(false);
    }

    public void LogBoxAnimal(InputAction.CallbackContext Obj)
    {
        OnButtonLogBoxClick();
    }

    public void ExitBoxAnimal(InputAction.CallbackContext Obj)
    {
        OnButtonExitBoxClick();
    }

    public void ExitGamePad(InputAction.CallbackContext Obj)
    {
        if(currentCollisionTag == "Box_Magazzino" ||  currentCollisionTag == "Box_Silo")
        {
            ExitMagazziniGamePad(Obj);
        }
        else if(currentCollisionTag == "statistics_cow" ||
             currentCollisionTag == "statistics_chicken" ||
             currentCollisionTag == "statistics_Sheep" ||
             currentCollisionTag == "statistics_pig")
        {
            ExitBoxAnimal(Obj);
        }
    }




}
