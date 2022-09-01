namespace RocketSimulation.Audio
{
    public interface IAudioService
    {
        public void PlaySound(Sound sound, bool looped = false);

        public void StopSound(Sound sound);
    }
}