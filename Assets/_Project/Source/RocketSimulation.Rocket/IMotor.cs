using RocketSimulation.Rocket;

namespace RocketSimulation
{
    public interface IMotor
    {
        public void Initialize(RocketBase rocketOwner, RocketData rocketData);

        public void AddForce();
    }
}