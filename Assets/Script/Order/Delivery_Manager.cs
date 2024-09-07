using UnityEngine;
using UnityEngine.UI;

public class Order_Manager : MonoBehaviour
{
    public GameObject[] SheetsOrder;
    public string[] OrderName = { "Chiesa", "Casa Laur", "Negozio di Mark", "Ospedale", "Falegname", "Palazzo Regale", "Borgo Pescatori", "Foresta Magica" };

    public Button confirmOrderButton; // Bottone per confermare l'ordine
    private OrderSheet currentSheet; // Riferimento al foglio di ordine attualmente selezionato

    void Start()
    {
        AssignRandomOrder();

        // Collega il metodo CompleteCurrentOrder al click del bottone
        if (confirmOrderButton != null)
        {
            confirmOrderButton.onClick.AddListener(CompleteCurrentOrder);
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
        }
    }

    public void CompleteCurrentOrder()
    {
        if (currentSheet != null)
        {
            currentSheet.CompleteOrder();
        }
        else
        {
            Debug.Log("Nessun foglio di ordine selezionato.");
        }
    }
}
