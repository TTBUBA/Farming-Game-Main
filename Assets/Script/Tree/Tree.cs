using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Tree : MonoBehaviour
{
    /* Variabili non in uso
     * 
    [SerializeField] public int LifeTree;
    [SerializeField] private int[] LifeValue = { 10, 20, 30 };
    */

    [SerializeField] private Sprite[] growthStages; // Fasi di crescita dell'albero
    [SerializeField] private float timeToGrow = 10f; // Tempo per rigenerare l'albero
    [SerializeField] public int currentStage = 0;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D circlecollider;
    [SerializeField] private CapsuleCollider2D Capsulecollider;
    [SerializeField] public GameObject BackGroundBar;
    [SerializeField] public Image BarLife;
    [SerializeField] private int legnoricevuto = 0;


    [Header("Ui")]
    public TextMeshProUGUI Text_Legno;

    [Header("Animation")]
    [SerializeField] private Animation textAnimation;
    [SerializeField] private Animator TreeAnimation;


    [Header("Ui_Controller")]
    [SerializeField] private GameObject Button_Controller;

    [Header("Ui_Keyboard")]
    [SerializeField] private GameObject Button_Keyboard;

    public bool UsingKeyboard = true;
    public bool IsCollision = false;

    public PlayerInput PlayerInput;
    public TrakingLocal trakingRaccolto;
    void Start()
    {
        circlecollider = GetComponent<CircleCollider2D>();
        Capsulecollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textAnimation = Text_Legno.GetComponent<Animation>();
        TreeAnimation = GetComponent<Animator>();
        spriteRenderer.sprite = growthStages[currentStage];
        currentStage = growthStages.Length;
        Text_Legno.gameObject.SetActive(false); // Nasconde il testo all'inizio

        
    }

    private void Update()
    {
        
        // Controlla la crescita dell'albero e gestisce il testo del legno
        if (currentStage >= 4)
        {
            legnoricevuto = 0;
        }

        if (currentStage >= 2)
        {
            Text_Legno.gameObject.SetActive(false);
        }

        ColliderTree();
        TrackerInputSystem(); // Verifica il tipo di input attivo
        ActiveImage(); // Mostra il pulsante corretto in base all'input
    }

    private void TrackerInputSystem()
    {
        if (PlayerInput != null)
        {
            var device = PlayerInput.currentControlScheme;
            UsingKeyboard = (device == "Keyboard");
        }
    }
    private void ActiveImage()
    {
        // Mostra il pulsante corretto in base all'input e la crescita dell'albero
        if (currentStage >= 3 && IsCollision)
        {
            BackGroundBar.SetActive(true);
            if (UsingKeyboard)
            {
                Button_Keyboard.SetActive(true);
                Button_Controller.SetActive(false);
            }
            else
            {
                Button_Controller.SetActive(true);
                Button_Keyboard.SetActive(false);
            }
        }
    }
    private void DisactiveImage()
    {
        // Disabilita i pulsanti
        Button_Keyboard.SetActive(false);
        Button_Controller.SetActive(false);

        // Disabilita i La barra della vita
        BackGroundBar.SetActive(false);
    }
    private void ColliderTree()
    {
        if(currentStage >= 3)
        {
            circlecollider.enabled = true;
            Capsulecollider.enabled = true;

        }
        else if(currentStage >= 0)
        {
            circlecollider.enabled = false;
            Capsulecollider.enabled = false;
        }
    }
    public void DecreseLifeTree()
    {
        BarLife.fillAmount -= Random.Range(0.02f, 0.2f);
        Debug.Log(BarLife.fillAmount);
  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            IsCollision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsCollision = false;
        DisactiveImage();
    }
    public void DestroyTree()
    {
        // Aggiorna il tracking del legno raccolto e avvia la rigenerazione dell'albero
        trakingRaccolto.AddLegna(legnoricevuto);
        StartCoroutine(GrowTree()); //Coroutine per far crescere l'albero
        DisactiveImage();
        
    }
    IEnumerator GrowTree()
    {
        currentStage = 0; // Resetta la crescita
        spriteRenderer.sprite = null; // Nasconde l'albero

        yield return new WaitForSeconds(3f); // Tempo prima di ricominciare la crescita

        for (int i = 0; i < growthStages.Length; i++)
        {
            yield return new WaitForSeconds(timeToGrow);
            currentStage = i;
            spriteRenderer.sprite = growthStages[currentStage];
           // Debug.Log($"Crescita albero: stage {currentStage}");
        }

        Text_PopUp(); // Mostra il testo del legno ricevuto
    }
    public IEnumerator MotionTree()
    {
        // Avvia l'animazione di caduta dell'albero e poi disabilita l'animazione dell'ascia
        TreeAnimation.SetBool("Motion", true);
        yield return new WaitForSeconds(1f);
        TreeAnimation.SetBool("Motion", false);

    }
    public void Text_PopUp()
    {
        legnoricevuto = Random.Range(10, 30); // Valore casuale del legno ricevuto
        Text_Legno.text = legnoricevuto.ToString();

        if (textAnimation != null)
        {
            textAnimation.Play();
        }

        Text_Legno.gameObject.SetActive(true);
    }
}
