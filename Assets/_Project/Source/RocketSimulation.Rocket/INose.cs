using UnityEngine;

namespace RocketSimulation
{
    public interface INose
    {
        public bool WasDecoupled { get; }

        public void Initialize();

        public void Decouple(Vector3 inertia);
    }
}