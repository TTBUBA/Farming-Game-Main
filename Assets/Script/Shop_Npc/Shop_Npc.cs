using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    
    public static Shop Instance { get; private set; }

    // Array
    public Image[] Box_Shop;
    public string[] ortaggioTypes;
    public Tracker_Box[] trackerBoxes;
    public InventorySlot[] inventorySlots;

    [SerializeField] public int currentIndex = 0;

    // Variabili per memorizzare l'ortaggio selezionato
    private int selectedOrtaggioIndex;
    private int selectedOrtaggioPrice;
    public string selectedOrtaggioType;

    // Variabile per memorizzare il totale corrente del portafoglio
    private int CurrentWallet = 0;
    public Text Text_Wallet;

    [SerializeField] private GameManager gamemanager;
    [SerializeField] private GameObject shop;

    // InputController
    [Header("UI Controller")]
    public GameObject[] Ui_Controller;
    public GameObject[] Ui_Keyboard;
    public GameObject Icon_Quit_Shop;

    [Header("Input Controller")]
    [SerializeField] private InputActionReference Icon_Controller_Shop;
    [SerializeField] private InputActionReference Button_NextSlot;
    [SerializeField] private InputActionReference Button_BackSlot;
    [SerializeField] private InputActionReference Button_IncreseQuantity;
    [SerializeField] private InputActionReference Button_DecreseQuantity;
    [SerializeField] private InputActionReference Button_Quit;

    // Input Manager
    public PlayerInput Playerinput;

    [SerializeField] private float timeElapsed;
    [SerializeField] private bool isHolding_Increse = false;     // Indica se il bottone è premuto
    [SerializeField] private bool isHolding_Decrese = false;

    public void Update()
    {
        TrackerDevice();
        
        if (isHolding_Increse && !isHolding_Decrese)
        {
            Increment_Quantity();
        }

        if (isHolding_Decrese && !isHolding_Increse)
        {
            Decrese_Quantity();
        }
        
        
    }

    // Metodo per confermare l'acquisto dell'ordine
    public void BuyOrder()
    {
        // Controlla se il giocatore ha abbastanza monete
        if (gamemanager.Coin >= CurrentWallet)
        {
            // Deduce il totale dal portafoglio del giocatore
            gamemanager.Coin -= CurrentWallet;

            // Cerca uno slot esistente per il tipo di seme e aumenta la quantità
            foreach (var trackerBox in trackerBoxes)
            {
                int quantityToAdd = trackerBox.CurrentValue;

                if (quantityToAdd > 0)
                {
                    foreach (var slot in inventorySlots)
                    {
                        if (slot.vegetableData.NameVegetable == trackerBox.Name_Box)
                        {
                            slot.IncreaseSeedQuantity(quantityToAdd);
                            break;
                        }
                    }
                }

            }

            // Reset del carrello e del totale nel portafoglio
            CurrentWallet = 0;
            foreach (var trackerbox in trackerBoxes)
            {
                trackerbox.CurrentValue = 0;
                trackerbox.Value_Quantity.text = "0";
                trackerbox.Value_Vegetables.text = "X0";
            }

            UpdateCarrelloTotale();

            Debug.Log("Ordine effettuato");
        }

    }

    public void NextSlot()
    {
        Box_Shop[currentIndex].transform.localScale = Vector2.one;

        currentIndex = (currentIndex + 1) % Box_Shop.Length;
        selectedOrtaggioType = ortaggioTypes[currentIndex];


        ActiveImageController();
    }

    public void BackSlot()
    {
        Box_Shop[currentIndex].transform.localScale = Vector2.one;

        currentIndex = (currentIndex - 1 + Box_Shop.Length) % Box_Shop.Length;
        selectedOrtaggioType = ortaggioTypes[currentIndex];

        ActiveImageController();
    }
    
    // Funzione da chiamare quando il pulsante viene premuto
    public void OnPointerDown_Increse()
    {    
        isHolding_Increse = true;
        timeElapsed = 0f;
    }

    // Funzione da chiamare quando il pulsante viene rilasciato
    public void OnPointerUp_Increse()
    {
        isHolding_Increse = false;
        timeElapsed = 0f; // Resetta il tempo trascorso
    }

    // Funzione da chiamare quando il pulsante viene premuto
    public void OnPointerDown_Decrese()
    {
        isHolding_Decrese = true;
        timeElapsed = 0f; 
    }

    // Funzione da chiamare quando il pulsante viene rilasciato
    public void OnPointerUp_Decrese()
    {
        isHolding_Decrese = false;
        timeElapsed = 0f; 
    }

    public void Increment_Quantity()
    {

        if (trackerBoxes[currentIndex].CurrentValue < 999)
        {
            timeElapsed += Time.deltaTime;



            int incrementValue = Mathf.RoundToInt(1f + timeElapsed * 2);
            

            trackerBoxes[currentIndex].CurrentValue += incrementValue;
            trackerBoxes[currentIndex].CurrentValue = Mathf.Clamp(trackerBoxes[currentIndex].CurrentValue, 0, trackerBoxes[currentIndex].MaxValue);


            trackerBoxes[currentIndex].Value_Quantity.text = trackerBoxes[currentIndex].CurrentValue.ToString();
            trackerBoxes[currentIndex].Value_Vegetables.text = "X" + trackerBoxes[currentIndex].CurrentValue.ToString();

            CurrentWallet += trackerBoxes[currentIndex].ortaggioPrices;
            UpdateCarrelloTotale();

            //Debug.Log(trackerBoxes[currentIndex].CurrentValue.ToString() + trackerBoxes[currentIndex].Name_Box);
        }
    }

    public void Decrese_Quantity()
    {
        if (trackerBoxes[currentIndex].CurrentValue > 0)
        {
            timeElapsed -= Time.deltaTime;

            int incrementValue = Mathf.RoundToInt(1f - timeElapsed * 2);

            trackerBoxes[currentIndex].CurrentValue -= incrementValue;


            
            trackerBoxes[currentIndex].Value_Quantity.text = trackerBoxes[currentIndex].CurrentValue.ToString();
            trackerBoxes[currentIndex].Value_Vegetables.text = "X" + trackerBoxes[currentIndex].CurrentValue.ToString();

            CurrentWallet -= trackerBoxes[currentIndex].ortaggioPrices;
            UpdateCarrelloTotale();

        }
    }

    private void UpdateCarrelloTotale()
    {
        Text_Wallet.text = CurrentWallet.ToString() + "$";
    }

    private void TrackerDevice()
    {
        if(Playerinput != null)
        {
            var CurrentDevice = Playerinput.currentControlScheme;

            foreach(var device in Playerinput.devices)
            {
                if(device is Keyboard)
                {
                    foreach (var iconKeyboard in Ui_Keyboard)
                    {
                        iconKeyboard.SetActive(true);
                        
                    }

                    foreach (var iconController in Ui_Controller)
                    {
                        iconController.SetActive(false);
                       
                    }
                }
                else if(device is Gamepad)
                {
                    foreach(var iconController in Ui_Controller)
                    {
                        iconController.SetActive(true);
                        
                    }

                    foreach (var iconKeyboard in Ui_Keyboard)
                    {
                        iconKeyboard.SetActive(false);

                    }
                }
            }
        }
    }

    // Metodo per chiudere il negozio
    public void QuitShop()
    {
        shop.SetActive(false);
    }


    //========== Input Controller ==========//
    private void OnEnable()
    {
        Icon_Controller_Shop.action.started += ShopOrderController;
        Button_NextSlot.action.started += ScrollNextSlot;
        Button_BackSlot.action.started += ScrollBackSlot;

        Button_IncreseQuantity.action.started += IncreseQuantiyController;
        Button_IncreseQuantity.action.canceled += IncreseQuantiyController;

        Button_DecreseQuantity.action.started += DecreseQuantiyController;
        Button_Quit.action.started += buttonQuit;

        Button_Quit.action.Enable();
        Icon_Controller_Shop.action.Enable();
        Button_NextSlot.action.Enable();
        Button_IncreseQuantity.action.Enable();
        Button_DecreseQuantity.action.Enable();
    }

    private void OnDisable()
    {
        Icon_Controller_Shop.action.started -= ShopOrderController;
        Button_NextSlot.action.started -= ScrollNextSlot;
        Button_BackSlot.action.started -= ScrollBackSlot;

        Button_IncreseQuantity.action.started -= IncreseQuantiyController;
        Button_IncreseQuantity.action.canceled -= IncreseQuantiyController;

        Button_DecreseQuantity.action.started -= DecreseQuantiyController;
        Button_DecreseQuantity.action.canceled -= DecreseQuantiyController;

        Button_Quit.action.started -= buttonQuit;

        Button_Quit.action.Disable();
        Icon_Controller_Shop.action.Disable();
        Button_NextSlot.action.Disable();
        Button_BackSlot.action.Disable();
        Button_IncreseQuantity.action.Disable();   
        Button_DecreseQuantity.action.Disable();
    }

    private void ShopOrderController(InputAction.CallbackContext context)
    {
        BuyOrder();
    }

    private void ScrollNextSlot(InputAction.CallbackContext context)
    {
        NextSlot();
    }

    private void ScrollBackSlot(InputAction.CallbackContext context)
    {
        BackSlot();
    }

    private void IncreseQuantiyController(InputAction.CallbackContext context)
    {
        //Debug.Log($"Context Phase: {context.phase}, Control: {context.control.name}");

        if (context.phase == InputActionPhase.Started)
        {
            isHolding_Increse = true;
            timeElapsed = 0f; // Resetta il tempo trascorso
            Increment_Quantity();
            
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isHolding_Increse = false;
            

        }


    }

    private void DecreseQuantiyController(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            isHolding_Decrese = true;
            timeElapsed = 0f;
            Decrese_Quantity();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            isHolding_Decrese = false;

        }
       
       
    }

    private void buttonQuit(InputAction.CallbackContext context)
    {
        QuitShop();
    }
    private void ActiveImageController()
    {
        switch (currentIndex)
        {
            case 0:
                ActionImage();
                break;

            case 1:
                ActionImage();
                break;

            case 2:
                ActionImage();
                break;
        }
    }

    private void ActionImage()
    {
        Box_Shop[currentIndex].transform.DOScale(new Vector3(1.05f, 1.05f , 1f), 0.2f).SetEase(Ease.InBounce);
    }



}
