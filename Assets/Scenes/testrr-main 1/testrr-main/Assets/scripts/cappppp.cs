using UnityEngine;

public class cappppp : MonoBehaviour
{
    [Header("Object to Pickup")]
    [SerializeField] private GameObject pickupObject;

    [Header("Pickup Settings")]
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private float holdDistance = 2f;
    [SerializeField] private float moveSpeed = 10f;

    [Header("References")]
    [SerializeField] private Transform playerCamera;

    [Header("Water Settings")]
    [SerializeField] private string waterTag = "Water";
    [SerializeField] private float shrinkSpeed = 2f;

    [Header("Particle Settings")]
    [SerializeField] private ParticleSystem waterParticles;

    // ⭐ NEW — separate movement speed during shrinking
    [SerializeField] private float shrinkMoveSpeed = 1f;

    private Rigidbody objectRb;
    private bool isHolding = false;
    private bool isInRange = false;
    private Vector3 originalScale;
    private bool isShrinking = false;

    // ⭐ Store water center point
    private Vector3 waterCenter;

    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;

        if (pickupObject != null)
        {
            objectRb = pickupObject.GetComponent<Rigidbody>();
            if (objectRb == null)
                Debug.LogError("Assigned object needs a Rigidbody component!");

            originalScale = pickupObject.transform.localScale;
        }

        if (waterParticles != null)
            waterParticles.Stop();
    }

    void Update()
    {
        if (pickupObject != null && pickupObject.activeSelf)
        {
            // ⭐ ONLY CHANGE → distance uses playerCamera instead of transform
            float distance = Vector3.Distance(playerCamera.position, pickupObject.transform.position);
            isInRange = distance <= pickupRange;

            Vector3 toObject = (pickupObject.transform.position - playerCamera.position).normalized;
            float angle = Vector3.Angle(playerCamera.forward, toObject);
            bool isInFront = angle < 60f;
            isInRange = isInRange && isInFront;
        }

        if (isInRange && pickupObject.activeSelf && !isShrinking)
        {
            if (Input.GetMouseButtonDown(0) && !isHolding)
                Pickup();

            if (Input.GetMouseButtonUp(0) && isHolding)
                Drop();
        }

        if (isHolding && pickupObject != null && pickupObject.activeSelf)
            MoveObject();

        // ⭐ SHRINKING + MOVING TOWARD WATER CENTER
        if (isShrinking && pickupObject != null && pickupObject.activeSelf)
        {
            pickupObject.transform.position = Vector3.MoveTowards(
                pickupObject.transform.position,
                waterCenter,
                shrinkMoveSpeed * Time.deltaTime
            );

            pickupObject.transform.localScale = Vector3.MoveTowards(
                pickupObject.transform.localScale,
                Vector3.zero,
                shrinkSpeed * Time.deltaTime
            );

            if (waterParticles != null && !waterParticles.isPlaying)
                waterParticles.Play();

            if (pickupObject.transform.localScale.magnitude <= 0.01f)
            {
                pickupObject.SetActive(false);
                isShrinking = false;

                if (waterParticles != null)
                    waterParticles.Stop();
            }
        }

        if (isHolding && (pickupObject == null || !pickupObject.activeSelf))
            Drop();
    }

    void Pickup()
    {
        if (objectRb != null)
        {
            isHolding = true;
            objectRb.useGravity = false;
            objectRb.isKinematic = false;
            objectRb.linearVelocity = Vector3.zero;
            objectRb.angularVelocity = Vector3.zero;
        }
    }

    void MoveObject()
    {
        Vector3 targetPosition = playerCamera.position + playerCamera.forward * holdDistance;
        Vector3 direction = targetPosition - pickupObject.transform.position;
        objectRb.linearVelocity = direction * moveSpeed;

        Quaternion targetRotation = playerCamera.rotation;
        Quaternion deltaRotation = targetRotation * Quaternion.Inverse(pickupObject.transform.rotation);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        if (angle > 180f) angle -= 360f;

        objectRb.angularVelocity = Mathf.Abs(angle) > 0.01f
            ? axis * angle * Mathf.Deg2Rad * moveSpeed * 5f
            : Vector3.zero;
    }

    void Drop()
    {
        if (objectRb != null)
        {
            objectRb.useGravity = true;
            objectRb.AddForce(playerCamera.forward * 2f, ForceMode.Impulse);
            isHolding = false;
        }
    }

    public void ResetObject()
    {
        if (pickupObject != null)
        {
            pickupObject.SetActive(true);
            pickupObject.transform.localScale = originalScale;
            isHolding = false;
            isShrinking = false;

            if (objectRb != null)
            {
                objectRb.isKinematic = false;
                objectRb.useGravity = true;
            }

            if (waterParticles != null)
                waterParticles.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(waterTag))
        {
            isShrinking = true;
            isHolding = false;

            waterCenter = other.bounds.center;
        }
    }

    void OnDrawGizmos()
    {
        if (pickupObject != null)
        {
            Gizmos.color = isInRange ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, pickupObject.transform.position);
            Gizmos.color = new Color(0, 1, 0, 0.2f);
            Gizmos.DrawWireSphere(transform.position, pickupRange);
        }
    }
}
