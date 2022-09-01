using UnityEngine;

namespace RocketSimulation.Rocket
{
    public class RocketMotor : MonoBehaviour, IMotor
    {
        private RocketBase _rocketOwner;
        private RocketData _rocketData;

        public void Initialize(RocketBase rocketOwner, RocketData rocketData)
        {
            _rocketOwner = rocketOwner;
            _rocketData = rocketData;
        }

        public void AddForce()
        {
            Vector3 direction = Vector3.up * _rocketData.Force;

            if (_rocketOwner.Rigidbody.velocity.y > _rocketData.WindHeight)
            {
                direction = ApplyWindInfluency(direction);
            }
            _rocketOwner.Rigidbody.AddForce(direction, _rocketData.ForceMode);
        }

        private Vector3 ApplyWindInfluency(Vector3 direction)
        {
            direction += _rocketData.Wind * Vector3.right;

            return direction;
        }
    }
}