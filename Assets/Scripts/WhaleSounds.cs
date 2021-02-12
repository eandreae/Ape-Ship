using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSounds : MonoBehaviour
{
    public AudioSource[] whaleSounds;

    public int totalSounds = 4;

    public float minTime = 55;

    public float maxTime = 65;
    // Start is called before the first frame update
    void Start()
    {
        //Start off by playing some whale audio after the cooldown
        float cooldown = Random.Range(minTime, maxTime);
        Invoke("PlayWhaleAudio", cooldown);
    }

    void PlayWhaleAudio()
    {
        int whichAudio = Random.Range(0, totalSounds);

        if (whichAudio == 0)
        {
            whaleSounds[0].Play();
        }
        else if (whichAudio == 1)
        {
            whaleSounds[1].Play();
        }
        else if (whichAudio == 2)
        {
            whaleSounds[2].Play();
        }
        else if (whichAudio == 3)
        {
            whaleSounds[3].Play();
        }
        float cooldown = Random.Range(minTime, maxTime);
        Invoke("PlayWhaleAudio", cooldown);
    }
}
