using UnityEngine;

public class DoorControllerZ : MonoBehaviour
{
    [SerializeField] private GameObject player;        // Reference to your player
     //public GameObject interactUI;    // The UI text to show
    //public GameObject interactUITransform;
    public Transform door;             // assign the door (can be same object)
    public float openAngle = 90f;      // how much to rotate (Z-axis)
    public float openSpeed = 2f;       // rotation speed
    public KeyCode interactKey = KeyCode.E;
    public float interactDistance = 2f;
    private bool isShown = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;

    void Start()
    {
        if (door == null) door = transform;
        closedRotation = door.localRotation;
        // rotate around Z instead of Y
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

 
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Camera cam = Camera.main;
            if (cam == null) return;
            float dist = Vector3.Distance(cam.transform.position, door.position);
            if (dist <= interactDistance)
            {
                isOpen = !isOpen;
            }
        }

        Quaternion target = isOpen ? openRotation : closedRotation;
        door.localRotation = Quaternion.Slerp(door.localRotation, target, Time.deltaTime * openSpeed);



        float distance = Vector3.Distance(transform.position, player.transform.position);
   


        if (distance <= interactDistance && !isShown)
        {
            //interactUI.SetActive(true);
            isShown = true;
        }
        else if (distance > interactDistance && isShown)
        {
            //interactUI.SetActive(false);
            isShown = false;
        }

        // Optional: Detect interaction
        if (isShown && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with " + gameObject.name);
            // Add your action here (open door, pick item, etc.)
        }

    }



}
