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
    float BloodFill;
    public Material BLOOD_ON_BOARD;


    void Start()
    {
        BLOOD_ON_BOARD.SetColor("_SideColor", Color.red);
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
