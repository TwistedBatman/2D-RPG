using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GatherersExterior
{
public class PickupResources : MonoBehaviour
{
    [field: SerializeField] public ResourceInventory Inventory { get; private set; }
    [field: SerializeField] public Collider2D PickupRadius { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ResourcePickup pickup = collision.GetComponent<ResourcePickup>();

        if (pickup != null)
        {
            // It's a resource so pick it up
            Inventory.AddResources(pickup.Resource, pickup.Amount);
            Destroy(pickup.gameObject);
        }
    }
}
}