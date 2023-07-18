using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBGMusic : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;

    private void Start()
    {
        AAudioSetup.RequestBGMusic(musicClip);
    }
}
