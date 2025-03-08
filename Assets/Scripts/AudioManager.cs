using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource soundFXObject;
    public AudioClip[] musicList;

    private AudioSource musicSource;
    private bool isPlaying = false;

    int currentSongIndex = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        musicSource = instance.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (musicList.Length > 0 && !isPlaying)
            PlayMusic();

        if (!musicSource.isPlaying)
            isPlaying = false;
    }
    public void PlaySoundFX(AudioClip audioClip, Transform audioLocation, float volume)
    {
        //These four lines of code create a new object that plays a sound in a certain location
        //This should be used when playing ANY sound
        //The reason we do this is so we can control which mixer track the sound plays in
        AudioSource soundFXSource = Instantiate(soundFXObject, audioLocation.position, Quaternion.identity);
        soundFXSource.clip = audioClip;
        soundFXSource.volume = volume;
        soundFXSource.Play();

        //The object that is created will get deleted right after the clip ends
        float clipLength = audioClip.length;
        Destroy(soundFXSource.gameObject, clipLength);
    }

    public void PlayMusic()
    {
        if (currentSongIndex == musicList.Length)
        {
            currentSongIndex = 0;
        }

        musicSource.clip = musicList[currentSongIndex++];
        musicSource.volume = 1f;
        musicSource.Play();
        isPlaying = true;
    }
}
