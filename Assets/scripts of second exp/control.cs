using UnityEngine;

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
    void Awake()
    {
        exp_number_ = Random.Range(1, 9);
    }

    void Start()
    {

        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
        Debug.Log(exp_number_);
    }
    private void Update()
    {
        BloodFill = bloodShadeer.GetFloat("_Fill");
         if(counter == 2 && (exp_number_ == 1 || exp_number_ == 3 || exp_number_ == 6))
        {

            panel.SetActive(true);
        } else if(counter == 1 && (exp_number_ == 2 || exp_number_ == 7 || exp_number_ == 4))
        {
            panel.SetActive(true);
        }
        else if(counter == 3 && (exp_number_ == 5 ))
        {
            panel.SetActive(true);

        }
        else if(counter == 0 && exp_number_ == 8)
        {
            panel.SetActive(true);
        }

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
