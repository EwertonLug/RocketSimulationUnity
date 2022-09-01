using RocketSimulation.Audio;
using RocketSimulation.Core;
using RocketSimulation.VisualEffect;
using UnityEngine;

namespace RocketSimulation.Rocket
{
    [RequireComponent(typeof(Rigidbody))]
    public class MyRocket : RocketBase
    {
        [SerializeField] private RocketData _rocketData;

        [SerializeField] private Transform _parachuteObjectRef;
        [SerializeField] private Transform _effectMotorPositionRef;

        private Rigidbody _rigidBody;
        private IMotor _rocketMotor;
        private IParachute _parachute;
        private INose _rocketNose;
        private bool _isInitialized;
        private float _currentFuelLifeSeconds;
        private bool _isInFloor;
        private IAudioService _audioService;
        private IVisualEffectService _visualEffectService;
        public override Rigidbody Rigidbody => _rigidBody;

        public override void Initialize()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                _rigidBody = GetComponent<Rigidbody>();
                _rigidBody.isKinematic = false;
                _rocketMotor = GetComponent<IMotor>();
                _rocketMotor.Initialize(this, _rocketData);
                _parachute = GetComponent<IParachute>();
                _parachute.Initialize(this, _rocketData, _parachuteObjectRef);
                _rocketNose = GetComponentInChildren<INose>();
                _rocketNose.Initialize();
                _audioService = ServiceLocator.Current.Get<IAudioService>();
                _audioService.PlaySound(Sound.Rocket_Launch_001);
                _visualEffectService = ServiceLocator.Current.Get<IVisualEffectService>();
                _visualEffectService.Instantiate(Effect.PropulsorParticle, _effectMotorPositionRef.position, _effectMotorPositionRef.rotation, _effectMotorPositionRef);
            }
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                _currentFuelLifeSeconds += Time.deltaTime;

                if (_currentFuelLifeSeconds <= _rocketData.FuelLifeInSeconds)
                {
                    StartMotor();
                    LookToVelocity();
                }
                else
                {
                    _visualEffectService.Stop(Effect.PropulsorParticle);

                    if (CanEjectNose())
                    {
                        EjectNose();
                    }

                    if (CanOpenParachute())
                    {
                        OpenParachute();
                    }

                    if (_parachute.WasOpened && !_isInFloor)
                    {
                        LookToUp();
                    }
                }
            }
        }

        private bool CanOpenParachute()
        {
            return _rigidBody.velocity.y <= -5 && !_parachute.WasOpened;
        }

        private bool CanEjectNose()
        {
            return _rigidBody.velocity.y <= 3 && !_rocketNose.WasDecoupled;
        }

        private void StartMotor()
        {
            _rocketMotor.AddForce();
        }

        private void OpenParachute()
        {
            _parachute.Open();
            _audioService.PlaySound(Sound.Rocket_Parachute_Opening_001);
            _audioService.PlaySound(Sound.Rocket_Wind_001, true);
        }

        private void EjectNose()
        {
            _rocketNose.Decouple(_rigidBody.velocity);
        }

        private void LookToVelocity()
        {
            if (_rigidBody.velocity.magnitude > .1f)
            {
                Vector3 velocityDirection = (transform.position + _rigidBody.velocity) - transform.position;

                Quaternion rotation = Quaternion.LookRotation(velocityDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, .1f);

                Debug.DrawLine(transform.position, transform.position + _rigidBody.velocity, Color.green);
            }
        }

        private void LookToUp()
        {
            if (_rigidBody.velocity.magnitude > .1f)
            {
                Vector3 upDirection = (_rigidBody.position + Vector3.up) - _rigidBody.position;

                Quaternion rotation = Quaternion.LookRotation(upDirection, Vector3.up);
                _rigidBody.rotation = Quaternion.Slerp(_rigidBody.rotation, rotation, 0.01f);

                Debug.DrawLine(_rigidBody.position, _rigidBody.position + Vector3.up, Color.green);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Tags.Floor))
            {
                _parachute.Close();
                _audioService.StopSound(Sound.Rocket_Wind_001);
                _audioService.PlaySound(Sound.Rocket_Impact_001);
                _audioService.PlaySound(Sound.Laucher_Sucess_001);
                _isInFloor = true;
            }
        }
    }
}