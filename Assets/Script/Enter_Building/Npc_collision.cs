using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_ : MonoBehaviour
{
    public GameObject Box;
    public GameObject Shop;

    private Move_Player Move_Player;
    private Player_Manager PlayerMager;
    
    // Start is called before the first frame update
    void Start()
    {
        Move_Player = FindAnyObjectByType<Move_Player>();
        PlayerMager = FindAnyObjectByType<Player_Manager>();

        
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
}
