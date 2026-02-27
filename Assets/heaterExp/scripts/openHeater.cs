using UnityEngine;

public class openHeater : MonoBehaviour
{
    public static bool buttonIsOpen = false;
    public ParticleSystem fire;
    public GameObject fireObject;

    bool lastState = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    buttonIsOpen = !buttonIsOpen;
                }
            }
        }

        // Ì ‰›– ›ﬁÿ ·„« «·Õ«·…   €Ì—
        if (buttonIsOpen != lastState)
        {
            lastState = buttonIsOpen;

            if (buttonIsOpen)
            {
                fireObject.SetActive(true);
                fire.Play();
            }
            else
            {
                fire.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                fireObject.SetActive(false);
            }
        }
    }
}
