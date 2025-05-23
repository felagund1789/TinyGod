using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Background Music Settings")]
        [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>();
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float volume = 0.5f;

        private int _currentTrackIndex = 0;

        void Awake()
        {
            // DontDestroyOnLoad(gameObject); // Commented out because it would play multiple sounds if we navigated to main menu
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
            if (!audioSource.isPlaying && (Time.timeScale != 0f))
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
}