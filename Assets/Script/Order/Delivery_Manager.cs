using UnityEngine;
using UnityEngine.UI;

public class Order_Manager : MonoBehaviour
{
    public GameObject[] SheetsOrder;
    public string[] OrderName = {
    "Chiesa", "Casa Laur", "Negozio di Mark", "Ospedale", "Falegname", "Palazzo Regale", "Borgo Pescatori", "Foresta Magica",
    "Fruttivendolo", "Macellaio", "Panificio Rosa", "Farmacia Verde", "Trattoria Belvedere", "Taverna dei Saggi", "Locanda del Porto",
    "Drogheria Antica", "Emporio del Nord", "Oreficeria Dorata", "Sartoria Bianca", "Biblioteca Storica", "Casa dei Pini",
    "Villaggio dei Lupi", "Stalla di Luca", "Castello del Duca", "Fabbro di Leon", "Bottega della Lana", "Cantina del Borgo",
    "Torre del Drago", "Giardino delle Erbe", "Rifugio dei Cacciatori", "Pasticceria Dolci Sogni", "Merceria Arcobaleno",
    "Vigneto della Collina", "Bancarella di Sara", "Villa dei Sogni", "Caserma dei Cavalieri", "Erboristeria Solare",
    "Allevamento del Bosco", "Ponte dei Mercanti", "Carovana di Lina", "Rimessa dei Carri"};


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
            //orderSheet.ActiveVegetables();
            lastCheckedHour = timeManager.Hour;// Aggiorna l'ora controllata
            //Debug.Log("Random Order Active");
        }

        // Resetta il controllo se l'ora torna sotto le 10 (ad esempio, nuovo giorno)
        if (timeManager.Hour < 10)
        {
            lastCheckedHour = -1;// Resetta la variabile per riattivare l'ordine il giorno successivo
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
                string randomName = OrderName[Random.Range(0, OrderName.Length)];
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
