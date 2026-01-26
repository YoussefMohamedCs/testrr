using UnityEngine;
using DG.Tweening;

public class PointCollisionDetector : MonoBehaviour
{
    public bool hasCollided = false;
    private Rigidbody rb;
    private Vector3 lastPosition; // Track position to prevent tunneling

    [Header("Flatten Settings")]
    public float flattenScaleX = 3f;
    public float flattenScaleZ = 3f;
    public float flattenScaleY = 0.1f;
    public float flattenDuration = 1.5f;

    [Header("Fade Out Settings")]
    public float fadeDelay = 1.2f;
    public float fadeDuration = 0.3f;

    private Vector3 originalScale;
    private Renderer rend;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        hasCollided = false;
        lastPosition = transform.position; // Initialize tracking

        if (originalScale == Vector3.zero)
        {
            originalScale = transform.localScale;
        }
    }

    private void FixedUpdate()
    {
        // ANTI-TUNNELING LOGIC
        if (hasCollided) return;

        Vector3 currentPos = transform.position;
        Vector3 direction = currentPos - lastPosition;
        float distance = direction.magnitude;

        if (distance > 0)
        {
            // Check if we moved through any collider this frame
            if (Physics.Raycast(lastPosition, direction.normalized, out RaycastHit hit, distance))
            {
                transform.position = hit.point; // Snap to the surface
                HandleCollision(hit.collider.gameObject.name);
            }
        }
        lastPosition = currentPos;
    }

    public void ResetOriginalScale(Vector3 scale)
    {
        originalScale = scale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided) HandleCollision(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasCollided) HandleCollision(other.gameObject.name);
    }

    private void HandleCollision(string hitObjectName)
    {
        hasCollided = true;
        Debug.Log($"Caught collision with: {hitObjectName}");

        FreezePosition();
        AnimateOnCollision();
    }

    private void FreezePosition()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void AnimateOnCollision()
    {
        transform.DOKill();
        if (rend != null && rend.material != null) rend.material.DOKill();

        Vector3 flattenedScale = new Vector3(
            originalScale.x * flattenScaleX / 2,
            originalScale.y * flattenScaleY / 2,
            originalScale.z * flattenScaleZ / 2
        );

        transform.DOScale(flattenedScale, flattenDuration).SetEase(Ease.OutQuad);

        DOVirtual.DelayedCall(fadeDelay, () =>
        {
            if (rend != null && rend.material.HasProperty("_Color"))
            {
                rend.material.DOFade(0f, fadeDuration)
                    .OnComplete(() => gameObject.SetActive(false));
            }
            else
            {
                transform.DOScale(Vector3.zero, fadeDuration)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() => gameObject.SetActive(false));
            }
        });
    }
}