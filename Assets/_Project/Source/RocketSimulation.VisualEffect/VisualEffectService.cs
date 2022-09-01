using RocketSimulation.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketSimulation.VisualEffect
{
    public class VisualEffectService : MonoBehaviour, IVisualEffectService
    {
        private Dictionary<Effect, ParticleSystem> _effectsClipPair;
        private Dictionary<ParticleSystem, bool> _lastParticlesInstantiates = new Dictionary<ParticleSystem, bool>();

        public void Instantiate(Effect effect, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            try
            {
                GameObject particle = GameObject.Instantiate(_effectsClipPair[effect].gameObject, position, rotation, parent);
                particle.name = effect.ToString();
                _lastParticlesInstantiates.Add(particle.GetComponent<ParticleSystem>(), true);
            }
            catch (Exception)
            {
                Debug.LogWarning($"Effect {effect} not registred");
            }
        }

        public void Stop(Effect effect)
        {
            foreach (KeyValuePair<ParticleSystem, bool> particle in _lastParticlesInstantiates)
            {
                if (particle.Key != null)
                {
                    if (particle.Key.name == effect.ToString())
                    {
                        particle.Key.Stop();
                        break;
                    }
                    else
                    {
                        Debug.LogWarning($"Effect {effect} not Instantiated");
                    }
                }
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Create()
        {
            GameObject visualEffectService = new GameObject();
            visualEffectService.name = nameof(VisualEffectService);
            VisualEffectService visualEffectComponent = visualEffectService.AddComponent<VisualEffectService>();

            Dictionary<Effect, ParticleSystem> effectPair = new Dictionary<Effect, ParticleSystem>();

            foreach (Effect effect in Enum.GetValues(typeof(Effect)))
            {
                effectPair[effect] = Resources.Load<ParticleSystem>("VisualEffects/" + effect.ToString());
            }

            visualEffectComponent.RegisterEffects(effectPair);

            DontDestroyOnLoad(visualEffectService);

            ServiceLocator.Current.Register<IVisualEffectService>(visualEffectComponent);
        }

        private void Release(Scene scene, LoadSceneMode mode)
        {
            _lastParticlesInstantiates.Clear();
        }

        private void RegisterEffects(Dictionary<Effect, ParticleSystem> effectPair)
        {
            _effectsClipPair = new Dictionary<Effect, ParticleSystem>();
            _effectsClipPair = effectPair;

            SceneManager.sceneLoaded += Release;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= Release;
        }
    }
}