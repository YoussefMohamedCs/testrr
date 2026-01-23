using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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
    public Material wrong_answer;

    public Material mat;
    public GameObject text_beside_button;

    private Image[] buttonImages;
    private int exp_selected_number;
    private int exp_num;

    void Start()
    {
        exp_num = control.exp_number_;

        // Cache all images ONCE (no lag at runtime)
        buttonImages = new Image[]
        {
            Aplus.image, Aminus.image, Bplus.image, Bminus.image,
            ABplus.image, ABminus.image, Oplus.image, Ominus.image
        };
    }

    void Select(Button b, int num)
    {
        // Clear materials

        foreach (Image img in buttonImages)
            img.material = null;

        // Set material to the selected button



        exp_selected_number = num;

        if (exp_num == exp_selected_number)
        {
            b.image.material = mat;
        }
        else
        {
            b.image.material = wrong_answer;
        }
    }

    public void AplusPress() => Select(Aplus, 1);
    public void AminusPress() => Select(Aminus, 2);
    public void BplusPress() => Select(Bplus, 3);
    public void BminusPress() => Select(Bminus, 4);
    public void ABplusPress() => Select(ABplus, 5);
    public void ABminusPress() => Select(ABminus, 6);
    public void Opluspress() => Select(Oplus, 7);
    public void OminusPress() => Select(Ominus, 8);

    public void SUBMIT()
    {
        if (exp_num == exp_selected_number)
        {
            myText.text = "correct!";
            text_beside_button.SetActive(true);
        }
        else
        {
            myText.text = "false!";
        }
    }
}
