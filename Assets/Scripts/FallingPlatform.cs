using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] float timeBeforeFall;
    [SerializeField] GameObject platform;
    Vector3 startPos;
    private void Start()
    {
        startPos.x = transform.position.x;
        startPos.y = transform.position.y;
        startPos.z = transform.position.z;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red" || other.tag == "Blue")
        {
            StartCoroutine(Shake(timeBeforeFall, 0.1f));
        }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = platform.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            platform.transform.localPosition = new Vector3(x + originalPos.x, originalPos.y, z + originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        platform.transform.localPosition = originalPos;
        platform.SetActive(false);
    }
}
