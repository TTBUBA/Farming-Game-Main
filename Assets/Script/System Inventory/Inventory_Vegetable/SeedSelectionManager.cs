using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System;
using DG.Tweening;

public class SeedSelectionManager : MonoBehaviour
{
    public GameObject seedSelectionPanel; // Pannello di selezione semi
    public Slot_Vegetable[] seedSlots; // Array di slot per gli ortaggi
    public int selectedSlotIndex = 0; // Indice dello slot selezionato
    [SerializeField] private bool IsOpenseedSelection = false;

    [Header("Ui_Seed")]
    public Image ImageSeedSelect; // Immagine del seme selezionato
    public Sprite DefultSeed; // Sprite di default per il seme
    public Text QuantitySeedSelect; // Testo che mostra la quantità del seme selezionato
    public GameObject QuantitySeed; // Oggetto UI per mostrare la quantità del seme
    public GameObject Button_Quit; // Bottone per chiudere il pannello

    [Header("Ui_Controller")]
    public GameObject Ui_OpenSeedSelection_controller; 
    public GameObject Ui_CloseSeedSelection_controller; 

    [Header("Ui_Keyboard")]
    public GameObject Ui_CloseSeedSelection_Keyboard; 

    [Header("Input_Controller")]
    public InputActionReference OpenSeedSelection_controller; 
    public InputActionReference CloseSeedSelection_controller; 
    public InputActionReference nextSlot_controller;
    public InputActionReference backSlot_controller;

    [Header("Input_Keyboard")]
    public InputActionReference OpenSeedSelection_Keyboard; 
    public InputActionReference CloseSeedSelection_Keyboard; 

    public GameManager gameManager;
    public Player_Manager playerManager; 
    public InventoryManager inventoryManager; 

    private void Update()
    {
        UpdateUi(); // Aggiorna la UI ogni frame
        CheakDeviceUsing(); // Controlla il dispositivo in uso (tastiera o controller)
    }

    // Mostra il pannello di selezione semi
    public void OpenSeedSelection()
    {
        seedSelectionPanel.SetActive(true); // Attiva il pannello di selezione
        Button_Quit.SetActive(true); // Mostra il bottone per chiudere il pannello
        UpdateSeedSlots(); // Aggiorna gli slot con i dati degli ortaggi
        IsOpenseedSelection = true;
    }

    // Nasconde il pannello di selezione semi
    public void CloseSeedSelection()
    {
        seedSelectionPanel.SetActive(false); // Disattiva il pannello di selezione
        Button_Quit.SetActive(false); // Nasconde il bottone per chiudere il pannello
        IsOpenseedSelection = false;
    }

    // Aggiorna l'interfaccia utente in base allo slot selezionato
    private void UpdateUi()
    {

        Slot_Vegetable selectedSlot = inventoryManager.currentSelectedSlot; // Ottiene lo slot attualmente selezionato
        if( selectedSlot != null )
        {
            QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString(); // Aggiorna il testo con la quantità dell'ortaggio selezionato
        }
       
    }

    // Controlla se si sta usando la tastiera o il controller
    private void CheakDeviceUsing()
    {
        if (gameManager.UsingKeyboard) // Se si sta usando la tastiera
        {
            Ui_CloseSeedSelection_Keyboard.SetActive(true); // Mostra la UI per la chiusura tramite tastiera
            Ui_OpenSeedSelection_controller.SetActive(false);
            Ui_CloseSeedSelection_controller.SetActive(false); // Nasconde la UI per la chiusura tramite controller
        }
        else // Se si sta usando il controller
        {
            Ui_OpenSeedSelection_controller.SetActive(true);
            Ui_CloseSeedSelection_controller.SetActive(true); // Mostra la UI per la chiusura tramite controller
            Ui_CloseSeedSelection_Keyboard.SetActive(false); // Nasconde la UI per la chiusura tramite tastiera
        }
    }

    // Aggiorna gli slot con i dati attuali degli ortaggi
    void UpdateSeedSlots()
    {
        for (int i = 0; i < seedSlots.Length; i++)
        {
            var vegetableData = GetVegetableData(i); // Ottieni i dati dell'ortaggio per l'indice corrente
            if (vegetableData != null)
            {
                seedSlots[i].SetSlot(vegetableData); // Imposta i dati nell'apposito slot
            }
        }
    }

    // Restituisce i dati dell'ortaggio corrispondente all'indice fornito
    VegetableData GetVegetableData(int index)
    {
        return /* Dati ortaggio, ad esempio: */ null; // Placeholder per la restituzione dei dati dell'ortaggio
    }

