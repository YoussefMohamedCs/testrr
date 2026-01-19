using UnityEngine;

public class water_change_color : MonoBehaviour
{
    [Header("References")]
    public Renderer waterRenderer;

    [Header("Color Change Settings")]
    public Color targetTopColor = Color.red;
    public Color targetSideColor = Color.red;
    public float colorChangeSpeed = 1f;

    [Header("Wave Settings (Shader Controlled)")]
    public float baseWaveHeight = 0.02f;
    public float baseWaveSpeed = 1f;

    public float touchWaveHeight = 0.15f;
    public float touchWaveSpeed = 2f;
    public float rippleLerpSpeed = 2f;

    [Header("Trigger Settings")]
    public string triggerTag = "Capsule";

    private Material mat;

    private Color currentTopColor;
    private Color currentSideColor;

    private float waveHeight;
    private float waveSpeed;

    private bool touching = false;

    private void Start()
    {
        if (waterRenderer == null)
            waterRenderer = GetComponent<Renderer>();

        mat = waterRenderer.material;

        currentTopColor = mat.GetColor("_TopColor");
        currentSideColor = mat.GetColor("_SideColor");

        waveHeight = baseWaveHeight;
        waveSpeed = baseWaveSpeed;
    }

    private void Update()
    {
        // -------- COLOR CHANGE --------
        if (touching)
        {
            currentTopColor = Color.Lerp(currentTopColor, targetTopColor, Time.deltaTime * colorChangeSpeed);
            currentSideColor = Color.Lerp(currentSideColor, targetSideColor, Time.deltaTime * colorChangeSpeed);
        }

        mat.SetColor("_TopColor", currentTopColor);
        mat.SetColor("_SideColor", currentSideColor);

        // -------- SEND MOVEMENT TO SHADER --------
        float targetHeight = touching ? touchWaveHeight : baseWaveHeight;
        float targetSpeed = touching ? touchWaveSpeed : baseWaveSpeed;

        waveHeight = Mathf.Lerp(waveHeight, targetHeight, Time.deltaTime * rippleLerpSpeed);
        waveSpeed = Mathf.Lerp(waveSpeed, targetSpeed, Time.deltaTime * rippleLerpSpeed);

        mat.SetFloat("_WaveHeight", waveHeight);
        mat.SetFloat("_WaveSpeed", waveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
            touching = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
            touching = false;
    }
}
