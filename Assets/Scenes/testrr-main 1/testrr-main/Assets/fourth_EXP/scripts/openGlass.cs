using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
public class openGlass : MonoBehaviour
{
    int detectTheNumberOfOpening = 1;
    public GameObject theGlass;
    public event EventHandler onGlassClick;
    private Vector3 originalPosition;
    private Vector3 offset = new Vector3(0f, 0.1f, 0.11f);
    private bool isOpen = false;

    private void Start()
    {
        originalPosition = theGlass.transform.position;
    }
    public void Click()
    {
        onGlassClick?.Invoke(this, EventArgs.Empty);
    }


    public void Moves()
    {
        if (!isOpen)
        {
            // Move to target
            Vector3 target = isOpen ? originalPosition : originalPosition + offset;
            theGlass.transform.DOMove(target, 1f);
        }
        else
        {
            // Move back to original
            theGlass.transform.DOMove(originalPosition, 1f);
        }

        isOpen = !isOpen; // flip the state
    }
}


