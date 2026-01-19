using UnityEngine;

public class BottleTop : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private bool isDragging = false;
    private float zDist;
    private Vector3 offset;
    public Material mat;

    public float followStrength = 40f;  // قوة الحركة الفيزيائية

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        rb.isKinematic = false;
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
    }

    void OnMouseDown()
    {
        //if (!CompareTag("RedWell")) return;

        isDragging = true;

        rb.useGravity = false;   // no gravity during drag
        rb.isKinematic = false;  // IMPORTANT: dynamic to prevent passing

        zDist = Vector3.Distance(cam.transform.position, transform.position);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDist;
        offset = transform.position - cam.ScreenToWorldPoint(mousePos);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDist;

        Vector3 target = cam.ScreenToWorldPoint(mousePos) + offset;

        // Only X/Y
        target.z = transform.position.z;

        // Instead of MovePosition → use velocity for REAL physics movement
        Vector3 direction = target - transform.position;

        rb.linearVelocity = direction * followStrength * Time.deltaTime;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        rb.useGravity = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BLOOD") || other.CompareTag("WHITE") || other.CompareTag("YELLOW") || other.CompareTag("BLUE"))
        {
            mat.SetFloat("_Fill", 1.0f);
        }
       
    }
}
