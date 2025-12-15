using UnityEngine;
using System.Collections;

using DG.Tweening;
using UnityEngine.UIElements;
public class Anti_D : MonoBehaviour
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
    float whiteFill;
    bool BLOOD_IS_SET = false;
    int exp_num;
    public Texture2D imgBlood;
    public GameObject AntiDcylinder;

    void Start()
    {
     
        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
        exp_num = control.exp_number_;

    }
    private void Update()
    {
        BloodFill = bloodShadeer.GetFloat("_Fill");
        whiteFill = whiteSHader.GetFloat("_Fill");
        float timer = control.secs;
        Debug.Log(timer);
        if ((exp_num == 1 || exp_num == 3 || exp_num == 5 || exp_num == 7) &&  (timer == 0))
        {

            StartCoroutine(ChangeColorGradually());


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
    IEnumerator ChangeColorGradually()
    {
        Color startColor = BLOOD_ON_BOARD.GetColor("_SideColor");
        Color endColor = new Color(1f, 1f, 1f, 0f); // white with alpha 0
        BLOOD_ON_BOARD.SetTexture("_MyTexture", imgBlood);

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
            blood.transform.DOScale(new Vector3(-0.05995422f , -0.0007622794f , -0.05995422f), 1f);

            BLOOD_IS_SET = true;
        }

        
            if (collision.gameObject.CompareTag("WhiteWell") && BLOOD_IS_SET == true && whiteFill != 0)
            {

             
                point1.SetActive(true);
                point2.SetActive(true);
            Invoke("HideFirstPoint", 0.5f);
            Invoke("HideSecondPoint", 0.8f);
            control.isAntiD = true;
            whiteSHader.SetFloat("_Fill", 0f);
            AntiDcylinder.SetActive(true);
            AntiDcylinder.transform.DOScale(new Vector3(0.13f, 0.0001f, 0.13f), 1f);


        } 
       
    }

   


}
