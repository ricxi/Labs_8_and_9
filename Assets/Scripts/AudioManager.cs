using System.Collections;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        StartCoroutine(PlayBgMusicDelayed());
    }

    private IEnumerator PlayBgMusicDelayed()
    {
        yield return new WaitForSeconds(3f);
        PlayBgMusic();
    }

    public void PlayJumpSFX()
    {
        sfxSource.clip = jumpSFX;
        sfxSource.Play();
    }

    public void PlayDeathSFX()
    {
        sfxSource.clip = deathSFX;
        sfxSource.Play();
    }

    public void PlayBgMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
}
