using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music Settings")]
    [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volume = 0.5f;

    private int _currentTrackIndex = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (musicTracks.Count > 0)
            PlayTrack(_currentTrackIndex);
    }

    void Update()
    {
        if (!audioSource.isPlaying)
            PlayNextTrack();
    }

    private void PlayTrack(int index)
    {
        if (index < 0 || index >= musicTracks.Count) return;

        _currentTrackIndex = index;
        audioSource.clip = musicTracks[_currentTrackIndex];
        audioSource.Play();
    }

    private void PlayNextTrack()
    {
        _currentTrackIndex = (_currentTrackIndex + 1) % musicTracks.Count;
        PlayTrack(_currentTrackIndex);
    }
}