    // Seleziona il seme e aggiorna l'interfaccia utente
    public void SelectSeed(Slot_Vegetable selectedSlot)
    {
        ImageSeedSelect.sprite = selectedSlot.vegetableData.IconVegetable; // Aggiorna l'immagine del seme selezionato
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString(); // Aggiorna il testo della quantità
        QuantitySeed.SetActive(true); // Mostra la UI della quantità
        inventoryManager.SetCurrentSelectedSlot(selectedSlot); // Aggiorna lo slot selezionato nell'inventario

        UpdateUi();
        
        
    }

    private void NextSlotVegetable()
    {
        seedSlots[selectedSlotIndex].transform.localScale = Vector3.one;

        selectedSlotIndex = (selectedSlotIndex + 1) % seedSlots.Length;
        SelectSeed(seedSlots[selectedSlotIndex]);
        AnimationSeedSelection();
    }

    private void BackSlotVegetable()
    {
        seedSlots[selectedSlotIndex].transform.localScale = Vector3.one;

        selectedSlotIndex = (selectedSlotIndex + seedSlots.Length - 1) % seedSlots.Length;
        SelectSeed(seedSlots[selectedSlotIndex]);
        AnimationSeedSelection();
    }
    //========INPUT SETTING========//
    private void OnEnable()
    {
        //Keyboard
        OpenSeedSelection_Keyboard.action.Enable(); // Abilita l'azione per aprire la selezione tramite tastiera
        CloseSeedSelection_Keyboard.action.Enable(); // Abilita l'azione per chiudere la selezione tramite tastiera

        OpenSeedSelection_Keyboard.action.started += OpenSeedPanel_Keyboard; // Aggiunge il listener per aprire il pannello
        CloseSeedSelection_Keyboard.action.started += CloseSeedPanel_Keyboard; // Aggiunge il listener per chiudere il pannello

        //controller
        OpenSeedSelection_controller.action.Enable(); // Abilita l'azione per aprire la selezione tramite controller
        CloseSeedSelection_controller.action.Enable(); // Abilita l'azione per chiudere la selezione tramite controller
        nextSlot_controller.action.Enable();
        backSlot_controller.action.Enable();

        nextSlot_controller.action.started += NextSlot_Controller;
        backSlot_controller.action.started += BackSlot_Controller;
        OpenSeedSelection_controller.action.started += OpenSeedPanel_Controller; // Aggiunge il listener per aprire il pannello
        CloseSeedSelection_controller.action.started += CloseSeedPanel_Controller; // Aggiunge il listener per chiudere il pannello
    }

    private void OnDisable()
    {
        //Keyboard
        OpenSeedSelection_Keyboard.action.Disable(); // Disabilita l'azione per aprire la selezione tramite tastiera
        CloseSeedSelection_Keyboard.action.Disable(); // Disabilita l'azione per chiudere la selezione tramite tastiera

        OpenSeedSelection_Keyboard.action.started -= OpenSeedPanel_Keyboard; // Rimuove il listener per aprire il pannello
        CloseSeedSelection_Keyboard.action.started -= CloseSeedPanel_Keyboard; // Rimuove il listener per chiudere il pannello

        //controller
        OpenSeedSelection_controller.action.Disable(); // Disabilita l'azione per aprire la selezione tramite controller
        CloseSeedSelection_controller.action.Disable(); // Disabilita l'azione per chiudere la selezione tramite controller
        nextSlot_controller.action.Disable();
        backSlot_controller.action.Disable();

        nextSlot_controller.action.started -= NextSlot_Controller;
        backSlot_controller.action.started -= BackSlot_Controller;
        OpenSeedSelection_controller.action.started -= OpenSeedPanel_Controller; // Rimuove il listener per aprire il pannello
        CloseSeedSelection_controller.action.started -= CloseSeedPanel_Controller; // Rimuove il listener per chiudere il pannello

    }


    //========INPUT KEYBOARD========//
    private void OpenSeedPanel_Keyboard(InputAction.CallbackContext context)
    {
        OpenSeedSelection(); // Chiama il metodo per aprire il pannello
    }

    private void CloseSeedPanel_Keyboard(InputAction.CallbackContext context)
    {
        CloseSeedSelection(); // Chiama il metodo per chiudere il pannello
    }

    //========INPUT CONTROLLER========//
    private void OpenSeedPanel_Controller(InputAction.CallbackContext context)
    {
        OpenSeedSelection(); // Chiama il metodo per aprire il pannello
    }

    private void CloseSeedPanel_Controller(InputAction.CallbackContext context)
    {
        CloseSeedSelection(); // Chiama il metodo per chiudere il pannello
    }

    private void NextSlot_Controller(InputAction.CallbackContext context)
    {
        if (IsOpenseedSelection)
        {
            NextSlotVegetable();
        }
            
    }
    private void BackSlot_Controller(InputAction.CallbackContext context)
    {
        if (IsOpenseedSelection)
        {
            BackSlotVegetable();
        }
    }

    private void AnimationSeedSelection()
    {
        seedSlots[selectedSlotIndex].transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f),0.4f).SetEase(Ease.Linear);
    }
}
