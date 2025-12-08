using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    float verticalRotation = 0f;
    CharacterController controller;



    void Start()
    {
       
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock and hide the cursor
       if (GameManger_main_menue.Status == "resumeGame")
        {
            float x = PlayerPrefs.GetFloat("LastX");
            float z = PlayerPrefs.GetFloat("LastZ");
            transform.position = new Vector3(x, transform.position.y, z);
        }

        //    if (PlayerPrefs.HasKey("LastX"))
            /*{
                float x = PlayerPrefs.GetFloat("LastX");
                float z = PlayerPrefs.GetFloat("LastZ");

                transform.position = new Vector3(x, transform.position.y, z);
            }
            //}
            // to save the scene 

            //void OnDisable()
            {
                // Save the player’s current position when leaving the scene
                PlayerPrefs.SetFloat("LastX", transform.position.x);
                PlayerPrefs.SetFloat("LastY", transform.position.y);
                PlayerPrefs.SetFloat("LastZ", transform.position.z);
                PlayerPrefs.SetString("LastScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();
            }
           */ //// to save the scene is ended

    }

        void Update()
        {
            PlayerPrefs.SetFloat("LastX", transform.position.x);
            PlayerPrefs.SetFloat("LastY", transform.position.y);
            PlayerPrefs.SetFloat("LastZ", transform.position.z);
            PlayerPrefs.SetString("LastScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            // Mouse look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);

            // Movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // 🏃 Check if Left Shift is held down
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * currentSpeed * Time.deltaTime);
        }
    }

