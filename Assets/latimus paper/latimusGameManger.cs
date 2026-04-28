using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class latimusGameManger : MonoBehaviour
{

    bool handStatus = false;
    public GameObject hand;
    void Start()
    {
 


    }


    void Update()
    {
      
    

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handStatus = !handStatus;
            hand.SetActive(handStatus);
        }
    }





}
