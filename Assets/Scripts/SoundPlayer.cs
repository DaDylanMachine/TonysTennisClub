using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    public AudioSource audioData;

    // Update is called once per frame
    public void soundPlay()
    {
        audioData.Play(0);
    }
}
