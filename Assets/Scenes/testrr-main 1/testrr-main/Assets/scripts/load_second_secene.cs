using UnityEngine;
using UnityEngine.SceneManagement;

public class load_second_secene : MonoBehaviour
{

    [SerializeField] private GameObject player;      // Reference to your player
    [SerializeField] private GameObject interactUI;  // Reference to your UI

    public float interactDistance = 2f;

    private bool isShown = false;
    private bool hasPlayed = false;

    private void Start()
    {
        if (interactUI != null)
            interactUI.SetActive(false);


    }

    private void FixedUpdate()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        // When player gets close enough
        if (distance <= interactDistance && !isShown)
        {
            interactUI.SetActive(true);
            isShown = true;




        }
        // When player leaves the area
        else if (distance > interactDistance && isShown)
        {
            interactUI.SetActive(false);
            isShown = false;


        }

        // Interaction key
        if (isShown && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with " + gameObject.name);
            SceneManager.LoadScene("second_exp");
            // Add your interaction logic here
        }
    }
}


