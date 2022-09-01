using UnityEngine;

namespace RocketSimulation.Rocket
{
    public class RocketVisualEffects : MonoBehaviour
    {
        [SerializeField] private Transform _motorParticleRef;

        [SerializeField] private AudioClip _lauchSfx;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ShowMotorParticles()
        {
            _motorParticleRef.gameObject.SetActive(true);
            _audioSource.PlayOneShot(_lauchSfx);
        }

        public void HideMotorParticles()
        {
            _motorParticleRef.gameObject.SetActive(false);
        }
    }
}