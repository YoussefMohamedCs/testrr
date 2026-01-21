using UnityEngine;

public class beaker : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    private bool isDragging;
    public float forcePower = 50f;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.linearVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (!isDragging) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 targetPoint = ray.GetPoint(enter);
            Vector3 direction = targetPoint - transform.position;

            rb.AddForce(direction * forcePower, ForceMode.Force);
        }
    }
}

