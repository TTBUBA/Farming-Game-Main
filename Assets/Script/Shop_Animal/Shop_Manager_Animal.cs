using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shop_Manager_Animal : MonoBehaviour
{
    [Header("Ui")]
    public GameObject Image_Shop_Open;
    public GameObject Image_Shop_Close;
    public GameObject Ui_Bar_Shop;


    [Header("Input Controller")]
    public InputActionReference Button_Open_Shop;
    public InputActionReference Button_Close_Shop;
    public InputActionReference Button_Next_Slot;
    public InputActionReference Button_Back_Slot;
    [Header("Ui Controller")]
    public GameObject button_Log_Shop;
    public GameObject button_Exit_Shop;
    public GameObject[] Ui_Controller;

    [Header("Ui Keyboard")]
    public GameObject button_exit_Shop;

    public bool IsShopOpen = false;
    public int currentIndex = 0;
    public Image[] Box_Shop;
    public PlayerInput Playerinput;

    // Update is called once per frame
    void Update()
    {
        TrackerDevice();
    }

    //Tracker Device 
    private void TrackerDevice()
    {
        if (Playerinput != null)
        {
            var Device = Playerinput.currentControlScheme;

            foreach (var device in Playerinput.devices)
            {
                if (device is Keyboard)
                {
                    //controller
                    button_Log_Shop.SetActive(false);
                    button_Exit_Shop.SetActive(false);

                    //keyboard
                    button_exit_Shop.SetActive(true);

                    foreach (var Uicontroller in Ui_Controller)
                    {
                        Uicontroller.SetActive(false);
                    }
                }
                else if (device is Gamepad)
                {

                    foreach(var Uicontroller in Ui_Controller)
                    {
                        Uicontroller.SetActive(true);
                    }

                    button_Log_Shop.SetActive(true);
                    button_Exit_Shop.SetActive(true);

                    //keyboard
                    button_exit_Shop.SetActive(false);

                }

            }

        }
    }
    public void Open_Shop()
    {


        //oggeti da disattivare appena viene premuto il tasto shop
        Image_Shop_Open.SetActive(false);

        //oggeti da attivare appena viene premuto il tasto shop
        Ui_Bar_Shop.SetActive(true);

        Image_Shop_Close.SetActive(true);



    }

    public void Close_Shop()
    {


        //oggeti da attivare 
        Image_Shop_Open.SetActive(true);

        //oggeti da dissativare 
        Ui_Bar_Shop.SetActive(false);
        Image_Shop_Close.SetActive(false);
        
    }

    public void NextBoxShopSelect()
    {
        Box_Shop[currentIndex].transform.localScale = Vector2.one;

        currentIndex = (currentIndex + 1)  % Box_Shop.Length;
        ActiveImageBox();
    }

    public void BackBoxShopSelect()
    {
        Box_Shop[currentIndex].transform.localScale = Vector2.one;
        currentIndex = (currentIndex - 1 + Box_Shop.Length) % Box_Shop.Length;
        ActiveImageBox();
    }

    //Input Controller
    private void OnEnable()
    {
        Button_Open_Shop.action.started += OpenShopController;
        Button_Close_Shop.action.started += CloseShopController;
        Button_Next_Slot.action.started += ScrollNextBoxAnimal;
        Button_Back_Slot.action.started += ScrollBackBoxAnimal;


        Button_Open_Shop.action.Enable();
        Button_Close_Shop.action.Enable();
        Button_Next_Slot.action.Enable();
        Button_Back_Slot.action.Enable();
    }

    private void OnDisable()
    {
        Button_Open_Shop.action.started -= OpenShopController;
        Button_Close_Shop.action.started -= CloseShopController;
        Button_Next_Slot.action.started -= ScrollNextBoxAnimal;
        Button_Back_Slot.action.started -= ScrollBackBoxAnimal;

        Button_Open_Shop.action.Disable();
        Button_Close_Shop.action.Disable();
        Button_Next_Slot.action.Disable();
        Button_Back_Slot.action.Disable();

    }

    public void ScrollNextBoxAnimal(InputAction.CallbackContext context)
    {
        NextBoxShopSelect();
    }
    public void ScrollBackBoxAnimal(InputAction.CallbackContext context)
    {
        BackBoxShopSelect();
    }
    public void OpenShopController(InputAction.CallbackContext context)
    {
        Open_Shop();
        Debug.Log("X open Shop");
    }
    public void CloseShopController(InputAction.CallbackContext context)
    {
        Close_Shop();
    }

    public void ActiveImageBox()
    {
        switch(currentIndex)
        {
            case 0:
                ImageHoverController();
            break;
            case 1:
                ImageHoverController();
                break;
            case 2:
                ImageHoverController();
                break;
            case 3:
                ImageHoverController();
                break;
        }
    }

    private void ImageHoverController()
    {
        Box_Shop[currentIndex].transform.DOScale(new Vector2(1.1f, 1.1f), 0.2f).SetEase(Ease.InBounce);
    }
}
