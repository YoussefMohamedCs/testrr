using UnityEngine;

public class led : MonoBehaviour
{
    public Material openButton;
    public Material closeButton;
    bool buttonStatus;

 
    // Update is called once per frame
    void Update()
    {
        buttonStatus = button.buttonIsOpen;

        if (buttonStatus)
        {
            GetComponent<Renderer>().material = openButton;
        }
        else
        {
            GetComponent<Renderer>().material = closeButton;
        }
    }
}
