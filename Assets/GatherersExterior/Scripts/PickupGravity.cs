using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherersExterior
{
    /// <summary>
    /// Moves nearby pickups towards this game object's center by a set amount per second
    ///
    /// Directly changes the position rather than using rigidbody so every pickup in the game
    /// doesn't need a rigidbody just for this effect
    /// </summary>
    public class PickupGravity : MonoBehaviour
    {
        [field: SerializeField] public float UnitsPerSecond { get; private set; } = 1f / 32f;
        [field: SerializeField] public Collider2D GravityCollider { get; private set; }

        private List<ResourcePickup> _nearbyResources = new();

        private void FixedUpdate()
        {
            List<ResourcePickup> nullPickups = new();

            foreach (ResourcePickup nearbyResource in _nearbyResources)
            {
                if (nearbyResource != null)
                {
                    Vector2 directionToGravityCenter = (transform.position - nearbyResource.transform.position).normalized;

                    nearbyResource.transform.Translate(directionToGravityCenter * UnitsPerSecond);
                }
                else
                {
                    nullPickups.Add(nearbyResource);
                }
            }

            // Cleanup after gravity is finished
            if (nullPickups.Count > 0)
            {
                _nearbyResources = _nearbyResources.Except(nullPickups).ToList();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ResourcePickup nearbyResource = collision.GetComponent<ResourcePickup>();

            if (nearbyResource)
            {
                _nearbyResources.Add(nearbyResource);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ResourcePickup nearbyResource = collision.GetComponent<ResourcePickup>();

            if (nearbyResource)
            {
                _nearbyResources.Remove(nearbyResource);
            }
        }
    }
}