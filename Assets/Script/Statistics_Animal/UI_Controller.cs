using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_Controller_Animal : MonoBehaviour
{
    public Image[] images;
    public int currentIndex = 0;

    // Input Controller
    public InputActionReference Controller_Right;
    public InputActionReference Controller_Left;

    private void OnEnable()
    {
        Controller_Right.action.started += ScrollForwardController;
        Controller_Left.action.started += ScrollBackwardController;

        Controller_Right.action.Enable();
        Controller_Left.action.Enable();
    }

    private void OnDisable()
    {
        Controller_Right.action.started -= ScrollForwardController;
        Controller_Left.action.started -= ScrollBackwardController;


        Controller_Right.action.Disable();
        Controller_Left.action.Disable();
    }

    public void ScrollForward()
    {
        images[currentIndex].transform.localScale = Vector2.one;

        currentIndex = (currentIndex + 1) % images.Length;
        //UpdateImageVisibility();
        ActiveImage();
    }

    public void ScrollBackward()
    {
        images[currentIndex].transform.localScale = Vector2.one;

        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        // UpdateImageVisibility();
        ActiveImage();
    }


    //controller
    public void ScrollForwardController(InputAction.CallbackContext obj)
    {
        ScrollForward();
    }

    public void ScrollBackwardController(InputAction.CallbackContext obj)
    {
        ScrollBackward();
    }

    // Serve per far rimanere attiva solo una tab quella selezionata //
    /*
    public void UpdateImageVisibility()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(i == currentIndex);
        }
    }
    */
    //===============================================================//

    public void ActiveImage()
    {
        switch(currentIndex)
        {
            case 0:
                ActionForImage1();
                break;

            case 1:
                ActionForImage2();
                break;

            case 2:
                ActionForImage3();
                break;
        }
    }

    private void ActionForImage1()
    {
        
        images[currentIndex].transform.DOScale(new Vector2(1.15f, 1.15f), 0.2f).SetEase(Ease.InBounce);
    }

    private void ActionForImage2()
    {
       
        images[currentIndex].transform.DOScale(new Vector2(1.15f, 1.15f), 0.2f).SetEase(Ease.InBounce);
    }

    private void ActionForImage3()
    {
        
        images[currentIndex].transform.DOScale(new Vector2(1.15f, 1.15f), 0.2f).SetEase(Ease.InBounce);
    }
}
