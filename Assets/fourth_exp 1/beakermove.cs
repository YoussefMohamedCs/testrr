using UnityEngine;

public class beakermove : MonoBehaviour
{
    public float pickupRange = 2f;
    public float moveSpeed = 10f;

    private Rigidbody rb;
    private Camera cam;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(
            GameObject.FindGameObjectWithTag("Player").transform.position,
            transform.position
        );

        if (distanceToPlayer <= pickupRange)
        {
            if (Input.GetMouseButtonDown(0))
                isDragging = true;

            if (Input.GetMouseButtonUp(0))
                isDragging = false;
        }
    }

    void FixedUpdate()
    {
        if (!isDragging) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Vertical plane → X & Y movement
        Plane plane = new Plane(Vector3.forward, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 targetPos = ray.GetPoint(distance);
            Vector3 direction = (targetPos - rb.position);

            rb.linearVelocity = direction * moveSpeed;
        }
    }
}
