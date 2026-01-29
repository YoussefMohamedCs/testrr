using UnityEngine;

public class cylinder : MonoBehaviour
{

    public float openAngle = 90f;   // “«ÊÌ… «·› Õ
    public float speed = 2f;         // ”—⁄… «·› Õ

    public static bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y - openAngle, // › Õ „‰ «·Ì„Ì‰ ··‘„«·
            transform.eulerAngles.z
        );
    }

    void OnMouseDown()
    {
        isOpen = !isOpen;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                openRotation,
                Time.deltaTime * speed
            );
        }
        else
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                closedRotation,
                Time.deltaTime * speed
            );
        }
    }
}


