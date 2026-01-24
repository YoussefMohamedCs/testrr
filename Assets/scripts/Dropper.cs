using UnityEngine;
using System.Collections;

public class Dropper : MonoBehaviour
{
    public GameObject waterDropPrefab;
    public Transform dropPoint;
    public float dropRate = 0.5f;

    private bool isDropping = false;
    public KeyCode dropKey = KeyCode.Space; // «Œ «—Ì «·“— «··Ì  Õ»Ì

    void Update()
    {
        // ·„«  œÊ”Ì ⁄·Ï «·“— «·„Õœœ
        if (Input.GetKeyDown(dropKey) && !isDropping)
        {
            StartCoroutine(DropWater());
        }
        // ·Ê ⁄«Ì“…  Êﬁ› «·≈”ﬁ«ÿ »«·÷€ÿ ⁄·Ï ‰›” «·“—
        else if (Input.GetKeyDown(dropKey) && isDropping)
        {
            isDropping = false;
        }
    }

    IEnumerator DropWater()
    {
        isDropping = true;

        while (isDropping)
        {
            Instantiate(waterDropPrefab, dropPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(dropRate);
        }
    }
}

