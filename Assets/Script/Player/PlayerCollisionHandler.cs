using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private GameManager gameManger;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        gameManger = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        // quando il player entra in collisione con la ZonaCostruzioni 
        if (collider.gameObject.CompareTag("ZonaCostruzioni"))
        {
          //gameManger.HandleCollisionZonaCostruzioni();

        }


        if (collider.gameObject.CompareTag("ColliderDialogue"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

                
    }
}
