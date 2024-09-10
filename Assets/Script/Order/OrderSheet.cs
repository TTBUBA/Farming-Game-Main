using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class OrderSheet : MonoBehaviour
{
    public GameObject[] Text;
    public Text OrderNameText;
    public Text RewardText;
    public List<GameObject> BoxVegetables;
    

    public List<Item> items;

    public Dictionary<Item.ItemType , int> requiredItems = new Dictionary<Item.ItemType , int>();
    public Dictionary<Item.ItemType, int> currentItems = new Dictionary<Item.ItemType, int>();
    public Dictionary<int, Item> KeyValuePairs = new Dictionary< int , Item>();

    public string NameOrder;
    public int reward;
    

    public GameManager GameManager;
    public Tracking_Item trackingItem;

    public Image ImageSheet;
    public Sprite Sheet_Torn;
    private List<int> selectedVegetableIndices = new List<int>();

    public void Start()
    {
        ImageSheet = GetComponent<Image>();
    }
    public void Update()
    {
        //TEST

        if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateRandomVegetables();
            
        }
    }
    public void SetOrder(int rewardAmount, string name)
    {
        NameOrder = name;
        reward = rewardAmount;

        //UI
        OrderNameText.text = NameOrder;
        RewardText.text = reward.ToString() + "$";

        ActivateRandomVegetables();
        
    }

    public void ActivateRandomVegetables()
    {
        HideVegetables();
        

        int RandomNumberVegetable = Random.Range(1, Mathf.Min(5, BoxVegetables.Count) + 1);


        selectedVegetableIndices.Clear();
        selectedVegetableIndices = Enumerable.Range(0, BoxVegetables.Count)
                .OrderBy(x => Random.Range(0, BoxVegetables.Count))
                .Take(RandomNumberVegetable).ToList();


        selectedVegetableIndices.ForEach(index =>
        {
            BoxVegetables[index].SetActive(true);

            items[index].SetRequiredQuantity(Random.Range(20, 200));
            items[index].UpdateUiItem();
           
        });



    }
    public void ShowVegetables()
    {
        HideVegetables();

        // Attiva solo gli ortaggi selezionati per questo foglio
        foreach (int Index in selectedVegetableIndices)
        {
            BoxVegetables[Index].SetActive(true);
            items[Index].UpdateUiItem();
        }

        //Debug.Log("Foglio selezionato: " + NameOrder + " con ricompensa: " + reward);
        
    }
    public void HideVegetables()
    {
        foreach (GameObject vegetable in BoxVegetables)
        {
            vegetable.SetActive(false);
        }

    }

    public void HideText()
    {
        foreach (GameObject Text in Text)
        {
            Text.SetActive(false);
        }


    }

    public void ActiveVegetables()
    {
        foreach (GameObject vegetable in BoxVegetables)
        {
            vegetable.SetActive(true);
        }


    }

    public void ActiveText()
    {
        foreach (GameObject Text in Text)
        {
            Text.SetActive(true);
        }
    }

    public bool CanCompleteOrder()
    {
        foreach(int index in selectedVegetableIndices )
        {
            if (items[index] == null || items[index].CurrentQuantity < items[index].RequiredQuantity)
            {
                return false;
            }
        }
        return true;
    }
    public void CompleteOrder()
    {
        if (CanCompleteOrder())
        {
            GameManager.Coin += reward;
            foreach (int index in selectedVegetableIndices)
            {

                switch (items[index].itemType)
                {
                    case Item.ItemType.Carota:
                        trackingItem.RaccoltoCarote = (Mathf.Max(0, items[index].CurrentQuantity - items[index].RequiredQuantity));
                        break;
                    case Item.ItemType.Cavolo:
                        trackingItem.Raccoltocavolo = (Mathf.Max(0, items[index].CurrentQuantity - items[index].RequiredQuantity));
                        break;
                    case Item.ItemType.Patata:
                        trackingItem.RaccoltoPatate = (Mathf.Max(0, items[index].CurrentQuantity - items[index].RequiredQuantity));
                        break;
                    case Item.ItemType.Grano:
                        trackingItem.RaccoltoGrano = (Mathf.Max(0, items[index].CurrentQuantity - items[index].RequiredQuantity));
                        break;
                }

                items[index].UpdateUiItem();
                //Debug.Log(item.CurrentQuantity);
            }

            HideVegetables();
            HideText();
            //gameObject.SetActive(false);
            ImageSheet.sprite = Sheet_Torn;


        }

        
    }


    public void AnimationSheet()
    {
        //Animazione che serve per avere la sensazione di click 
        transform.DOScale(new Vector2(1.02f, 1.02f), 0.1f).SetEase(Ease.InBounce).OnComplete(() => 
        {
            transform.DOScale(Vector2.one, 0.1f).SetEase(Ease.InBounce);
        });


        //Animazione che serve per far ruotare il foglio al click
        transform.DORotate(new Vector3(0, 0, -3f), 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 0, 3f), 0.2f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                transform.DORotate(Vector2.one, 0.2f).SetEase(Ease.InBounce);
            });
        });


    }
}