using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AAudioSetup : MonoBehaviour
{
    private static AAudioSetup _instance;

    [SerializeField] private int sfxAmount;
    [SerializeField] private float initialVolume;
    [SerializeField] private float sfxVolume;


    private AudioSource bgMusicSource;
    private AudioSource bgMusicSwitchSource;

    private int sfxIndex = 0;
    private AudioSource[] sfxAudioSourceContainer;

    private Coroutine bgMusicRoutine;

    private void Awake()
    {
        if (_instance != null) 
        { 
            Destroy(gameObject);
            return;
        }
        _instance = this;

        bgMusicSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        bgMusicSource.volume = initialVolume;
        bgMusicSource.loop = true;
        bgMusicSwitchSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));

        bgMusicSwitchSource.playOnAwake = false;


        sfxAudioSourceContainer = new AudioSource[sfxAmount];
        for(int i = 0; i < sfxAmount; i++)
        {
            sfxAudioSourceContainer[i] = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
            sfxAudioSourceContainer[i].playOnAwake = false;
            sfxAudioSourceContainer[i].volume = sfxVolume;
        }
    }

    private void PlaySFX(AudioClip clipToPlay)
    {
        sfxIndex = ++sfxIndex % sfxAmount;
        sfxAudioSourceContainer[sfxIndex].Stop();
        sfxAudioSourceContainer[sfxIndex].clip = clipToPlay;
        sfxAudioSourceContainer[sfxIndex].Play();
    }

    private void SwitchBGMusic(AudioClip nextMusic)
    {
        bgMusicSwitchSource.clip = bgMusicSource.clip;
        bgMusicSwitchSource.volume = bgMusicSource.volume;
        bgMusicSource.clip = nextMusic;

        bgMusicSource.Stop();
        bgMusicSwitchSource.Play();
        if(bgMusicRoutine != null) { StopCoroutine(bgMusicRoutine); }
        bgMusicRoutine = StartCoroutine(BGSwitcher());
    }

    IEnumerator BGSwitcher()
    {
        float volume = bgMusicSource.volume;
        float steps = 50.0f;
        for(float step = steps; step > 0; step--)
        {
            bgMusicSwitchSource.volume = volume * step / steps;
            yield return new WaitForSeconds(0.03f);
        }
        bgMusicSwitchSource.Stop();

        bgMusicSource.volume = 0;
        bgMusicSource.Play();
        for (float step = 1; step < 50; step++)
        {
            bgMusicSource.volume = initialVolume * step / steps;
            yield return new WaitForSeconds(0.03f);
        }
        bgMusicSource.volume = initialVolume;
        yield return null;

    }

    public static void RequestSFXPlay(AudioClip clip)
    {
        _instance.PlaySFX(clip);
    }

    public static void RequestBGMusic(AudioClip music)
    {
        _instance.SwitchBGMusic(music);
    }


}
