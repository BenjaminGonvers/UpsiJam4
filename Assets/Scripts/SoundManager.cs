using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private const int MAX_SOUND_COUNT = 16;
    private const int MAX_MUSIC_COUNT = 2;
    private const float VELOCITY = 100;

    [Header("Menu")] 
    [SerializeField] private AudioClip clickBtn;
    [SerializeField] private AudioClip switchBtn;

    [Header("Event")]
    [SerializeField] private AudioClip Alert;

    [Header("BreakingNews")]
    [SerializeField] private AudioClip[] _breakingNews;



    [Header("Music")] [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip inGame;
    [SerializeField] private AudioClip scoreMenu;

    [Header("Mixer")] [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioMixerGroup groupSound;
    [SerializeField] private AudioMixerGroup groupVoice;
    [SerializeField] private AudioMixerGroup groupMusic;

    private List<AudioSource> m_SoundsSource;
    private List<AudioSource> m_MusicsSource;

    private int _transitionState = 1;

    private float _volumeMusic0 = 100;
    private float _volumeMusic1 = 0;

    public enum Type
    {
        Master = 0,
        Sound = 1,
        Menu = 2,
        Music = 3,
        Voice = 4
    }

    public enum Menu
    {
        Switch = 0,
        Press = 1,
    }

    public enum Sound
    {
        Alert = 0,
        BreakingNews = 1
    }

    public enum Voice
    {
        Ready = 0,
        Go = 1
    }

    public enum Music
    {
        MainMenu = 0,
        InGame = 1,
        ScoreMenu = 2
    }

    private void Start()
    {
        gameObject.tag = "Sound";
        if (GameObject.FindGameObjectsWithTag("Sound").Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }

        m_SoundsSource = new List<AudioSource>();
        for (int i = 0; i < MAX_SOUND_COUNT; i++)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = groupSound;
            m_SoundsSource.Add(audioSource);
        }

        m_MusicsSource = new List<AudioSource>();
        for (int i = 0; i < MAX_MUSIC_COUNT; i++)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = groupMusic;
            audioSource.loop = true;
            m_MusicsSource.Add(audioSource);
        }

        m_MusicsSource[0].volume = 1.0f;
        m_MusicsSource[1].volume = 0.0f;

        m_MusicsSource[0].clip = mainMenu;
        m_MusicsSource[0].Play();
    }

    public void PlaySound(Sound sound)
    {
        AudioClip audioResource = null;
        switch (sound)
        {
            case Sound.Alert:
                audioResource = Alert;
                break;

            case Sound.BreakingNews:
                audioResource = _breakingNews[Random.Range(0, _breakingNews.Count() - 1)];
                break;

        }

        // One AudioSource is free ?
        foreach (AudioSource soundSource in m_SoundsSource)
        {
            if (soundSource.isPlaying) continue;

            soundSource.clip = audioResource;
            float oldPitch = soundSource.pitch;
            soundSource.pitch = Random.Range(0.9f, 1.1f);
            soundSource.Play();
            soundSource.pitch = oldPitch;
            break;
        }
    }

    public void PlayVoice(Voice voice)
    {
        AudioClip audioResource = null;
        switch (voice)
        {
            //To Complete
        }

        // One AudioSource is free ?
        foreach (AudioSource soundSource in m_SoundsSource)
        {
            if (soundSource.isPlaying) continue;

            soundSource.clip = audioResource;
            soundSource.Play();
            break;
        }
    }

    public void PlayMusic(Music music)
    {
        AudioClip audioResource = null;
        switch (music)
        {
            case Music.MainMenu:
                audioResource = mainMenu;
                break;
            case Music.InGame:
                audioResource = inGame;
                break;
            case Music.ScoreMenu:

                break;
        }

        foreach (AudioSource musicSource in m_MusicsSource)
        {
            if (musicSource.isPlaying) continue;

            musicSource.clip = audioResource;
            musicSource.Play();
        }

        _transitionState *= -1;
    }

    private void TransitionMusic(float deltaTime)
    {
        if (m_MusicsSource[0].isPlaying)
        {
            _volumeMusic0 += deltaTime * _transitionState * VELOCITY;
            if (_volumeMusic0 < 0)
            {
                _volumeMusic0 = 0;
                m_MusicsSource[0].Stop();
            }

            if (_volumeMusic0 > 100)
            {
                _volumeMusic0 = 100;
            }

            m_MusicsSource[0].volume = _volumeMusic0 / 100.0f;
        }

        if (m_MusicsSource[1].isPlaying)
        {
            _volumeMusic1 += deltaTime * -_transitionState * VELOCITY;
            if (_volumeMusic1 < 0)
            {
                _volumeMusic1 = 0;
                m_MusicsSource[1].Stop();
            }

            if (_volumeMusic1 > 100)
            {
                _volumeMusic1 = 100;
            }

            m_MusicsSource[1].volume = _volumeMusic1 / 100.0f;
        }
    }

    private void Update()
    {
        TransitionMusic(Time.deltaTime);
    }

    private static int CalcVolumeToDecibel(int volume)
    {
        return (int)(20 * Mathf.Log10(volume / 100f));
    }

    void SetVolume(Type type, int volume)
    {
        int dB = CalcVolumeToDecibel(volume);
        switch (type)
        {
            case Type.Master:
                mixer.SetFloat("Master", dB);
                break;
            case Type.Music:
                mixer.SetFloat("Music", dB);
                break;
            case Type.Sound:
                mixer.SetFloat("Sound", dB);
                break;
            case Type.Voice:
                mixer.SetFloat("Voice", dB);
                break;
        }
    }
}