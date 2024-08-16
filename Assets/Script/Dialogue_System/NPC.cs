using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Oggetti da dissattivare")]
    public GameObject inventory;

    [Header("Oggetti da Attivare ")]
    public GameObject Shop_Npc;

    [Header("Dialogue")]
    public Dialogue dialogue;
    private Move_Player MovePlayer;
    private DialogueManager dialogueManager;
    public Animator animator;
    public bool IsCollsionion = false;
    public bool ShopActive;    

    void Start()
    {
        MovePlayer = FindAnyObjectByType<Move_Player>();

        if (MovePlayer == null )
        {
            Debug.LogError("non trovato");
        }
    }


    // Questo codice serve per disabilitare la camminata al player quando entra in collisione con un Npc
    
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            //Ogetti da dissattivare 
            inventory.SetActive(false);

            // MovePlayer.speed = 0;
            IsCollsionion = true;
            
            if (IsCollsionion == true)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            }
           
            
        }
    }

    public void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Ogetti da Attivare
           
            inventory.SetActive(true);
            // Quando il giocatore esce dal collider del NPC, impostiamo IsCollsionion a false
            IsCollsionion = false;

            // Se il riferimento a dialogueManager è null, lo cerchiamo una volta usando FindObjectOfType
            if (dialogueManager == null)
            {
                dialogueManager = FindObjectOfType<DialogueManager>();
            }

            // verifica se l'animator del dialogueManager ha il parametro "IsOpen" impostato a true
            if (dialogueManager.animator.GetBool("IsOpen"))
            {
                // Se il dialogo è aperto, si chiude chiamando il metodo EndDialogue del dialogueManager
                Debug.Log("Ending dialogue");
                dialogueManager.EndDialogue();
            }



              CloseShop();
        }
    }


    public void OpenShop()
    {
        if (ShopActive == true)
        {
            Shop_Npc.SetActive(true);
        }
    }

    public void CloseShop()
    {
        if (ShopActive == true)
        {
            Shop_Npc.SetActive(false);
        }
    }
    





}
