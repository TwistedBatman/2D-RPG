using System;
using UnityEngine;

namespace GatherersExterior
{
    /// <summary>
    /// An inventory that takes stock of the resources the character picks up and keeps track of types and count
    /// </summary>
    public class ResourceInventory : MonoBehaviour
    {
        [field: SerializeField] private SerializableDictionary<Resource, int> Resources { get; set; } = new();

        /// <summary>
        /// Adds the amount to the existing resource keyvalue pair or creates a new one with the amount if none exist
        /// </summary>
        /// <param name="type">The resource to add to the Supplies</param>
        /// <param name="amount">The count of the resource being added to the supplies</param>
        public void AddResources(Resource type, int amount)
        {
            int oldAmount = 0;

            // Get the old count if exists
            Resources.TryGetValue(type, out oldAmount);

            // Replace old value
            Resources[type] = oldAmount += amount;
        }

        /// <summary>
        /// Tries to remove the amount of resources from the resources in the Supplies dictionary
        /// Fails if there is not enough resources to do so
        /// </summary>
        /// <param name="type">Resource to remove</param>
        /// <param name="amount">Amount of the resource to remove</param>
        /// <returns>If the expected amount of resources were removed or if removing the resources failed</returns>
        public bool RemoveResources(Resource type, int amount)
        {
            if (Resources[type] > amount)
            {
                Resources[type] -= amount;
                return true;
            }

            // Not enough resources
            return false;
        }
    }
}