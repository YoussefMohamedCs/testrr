using Unity.VisualScripting;
using UnityEngine;

public class headterButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool buttonIsOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // clicked THIS object
                {
                    buttonIsOpen = !buttonIsOpen;
                    //Debug.Log(buttonIsOpen);
                }
            }
        }
    }
}

