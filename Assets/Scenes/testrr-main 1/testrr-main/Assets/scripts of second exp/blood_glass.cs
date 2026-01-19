using UnityEngine;

public class blood_glass : MonoBehaviour
{
    public GameObject ph;


    private void OnMouseEnter()
    {
        ph.SetActive(true);
    }
    private void OnMouseExit()
    {
        ph.SetActive(false);

    }
}
