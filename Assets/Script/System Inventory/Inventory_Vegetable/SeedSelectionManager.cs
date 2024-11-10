using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SeedSelectionManager : MonoBehaviour
{
    public GameObject seedSelectionPanel; // Pannello di selezione semi
    public Slot_Vegetable[] seedSlots; // Array di slot per gli ortaggi
    public int selectedSlotIndex = 0; // Indice dello slot selezionato

    [Header("Ui_Seed")]
    public Image ImageSeedSelect; // Immagine del seme selezionato
    public Sprite DefultSeed; // Sprite di default per il seme
    public Text QuantitySeedSelect; // Testo che mostra la quantità del seme selezionato
    public GameObject QuantitySeed; // Oggetto UI per mostrare la quantità del seme
    public GameObject Button_Quit; // Bottone per chiudere il pannello

    [Header("Ui_Controller")]
    public GameObject Ui_OpenSeedSelection_controller; // UI per l'apertura della selezione semi tramite controller
    public GameObject Ui_CloseSeedSelection_controller; // UI per la chiusura della selezione semi tramite controller

    [Header("Ui_Keyboard")]
    public GameObject Ui_CloseSeedSelection_Keyboard; // UI per la chiusura della selezione semi tramite tastiera

    [Header("Input_Controller")]
    public InputActionReference OpenSeedSelection_controller; // Riferimento per aprire la selezione semi tramite controller
    public InputActionReference CloseSeedSelection_controller; // Riferimento per chiudere la selezione semi tramite controller
    [Header("Input_Keyboard")]
    public InputActionReference OpenSeedSelection_Keyboard; // Riferimento per aprire la selezione semi tramite tastiera
    public InputActionReference CloseSeedSelection_Keyboard; // Riferimento per chiudere la selezione semi tramite tastiera

    public GameManager gameManager; // Riferimento al gestore del gioco
    public Player_Manager playerManager; // Riferimento al gestore del giocatore
    public InventoryManager inventoryManager; // Riferimento al gestore dell'inventario

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
    }

    // Nasconde il pannello di selezione semi
    public void CloseSeedSelection()
    {
        seedSelectionPanel.SetActive(false); // Disattiva il pannello di selezione
        Button_Quit.SetActive(false); // Nasconde il bottone per chiudere il pannello
        //QuantitySeed.SetActive(false); // Nasconde la UI della quantità dei semi
        //ImageSeedSelect.sprite = DefultSeed; // Ripristina l'immagine di default
    }

    // Aggiorna l'interfaccia utente in base allo slot selezionato
    private void UpdateUi()
    {
        Slot_Vegetable selectedSlot = seedSlots[selectedSlotIndex]; // Ottiene lo slot attualmente selezionato
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString(); // Aggiorna il testo con la quantità dell'ortaggio selezionato
    }

    // Controlla se si sta usando la tastiera o il controller
    private void CheakDeviceUsing()
    {
        if (gameManager.UsingKeyboard) // Se si sta usando la tastiera
        {
            Ui_CloseSeedSelection_Keyboard.SetActive(true); // Mostra la UI per la chiusura tramite tastiera
            Ui_CloseSeedSelection_controller.SetActive(false); // Nasconde la UI per la chiusura tramite controller
        }
        else // Se si sta usando il controller
        {
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
        selectedSlotIndex++; // Incrementa l'indice dello slot selezionato
        ImageSeedSelect.sprite = selectedSlot.vegetableData.IconVegetable; // Aggiorna l'immagine del seme selezionato
        QuantitySeedSelect.text = selectedSlot.vegetableData.quantity.ToString(); // Aggiorna il testo della quantità
        QuantitySeed.SetActive(true); // Mostra la UI della quantità
        inventoryManager.SetCurrentSelectedSlot(selectedSlot); // Aggiorna lo slot selezionato nell'inventario
    }

    //========INPUT GENERAL========//
    private void OnEnable()
    {
        OpenSeedSelection_Keyboard.action.Enable(); // Abilita l'azione per aprire la selezione tramite tastiera
        CloseSeedSelection_Keyboard.action.Enable(); // Abilita l'azione per chiudere la selezione tramite tastiera

        OpenSeedSelection_Keyboard.action.started += OpenSeedPanel_Keyboard; // Aggiunge il listener per aprire il pannello
        CloseSeedSelection_Keyboard.action.started += CloseSeedPanel_Keyboard; // Aggiunge il listener per chiudere il pannello
    }

    private void OnDisable()
    {
        OpenSeedSelection_Keyboard.action.Disable(); // Disabilita l'azione per aprire la selezione tramite tastiera
        CloseSeedSelection_Keyboard.action.Disable(); // Disabilita l'azione per chiudere la selezione tramite tastiera

        OpenSeedSelection_Keyboard.action.started -= OpenSeedPanel_Keyboard; // Rimuove il listener per aprire il pannello
        CloseSeedSelection_Keyboard.action.started -= CloseSeedPanel_Keyboard; // Rimuove il listener per chiudere il pannello
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
    private void OnEnableController()
    {
        OpenSeedSelection_controller.action.Enable(); // Abilita l'azione per aprire la selezione tramite controller
        CloseSeedSelection_controller.action.Enable(); // Abilita l'azione per chiudere la selezione tramite controller

        OpenSeedSelection_controller.action.started += OpenSeedPanel_Controller; // Aggiunge il listener per aprire il pannello
        CloseSeedSelection_controller.action.started += CloseSeedPanel_Controller; // Aggiunge il listener per chiudere il pannello
    }

    private void OnDisableController()
    {
        OpenSeedSelection_controller.action.Disable(); // Disabilita l'azione per aprire la selezione tramite controller
        CloseSeedSelection_controller.action.Disable(); // Disabilita l'azione per chiudere la selezione tramite controller

        OpenSeedSelection_controller.action.started -= OpenSeedPanel_Controller; // Rimuove il listener per aprire il pannello
        CloseSeedSelection_controller.action.started -= CloseSeedPanel_Controller; // Rimuove il listener per chiudere il pannello
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
}
