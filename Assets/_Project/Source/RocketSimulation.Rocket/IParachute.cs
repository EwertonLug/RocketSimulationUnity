using RocketSimulation.Rocket;
using UnityEngine;

namespace RocketSimulation
{
    public interface IParachute
    {
        public bool WasOpened { get; }

        public void Initialize(RocketBase rocketOwner, RocketData rocketData, Transform parachuteObjectRef);

        public void Open();

        public void Close();
    }
}