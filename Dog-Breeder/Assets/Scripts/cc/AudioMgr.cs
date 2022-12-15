using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : SingletonMono<AudioMgr>
{
    [SerializeField] public AudioSource bgmSource, fxSource;

    [SerializeField]
    private AudioClip nextUI,
                      nextDay,
                      sellUI,
                      notebook,
                      playerMove,
                      dogBark,
                      dogWhining,
                      openDoor;
    [SerializeField]
    private AudioClip StartMenuBgm,
                      RoomBgm;


    public float BgmVolumn
    {
        get => bgmSource.volume;
        set => bgmSource.volume = Mathf.Clamp01(value);
    }

    public bool IsBgmPlaying => bgmSource.isPlaying;

    public void PlayBgm() => bgmSource.UnPause();

    public void PauseBgm() => bgmSource.Pause();


    protected override void OnInstanceAwake()
    {
        DontDestroyOnLoad(this);
        //bgmSource.Play();
    }


    public float FxVolumn
    {
        get => fxSource.volume;
        set => fxSource.volume = Mathf.Clamp01(value);
    }


    public void PlayFx(AudioFxType fx)
    {
        fxSource.Stop();
        fxSource.clip = fx switch
        {
            AudioFxType.NextUI => nextUI,
            AudioFxType.NextDay => nextDay,
            AudioFxType.PlayerMove => playerMove,
            AudioFxType.OpenDoor => openDoor,
            _ => null
        };
        fxSource.Play();
    }

    public void ClickUI()
    {
        fxSource.Stop();
        fxSource.clip = nextUI;
        fxSource.Play();
    }

    public void PlayGameBgm(AudioBgmType bgm)
    {
        bgmSource.Stop();
        bgmSource.clip = bgm switch
        {
            AudioBgmType.StartMenuBgm => StartMenuBgm,
            AudioBgmType.RoomBgm => RoomBgm,
            _ => null
        };
        bgmSource.Play();
    }
}


public enum AudioFxType
{
    //
    NextUI,
    NextDay,
    PlayerMove,
    OpenDoor
}
public enum AudioBgmType
{
    StartMenuBgm,
    RoomBgm
}