using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManger : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;   // Unlock cursor
        Cursor.visible = true;                   // Always show cursor
    }

}
