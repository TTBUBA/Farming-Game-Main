// Gestisce la selezione degli slot della hotbar, permettendo al giocatore di cambiare strumento premendo i tasti numerici. 
// Tiene traccia dello slot attualmente selezionato e gestisce l'interazione tra gli slot della hotbar.
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;


public class HotbarManager : MonoBehaviour
{
    public Slot_Tools[] hotbarSlots; // Hotbar con 4 slot
    public int selectedHotbarSlotIndex = 0; // Indice dello slot selezionato

    [Header("INPUT KEYBOARD")]
    public InputActionReference NextSlot_Controller;
    public InputActionReference BackSlot_Controller;

    void Start()
    {
        hotbarSlots[selectedHotbarSlotIndex].Select(); // Seleziona lo slot iniziale al caricamento
    }

    void Update()
    {
        ChangeHotbarSlot(); // Controlla la selezione degli slot della hotbar ogni frame
    }

    public void ChangeHotbarSlot()
    {
        // Cambia lo slot selezionato in base ai tasti numerici premuti
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectHotbarSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectHotbarSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectHotbarSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectHotbarSlot(3);
    }

    void SelectHotbarSlot(int index)
    {
        // Deseleziona tutti gli slot
        foreach (var slot in hotbarSlots)
        {
            slot.Deselect();
        }

        Debug.Log(hotbarSlots[selectedHotbarSlotIndex].toolsData.NameTools); // Messaggio di debug per il nome dello strumento selezionato

        selectedHotbarSlotIndex = index; // Aggiorna l'indice dello slot selezionato
        hotbarSlots[selectedHotbarSlotIndex].Select(); // Seleziona il nuovo slot
    }

    private void NextSlotVegetable()
    {
        hotbarSlots[selectedHotbarSlotIndex].transform.localScale = Vector3.one;

        selectedHotbarSlotIndex = (selectedHotbarSlotIndex + 1) % hotbarSlots.Length;
        SelectHotbarSlot(selectedHotbarSlotIndex);
        //AnimationSeedSelection();
    }

    private void BackSlotVegetable()
    {

    }

    //===========INPUT SETTING===========//
    private void OnEnable()
    {
        NextSlot_Controller.action.Enable();
        BackSlot_Controller.action.Enable();

        NextSlot_Controller.action.started += NextSlotHotBar_Controller;
        BackSlot_Controller.action.started += BackSlotHotBar_Controller;
    }
    private void OnDisable()
    {

        NextSlot_Controller.action.Disable();
        BackSlot_Controller.action.Disable();

        NextSlot_Controller.action.started -= NextSlotHotBar_Controller;
        BackSlot_Controller.action.started -= BackSlotHotBar_Controller;
    }

    //===========INPUT CONTROLLER=========//
    private void NextSlotHotBar_Controller(InputAction.CallbackContext context)
    {
        NextSlotVegetable();
    }

    private void BackSlotHotBar_Controller(InputAction.CallbackContext context)
    {
        
    }
}
