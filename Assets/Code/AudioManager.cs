using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM ====")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;
    int bgmIndex;

    [Header("#SFX ====")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayer;
    int channelIndex;

    public enum Bgm
    {
        MainTheme0
    }

    public enum Sfx
    {
        jump, gameover, falling, falling2, dead, dog, countdown, coin
    }

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;

        // ?ö®Í≥ºÏùå Ï¥àÍ∏∞?ôî
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];

        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            sfxPlayer[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayer[index].playOnAwake = false;
            sfxPlayer[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        int index = (int)bgm;
        if (index < 0 || index >= bgmClips.Length) return;
        bgmPlayer.clip = bgmClips[index];
        bgmPlayer.Play();
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayer.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayer.Length;

            if (sfxPlayer[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayer[loopIndex].clip = sfxClip[(int)sfx];
            sfxPlayer[loopIndex].Play();
            break;
        }
    }
    public void StopBgm()
    {
        bgmPlayer.Stop();
        //bgmPlayer.clip = null;
    }

    public void StopSfx(Sfx sfx)
    {
        foreach (AudioSource sfxSource in sfxPlayer)
        {
            if (sfxSource.isPlaying && sfxSource.clip == sfxClip[(int)sfx])
            {

                sfxSource.Stop();
                //sfxSource.clip = null;
                break;
            }
        }
    }

    // Set individual volume for BGM
    public void SetBgmVolume(float volume)
    {
        bgmVolume = Mathf.Clamp(volume, 0f, 1f);
        bgmPlayer.volume = bgmVolume;
    }

    // Set individual volume for SFX
    public void SetSfxVolume(float volume)
    {
        sfxVolume = Mathf.Clamp(volume, 0f, 1f);
        foreach (AudioSource sfxSource in sfxPlayer)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = sfxVolume;
            }
        }
    }

    public float GetBgmVolume()
    {
        return bgmPlayer.volume;
    }

    public float GetSfxVolume()
    {
        return sfxVolume;
    }
}
