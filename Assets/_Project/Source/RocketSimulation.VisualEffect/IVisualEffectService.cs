using UnityEngine;

namespace RocketSimulation.VisualEffect
{
    public interface IVisualEffectService
    {
        public void Instantiate(Effect effect, Vector3 position, Quaternion rotation, Transform parent = null);

        public void Stop(Effect effect);
    }
}