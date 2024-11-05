using System.Security.Cryptography;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfXSource;
    [SerializeField] AudioSource playerSound;

    public AudioClip background;
    public AudioClip death;
    public AudioClip jump;
    public AudioClip oneUp;
    public AudioClip grow;
    public AudioClip stomp;
    public AudioClip star;
    public AudioClip itemShow;
    public AudioClip coin;
    public AudioClip pipe;

    private void Awake()
    {
        if(Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void playReg()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void playStar()
    {
        musicSource.clip = star;
        musicSource.Play();
    }
    public void playDeath()
    {
        musicSource.clip = death;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfXSource.PlayOneShot(clip);
    }

    public void PlayerPlay(AudioClip clip)
    {
        playerSound.PlayOneShot(clip);
    }
}
