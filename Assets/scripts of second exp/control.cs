using TMPro;
using Unity;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class control : MonoBehaviour
{

    public TextMeshPro waitingText;
    public GameObject waitingTextObject;
    public static int the_numbers_of_testing = 0;
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
    int counterTimer = 0;
    void Awake()
    {
        exp_number_ = Random.Range(1, 9);
    }

    void Start()
    {

        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
        Debug.Log(exp_number_);
    }

 
    void FixedUpdate()
    {
       

        if (the_numbers_of_testing == 3)
        {
            waitingText.text = "wait for " + Mathf.Floor(counterTimer * Time.deltaTime) + " seconds";
            waitingTextObject.SetActive(true);
            counterTimer++;
        }
       if(Mathf.Floor(counterTimer * Time.deltaTime) == 6)
        {
            waitingTextObject.SetActive(false);
            the_numbers_of_testing = 0;
        }
     




            BloodFill = bloodShadeer.GetFloat("_Fill");
         if(the_numbers_of_testing == 3 && (exp_number_ == 1 || exp_number_ == 3 || exp_number_ == 6))
        {
            Invoke("displayQuizPanel" , 5f);


        } else if(the_numbers_of_testing == 3 && (exp_number_ == 2 || exp_number_ == 7 || exp_number_ == 4))
        {
            Invoke("displayQuizPanel", 5f);
        }
        else if(the_numbers_of_testing == 3 && (exp_number_ == 5 ))
        {

            Invoke("displayQuizPanel", 5f);
        }
        else if(the_numbers_of_testing == 3 && exp_number_ == 8)
        {
            Invoke("displayQuizPanel", 5f);
        }

    }
    void displayQuizPanel()
    {
        panel.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BloodWell") && BloodFill != 0)
        {
            bloodShadeer.SetFloat("_Fill", 0f);
            blood.SetActive(true);
        }
    }
}
