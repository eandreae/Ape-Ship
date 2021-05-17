using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatFloatEvent : UnityEvent<float, float> {}

public class StomachRumble : MonoBehaviour
{
    public float duration;
    public float magnitude;
    public float frequency;
    public FloatFloatEvent OnStomachRumble;
    public void OnBrokenStomach()
    {
        StartCoroutine(Rumble(duration, magnitude, frequency));
    }

    IEnumerator Rumble (float dur, float mag, float freq)
    {
        // Infinite loop is broken by StopAllCoroutines from StomachTarget On Node Fix
        while(true) {
            OnStomachRumble.Invoke(dur, mag);
            float elapsed = 0.0f;
            while (elapsed < freq) {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
