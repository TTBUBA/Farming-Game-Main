using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Npc_collision : MonoBehaviour
{
    public GameObject Box;
    public GameObject Shop;
    public Text Box_Npc_Text;
    private Move_Player Move_Player;
    private Player_Manager PlayerMager;

    [Header("Ui Controller")]
    public GameObject Ui_Log_Controller;

    [Header("Input Controller")]
    public InputAction Log_Shop;

    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        Move_Player = FindAnyObjectByType<Move_Player>();
        PlayerMager = FindAnyObjectByType<Player_Manager>();
        PlayerInput = FindAnyObjectByType<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("t"))
        {
            Box.SetActive(false);
            Shop.SetActive(true);
            Move_Player.speed = 0f;
            
        }

        trackerdevice();
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
            Box.SetActive(true);
            
        }
    }

    public void ExitShop()
    {

        Shop.SetActive(false);
        Move_Player.speed = 5;
    }

    //Input Controller

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
