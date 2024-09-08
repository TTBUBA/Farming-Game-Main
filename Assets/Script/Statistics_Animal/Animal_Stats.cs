using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Animal : MonoBehaviour
{
    public string NameAnimal;
    public string NameProductAnimal;
    public string RequiredSackName; // Nome del sacchetto richiesto per questo animale
    

    // Statistiche dell'animale
    public float hunger = 0f;  // Livello di fame iniziale
    public float thirst = 0f;  // Livello di sete iniziale
    public float production = 0f;  // Produzione iniziale
    public int quantity;


    // UI Statistiche dell'animale
    public Image barHunger;
    public Image barThirst;
    public Image barProduction;
    //public Text Text_Quantity_AnimalProduct;
    public Text Text_Animal_fence_counter;

    // Tempi di aggiornamento
    public float hungerDecreaseRate;
    public float thirstDecreaseRate;
    public float productionIncreaseRate;

    private float minValue = 0f;
    private float maxValue = 1f;


    //comanda controller
    public InputActionReference Button_Incrase_hunger;
    public InputActionReference Button_Incrase_Thirst;


    // UI CONTROLLER
    public GameObject Icon_Increse_Hunger_Controller;
    public GameObject Icon_Increse_Thirst_Controller;

    // UI Tastiera
    public GameObject Icon_Increse_Hunger_KeyBoard;
    public GameObject Icon_Increse_Thirst_KeyBoard;

    // Riferimento al Mill_Manager
    public Mill_Manager millManager;
    public GameManager gameManager;



    public PlayerInput Playerinput;
    public UI_Controller_Animal UiController;
    void Start()
    {
        hunger = 0f;
        thirst = 0f;

        barHunger.fillAmount = 0f;
        barThirst.fillAmount = 0f;
        barProduction.fillAmount = 0f;

    }

    void Update()
    {
        DecreaseHunger();
        IncreaseProduction();
        UpdateUi();
        ShowAppropriateIcons();
    }

    private void DecreaseHunger()
    {
        if (gameManager != null && gameManager.GetAnimalCount(NameAnimal) > 0)
        {
            hunger -= Time.deltaTime / hungerDecreaseRate;

            hunger = Mathf.Clamp(hunger, minValue, maxValue);

            if (hunger <= 0.2f)
            {
                // Modifica il tasso di aumento della produzione solo quando la fame è critica
                productionIncreaseRate = 400f;
            }
        }
        

        UpdateHungerBar();
    }
    public void decreaseThirst()
    {
        if(gameManager != null && gameManager.GetAnimalCount(NameAnimal) > 0)
        {
            thirst -= Time.deltaTime / thirstDecreaseRate;
            thirst = Mathf.Clamp(hunger, minValue, maxValue);
        }
    }

    private void IncreaseProduction()
    {
        if (gameManager != null && gameManager.GetAnimalCount(NameAnimal) > 0)
        {
            if (hunger > 0f)
            {
                // Incrementa la produzione solo se la fame è sopra 0
                production += Time.deltaTime / productionIncreaseRate;
                production = Mathf.Clamp(production, minValue, maxValue);
            }

            // Se la produzione raggiunge 1, la produzione viene azzerata e si ferma
            if (production >= 1f)
            {
                production = 0f;

                quantity += 1;
            }


            UpdateProductionbar();
        }
        

    }

    public void ButtonIncraseHunger()
    {
        SackData selectedSack = millManager.sackDataArray[millManager.selectedSackIndex];
        if(selectedSack.NameSack == RequiredSackName && selectedSack.quantity >= 1)
        {
            hunger += 0.1f;
            selectedSack.quantity -= 1;
            UpdateHungerBar();
        }
        
    }

    public void ButtonIncraseThirst()
    {
        thirst += 0.1f;
    }


    public void UpdateUi()
    {
        //  mostra la quantita del animale prodotto 
        // Text_Quantity_AnimalProduct.text = NameProductAnimal + " = "  + quantity.ToString();

        Text_Animal_fence_counter.text = gameManager.GetAnimalCount(NameAnimal).ToString() + " / 10";
    }


    private void UpdateHungerBar()
    {
        if (barHunger != null)
        {
            barHunger.fillAmount = hunger;
        }
    }

    private void UpdateThirstBar()
    {
        if (barThirst != null)
        {
            barThirst.fillAmount = thirst;
        }
    }

    private void UpdateProductionbar()
    {
        if (barProduction != null)
        {
            barProduction.fillAmount = production;
        }
    }

    // Mostra le icone appropriate a seconda se si utilizza un gamepad o una tastiera
    private void ShowAppropriateIcons()
    {
       if(Playerinput != null)
        {
            var currentDevice = Playerinput.currentControlScheme;

            foreach (var Device in Playerinput.devices)
            {
                if (Device is Gamepad)
                {
                        Icon_Increse_Hunger_Controller.SetActive(true);
                        Icon_Increse_Thirst_Controller.SetActive(true);
                        Icon_Increse_Hunger_KeyBoard.SetActive(false);
                        Icon_Increse_Thirst_KeyBoard.SetActive(false);
                }
                else if (Device is Keyboard)
                {
                        Icon_Increse_Hunger_KeyBoard.SetActive(true);
                        Icon_Increse_Thirst_KeyBoard.SetActive(true);
                        Icon_Increse_Hunger_Controller.SetActive(false);
                        Icon_Increse_Thirst_Controller.SetActive(false);
                }
            }
        }

    }

    // Comandandi Controller

    private void OnEnable()
    {
        Button_Incrase_hunger.action.started += Incrase_hunger_Controller;
        Button_Incrase_Thirst.action.started += Incrase_Thirst_Controller;

        Button_Incrase_hunger.action.Enable();
        Button_Incrase_Thirst.action.Enable();
    }

    private void OnDisable()
    {
        Button_Incrase_hunger.action.started -= Incrase_hunger_Controller;
        Button_Incrase_Thirst.action.started -= Incrase_Thirst_Controller;


        Button_Incrase_hunger.action.Disable();
        Button_Incrase_Thirst.action.Disable();
    }

    public void Incrase_hunger_Controller(InputAction.CallbackContext context)
    {
        if(UiController.currentIndex == 1)
        {
            SackData selectedSack = millManager.sackDataArray[millManager.selectedSackIndex];
            if (selectedSack.NameSack == RequiredSackName && selectedSack.quantity >= 1)
            {
                hunger += 0.1f;
                selectedSack.quantity -= 1;
                UpdateHungerBar();

            }
        }

        
    }

    public void Incrase_Thirst_Controller(InputAction.CallbackContext context)
    {
        if (UiController.currentIndex == 2)
        {
            thirst += 0.1f;
            UpdateThirstBar();
        }
        

    }





}
