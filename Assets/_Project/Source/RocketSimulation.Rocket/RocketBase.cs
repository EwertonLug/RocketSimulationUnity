using UnityEngine;

namespace RocketSimulation.Rocket
{
    public abstract class RocketBase : MonoBehaviour
    {
        public abstract Rigidbody Rigidbody { get; }

        public abstract void Initialize();
    }
}