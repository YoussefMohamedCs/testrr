using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using TMPro;

public class panel_control : MonoBehaviour
{
    public TMP_Text myText;
    public Button Aplus;
    public Button Aminus;
    public Button Bplus;
    public Button Bminus;
    public Button ABplus;
    public Button ABminus;
    public Button Oplus;
    public Button Ominus;
    public Material mat;
    int exp_selected_number;
    int exp_num;

    public GameObject text_beside_button;
    void Start()
    {
        exp_num = control.exp_number_;
    }


public void AplusPress()
    {
        Aplus.GetComponent<Image>().material = mat;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 1;
    }

    public void AminusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = mat;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 2;
    }

    public void BplusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = mat;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 3;
    }
    public void BminusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = mat;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 4;
    }

    public void ABplusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = mat;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 5;
    }
    public void ABminusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = mat;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 6;
    }

    public void Opluspress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = mat;
        Ominus.GetComponent<Image>().material = null;
        exp_selected_number = 7;
    }


    public void OminusPress()
    {
        Aplus.GetComponent<Image>().material = null;
        Aminus.GetComponent<Image>().material = null;
        Bplus.GetComponent<Image>().material = null;
        Bminus.GetComponent<Image>().material = null;
        ABplus.GetComponent<Image>().material = null;
        ABminus.GetComponent<Image>().material = null;
        Oplus.GetComponent<Image>().material = null;
        Ominus.GetComponent<Image>().material = mat;
        exp_selected_number = 8;
    }

    public void SUBMIT()
    {
        if(exp_num == exp_selected_number)
        {
            myText.text = "correct!";
            text_beside_button.SetActive(true);
        }
    }
}

