using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECPlaySoundEffectOnStart : MonoBehaviour
{

    [SerializeField] private AudioClip sfxClip;

    void Start()
    {
        AAudioSetup.RequestSFXPlay(sfxClip);
    }
}
