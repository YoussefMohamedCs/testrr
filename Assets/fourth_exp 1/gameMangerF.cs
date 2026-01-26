using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class gameMangerF : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Material correct;
    public Material incorrect;
    public Button trueButton;
    public Material isDisables;
    public Button falseButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;   // Unlock cursor
        Cursor.visible = true;                   // Always show cursor
    }

    public void clicktrue()
    {
        trueButton.image.material = correct;
        falseButton.interactable = false;
        falseButton.image.material = isDisables;
    }


    public void clickfalse()
    {
        falseButton.image.material = incorrect;
        trueButton.interactable = false;
    }
}
