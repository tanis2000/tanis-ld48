using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager instance => _instance;
        
        public AudioSource audioSourcePrefab;

        private void Awake()
        {
            _instance = this;
        }

        public void PlayAtPosition(AudioClip clip, Vector3 position, float volume = 1f)
        {
            var audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }
}
