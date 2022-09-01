using RocketSimulation.Audio;
using RocketSimulation.Core;
using RocketSimulation.Rocket;
using RocketSimulation.VisualEffect;
using System.Collections;
using UnityEngine;

namespace RocketSimulation
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] private RocketBase _rocket;
        [SerializeField] private Transform _effectPosition;

        private int _delayInSeconds = 10;
        private IAudioService _audioService;
        private IVisualEffectService _visualEffectService;

        private IEnumerator Start()
        {
            _audioService = ServiceLocator.Current.Get<IAudioService>();
            _visualEffectService = ServiceLocator.Current.Get<IVisualEffectService>();
            _audioService.PlaySound(Sound.Launcher_Countdown_10);
            yield return new WaitForSeconds((float)_delayInSeconds);
            _rocket.Initialize();
            _visualEffectService.Instantiate(Effect.Launcher_Smoke, 
                                                _effectPosition.position, 
                                                _effectPosition.rotation, _effectPosition);

        }
    }
}