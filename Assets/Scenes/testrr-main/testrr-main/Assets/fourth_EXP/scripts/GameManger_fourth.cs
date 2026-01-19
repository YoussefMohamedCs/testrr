using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManger_fourth : MonoBehaviour
{
    public Camera mainCamera;


    void Start()
    {
        StartCoroutine(firstMoveOFCam(5f));


    }

    IEnumerator firstMoveOFCam(float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = new Vector3(-85.005f, 3.724f, -147.144f);
        Vector3 endPos = new Vector3(-86.4696f, 2.835006f, -145.9612f);

        while (elapsed < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                openGlass glass = hit.collider.GetComponent<openGlass>();
                if (glass != null)
                {
                    glass.Moves();

                }
            }
        }
    }



}
