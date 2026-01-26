using DG.Tweening;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class vibration : MonoBehaviour
{
    [Header("Vibration Settings")]
    public float amplitude = 0.05f;
    public float frequency = 40f;
    public static bool exp_is_end = false;
    public GameObject quizSystem;
    private Vector3 startLocalPos;
    private Vector3 lastWorldPos;
    private Quaternion startLocalRot; // Use Quaternion for rotation
    public Material beakerLiquid;
    private Coroutine vibrationRoutine;
    private Coroutine beakerRoutine;
    public GameObject beaker;
    private Color maincolor;
    private Coroutine Timer_cort;
    bool beakerIsOn = false;
   
    bool allPointsDone_status;

    void Start()
    {
        startLocalPos = transform.localPosition;
        lastWorldPos = transform.position;
        startLocalRot = transform.localRotation;
        maincolor = new Color(1.0f, 0.82f, 0.76f, 1f);
        beakerLiquid.SetColor("_SideColor", maincolor);
    }


   
    public void StartVibration()
    {
        if (vibrationRoutine == null)
        {
            vibrationRoutine = StartCoroutine(VibrateX());
            //beakerRoutine = StartCoroutine(VibrateBeaker());
            StartCoroutine(ExecuteAfterTime(5f));
            Rigidbody rbofBeaker = beaker.GetComponent<Rigidbody>();
            rbofBeaker.constraints = RigidbodyConstraints.None;


        }
            
    }




    public void StopVibration()
    {
        if (vibrationRoutine != null)
        {
            StopCoroutine(vibrationRoutine);
            //StopCoroutine(beakerRoutine);
            StartCoroutine(ExecuteAfterTime(5f));
            vibrationRoutine = null;
            beakerRoutine = null;
            transform.localPosition = startLocalPos;
            Rigidbody rbofBeaker = beaker.GetComponent<Rigidbody>();
            rbofBeaker.constraints = RigidbodyConstraints.FreezeRotation;
            //beaker.transform.DOMove(startLocalPos, 0.2f).SetEase(Ease.OutQuad);
        }
    }

    IEnumerator VibrateX()
    {
        while (true)
        {
            float time = Time.time * frequency;

            // Movement in Z axis (from your original code)
            float z = Mathf.Sin(time) * amplitude;

            // Rotation on X-axis (front/back edges tilt)
            float tiltX = Mathf.Sin(time) * amplitude * 50f;

            // Rotation on Z-axis with delay (left/right edges tilt - happens after X)
            float tiltZ = Mathf.Sin(time + Mathf.PI * 0.5f) * amplitude * 50f;

            // Apply position (keep your original Z movement)
            transform.localPosition = new Vector3(
                startLocalPos.x,
                startLocalPos.y,
                startLocalPos.z 
            );

            // Apply rotation (X first, then Z with delay)
            transform.localRotation = Quaternion.Euler(tiltX*3f, 0, tiltZ*3f);

            yield return null;
        }
    }


    IEnumerator ExecuteAfterTime(float time)
    {
        // This line tells Unity to wait for 5 seconds
        yield return new WaitForSeconds(time);


        if (allPointsDone_status)
        {
            StartCoroutine(ChangeColorGradually());

        }


    }

    IEnumerator VibrateBeaker()
    {
        while (true)
        {
            float time = Time.time * frequency;

            // Movement in Z axis (from your original code)
            float z = Mathf.Sin(time) * amplitude;

            // Rotation on X-axis (front/back edges tilt)
            float tiltX = Mathf.Sin(time) * amplitude * 50f;

            // Rotation on Z-axis with delay (left/right edges tilt - happens after X)
            float tiltZ = Mathf.Sin(time + Mathf.PI * 0.5f) * amplitude * 50f;

            // Apply position (keep your original Z movement)
            beaker.transform.localPosition = new Vector3(
                beaker.transform.position.x,
                 beaker.transform.position.y  ,
                  beaker.transform.localPosition.z 
            );

            // Apply rotation (X first, then Z with delay)
            beaker.transform.localRotation = Quaternion.Euler(tiltX * 1.2f, 0, tiltZ * 1.2f);

            yield return null;
        }
    }




    //IEnumerator VibrateBeaker()
    //{
    //    while (true)
    //    {
    //        float x = Mathf.Sin(Time.time * frequency) * amplitude;
    //        float z = Mathf.Sin(Time.time * frequency) * amplitude;


    //        beaker.transform.localPosition = new Vector3(
    //            beaker.transform.position.x ,
    //            beaker.transform.position.y  ,
    //            beaker.transform.localPosition.z + z
    //        );

    //        yield return null;
    //    }
    //}

    IEnumerator ChangeColorGradually()
    {
        Color startColor = beakerLiquid.GetColor("_SideColor");
        Color endColor = new Color(1f, 0.41f, 0.7f, 1f); // white with alpha 0

        float duration = 2f; // time of transition
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            Color newColor = Color.Lerp(startColor, endColor, t / duration);
            beakerLiquid.SetColor("_SideColor", newColor);

            yield return null;
        }
           exp_is_end = true;
        StopCoroutine(vibrationRoutine);
        StopCoroutine(beakerRoutine);
        button.buttonIsOpen = false;


    }

void FixedUpdate()
    {
        lastWorldPos = transform.position;
    }

    // 🔥 MAKE BEAKER MOVE WITH VIBRATION
    void OnCollisionStay(Collision collision)
    {
        beakerIsOn = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        beakerIsOn = false;

      
    }

    void Update()
    {
        bool buttonStatus = button.buttonIsOpen;
        allPointsDone_status = points.allPointsDone;

        if (buttonStatus && beakerIsOn)
        {
            StartVibration();
    
        }
        else
        {
            StopVibration();
        

        }
        if (exp_is_end)
        {
            quizSystem.SetActive(true);
        }

    }




 


}
