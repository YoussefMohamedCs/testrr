using UnityEngine;
using System.Collections;

public class papaerMove : MonoBehaviour
{
    public GameObject paper;
    public GameObject water;
    private int theNumebr = 0;

    private Material paperMat;
    private bool isInWater = false;
    private float savedFill = 0f; // »ÌÕ›Ÿ ¬Œ— ﬁÌ„… ·„«  ÿ·⁄ „‰ «·„«Ì…

    //void Start()
    //{
    //    paperMat = paper.GetComponent<Renderer>().material;
    //    paperMat.SetFloat("_FillAmount", 0f);

    //    if (paper.CompareTag("paper1"))
    //    {

    //    }
    //}

    void Start()
    {
        paperMat = paper.GetComponent<Renderer>().material;
        paperMat.SetFloat("_FillAmount", 0f);

        if (paper.CompareTag("paper1") || paper.CompareTag("paper2"))
        {
            // «··Ê‰ «·√”«”Ì ··Ê—ﬁ… = »‰Ì
            paperMat.SetColor("_PaperColor", new Color(0.36f, 0.20f, 0.09f));

            // ·Ê‰ «·„«Ì… ·„«  ·„”Â«
            paperMat.SetColor("_BottomColor", new Color(0.1f, 0.4f, 1.0f));
            paperMat.SetColor("_TopColor", new Color(0.0f, 0.8f, 1.0f));
        }
        else if (paper.CompareTag("paper3") || paper.CompareTag("paper4"))
        {
            // «··Ê‰ «·√”«”Ì ··Ê—ﬁ… = √Œ÷—
            paperMat.SetColor("_PaperColor", new Color(0.05f, 0.35f, 0.10f));

            // ·Ê‰ «·„«Ì… ·„«  ·„”Â«
            paperMat.SetColor("_BottomColor", new Color(0.1f, 0.4f, 1.0f));
            paperMat.SetColor("_TopColor", new Color(0.0f, 0.8f, 1.0f));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    if (theNumebr == 0)
                    {
                        StartCoroutine(HandlePositionOfPaper());
                        theNumebr++;
                    }
                }
            }
        }

        // »‰ÕœÀ «··Ê‰ »” ·Ê «·Ê—ﬁ… ÃÊÂ «·„«Ì… ›⁄·«
        if (isInWater && water != null)
        {
            UpdateFillBasedOnWater();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == water)
        {
            isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == water)
        {
            isInWater = false;
            // »‰Õ›Ÿ «·ﬁÌ„… «·Õ«·Ì… Ê„‘ »‰—Ã⁄Â« ··’›—
            savedFill = paperMat.GetFloat("_FillAmount");
        }
    }

    private void UpdateFillBasedOnWater()
    {
        Bounds waterBounds = water.GetComponent<Collider>().bounds;
        Bounds paperBounds = paper.GetComponent<Renderer>().bounds;

        float paperBottom = paperBounds.min.y;
        float paperTop = paperBounds.max.y;
        float paperHeight = paperTop - paperBottom;

        float waterSurface = waterBounds.max.y;

        float newFill;

        if (waterSurface <= paperBottom)
        {
            newFill = savedFill; // „Õ›ÊŸ
        }
        else if (waterSurface >= paperTop)
        {
            newFill = 1f; // €—ﬁ  ﬂ·Â«
        }
        else
        {
            float submergedHeight = waterSurface - paperBottom;
            newFill = submergedHeight / paperHeight;
        }

        // »‰Õÿ œ«Ì„« «·√⁄·Ï »Ì‰ «·ﬁÌ„… «·ÃœÌœ… Ê«·„Õ›ÊŸ…
        // ⁄‘«‰ «··Ê‰ „Ì—Ã⁄‘ ·√ﬁ· „‰ «··Ì « ·Ê¯‰ ›⁄·«
        savedFill = Mathf.Max(savedFill, newFill);
        paperMat.SetFloat("_FillAmount", savedFill);
    }

    private IEnumerator HandlePositionOfPaper()
    {
        Vector3 targetPosition = new Vector3(-115.802f, 2.491f, -238.583f);
        Quaternion targetRotation = Quaternion.Euler(90f, 0f, 0f);
        float duration = 1f;
        float elapsed = 0f;
        Vector3 startPosition = paper.transform.position;
        Quaternion startRotation = paper.transform.rotation;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smoothT = Mathf.SmoothStep(0f, 1f, t);
            paper.transform.position = Vector3.Lerp(startPosition, targetPosition, smoothT);
            paper.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, smoothT);
            yield return null;
        }

        paper.transform.position = targetPosition;
        paper.transform.rotation = targetRotation;
    }
}