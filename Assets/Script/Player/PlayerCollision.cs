using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCollision : MonoBehaviour
{
    [Header("UI Button_Log_Exit")]
    public GameObject Ui_Button_Log;
    public GameObject Ui_Button_Exit;

    [Header("UI Statistics - Animal")]
    public GameObject Ui_Chicken;
    public GameObject Ui_Cow;
    public GameObject Ui_Sheep;
    public GameObject Ui_Pig;

    [Header("UI Statistics - Magazzini")]
    public GameObject Ui_Silo;
    public GameObject Ui_Magazzino;

    [Header("UI Showcase")]
    public GameObject Ui_Order;

    [Header("UI Mill")]
    public GameObject Ui_Mill;

    [Header("KeyBoard Controls")]
    public InputActionReference Button_Log_KeyBoard;

    [Header("UI-DEVICE")]
    public GameObject Ui_Keyboard;
    public GameObject Ui_Controller;


    private string currentCollisionTag;
    private PlayerInput Playerinput;

    private void Start()
    {
        Playerinput = GetComponent<PlayerInput>();
        Ui_Button_Log.SetActive(false);
        Ui_Button_Exit.SetActive(false);
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
                    Ui_Keyboard.SetActive(true);
                    Ui_Controller.SetActive(false);
                }
                else if (device is Gamepad)
                {
                    Ui_Controller.SetActive(true);
                    Ui_Keyboard.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        currentCollisionTag = collider.gameObject.tag;
        Ui_Button_Log.SetActive(true);  // Mostra solo il bottone
        TrackerDevice();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        ResetUI();  // Quando esci dalla collisione, resetta tutto
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

        currentCollisionTag = null;
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
        }
    }

    private void OnEnable()
    {
        Button_Log_KeyBoard.action.Enable();

        Button_Log_KeyBoard.action.started += Button_Log;

    }

    private void OnDisable()
    {
        Button_Log_KeyBoard.action.Disable();
        Button_Log_KeyBoard.action.started -= Button_Log;
       
    }
    private void Button_Log(InputAction.CallbackContext context)
    {

       ShowCurrentUI();  // Mostra la UI associata al tag corrente
       Debug.Log("click");
        
    }

    public void ShowUicurrent()
    {
        ShowCurrentUI();
    }
}
