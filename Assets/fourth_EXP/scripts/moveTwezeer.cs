using UnityEngine;
using System.Collections;
public class move : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private bool isDragging = false;
    private float zDist;
    private Vector3 offset;
    public GameObject cap1;
    public GameObject cap2;
    public GameObject cap3;
    public GameObject cap4;
    public GameObject cap5;
    public GameObject cap6;
    public GameObject cap7;
    public GameObject cap8;
    int cap1counter = 0;
    int cap2counter = 2;
    int cap3counter = 4;
    int cap4counter = 6;
    bool capIsPickUp = false;
    bool capsolaOneIsPicked = false;
    bool capsolaTwoIsPicked = false;
    bool capsolaThreeIsPicked = false;
    bool capsolaFourIsPicked = false;
    private GameObject pickedItem;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        rb.isKinematic = false;
        rb.useGravity = false;
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

        //rb.linearVelocity = direction * followStrength * Time.deltaTime;
        rb.linearVelocity = direction * Time.deltaTime * 4000f;
        //rb.linearVelocity = Vector3.ClampMagnitude(direction * 60f, 100f);
        //rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * 60f, 0.5f);
        //rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, direction * 700f, 0.12f);
        //rb.linearVelocity = Vector3.ProjectOnPlane(direction, rb.linearVelocity.normalized) * Time.deltaTime * 700f;


    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject == cap1 || collision.gameObject == cap2 || collision.gameObject == cap3 || collision.gameObject == cap4 || collision.gameObject == cap5 || collision.gameObject == cap6 || collision.gameObject == cap7 || collision.gameObject == cap8)
        {
            pickedItem = collision.gameObject;
            collision.gameObject.GetComponent<Collider>().enabled = false;
        }
        else
        {
            collision.gameObject.GetComponent<Collider>().enabled = true;

        }


        if (collision.gameObject.CompareTag("Paper1"))
        {
            capsolaOneIsPicked = true;
            capsolaTwoIsPicked = false;
            capsolaFourIsPicked = false;
            capsolaThreeIsPicked = false;

            if (cap1counter == 0)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap1));
            }
            else if (cap1counter == 1)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap2));
            }

        }


        if (collision.gameObject.CompareTag("Paper2"))
        {
            capsolaOneIsPicked = false;
            capsolaTwoIsPicked = true;
            capsolaFourIsPicked = false;
            capsolaThreeIsPicked = false;
            if (cap2counter == 2)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap3));
            }
            else if (cap2counter == 3)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap4));
            }
        }

        if (collision.gameObject.CompareTag("Paper3"))
        {
            capsolaOneIsPicked = false;
            capsolaTwoIsPicked = false;
            capsolaFourIsPicked = false;
            capsolaThreeIsPicked = true;
            if (cap3counter == 4)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap5));
            }
            else if (cap3counter == 5)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap6));
            }
        }

        if (collision.gameObject.CompareTag("Paper4"))
        {
            capsolaTwoIsPicked = false;
            capsolaOneIsPicked = false;
            capsolaThreeIsPicked = false;
            capsolaFourIsPicked = true;
            if (cap4counter == 6)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap7));
            }
            else if (cap4counter == 7)
            {
                capIsPickUp = true;
                StartCoroutine(moveCap(cap8));
            }
        }


        if (collision.gameObject.CompareTag("targetPlace") && (cap1counter == 0 || cap2counter == 2 || cap3counter ==  4 || cap4counter == 6))
        {
            pickedItem.GetComponent<Collider>().enabled = true;

            if (cap1counter == 0 && capsolaOneIsPicked)
            {
                cap1.transform.position = new Vector3(-86.2983f, 2.6709f, -145.4648f);
                cap1.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap1counter = 1;

            }
            else if (cap2counter == 2 && capsolaTwoIsPicked)
            {
                cap3.transform.position = new Vector3(-86.4266f, 2.6636f, -145.4804f);
                cap3.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap2counter = 3;

            }else if(cap3counter == 4 && capsolaThreeIsPicked)
            {
                cap5.transform.position = new Vector3(-86.3047f, 2.6219f, -145.573f);
                cap5.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap3counter = 5;
            }
            else if (cap4counter == 6 && capsolaFourIsPicked)
            {
                cap7.transform.position = new Vector3(-86.4084f, 2.6229f, -145.5856f);
                cap7.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap4counter = 7;
            }
            capIsPickUp = false;
        }

        if (collision.gameObject.CompareTag("targetPlace2") && (cap1counter == 1 || cap2counter == 3 || cap3counter ==5 || cap4counter == 7))
        {
            pickedItem.GetComponent<Collider>().enabled = true;

            if (cap1counter == 1 && capsolaOneIsPicked)
            {
                cap2.transform.position = new Vector3(-86.5986f, 2.6628f, -145.4899f);
                cap2.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap1counter = 99;
            }
            else if (cap2counter == 3 && capsolaTwoIsPicked)
            {
                cap4.transform.position = new Vector3(-86.717f, 2.655f, -145.504f);
                cap4.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap2counter = 99;
            }else if (cap3counter == 5 && capsolaThreeIsPicked)
            {
                cap6.transform.position = new Vector3(-86.5882f, 2.6171f, -145.582f);
                cap6.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap3counter = 99;
            }
            else if (cap4counter == 7 && capsolaFourIsPicked)
            {
                cap8.transform.position = new Vector3(-86.6803f, 2.6101f, -145.5938f);
                cap8.transform.rotation = Quaternion.Euler(-32.14f, -5.74f, 5.3f);
                cap4counter = 99;
            }
            capIsPickUp = false;

        }
    }


    


    IEnumerator moveCap(GameObject cap)
    {
        float elapsed = 0f;


        while (capIsPickUp)
        {
            cap.transform.position = new Vector3(transform.position.x ,  transform.position.y - 0.10f , transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

}
