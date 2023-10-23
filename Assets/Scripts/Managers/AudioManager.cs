using Gameplay.SlotMachine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource soundsSource;

        [SerializeField] private AudioClip stopSound;
        [SerializeField] private AudioClip spinStartSound;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip buttonSound;

        protected override void Awake()
        {
            base.Awake();
        }

        public void PlayStartSound()
        {
            soundsSource.PlayOneShot(spinStartSound);
        }

        public void PlayStopSound()
        {
            soundsSource.PlayOneShot(stopSound);
        }

        public void PlayWinSound()
        {
            soundsSource.PlayOneShot(winSound);
        }

        public void PlayButon()
        {
            soundsSource.PlayOneShot(spinStartSound);
        }
    }
}