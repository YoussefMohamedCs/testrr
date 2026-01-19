using UnityEngine;
using System.Collections;
using DG.Tweening;
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
    public Texture2D imgBlood;
    bool BLOOD_IS_SET = false;
    bool isRunning = false;
    int exp_num;
    public GameObject AntiBCylinder;

    void Start()
    {
        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
      exp_num  = control.exp_number_;
    }

    public void Update()
    {
        BloodFill = bloodShadeer.GetFloat("_Fill");
        YellowFill = yellowShader.GetFloat("_Fill");
        float timer = control.secs;

        if ((exp_num == 3 || exp_num == 4 || exp_num == 5 || exp_num == 6) && (timer == 0))
        {


            StartCoroutine(ChangeColorGradually());



        }
    }



    IEnumerator ChangeColorGradually()
    {
        Color startColor = BLOOD_ON_BOARD.GetColor("_SideColor");
        Color endColor = new Color(1f, 1f, 1f, 0f); // white with alpha 0


        float duration = 2f; // time of transition
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Color newColor = Color.Lerp(startColor, endColor, t / duration);
            BLOOD_ON_BOARD.SetColor("_SideColor", newColor);
            BLOOD_ON_BOARD.SetTexture("_MyTexture", imgBlood);

            yield return null;
        }
    }

    IEnumerator changeNormalColor()
    {
        Color startColor = BLOOD_ON_BOARD.GetColor("_SideColor");
        Color endColor = new Color(1f, 80f / 255f, 0f, 1f);


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
            blood.transform.DOScale(new Vector3(-0.05995422f, -0.0007622794f, -0.05995422f), 1f);
        }

    
        
            if (collision.gameObject.CompareTag("YellowWell") && BLOOD_IS_SET == true && YellowFill != 0)
            {
            
            point1.SetActive(true);
            point2.SetActive(true);
            Invoke("HideFirstPoint", 0.5f);
            Invoke("HideSecondPoint", 0.8f);
            control.isAntiB = true;
            yellowShader.SetFloat("_Fill", 0f);
            AntiBCylinder.SetActive(true);
            AntiBCylinder.transform.DOScale(new Vector3(0.13f, 0.0001f, 0.13f), 1f);

        }
        }
      
    


}
