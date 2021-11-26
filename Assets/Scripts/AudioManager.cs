using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSoundClips
{
    Attack,
    Win,
    Gameover,
    Grunt
}

public enum EnviromentSoundClip
{
    LevelMusic,
    MenuMusic
}

public enum TrapSoundsClip
{
    FireRise,
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource trapAudioSource;
    [SerializeField, Range(0, 1)] private float musicInitialVolumen;

    [Header("Background Music")]
    [SerializeField] private AudioClip levelMusic;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip swordattack;
    [SerializeField] private AudioClip grunt;

    [Header("Trap Sounds")]
    [SerializeField] private AudioClip fireRise;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void EnviromentMusic(EnviromentSoundClip soundClip)
    {
  
        switch (soundClip)
        {
            case EnviromentSoundClip.LevelMusic:
                musicAudioSource.clip = levelMusic;
                break;
        }
        musicAudioSource.volume = musicInitialVolumen;
        musicAudioSource.Play();
    }

    public void PlayPlayerSound(PlayerSoundClips soundClip)
    {
        playerAudioSource.volume = 1f;
        switch (soundClip)
        {
            case PlayerSoundClips.Attack:
                playerAudioSource.PlayOneShot(swordattack);
                break;
            case PlayerSoundClips.Grunt:
                playerAudioSource.PlayOneShot(grunt);
                break;
        }
    }
    // public void PlayTrapSound(TrapSoundsClip soundClip)
    // {
    //     trapAudioSource.volume = 0.000001f;
    //     switch (soundClip)
    //     {
    //         case TrapSoundsClip.FireRise:
    //             playerAudioSource.PlayOneShot(fireRise);
    //             break;
    //     }
    // }

    }
