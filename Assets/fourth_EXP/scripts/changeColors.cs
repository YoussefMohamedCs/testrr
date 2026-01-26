using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColors : MonoBehaviour
{
    [Header("Material Settings")]
    public Material targetMaterial;

    [Header("Target Colors")]
    public Color targetTopColor = Color.red;
    public Color targetMiddleColor = Color.green;
    public Color targetBottomColor = Color.blue;

    [Header("Animation Settings")]
    public float delayBeforeChange = 2f; // «·«‰ Ÿ«— ﬁ»· «· €ÌÌ—
    public float colorDuration = 2f; // „œ… ŸÂÊ— «··Ê‰ Ê«·ŒÿÊÿ
    public float stripeFadeDuration = 2f; // „œ… ŸÂÊ— «·ŒÿÊÿ

    [Header("Collision Settings")]
    public bool changeOnCollision = true;

    private Coroutine changeCoroutine;

    void Start()
    {
        targetMaterial.SetColor("_TopColor", new Color(1f, 0.96f, 0.71f, 1f));
        targetMaterial.SetColor("_MiddleColor", new Color(1f, 0.96f, 0.71f, 1f));
        targetMaterial.SetColor("_BottomColor", new Color(1f, 0.96f, 0.71f, 1f));
        if (targetMaterial == null)
        {
            Image img = GetComponent<Image>();
            if (img != null)
            {
                targetMaterial = new Material(img.material);
                img.material = targetMaterial;
            }
        }

        if (targetMaterial != null)
        {
            targetMaterial.SetFloat("_TopStripeAlpha", 0);
            targetMaterial.SetFloat("_MiddleStripeAlpha", 0);
            targetMaterial.SetFloat("_BottomStripeAlpha", 0);

            targetMaterial.SetFloat("_FillTop", 0);
            targetMaterial.SetFloat("_FillMiddle", 0);
            targetMaterial.SetFloat("_FillBottom", 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin") && changeOnCollision)
        {
            Debug.Log("Pin collided! Starting color change after 2 seconds...");

            // ≈Ìﬁ«› √Ì  €ÌÌ— ”«»ﬁ
            if (changeCoroutine != null)
                StopCoroutine(changeCoroutine);

            // »œ¡ «· €ÌÌ— »⁄œ À«‰Ì Ì‰
            changeCoroutine = StartCoroutine(ChangeAllAfterDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pin") && changeOnCollision)
        {
            Debug.Log("Pin triggered! Starting color change after 2 seconds...");

            if (changeCoroutine != null)
                StopCoroutine(changeCoroutine);

            changeCoroutine = StartCoroutine(ChangeAllAfterDelay());
        }
    }

    IEnumerator ChangeAllAfterDelay()
    {
        // «·«‰ Ÿ«— À«‰Ì Ì‰
        yield return new WaitForSeconds(delayBeforeChange);

        Debug.Log("Starting color and stripe change now!");

        //  €ÌÌ— ﬂ· «·√·Ê«‰ Ê«·ŒÿÊÿ
        yield return StartCoroutine(SmoothSetAllColors(true));
    }

    IEnumerator SmoothSetAllColors(bool showAllStripes)
    {
        Color startTop = targetMaterial.GetColor("_TopColor");
        Color startMiddle = targetMaterial.GetColor("_MiddleColor");
        Color startBottom = targetMaterial.GetColor("_BottomColor");

        float t = 0f;

        //  €ÌÌ— «·√·Ê«‰  œ—ÌÃÌ« ·„œ… À«‰Ì Ì‰
        while (t < colorDuration)
        {
            t += Time.deltaTime;
            float normalized = t / colorDuration;
            float smoothValue = Mathf.SmoothStep(0, 1, normalized);

            targetMaterial.SetColor("_TopColor", Color.Lerp(startTop, targetTopColor, smoothValue));
            targetMaterial.SetColor("_MiddleColor", Color.Lerp(startMiddle, targetMiddleColor, smoothValue));
            targetMaterial.SetColor("_BottomColor", Color.Lerp(startBottom, targetBottomColor, smoothValue));

            yield return null;
        }

        targetMaterial.SetColor("_TopColor", targetTopColor);
        targetMaterial.SetColor("_MiddleColor", targetMiddleColor);
        targetMaterial.SetColor("_BottomColor", targetBottomColor);

        // ≈ŸÂ«— ﬂ· «·ŒÿÊÿ  œ—ÌÃÌ«
        if (showAllStripes)
        {
            //targetMaterial.SetFloat("_FillBottom", 1);
            StartCoroutine(fill());
            // ≈ŸÂ«— «·ŒÿÊÿ «·À·«À… ›Ì ‰›” «·Êﬁ  ·„œ… À«‰Ì Ì‰
            StartCoroutine(FadeInStripes("_TopStripeAlpha"));
            StartCoroutine(FadeInStripes("_MiddleStripeAlpha"));
            StartCoroutine(FadeInStripes("_BottomStripeAlpha"));
        }
    }
    IEnumerator fill()
    {
        yield return new  WaitForSeconds(0.1f);
        targetMaterial.SetFloat("_FillTop", 1);
        yield return new WaitForSeconds(0.1f);
        targetMaterial.SetFloat("_FillMiddle", 1);
        yield return new WaitForSeconds(0.1f);
        targetMaterial.SetFloat("_FillBottom", 1);




    }

    IEnumerator FadeInStripes(string stripeProperty)
    {

        targetMaterial.SetFloat(stripeProperty, 1);
        yield return new WaitForSeconds(0.1f);
        targetMaterial.SetFloat(stripeProperty, 1);
        yield return new WaitForSeconds(0.1f);
        targetMaterial.SetFloat(stripeProperty, 1);
    }
    }