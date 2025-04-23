
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource WalkSource;

    public AudioClip background;
    public AudioClip background2;
    public AudioClip munchPellet;
    public AudioClip munchPowerPellet;
    public AudioClip munchGhost;
    public AudioClip death;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void switchBackground()
    {
        musicSource.clip = background2;
        musicSource.Play();
    }

    public void EatPellet()
    {
        WalkSource.clip = munchPellet;

        if (!WalkSource.isPlaying && WalkSource.clip == munchPellet)
        {
            WalkSource.Play();
        }
    }

    public void EatPowerPellet()
    {
        SFXSource.clip = munchPowerPellet;
        if (!SFXSource.isPlaying && SFXSource.clip == munchPowerPellet)
        {
            SFXSource.Play();
        }

    }
    public void EatGhost()
    {
        SFXSource.clip = munchGhost;
        if (!SFXSource.isPlaying && SFXSource.clip == munchGhost)
        {
            SFXSource.Play();
        }
        
    }

    public void PacmanEat()
    {
        SFXSource.clip = death;
        if (!SFXSource.isPlaying && SFXSource.clip == death)
        {
            SFXSource.Play();
        }
    }

    public void OffMusic()
    {
        musicSource.mute = true;
        SFXSource.mute = true;
    }

    public void OnMusic()
    {
        musicSource.mute = false;
        SFXSource.mute = false;
    }
}
