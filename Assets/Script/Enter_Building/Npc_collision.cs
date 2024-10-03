using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Npc_collision : MonoBehaviour
{
    public GameObject Ui_Npc;
    public GameObject Shop;
    public Text Box_Npc_Text;

    public bool IsCollision = false;

    [Header("Ui Controller")]
    [SerializeField] private GameObject Ui_Log_Controller;

    [Header("Input Controller")]
    [SerializeField] private InputAction Log_Shop;

    [SerializeField] private PlayerInput PlayerInput;
    [SerializeField] private Move_Player Move_Player;
    [SerializeField] private Player_Manager PlayerMager;

    // Update is called once per frame
    void Update()
    {
        trackerdevice();
        ActiveShop();
    }
    private void trackerdevice()
    {
        if(PlayerInput != null)
        {
            var Device = PlayerInput.defaultControlScheme;

            foreach(var device in PlayerInput.devices)
            {
                if(device is Keyboard)
                {
                    Ui_Log_Controller.SetActive(false);
                    Box_Npc_Text.text = "Hello welcome to my shop \r\nClick T to access the shop";
                }
                else if(device is Gamepad)
                {
                    Ui_Log_Controller.SetActive(true);
                    Box_Npc_Text.text = "Hello welcome to my shop \r\nClick     to access the shop";
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            IsCollision = true;
            Ui_Npc.SetActive(true);
            ActiveShop();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            IsCollision = false;
            Ui_Npc.SetActive(false);
        }
    }

    public void ActiveShop()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Ui_Npc.SetActive(false);
            Shop.SetActive(true);
            Debug.Log("click");
        }
    }
    public void ExitShop()
    {
        Shop.SetActive(false);
    }

    //========Input Controller========//
    private void OnEnable()
    {
        Log_Shop.Enable();

        Log_Shop.performed += Log_shop;
    }

    private void OnDisable()
    {
        Log_Shop.Disable();

        Log_Shop.performed -= Log_shop;
    }

    private void Log_shop(InputAction.CallbackContext context)
    {
        Shop.SetActive(true);
    }
}
