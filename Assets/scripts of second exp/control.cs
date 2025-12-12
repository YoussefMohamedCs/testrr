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
