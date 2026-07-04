using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Ambient Music")]
    public AudioClip ambientMusic;
    [Range(0f, 1f)] public float ambientVolume = 0.2f;

    [Header("UI Sounds")]
    public AudioClip buttonClickSound;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private bool musicEnabled = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = ambientVolume;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
    }

    void Start()
    {
        musicEnabled = PlayerPrefs.GetInt("Music", 1) == 1;

        if (ambientMusic != null)
        {
            musicSource.clip = ambientMusic;
            musicSource.Play();
            musicSource.mute = !musicEnabled;
        }
    }

    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            sfxSource.PlayOneShot(buttonClickSound, sfxVolume);
    }
    public void SetMusicVolume(float volume)
    {
        ambientVolume = volume;
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;

        musicSource.mute = !musicEnabled;

        PlayerPrefs.SetInt("Music", musicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsMusicEnabled()
    {
        return musicEnabled;
    }
}