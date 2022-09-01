using RocketSimulation.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace RocketSimulation.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        private List<AudioSource> _audiosSourcePool;
        private Dictionary<Sound, AudioClip> _soundAudioClipPair;

        public void PlaySound(Sound sound, bool looped = false)
        {
            AudioSource audioSource = GetAvaliableAudioSource();
            audioSource.name = sound.ToString();

            if (looped)
            {
                audioSource.loop = looped;
                audioSource.clip = _soundAudioClipPair[sound];
                audioSource.Play();
            }
            else
            {
                _ = PlayOneShoot(sound, audioSource);
            }
        }

        private async Task PlayOneShoot(Sound sound, AudioSource audioSource)
        {
            audioSource.PlayOneShot(_soundAudioClipPair[sound]);
            await Task.Delay(TimeSpan.FromSeconds(_soundAudioClipPair[sound].length));
            audioSource.gameObject.SetActive(false);
        }

        public void StopSound(Sound sound)
        {
            foreach (var audioSource in _audiosSourcePool)
            {
                if (audioSource.name == sound.ToString())
                {
                    audioSource.loop = false;
                    audioSource.Stop();
                    audioSource.gameObject.SetActive(false);
                }
            }
        }

        private AudioSource GetAvaliableAudioSource()
        {
            foreach (var audioSource in _audiosSourcePool)
            {
                if (!audioSource.gameObject.activeInHierarchy)
                {
                    audioSource.gameObject.SetActive(true);
                    return audioSource;
                }
            }

            return null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Create()
        {
            GameObject audioService = new GameObject();
            audioService.name = nameof(AudioService);
            AudioService audioServiceComponent = audioService.AddComponent<AudioService>();

            Dictionary<Sound, AudioClip> soundAudioClipPair = new Dictionary<Sound, AudioClip>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                soundAudioClipPair[sound] = Resources.Load<AudioClip>("SoundEffects/" + sound.ToString());
            }

            List<AudioSource> audiosSourcePool = new List<AudioSource>();

            for (int i = 0; i < 10; i++)
            {
                GameObject audioSorce = new GameObject();
                AudioSource audioSourceComponent = audioSorce.AddComponent<AudioSource>();
                audioSorce.name = nameof(AudioSource);
                audioSorce.transform.SetParent(audioService.transform);
                audioSorce.SetActive(false);
                audiosSourcePool.Add(audioSourceComponent);
            }

            audioServiceComponent.RegisterClips(soundAudioClipPair);
            audioServiceComponent.RegisterPool(audiosSourcePool);

            DontDestroyOnLoad(audioService);

            ServiceLocator.Current.Register<IAudioService>(audioServiceComponent);
        }

        private void RegisterPool(List<AudioSource> audiosSourcePool)
        {
            _audiosSourcePool = audiosSourcePool;
        }

        private void RegisterClips(Dictionary<Sound, AudioClip> soundAudioClipPair)
        {
            _soundAudioClipPair = soundAudioClipPair;
        }
    }
}