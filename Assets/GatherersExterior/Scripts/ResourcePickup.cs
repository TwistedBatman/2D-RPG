using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
    public class ResourcePickup : MonoBehaviour
    {
        [field: SerializeField] public int Amount { get; private set; } = 1;

        [field: SerializeField] public Resource Resource { get; private set; }
    }
}