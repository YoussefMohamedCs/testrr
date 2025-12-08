using UnityEngine;
using System.Collections;
public class Anti_A : MonoBehaviour
{
    public GameObject blood;
    public Material bloodShadeer;
    public Material yellowShader;
    public Material BlueShader;
    public Material whiteSHader;
    public Material BLOOD_ON_BOARD;
    public GameObject point1;
    public GameObject point2;
    float BloodFill;
    float BlueFill;
    bool BLOOD_IS_SET = false;


    void Start()
    {
        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);

    }
    private void Update()
    {
        BloodFill = bloodShadeer.GetFloat("_Fill");
        BlueFill = BlueShader.GetFloat("_Fill");
    }

    IEnumerator ChangeColorGradually()
    {
        Color startColor = new Color(0.7686f, 0.7843f, 0.8118f, 0f);
        Color endColor = new Color(0.7686f, 0.7843f, 0.8118f, 1f);

        float duration = 2f; // time of transition
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Color newColor = Color.Lerp(startColor, endColor, t / duration);
            BLOOD_ON_BOARD.SetColor("_SideColor", newColor);

            yield return null;
        }
    }


    void HideFirstPoint()
    {
        point1.SetActive(false);

    }
    void HideSecondPoint()
    {
        point2.SetActive(false);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BloodWell") && BloodFill != 0)
        {
            bloodShadeer.SetFloat("_Fill", 0f);
            blood.SetActive(true);
            BLOOD_IS_SET = true;
        }


        if (collision.gameObject.CompareTag("BlueWell") && BLOOD_IS_SET == true && BlueFill != 0)
        {
            StartCoroutine(ChangeColorGradually());
            point1.SetActive(true);
            point2.SetActive(true);
            Invoke("HideFirstPoint", 0.5f);
            Invoke("HideSecondPoint", 0.8f);
            BlueShader.SetFloat("_Fill", 0f);


        }
    }

}
