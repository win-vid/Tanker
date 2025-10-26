using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField] private AudioSource soundFXObject;

    // Track active audio sources
    private List<AudioSource> activeAudioSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    /// Plays a sound effect and returns its AudioSource so it can be stopped later.
    public AudioSource PlaySoundEffect(AudioClip clip, Transform spawnTransform, float volume)
    {
        if (clip != null)
        {
            AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.spatialBlend = 1f;      // <-- Important: make it 3D
            audioSource.minDistance = 1f;
            audioSource.maxDistance = 15f;
            audioSource.Play();

            activeAudioSources.Add(audioSource);
            StartCoroutine(RemoveAfterPlayback(audioSource));

            return audioSource;
        }
        return null;
    }

    // play looped sound effect
    public AudioSource PlayLoopedSoundEffect(AudioClip clip, Transform spawnTransform, float volume)
    {
        if (clip != null)
        {
            AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = true;              // Set to loop
            audioSource.spatialBlend = 1f;       // <-- Important: make it 3D
            audioSource.minDistance = 1f;
            audioSource.maxDistance = 15f;
            audioSource.Play();

            activeAudioSources.Add(audioSource);
            return audioSource;
        }
        return null;
    }

    /// Plays a random sound effect from an array and returns the AudioSource.
    public AudioSource PlayRandomSoundEffect(AudioClip[] clips, Transform spawnTransform, float volume)
    {
        if (clips != null && clips.Length > 0)
        {
            int randomIndex = Random.Range(0, clips.Length);
            AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = clips[randomIndex];
            audioSource.volume = volume;
            audioSource.spatialBlend = 1f;      // <-- Important: make it 3D
            audioSource.minDistance = 1f;
            audioSource.maxDistance = 15f;
            audioSource.Play();

            activeAudioSources.Add(audioSource);
            StartCoroutine(RemoveAfterPlayback(audioSource));

            return audioSource;
        }
        return null;
    }

    /// Stops and removes a specific sound.
    public void StopSoundEffect(AudioSource source)
    {
        if (source != null && activeAudioSources.Contains(source))
        {
            source.Stop();
            activeAudioSources.Remove(source);
            Destroy(source.gameObject);
        }
    }

    /// Coroutine to auto-remove AudioSource after it finishes playing.
    private IEnumerator RemoveAfterPlayback(AudioSource source)
    {
        yield return new WaitForSeconds(source.clip.length + 0.1f);
        if (activeAudioSources.Contains(source))
        {
            activeAudioSources.Remove(source);
        }
        if (source != null)
        {
            Destroy(source.gameObject);
        }
    }
}
