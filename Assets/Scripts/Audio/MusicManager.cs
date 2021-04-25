using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace App
{
    public class MusicManager : MonoBehaviour
    {
        private static MusicManager _instance;
        public static MusicManager instance => _instance;

        public AudioMixerGroup mixerGroup;

        public List<AudioSource> audioSources = new List<AudioSource>();

        public AudioSource currentAudioSource;

        public float crossFadeSpeed = 3f;

        private void Awake()
        {
            _instance = this;
        }
        
        private void Update()
        {
            AudioSource[] sources = audioSources.ToArray();
            foreach (AudioSource audioSource in sources)
            {
                if (currentAudioSource == audioSource)
                {
                    audioSource.volume = Mathf.Clamp01(audioSource.volume + Time.deltaTime * crossFadeSpeed);
                }
                else
                {
                    audioSource.volume = Mathf.Clamp01(audioSource.volume - Time.deltaTime * crossFadeSpeed);
                }
                if (audioSource != currentAudioSource && audioSource.volume < 0.01f)
                {
                    audioSources.Remove(audioSource);
                    Destroy(audioSource);
                }
            }
        }

        public void CrossFade(AudioClip music, bool startAtFull = false)
        {
            Debug.Log(string.Concat("Crossfade in ", music, " : ", startAtFull.ToString()));
            AudioSource audioSource = audioSources.Find((AudioSource d) => d.clip == music);
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = music;
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.volume = (startAtFull ? 1f : 0f);
                audioSource.loop = true;
                audioSource.Play();
                audioSources.Add(audioSource);
            }
            currentAudioSource = audioSource;
        }

    }
}
