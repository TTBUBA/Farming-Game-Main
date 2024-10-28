using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SeedSelectionManager : MonoBehaviour
{
    public GameObject seedSelectionPanel; // Pannello di selezione semi
    public InventorySlot[] seedSlots; // Slot per gli ortaggi
    public int selectedSlotIndex = 0;

    [Header("Ui_Seed")]
    public Image ImageSeedSelect;
    public Text QuantitySeedSelect;
    public GameObject Button_Quit;

    [Header("Ui_Controller")]
    public GameObject Ui_OpenSeedSelection_controller;
    public GameObject Ui_CloseSeedSelection_controller;

    [Header("Ui_Keyboard")] 
    public GameObject Ui_CloseSeedSelection_Keyboard;

    [Header("Input_Controller")]
    public InputActionReference OpenSeedSelection_controller;
    public InputActionReference CloseSeedSelection_controller;
    [Header("Input_Keyboard")]
    public InputActionReference OpenSeedSelection_Keyboard; 
    public InputActionReference CloseSeedSelection_Keyboard;

    public GameManager gameManager;
    public Player_Manager playerManager;
    public InventoryManager inventoryManager;

    public void Awake()
    {
        InventorySlot selectedSlot = seedSlots[0];
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString();
    }

    private void Update()
    {
        UpdateUi(); 
        CheakDeviceUsing();
    }
    public void OpenSeedSelection()
    {
        seedSelectionPanel.SetActive(true); // Mostra il pannello di selezione
        Button_Quit.SetActive(true);
        UpdateSeedSlots(); // Aggiorna gli slot con i dati degli ortaggi

    }

    public void CloseSeedSelection()
    {  
        seedSelectionPanel.SetActive(false); // Nasconde il pannello di selezione
        Button_Quit.SetActive(false);
    }

    private void UpdateUi()
    {
        InventorySlot selectedSlot = seedSlots[0];
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString();
    }
    private void CheakDeviceUsing()
    {
        if (gameManager.UsingKeyboard == true)
        {
            Ui_CloseSeedSelection_Keyboard.SetActive(true);
            Ui_CloseSeedSelection_controller.SetActive(false);
        }
        else
        {
            Ui_CloseSeedSelection_controller.SetActive(true);
            Ui_CloseSeedSelection_Keyboard.SetActive(false);
        }
    }

    void UpdateSeedSlots()
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            var vegetableData = GetVegetableData(i);
            if (vegetableData != null)
            {
                seedSlots[i].SetSlot(vegetableData);
            }
        }
    }

    VegetableData GetVegetableData(int index)
    {
        // Restituisci l'ortaggio corrispondente in base all'indice
        return /* Dati ortaggio, ad esempio: */ null;
    }

    public void SelectSeed(InventorySlot selectedSlot)
    {
        //Debug.Log(selectedSlot.vegetableData.NameVegetable + ":" + selectedSlot.vegetableData.quantity);
        //Debug.Log(selectedSlot.vegetableData);
        // Debug.Log(selectedSlot.vegetableData.ItemType);
        selectedSlotIndex++;
        ImageSeedSelect.sprite = selectedSlot.vegetableData.IconVegetable;
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString();

        inventoryManager.SetCurrentSelectedSlot(selectedSlot);// Aggiorna lo slot selezionato nell'inventario

    }
    //========INPUT GENERAL========//
    private void OnEnable()
    {
        OpenSeedSelection_Keyboard.action.Enable();
        CloseSeedSelection_Keyboard.action.Enable();

        OpenSeedSelection_Keyboard.action.started += OpenSeedPanel_Keyboard;
        CloseSeedSelection_Keyboard.action.started += CloseSeedPanel_Keyboard;
    }
    private void OnDisable()
    {
        OpenSeedSelection_Keyboard.action.Disable();
        CloseSeedSelection_Keyboard.action.Disable();

        OpenSeedSelection_Keyboard.action.started -= OpenSeedPanel_Keyboard;
        CloseSeedSelection_Keyboard.action.started -= CloseSeedPanel_Keyboard;
    }

    //========INPUT KEYBOARD========//
    private void OpenSeedPanel_Keyboard(InputAction.CallbackContext context)
    {
         OpenSeedSelection();   
    }

    private void CloseSeedPanel_Keyboard(InputAction.CallbackContext context)
    {
        CloseSeedSelection();     
    }
    //========INPUT CONTROLLER========//

}
