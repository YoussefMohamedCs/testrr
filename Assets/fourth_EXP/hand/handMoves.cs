using System.Collections;
using UnityEngine;

public class handMoves : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private bool isDragging = false;
    private float zDist;
    private Vector3 offset;

    public GameObject plan1;
    public GameObject plan2;
    public GameObject plan1child1;
    public GameObject plan1child2;
    public GameObject plan2child1;
    public GameObject plan2child2;
    bool EisPressed = false;
    bool QisPressed = false;
    bool doorIsopend;
    public GameObject hand;

    private GameObject pickedItem;
    private bool capIsPickUp = false;
    private bool cup1isHolded = false;

    private Vector3 MainoffsetCup = new Vector3(0.27f, 0.15f, 0.08f);
    private bool isRotating = false;


    private bool cup2isHolded = false;

    private GameObject[] childRotationList = null;


    public bool plan1insforn = false;
    public bool plan2insforn = false;

    public bool heaterbuttonStatus;


    public bool plan1IsBack = false;
    public bool plan2Isback = false;
    public bool expStatus = false;


    public Vector3 lastPosplan1child1;
    public Vector3 lastPsoplan1child2;
    public Vector3 lastposplan2child1;
    public Vector3 lastposplan2child2;
    public bool isRotated = false;

    private Vector3 startPsoOsplan1;
    private Quaternion startRotataeOfplan1;

    private Vector3 startPsoOsplan2;
    private Quaternion startRotataeOfplan2;





    private Vector3 rotateAbs = new Vector3(0.020f, 0.002f, 0.0233f);

    public float testx = -0.06f;
    public float testy = 0.19f;
    public float testz = 0.14f;



    public GameObject cap1;
    public GameObject cap2;
    public GameObject cap3;
    public GameObject cap4;
    public GameObject cap5;
    public GameObject cap6;
    public GameObject cap7;
    public GameObject cap8;

    void Start()
    {
        startPsoOsplan1 = plan1.transform.position;
        startRotataeOfplan1 = plan1.transform.rotation;

        startPsoOsplan2 = plan2.transform.position;
        startRotataeOfplan2 = plan2.transform.rotation;

        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        zDist = Vector3.Distance(cam.transform.position, transform.position);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDist;
        offset = transform.position - cam.ScreenToWorldPoint(mousePos);
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = zDist;
        Vector3 target = cam.ScreenToWorldPoint(mousePos) + offset;

        // Only X/Y drag
        target.z = transform.position.z;

        if (rb != null)
        {
            Vector3 direction = target - transform.position;
            rb.linearVelocity = direction * Time.deltaTime * 2000f;
        }
        else
        {
            transform.position = target;
        }
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        //if (rb != null)
        //rb.useGravity = true;
    }


    void OnCollisionEnter(Collision collision)
    {
        // Pick plan1
        if (collision.gameObject.CompareTag("Finish") || collision.gameObject.CompareTag("targetPlace") && !cup2isHolded && expStatus == false)
        {
            childRotationList = new GameObject[] { plan1child1, plan1child2 };

            PickCup(plan1, plan1child1, plan1child2);
        }
        // Pick plan2 if cup1 not held
        else if (collision.gameObject.CompareTag("Finish2") || collision.gameObject.CompareTag("targetPlace2") && !cup1isHolded && expStatus == false)
        {
            childRotationList = new GameObject[] { plan2child1, plan2child2 };

            PickCup(plan2, plan2child1, plan2child2);
        }
        else if (collision.gameObject.CompareTag("Base") && expStatus == true && cup1isHolded)
        {
            plan1.transform.position = new Vector3(-86.37f, 2.64f, -145.502f);
            plan1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            plan1child1.transform.position = startPsoOsplan1;
            plan1child2.transform.position = startPsoOsplan1;
            plan1child1.transform.rotation = Quaternion.Euler(0f, -90f, 25.589f);
            plan1child2.transform.rotation = Quaternion.Euler(0f, -90f, 25.589f);
            plan1IsBack = true;
            capIsPickUp = false;
            cup1isHolded = false;
        }
        else if (collision.gameObject.CompareTag("Base") && expStatus == true && cup2isHolded)
        {
            Debug.Log("expected back!!");

            plan2.transform.position = new Vector3(-86.644f, 2.64f, -145.502f);
            plan1.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            plan2child1.transform.position = startPsoOsplan2;
            plan2child2.transform.position = startPsoOsplan2;
            plan2child1.transform.rotation = Quaternion.Euler(0f, -90f, 25.589f);
            plan2child2.transform.rotation = Quaternion.Euler(0f, -90f, 25.589f);
            plan2Isback = true;
            capIsPickUp = false;
            cup2isHolded = false;
        }
    }



    void PickCup(GameObject cup, GameObject child1, GameObject child2)
    {
        // Disable colliders

        foreach (Collider col in cup.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }

        // Reset rotations
        cup.transform.rotation = Quaternion.identity;
        child1.transform.rotation = Quaternion.identity;
        child2.transform.rotation = Quaternion.identity;

        pickedItem = cup;
        capIsPickUp = true;
        if (cup == plan1) cup1isHolded = true;
        if (cup == plan2) cup2isHolded = true;




        // Start smooth follow
        StartCoroutine(MoveCupSmooth(cup, MainoffsetCup));

        // Store children for rotation



    }




    IEnumerator MoveCupSmooth(GameObject cup, Vector3 offset)
    {

        while (capIsPickUp && cup != null)
        {
            if (cup == plan1)
            {
                cup1isHolded = true;
                cup2isHolded = false;
            }
            else if (cup == plan2)
            {
                cup1isHolded = false;
                cup2isHolded = true;
            }


            if (expStatus == false || (expStatus == true && QisPressed == true))
            {
                Vector3 targetPos = hand.transform.position + hand.transform.rotation * offset;

                if (expStatus == true && QisPressed == true)
                {
                    cup.transform.position = Vector3.Lerp(cup.transform.position, transform.position + new Vector3(-0.05f, 0.17f, 0.2f), Time.deltaTime * 100f);

                }
                else
                {
                    cup.transform.position = Vector3.Lerp(cup.transform.position, transform.position + new Vector3(-0.27f, 0.15f, -0.08f), Time.deltaTime * 100f);

                }

            }
            else if (expStatus == true && EisPressed)
            {
                cup.transform.position = Vector3.Lerp(cup.transform.position, hand.transform.position + new Vector3(-0.06f, 0.19f, 0.14f), Time.deltaTime * 100f);

            }







            // Rotate children Z only
            if (childRotationList != null)
            {
                foreach (GameObject child in childRotationList)
                {
                    //child.transform.position = Vector3.Lerp(new Vector3(child.transform.position.x , child.transform.position.y , child.transform.position.z), new Vector3(child.transform.position.x, child.transform.position.y, child.transform.position.z +0.8f) , Time.deltaTime * 10f);
                }
            }

            yield return null;
        }
    }


    IEnumerator processEXP()
    {
        expStatus = true;
        yield return new WaitForSeconds(4f);


    }
    void Update()
    {


        doorIsopend = cylinder.isOpen;
        heaterbuttonStatus = headterButton.buttonIsOpen;

        if (heaterbuttonStatus && plan1insforn && plan2insforn)
        {
            StartCoroutine(processEXP());
            Debug.Log("exp done!");
        }


        if (Input.GetKeyDown(KeyCode.E) && !isRotating && !EisPressed)
        {
            isRotated = true;
            EisPressed = true;
            QisPressed = false;
            StartCoroutine(RotateHand(90f));
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !isRotating && !QisPressed)
        {
            EisPressed = false;
            QisPressed = true;
            StartCoroutine(RotateHand(-90f));
        }

        if (hand.transform.position.x >= -84.241f)
        {
            if (doorIsopend && capIsPickUp && EisPressed && !isRotating)
            {
                capIsPickUp = false;
                if (pickedItem == plan1)
                    plan1insforn = true;

                if (pickedItem == plan2)
                    plan2insforn = true;

                if (pickedItem == plan1)
                {
                    plan1.transform.position = new Vector3(-84.215f, 2.763f, -145.139f);
                    plan1child1.transform.position = new Vector3(-84.215f, 2.763f, -145.139f);
                    plan1child2.transform.position = new Vector3(-84.215f, 2.763f, -145.139f);

                }

                if (pickedItem == plan2)
                {
                    plan2.transform.position = new Vector3(-84.215f, 2.92f, -145.139f);
                    plan2child1.transform.position = new Vector3(-84.215f, 2.92f, -145.139f);
                    plan2child2.transform.position = new Vector3(-84.215f, 2.92f, -145.139f);
                }

                if (pickedItem == plan1) cup1isHolded = false;
                if (pickedItem == plan2) cup2isHolded = false;
            }
        }



        if (hand.transform.position.x >= -84.241f)
        {
            if (expStatus && plan1IsBack == false)
            {
                PickCup(plan1, plan1child1, plan1child2);
            }


            if (expStatus && plan1IsBack && plan1IsBack == true)
            {
                PickCup(plan2, plan2child1, plan2child2);
            }
        }
    }

    IEnumerator RotateHand(float rotateAngle)
    {
        isRotating = true;
        float duration = 1f; // smoother, faster rotation
        float elapsed = 0f;

        Quaternion startRotation = hand.transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, rotateAngle);


        if (rotateAngle == -90)
        {
            while (elapsed < duration)
            {
                float t = elapsed / duration;
                hand.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                // Update children Z rotation while rotating

                //child.transform.rotation = Quaternion.Euler(0f, 0f, hand.transform.eulerAngles.z);
                if (cup1isHolded)
                {
                    plan1child1.transform.position = Vector3.Lerp(new Vector3(plan1child1.transform.position.x, plan1child1.transform.position.y, plan1child1.transform.position.z), new Vector3(plan1child1.transform.position.x - rotateAbs.x, plan1child1.transform.position.y - rotateAbs.y, plan1child1.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                    plan1child2.transform.position = Vector3.Lerp(new Vector3(plan1child2.transform.position.x, plan1child2.transform.position.y, plan1child2.transform.position.z), new Vector3(plan1child2.transform.position.x - rotateAbs.x, plan1child2.transform.position.y - rotateAbs.y, plan1child2.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                    lastPsoplan1child2 = new Vector3(plan1child2.transform.position.x - 0.020f, plan1child2.transform.position.y - 0.002f, plan1child2.transform.position.z - 0.0233f);

                }
                else if (cup2isHolded)
                {
                    plan2child1.transform.position = Vector3.Lerp(new Vector3(plan2child1.transform.position.x, plan2child1.transform.position.y, plan2child1.transform.position.z), new Vector3(plan2child1.transform.position.x - rotateAbs.x, plan2child1.transform.position.y - rotateAbs.y, plan2child1.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                    lastposplan2child1 = new Vector3(plan2child1.transform.position.x - 0.020f, plan2child1.transform.position.y - 0.002f, plan2child1.transform.position.z - 0.0233f);
                    plan2child2.transform.position = Vector3.Lerp(new Vector3(plan2child2.transform.position.x, plan2child2.transform.position.y, plan2child2.transform.position.z), new Vector3(plan2child2.transform.position.x - rotateAbs.x, plan2child2.transform.position.y - rotateAbs.y, plan2child2.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                    lastposplan2child2 = new Vector3(plan2child2.transform.position.x - 0.020f, plan2child2.transform.position.y - 0.002f, plan2child2.transform.position.z - 0.0233f);
                }



                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap final rotation
            hand.transform.rotation = targetRotation;

            //child.transform.rotation = Quaternion.Euler(0f, 0f, hand.transform.eulerAngles.z);

            if (cup1isHolded)
            {
                plan1child1.transform.position = Vector3.Lerp(new Vector3(plan1child1.transform.position.x, plan1child1.transform.position.y, plan1child1.transform.position.z), new Vector3(plan1child1.transform.position.x - rotateAbs.x, plan1child1.transform.position.y - rotateAbs.y, plan1child1.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                plan1child2.transform.position = Vector3.Lerp(new Vector3(plan1child2.transform.position.x, plan1child2.transform.position.y, plan1child2.transform.position.z), new Vector3(plan1child2.transform.position.x - rotateAbs.x, plan1child2.transform.position.y - rotateAbs.y, plan1child2.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);


            }
            else if (cup2isHolded)
            {
                plan2child1.transform.position = Vector3.Lerp(new Vector3(plan2child1.transform.position.x, plan2child1.transform.position.y, plan2child1.transform.position.z), new Vector3(plan2child1.transform.position.x - rotateAbs.x, plan2child1.transform.position.y - rotateAbs.y, plan2child1.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
                plan2child2.transform.position = Vector3.Lerp(new Vector3(plan2child2.transform.position.x, plan2child2.transform.position.y, plan2child2.transform.position.z), new Vector3(plan2child2.transform.position.x - rotateAbs.x, plan2child2.transform.position.y - rotateAbs.y, plan2child2.transform.position.z - rotateAbs.z), Time.deltaTime * 10f);
            }

            isRotating = false;
        }
        else if (rotateAngle == 90)
        {

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                hand.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                // Update children Z rotation while rotating

                //child.transform.rotation = Quaternion.Euler(0f, 0f, hand.transform.eulerAngles.z);
                if (cup1isHolded)
                {
                    plan1child1.transform.position = Vector3.Lerp(new Vector3(plan1child1.transform.position.x, plan1child1.transform.position.y, plan1child1.transform.position.z), new Vector3(plan1child1.transform.position.x + rotateAbs.x, plan1child1.transform.position.y + rotateAbs.y, plan1child1.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
                    lastPosplan1child1 = plan1child1.transform.position;
                    plan1child2.transform.position = Vector3.Lerp(new Vector3(plan1child2.transform.position.x, plan1child2.transform.position.y, plan1child2.transform.position.z), new Vector3(plan1child2.transform.position.x + rotateAbs.x, plan1child2.transform.position.y + rotateAbs.y, plan1child2.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);


                }
                else if (cup2isHolded)
                {
                    plan2child1.transform.position = Vector3.Lerp(new Vector3(plan2child1.transform.position.x, plan2child1.transform.position.y, plan2child1.transform.position.z), new Vector3(plan2child1.transform.position.x + rotateAbs.x, plan2child1.transform.position.y + rotateAbs.y, plan2child1.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
                    plan2child2.transform.position = Vector3.Lerp(new Vector3(plan2child2.transform.position.x, plan2child2.transform.position.y, plan2child2.transform.position.z), new Vector3(plan2child2.transform.position.x + rotateAbs.x, plan2child2.transform.position.y + rotateAbs.y, plan2child2.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
                }




                elapsed += Time.deltaTime;
                yield return null;
            }

            // Snap final rotation
            hand.transform.rotation = targetRotation;

            //child.transform.rotation = Quaternion.Euler(0f, 0f, hand.transform.eulerAngles.z);

            if (cup1isHolded)
            {
                plan1child1.transform.position = Vector3.Lerp(new Vector3(plan1child1.transform.position.x, plan1child1.transform.position.y, plan1child1.transform.position.z), new Vector3(plan1child1.transform.position.x + rotateAbs.x, plan1child1.transform.position.y + rotateAbs.y, plan1child1.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
                plan1child2.transform.position = Vector3.Lerp(new Vector3(plan1child2.transform.position.x, plan1child2.transform.position.y, plan1child2.transform.position.z), new Vector3(plan1child2.transform.position.x + rotateAbs.x, plan1child2.transform.position.y + rotateAbs.y, plan1child2.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);


            }
            else if (cup2isHolded)
            {
                plan2child1.transform.position = Vector3.Lerp(new Vector3(plan2child1.transform.position.x, plan2child1.transform.position.y, plan2child1.transform.position.z), new Vector3(plan2child1.transform.position.x + rotateAbs.x, plan2child1.transform.position.y + rotateAbs.y, plan2child1.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
                plan2child2.transform.position = Vector3.Lerp(new Vector3(plan2child2.transform.position.x, plan2child2.transform.position.y, plan2child2.transform.position.z), new Vector3(plan2child2.transform.position.x + rotateAbs.x, plan2child2.transform.position.y + rotateAbs.y, plan2child2.transform.position.z + rotateAbs.z), Time.deltaTime * 10f);
            }





            isRotating = false;
        }
    }
}