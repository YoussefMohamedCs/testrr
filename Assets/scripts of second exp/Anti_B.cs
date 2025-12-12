using UnityEngine;
using System.Collections;
public class Anti_B : MonoBehaviour
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
    float YellowFill;
    bool BLOOD_IS_SET = false;
    int exp_num;

    void Start()
    {
        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
      exp_num  = control.exp_number_;
    }

    private void Update()
    {
        BloodFill = bloodShadeer.GetFloat("_Fill");
        YellowFill = yellowShader.GetFloat("_Fill");
    }

    IEnumerator ChangeColorGradually()
    {
        Color startColor = BLOOD_ON_BOARD.GetColor("_SideColor");
        Color endColor = new Color(1f, 110f / 215f, 7f / 215f, 0f);

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

        if(exp_num == 3 || exp_num == 4 || exp_num == 5 || exp_num == 6)
        {
            if (collision.gameObject.CompareTag("YellowWell") && BLOOD_IS_SET == true && YellowFill != 0)
            {
                StartCoroutine(ChangeColorGradually());
                point1.SetActive(true);
                point2.SetActive(true);
                Invoke("HideFirstPoint", 0.5f);
                Invoke("HideSecondPoint", 0.8f);
                yellowShader.SetFloat("_Fill", 0f);


            }
        }
      
    }


}
