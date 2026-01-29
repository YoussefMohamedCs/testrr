using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManger_fourth : MonoBehaviour
{
    public Camera mainCamera;
    bool handStatus = false;
    public GameObject hand;
    void Start()
    {
        //StartCoroutine(firstMoveOFCam(5f));


    }

    IEnumerator firstMoveOFCam(float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = new Vector3(-85.216f, 3.08f, -147.228f);
        Vector3 endPos = new Vector3(-86.518f, 2.711f, -146.109f);

        while (elapsed < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator secondMove(float duration)
    {
        float elapsed = 0f;
        Vector3 startPos =mainCamera.transform.position;
        Vector3 endPos = new Vector3(-85.739f, 2.915f, -146.629f);

        while (elapsed < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator thirdMove(float duration)
    {
        float elapsed = 0f;
        Vector3 startPos = mainCamera.transform.position;
        Vector3 endPos = new Vector3(-85.216f, 3.08f, -147.228f);

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
                openSecondGlass glassSecond = hit.collider.GetComponent<openSecondGlass>();

                if (glass != null)
                {
                    glass.Moves();

                }

                if(glassSecond != null)
                {
                    glassSecond.Moves();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(firstMoveOFCam(5f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(secondMove(5f));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(thirdMove(5f));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            handStatus = !handStatus;
            hand.SetActive(handStatus);
        }
    }

   



}
