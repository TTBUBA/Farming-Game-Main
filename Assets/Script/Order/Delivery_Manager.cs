using UnityEngine;
using UnityEngine.UI;

public class Order_Manager : MonoBehaviour
{
    public GameObject[] SheetsOrder;


    [SerializeField]
    private string[] Name = {
    "Church", "Laur's House", "Mark's Shop", "Hospital", "Carpenter", "Royal Palace", "Fishermen's Village", "Magic Forest",
    "Greengrocer", "Butcher", "Rosa's Bakery", "Green Pharmacy", "Belvedere Trattoria", "Wise Men's Tavern", "Port Inn",
    "Old Drugstore", "Northern Emporium", "Golden Jeweler", "White Tailor", "Historical Library", "Pine House",
    "Wolf Village", "Luca's Stable", "Duke's Castle", "Leon's Blacksmith", "Wool Shop", "Village Winery",
    "Dragon Tower", "Herb Garden", "Hunters' Refuge", "Sweet Dreams Bakery", "Rainbow Haberdashery",
    "Hill Vineyard", "Sara's Stall", "Dream Villa", "Knights' Barracks", "Solar Herbalist",
    "Forest Farm", "Merchants' Bridge", "Lina's Caravan", "Cart Depot"
     };



    public Button confirmOrderButton; // Bottone per confermare l'ordine
    private OrderSheet currentSheet; // Riferimento al foglio di ordine attualmente selezionato

    public Text NameOrderText;
    public Text PriceOrder;

    //Sound Effect
    public AudioSource completeOrder_Audio;

    //============//
    public TimeManager timeManager;
    //============//

    private int lastCheckedHour = -1; // Variabile per tracciare l'ultima ora verificata
    void Start()
    {
        AssignRandomOrder();

        // Collega il metodo CompleteCurrentOrder al click del bottone
        if (confirmOrderButton != null)
        {
            confirmOrderButton.onClick.AddListener(CompleteCurrentOrder);
        }
    }

    public void Update()
    {
        Test();
    }
    public void Test()
    {
       OrderSheet orderSheet = GetComponent<OrderSheet>();
        // Se l'ora è >= 10 e non abbiamo ancora eseguito l'assegnazione per quest'ora
        if (timeManager.Hour == 10 && timeManager.Hour != lastCheckedHour)
        {
            AssignRandomOrder();
            lastCheckedHour = timeManager.Hour;// Aggiorna l'ora controllata


            //Debug.Log("Random Order Active");
        }

        // Resetta il controllo se l'ora torna sotto le 10 (ad esempio, nuovo giorno)
        if (timeManager.Hour < 10)
        {
            lastCheckedHour = -1;// Resetta la variabile per riattivare l'ordine il giorno successivo
        }
        
        //Test
        if(Input.GetKeyDown(KeyCode.J))
        {
            AssignRandomOrder();
        }
    }
    public void AssignRandomOrder()
    {
        for (int i = 0; i < SheetsOrder.Length; i++)
        {
            OrderSheet Sheet = SheetsOrder[i].GetComponent<OrderSheet>();

            if (Sheet != null)
            {
                // Assegna un nome random per la consegna e un compenso random (tra 50 e 200 monete)
                string randomName = Name[Random.Range(0, Name.Length)];
                int RandomReward = Random.Range(40, 300);

                Sheet.SetOrder(RandomReward, randomName);


            }

            Button button = SheetsOrder[i].GetComponent<Button>();
            if (button != null)
            {
                int index = i;
                button.onClick.AddListener(() => OnSheetClick(index));
            }
        }
    }

    private void OnSheetClick(int clickedSheetIndex)
    {
        foreach (GameObject sheet in SheetsOrder)
        {
            OrderSheet deliverySheet = sheet.GetComponent<OrderSheet>();
            if (deliverySheet != null)
            {
                deliverySheet.HideVegetables();
            }
        }

        currentSheet = SheetsOrder[clickedSheetIndex].GetComponent<OrderSheet>();
        if (currentSheet != null)
        {
            currentSheet.ShowVegetables(); // Mostra gli ortaggi del foglio selezionato

            NameOrderText.text = currentSheet.NameOrder;
            PriceOrder.text = currentSheet.reward.ToString();
            currentSheet.AnimationSheet();
        }
    }

    public void CompleteCurrentOrder()
    {
        if (currentSheet != null)
        {
            currentSheet.CompleteOrder();
            completeOrder_Audio.Play();
        }
        else
        {
            Debug.Log("Nessun foglio di ordine selezionato.");
        }
    }
}
