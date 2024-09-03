using UnityEngine;
using UnityEngine.InputSystem;

public class AnimalPlacer : MonoBehaviour
{
    [Header("Prefab e Oggetti")]
    public GameObject objectToSpawn;
    public GameObject objectPrefab;

    [Header("Configurazione")]
    public string requiredPlacementID;
    public int cost;
    public GameManger gameManger;

    private GameObject objectInHand;
    private bool isPlacing = false;
    private Vector2 moveInput;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference buyAnimal;
    public InputAction PlaceAnimal;
    public InputActionReference cancelAction;
    public float SpedMoveController = 5f;
    public PlayerInput playerInput; // Riferimento a PlayerInput

    

    private void OnEnable()
    {
        moveAction.action.Enable();
        buyAnimal.action.Enable();
        cancelAction.action.Enable();
        PlaceAnimal.Enable();

        moveAction.action.performed += OnMove;
        buyAnimal.action.performed += OnBuyAnimal;
        cancelAction.action.performed += OnCancel;
        PlaceAnimal.performed += OnPlace;

        // Chiamata al tracker per rilevare il dispositivo
        TrackerDevice();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        buyAnimal.action.Disable();
        cancelAction.action.Disable();
        PlaceAnimal.Disable();

        moveAction.action.performed -= OnMove;
        buyAnimal.action.performed -= OnBuyAnimal;
        cancelAction.action.performed -= OnCancel;
        PlaceAnimal.performed -= OnPlace;

    }

    private void TrackerDevice()
    {
        if (playerInput != null)
        {
            var currentControlScheme = playerInput.currentControlScheme;

            foreach (var device in playerInput.devices)
            {
                if (device is Keyboard)
                {
                    Debug.Log("Dispositivo attivo: Tastiera");
                    // Azioni specifiche per la tastiera
                }
                else if (device is Gamepad)
                {
                    Debug.Log("Dispositivo attivo: Gamepad");
                    // Azioni specifiche per il gamepad
                }
            }
        }
    }


    void Update()
    {
        if (isPlacing && objectInHand != null)
        {
            Vector3 movePosition;
            if (moveInput != Vector2.zero)
            {
                movePosition = objectInHand.transform.position + new Vector3(moveInput.x, moveInput.y, 0) * SpedMoveController ;
            }
            else
            {
                movePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                movePosition.z = 0;
            }

            objectInHand.transform.position = movePosition;

            if (Input.GetMouseButtonUp(0))
            {
                PlaceObject(movePosition);
            }

            if (Input.GetMouseButtonUp(1))
            {
                CancelPlacingObject();
            }
        }
    }

    private void PlaceObject(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null && gameManger.Coin >= cost)
        {
            Id_Box id = hit.collider.GetComponent<Id_Box>();
            if (id != null && id.placementID == requiredPlacementID && gameManger.Coin >= cost)
            {
                gameManger.Coin -= cost;
                GameObject placedObject = Instantiate(objectPrefab, position, Quaternion.identity);

                Animal_Type animal = placedObject.GetComponent<Animal_Type>();
                if (animal != null)
                {
                    gameManger.IncrementAnimalCount(animal.Type);
                }

                Destroy(objectInHand);
                objectInHand = null;
                isPlacing = false;
            }
            else
            {
                Debug.Log("Errore: ID non trovato o monete insufficienti");
            }
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnBuyAnimal(InputAction.CallbackContext context)
    {
        if(objectInHand == null && gameManger.Coin >= cost)
        {
            objectInHand = Instantiate(objectToSpawn);
            isPlacing = true;
            gameManger.Coin -= cost;

        }
    }

    private void OnPlace(InputAction.CallbackContext context)
    {
        if (objectInHand == null)
        {
            Vector3 PlacePosition = objectInHand.transform.position;

            PlaceObject(PlacePosition);
        }

        Debug.Log("Click");
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        CancelPlacingObject();
    }

    public void CancelPlacingObject()
    {
        if (objectInHand != null)
        {
            Destroy(objectInHand);
            objectInHand = null;
            isPlacing = false;
            Debug.Log("Posizionamento annullato");
        }
    }
}
