using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleCamera : MonoBehaviour
{
    public void OnBrokenStomach()
    {
        Debug.Log("Broken Stomach");
        StartCoroutine(Rumble(1, 2, 5));
    }

    IEnumerator Rumble (float duration, float magnitude, float frequency)
    {
        // Infinite loop is broken by StopAllCoroutines from StomachTarget On Node Fix
        while(true) {
            StartCoroutine(Shake(duration, magnitude));
            float elapsed = 0.0f;
            while (elapsed < frequency) {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 origPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = origPos;
    }
}
