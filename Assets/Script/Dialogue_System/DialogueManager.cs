using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text NameText;                // Testo per il nome del personaggio nel dialogo
    public Text ContinueText;            // Testo per continuare il dialogo 
    public Text DialogueText;            // Testo per il testo del dialogo
    public Animator animator;            // Animator per gestire le animazioni del dialogo
    private NPC npc;
    private Queue<string> sentences;     // Coda delle frasi del dialogo

    //Input GamePad
    public InputActionReference Continue_Dialogue_Controller;

    //Ui GamePad
    public GameObject Icon_Controller;


    //Input General
    public PlayerInput Playerinput;
    void Start()
    {
        npc = FindAnyObjectByType<NPC>();
        // Inizializza la coda delle frasi
        sentences = new Queue<string>();
    }

    
    public void Update()
    {

        CheckDevice();
    }

    private void CheckDevice()
    {
        if(Playerinput != null)
        {
            var DeviceTracker = Playerinput.defaultControlScheme;

            foreach (var device in Playerinput.devices)
            {
                if(device is Keyboard)
                {
                    ContinueText.text = "Space To continue...";
                    // Se premuto il tasto Space, avanza al prossimo messaggio del dialogo
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        NextDisplayDialogue();
                    }
                    Icon_Controller.SetActive(false);
                }
                else if(device is Gamepad)
                {
                   
                    // Se premuto il tasto Quatrodato del controller, avanza al prossimo messaggio del dialogo
                    ContinueText.text = "To continue...";

                    Icon_Controller.SetActive(true);
                }
            }
        }
    }

    //==========Input Controller==========//
    private void OnEnable()
    {
        Continue_Dialogue_Controller.action.started += NextDialogueController;

        Continue_Dialogue_Controller.action.Enable();
    }

    private void OnDisable()
    {
        Continue_Dialogue_Controller.action.started -= NextDialogueController;

        Continue_Dialogue_Controller.action.Disable();
    }

    // Se premuto il tasto Quatrodato del controller, avanza al prossimo messaggio del dialogo
    private void NextDialogueController(InputAction.CallbackContext Obj)
    {
        NextDisplayDialogue();
    }
 


    public void StartDialogue(Dialogue dialogue)
    {
        // Imposta il parametro "IsOpen" dell'animator su true per mostrare il dialogo
        animator.SetBool("IsOpen", true);

        // Imposta il nome del personaggio nel dialogo
        NameText.text = dialogue.Name;

        // Pulisce la coda delle frasi
        sentences.Clear();

        // Aggiunge tutte le frasi del dialogo alla coda
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Avvia la visualizzazione del prossimo messaggio del dialogo
        NextDisplayDialogue();
    }

    // Passa al prossimo messaggio del dialogo
    public void NextDisplayDialogue()
    {
        // Se non ci sono più frasi nella coda, termina il dialogo
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Ottiene e mostra la prossima frase nella coda
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); // Ferma tutte le coroutine (se ce ne sono attive)
        StartCoroutine(TypeSentence(sentence)); // Avvia la coroutine per mostrare la frase carattere per carattere
    }

    // Coroutine per mostrare una frase carattere per carattere
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = ""; // Resetta il testo del dialogo

        // Itera su ogni carattere della frase
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter; // Aggiunge il carattere al testo del dialogo
            yield return null;           // Attende un frame prima di mostrare il prossimo carattere
        }
    }

    // Termina il dialogo
    public void EndDialogue()
    {
        StartCoroutine(CloseDialogueAndOpenShop());
    }

    private IEnumerator CloseDialogueAndOpenShop()
    {
        // Imposta il parametro "IsOpen" dell'animator su false per chiudere il dialogo
        animator.SetBool("IsOpen", false);

        yield return new WaitForSeconds(0.3f);

        npc.OpenShop();
    }
}
