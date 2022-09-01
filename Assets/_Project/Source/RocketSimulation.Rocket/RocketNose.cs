using RocketSimulation;
using RocketSimulation.Audio;
using RocketSimulation.Core;
using RocketSimulation.VisualEffect;
using UnityEngine;

namespace RocketSimulation.Rocket
{
    public class RocketNose : MonoBehaviour, INose
    {
        [SerializeField] private Transform _effectPosition;

        private Rigidbody _rigidbody;
        private Vector3 _velocity;
        private bool _wasDecoupled;
        private bool _isInitialized;
        private IAudioService _audioService;
        private IVisualEffectService _visualEffectService;
        public bool WasDecoupled => _wasDecoupled;

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioService = ServiceLocator.Current.Get<IAudioService>();
            _visualEffectService = ServiceLocator.Current.Get<IVisualEffectService>();
            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (_wasDecoupled)
            {
                GetComponent<Rigidbody>().velocity = _velocity;
            }
        }

        public void Decouple(Vector3 inertia)
        {
            if (_isInitialized)
            {
                _rigidbody.isKinematic = false;
                _velocity = inertia;
                transform.parent = null;
                _audioService.PlaySound(Sound.Rocket_EjectNose_001);
                _audioService.PlaySound(Sound.Rocket_Nose_Launch_001);
                _visualEffectService.Instantiate(Effect.Rocke_EffectNose_001, _effectPosition.position, _effectPosition.rotation, _effectPosition);
                _wasDecoupled = true;
            }
        }
    }
}