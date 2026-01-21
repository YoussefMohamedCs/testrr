using UnityEngine;

public class waterdrops : MonoBehaviour
{
    public ParticleSystem water;

    void OnMouseDown()
    {
        water.Play();
    }

    void OnMouseUp()
    {
        water.Stop();
    }
}

