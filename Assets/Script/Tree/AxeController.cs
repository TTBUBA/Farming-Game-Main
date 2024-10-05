using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeController : MonoBehaviour
{
    public Animator animator_Axe;
    public GameObject Axe;
    public Renderer axeRenderer;

    [Header("Input_Controller")]
    public InputAction ButtonDestroy_Controller;

    [Header("Input_Keyboard")]
    public InputAction ButtonDestroy_KeyBoard;

    [SerializeField] private Tree currentTree; // Riferimento all'albero con cui l'ascia è in contatto

    private void Start()
    {
        // Inizializzazione dei componenti
        animator_Axe = GetComponent<Animator>();
        axeRenderer = Axe.GetComponent<Renderer>();
        axeRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se l'oggetto colpito è un albero
        if (collision.gameObject.CompareTag("Tree"))
        {
            axeRenderer.enabled = true; // Mostra l'ascia
            currentTree = collision.gameObject.GetComponent<Tree>(); // Salva il riferimento all'albero corrente
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Quando l'ascia esce dal raggio di un albero
        if (collision.gameObject.CompareTag("Tree"))
        {
            axeRenderer.enabled = false; // Nasconde l'ascia
            currentTree = null; // Rimuove il riferimento all'albero
        }
    }

    private void OnEnable()
    {
        // Abilita gli input e associa gli eventi
        ButtonDestroy_Controller.Enable();
        ButtonDestroy_KeyBoard.Enable();

        ButtonDestroy_Controller.performed += DestroyTree_Controller;
        ButtonDestroy_KeyBoard.performed += DestroyTree_Keyboard;
    }

    private void OnDisable()
    {
        // Disabilita gli input e rimuovi gli eventi
        ButtonDestroy_Controller.Disable();
        ButtonDestroy_KeyBoard.Disable();

        ButtonDestroy_Controller.performed -= DestroyTree_Controller;
        ButtonDestroy_KeyBoard.performed -= DestroyTree_Keyboard;
    }

    private void DestroyTree(InputAction.CallbackContext context, bool usingKeyboard)
    {
        // Controlla se c'è un albero nel raggio
        if (currentTree != null && currentTree.UsingKeyboard == usingKeyboard && currentTree.currentStage >= 5)
        {
            // Abbatti l'albero corrente
            TreeController treeController = currentTree.GetComponent<TreeController>();
            ButtonDestroyTree(treeController); // Avvia l'animazione dell'ascia
            currentTree.DestroyTree(); // Distruggi l'albero specifico
            currentTree.Text_PopUp(); // Mostra il testo del legno ricevuto
        }
    }

    // Distruggi l'albero usando la tastiera
    private void DestroyTree_Keyboard(InputAction.CallbackContext context)
    {
        DestroyTree(context , true);
    }

    // Distruggi l'albero usando il controller
    private void DestroyTree_Controller(InputAction.CallbackContext context)
    {
        DestroyTree(context, false);
    }

    public void ButtonDestroyTree(TreeController treeController)
    {
        animator_Axe.SetBool("AxeAttivo", true);
        StartCoroutine(AnimatorFalse(treeController));
    }

    private IEnumerator AnimatorFalse(TreeController treeController)
    {
        // Avvia l'animazione di caduta dell'albero e poi disabilita l'animazione dell'ascia
        yield return new WaitForSeconds(0.3f);
        treeController.StartFallingAnimation();
        yield return new WaitForSeconds(2f);
        animator_Axe.SetBool("AxeAttivo", false);
    }
}
