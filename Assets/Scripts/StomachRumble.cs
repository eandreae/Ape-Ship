using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatFloatEvent : UnityEvent<float, float> {}

public class StomachRumble : MonoBehaviour
{
    public FloatFloatEvent OnStomachRumble;
    
    private IEnumerator rumbleCoroutine;

    public AudioSource angryWhaleSFX;

    public void OnBrokenStomach()
    {
        if(rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }

        rumbleCoroutine = Rumble(2f, 0.5f, 15f); 
        StartCoroutine(rumbleCoroutine);
    }
    public void OnDamagedStomach()
    {
        if(rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }
        rumbleCoroutine = Rumble(2.5f, 0.05f, 20f);
        StartCoroutine(rumbleCoroutine);
    }
    public void OnFixedStomach()
    {
        if(rumbleCoroutine != null)
        {
            StopCoroutine(rumbleCoroutine);
        }
        angryWhaleSFX.Stop();
    }

    IEnumerator Rumble (float dur, float mag, float freq)
    {
        angryWhaleSFX.Play();
        // Infinite loop is broken by StopAllCoroutines from StomachTarget On Node Fix
        while (true) {
            OnStomachRumble.Invoke(dur, mag);
            float elapsed = 0.0f;
            while (elapsed < freq) {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
