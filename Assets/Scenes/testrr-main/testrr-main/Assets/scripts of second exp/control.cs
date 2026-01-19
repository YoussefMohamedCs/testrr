using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

using DG.Tweening;
public class control : MonoBehaviour
{



    public GameObject blood;
    public Material bloodShadeer;
    public Material yellowShader;
    public Material BlueShader;
    public Material whiteSHader;
    public GameObject point1;
    public GameObject point2;
    private float BloodFill;
    public Material BLOOD_ON_BOARD;
    public static int exp_number_;
    public static int counter = 0;
    public GameObject panel;


    public static bool isAntiA = false;
    public static bool isAntiB = false;
    public static bool isAntiD = false;

    public TextMeshPro textCounter;
    public GameObject textCounterGameObject;

    public static  float timeLeft = 5f;
    public static int secs ;
    void Awake()
    {
        exp_number_ = Random.Range(1, 9);
        secs = 5;
    }

    void Start()
    {

        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
        Debug.Log(exp_number_);
    }
     void Update()
    {
       
       

        BloodFill = bloodShadeer.GetFloat("_Fill");
         if(isAntiA && isAntiB && isAntiD)
        {
            if (timeLeft <= 0f)
            {
                textCounterGameObject.SetActive(false);
                timeLeft = 0f;
                secs = -100;
                return;
            }

            timeLeft -= Time.deltaTime;

            int seconds = Mathf.CeilToInt(timeLeft);
            secs = seconds;

            Debug.Log(seconds);
            textCounterGameObject.SetActive(true);
            textCounter.text = "wait  " + seconds + " seconds for the result";
            Invoke("handleAppearingOfQuizPanel", 10f);
           

        }

   

    

    }
 
    void handleAppearingOfQuizPanel()
    {
        panel.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BloodWell") && BloodFill != 0)
        {
            bloodShadeer.SetFloat("_Fill", 0f);
            blood.SetActive(true);
            blood.transform.DOScale(new Vector3(-0.05995422f, -0.0007622794f, -0.05995422f), 1f);
        }
    }
}
