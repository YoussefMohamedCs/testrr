using UnityEngine;

public class pressE : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    private readonly Collider[] colliderrs = new Collider[3];
    [SerializeField] private int numFound;


    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliderrs, interactableMask);
    }

}
