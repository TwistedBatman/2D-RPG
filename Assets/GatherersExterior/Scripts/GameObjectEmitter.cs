using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace GatherersExterior
{
    /// <summary>
    /// Spawns particles that turn into game objects after they reach the bounds of the trigger zone
    ///
    /// NOTE: Make sure that the emission collider is 100% inside of the exit trigger collider
    /// or the exit callback will never be called (particle can't leave what it was never inside to begin with!)
    /// </summary>
    public class GameObjectEmitter : MonoBehaviour
    {
        private ParticleSystem _ps;
        private List<ParticleSystem.Particle> _exitParticles = new();

        [field: SerializeField] public GameObject ResourcePickupPrefab { get; private set; }

        private void Start()
        {
            _ps = GetComponent<ParticleSystem>();
        }

        /// <summary>
        /// Spawn the game object of the resouce when the particle triggers it's callback
        /// Be sure to use on Exit - Kill to have particles kill after game objects spawn
        /// </summary>
        private void OnParticleTrigger()
        {
            _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, _exitParticles);

            for (int i = 0; i < _exitParticles.Count; i++)
            {
                // Spawn a ResourcePickupPrefab at the location where the particle exited the trigger radius
                GameObject resourcePickup = Instantiate(ResourcePickupPrefab);
                resourcePickup.transform.position = _exitParticles[i].position;
            }
        }

        public void Emit(int count)
        {
            _ps.Emit(count);
        }
    }
}