using UnityEngine;
using System.Collections;
public class chnageColor : MonoBehaviour
{
    public Material mat;
    private bool heaterIsOpen;
    void Start()
    {
        mat.SetColor("_TopColor", new Color ( 0.353f , 0.098f , 1f ));
        mat.SetColor("_SideColor", new Color(0f, 0f, 0.980f));
    }

    // Update is called once per frame
    void Update()
    {
        heaterIsOpen = openHeater.buttonIsOpen;

        if ((heaterIsOpen))
        {
            StartCoroutine(ChangeColorGradually());
        }
    }



    IEnumerator ChangeColorGradually()
    {
        Color startColor = mat.GetColor("_SideColor");
        Color endColor = new Color(1f, 0.41f, 0.7f, 1f); // white with alpha 0

        float duration = 5f; // time of transition
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Color newColor = Color.Lerp(startColor, endColor, t / duration);
            mat.SetColor("_SideColor", newColor);

            yield return null;
        }



    }
}
