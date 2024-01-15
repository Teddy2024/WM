using UnityEngine;
using System;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;

    [SerializeField] private AudioBox _audioBox;
    int num;

    public float MusicVolume = 1f;
    public float SoundVolume = 1f;

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip clip, float vol = 1)
    {
        _soundsSource?.PlayOneShot(clip, vol);
    }

    public void StopAllAudio()
    {
        _musicSource.Pause();
        _soundsSource.Pause();
    }

    private void OnEnable() 
    {
        GameManager.OnGameStateChange += OnChange;
    }

    private void OnDisable() 
    {
        GameManager.OnGameStateChange -= OnChange;
    }

    private void Update() => GoNextBGM();

    public void PlayMainBGM()
    {
        PlayMusic(_audioBox.MainBgm);
        num = 0;
    }

    void GoNextBGM()
    {
        if (GameManager.Instance.State == GameState.InPlay && !_musicSource.isPlaying)
        {
            num++;
            if(num >= _audioBox.AllBGM.Length)num = 0;
            PlayMusic(_audioBox.AllBGM[num]);
        }
    }

    private void OnChange(GameState state)
    {
        if(state == GameState.MainMenu)
        {
            _musicSource.loop = true;
            PlayMainBGM();
        }
        else if(state == GameState.InPlay)
        {
            _musicSource.loop = false;
            PlayMusic(_audioBox.AllBGM[num]);
        }
        else if(state == GameState.ExitMenu)
        {
            AudioManager.Instance.StopAllAudio();
        }
        else if(state == GameState.Lose)
        {
            AudioManager.Instance.StopAllAudio();
            _musicSource.clip = null;
            num = 0;
        }
    }

    public void ChangeMasterVolume(float value, SoundType soundType)
    {
        switch(soundType)
        {
            case SoundType.Music:
            _musicSource.volume = value;
            MusicVolume = value;
            break;
            case SoundType.Sound:
            _soundsSource.volume = value;
            SoundVolume = value;
            break;
        }
    }
}
[Serializable] public enum SoundType
{
    Music = 0,
    Sound = 1
}
