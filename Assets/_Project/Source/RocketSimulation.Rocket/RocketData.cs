using UnityEngine;

namespace RocketSimulation.Rocket
{
    [CreateAssetMenu(menuName = "RocketSettings")]
    public class RocketData : ScriptableObject
    {
        [SerializeField] private float _force;
        [SerializeField] private float _smooth;
        [SerializeField] private float _fuellifeInSeconds;
        [SerializeField] private float _wind;
        [SerializeField] private float _windHeight;
        [SerializeField] private ForceMode _forceMode = ForceMode.Acceleration;

        public float Force => _force;
        public float Smooth => _smooth;
        public float FuelLifeInSeconds => _fuellifeInSeconds;
        public float Wind => _wind;
        public float WindHeight => _windHeight;
        public ForceMode ForceMode => _forceMode;
    }
}