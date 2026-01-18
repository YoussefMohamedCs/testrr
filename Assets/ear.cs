using UnityEngine;

public class ear : MonoBehaviour
{

    private Vector3 offset;
    private float zDistance;

    void OnMouseDown()
    {
        zDistance = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zDistance;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
