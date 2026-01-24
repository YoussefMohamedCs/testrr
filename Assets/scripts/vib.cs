using UnityEngine;
using System.Collections;
public class vib : MonoBehaviour
{
 
    public float vibrationDuration = 0.3f;
    public float vibrationStrength = 0.02f;
    public float vibrationSpeed = 40f;

    private Vector3 originalPosition;
    private Coroutine vibrationCoroutine;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (vibrationCoroutine != null)
            StopCoroutine(vibrationCoroutine);

        vibrationCoroutine = StartCoroutine(Vibrate());
    }

    IEnumerator Vibrate()
    {
        float elapsed = 0f;

        while (elapsed < vibrationDuration)
        {
            elapsed += Time.deltaTime;

            float x = Mathf.Sin(Time.time * vibrationSpeed) * vibrationStrength;
            float y = Mathf.Cos(Time.time * vibrationSpeed) * vibrationStrength;

            transform.localPosition = originalPosition + new Vector3(x, y, 0f);
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}