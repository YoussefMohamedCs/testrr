using System.Collections;
using UnityEngine;

public class points : MonoBehaviour
{
    public static bool allPointsDone = false;
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;

    public GameObject point4;
    public GameObject point5;

    [Header("Movement Settings")]
    public float fallSpeed = 0.3f; // Increased for better falling
    public float delayBetweenPoints = 0.3f; // Delay between each point

    [Header("Liquid Settings")]
    public string liquidTag = "Liquid"; // Tag for liquid objects

    private bool isDropping = false;

    // Store original positions and scales
    private Vector3 point1StartPos;
    private Vector3 point2StartPos;
    private Vector3 point3StartPos;
    private Vector3 point4StartPos;
    private Vector3 point5StartPos;

    private Vector3 point1StartScale;
    private Vector3 point2StartScale;
    private Vector3 point3StartScale;
    private Vector3 point4StartScale;
    private Vector3 point5StartScale;

    void Start()
    {
        // Store original positions
        point1StartPos = point1.transform.position;
        point2StartPos = point2.transform.position;
        point3StartPos = point3.transform.position;
        point4StartPos = point4.transform.position;
        point5StartPos = point5.transform.position;

        // Store original scales
        point1StartScale = point1.transform.localScale;
        point2StartScale = point2.transform.localScale;
        point3StartScale = point3.transform.localScale;
        point4StartScale = point4.transform.localScale;
        point5StartScale = point5.transform.localScale;

        // Make sure all points are inactive at start
        point1.SetActive(false);
        point2.SetActive(false);
        point3.SetActive(false);
        point4.SetActive(false);
        point5.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDropping)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // clicked THIS object
                {
                    StartCoroutine(DropPointsSequentially());
                }
            }
        }
    }

    IEnumerator DropPointsSequentially()
    {
        isDropping = true;

        // Reset all points to their original state
        ResetPoint(point1, point1StartPos, point1StartScale);
        ResetPoint(point2, point2StartPos, point2StartScale);
        ResetPoint(point3, point3StartPos, point3StartScale);
        ResetPoint(point4, point4StartPos, point4StartScale);
        ResetPoint(point5, point5StartPos, point5StartScale);

        // Drop point1
        yield return StartCoroutine(DropPoint(point1));
        yield return new WaitForSeconds(delayBetweenPoints);

        // Drop point2
        yield return StartCoroutine(DropPoint(point2));
        yield return new WaitForSeconds(delayBetweenPoints);

        // Drop point3
        yield return StartCoroutine(DropPoint(point3));
        yield return new WaitForSeconds(delayBetweenPoints);

        // Drop point4
        yield return StartCoroutine(DropPoint(point4));
        yield return new WaitForSeconds(delayBetweenPoints);

        // Drop point5
        yield return StartCoroutine(DropPoint(point5));

        isDropping = false;
        allPointsDone = true;
    }

    void ResetPoint(GameObject point, Vector3 startPosition, Vector3 startScale)
    {
        // Deactivate first
        point.SetActive(false);

        // Reset position
        point.transform.position = startPosition;

        // Reset scale
        point.transform.localScale = startScale;

        // Reset rotation
        point.transform.rotation = Quaternion.identity;

        // Reset collision detector
        PointCollisionDetector detector = point.GetComponent<PointCollisionDetector>();
        if (detector != null)
        {
            detector.hasCollided = false;
            detector.ResetOriginalScale(startScale);
        }

        // Reset rigidbody
        Rigidbody rb = point.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.None;
        }

        // Reset material alpha if it was faded
        Renderer rend = point.GetComponent<Renderer>();
        if (rend != null && rend.material.HasProperty("_Color"))
        {
            Color col = rend.material.color;
            col.a = 1f;
            rend.material.color = col;
        }
    }

    IEnumerator DropPoint(GameObject point)
    {
        point.SetActive(true);

        Rigidbody rb = point.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = point.AddComponent<Rigidbody>();
        }

        // Better smooth falling settings
        rb.useGravity = true;
        rb.linearDamping = 0.3f; // Less air resistance so it reaches the liquid
        rb.angularDamping = 0.5f;
        rb.mass = 0.2f; // Slightly heavier for consistent falling
        rb.interpolation = RigidbodyInterpolation.Interpolate; // Smoother movement
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Make sure collisions work properly
        Collider col = point.GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = false; // Use regular collision, not trigger
        }

        // Apply smooth downward force
        rb.linearVelocity = Vector3.down * fallSpeed;

        // Add collision detector component
        PointCollisionDetector detector = point.GetComponent<PointCollisionDetector>();
        if (detector == null)
        {
            detector = point.AddComponent<PointCollisionDetector>();
        }

        // Reset collision flag
        detector.hasCollided = false;

        // Wait until it collides
        while (!detector.hasCollided)
        {
            yield return null;
        }

        // Stop movement after collision
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}