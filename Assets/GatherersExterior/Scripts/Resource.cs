using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
    [CreateAssetMenu(fileName = "Resource", menuName = "GatherersExterior/Resource")]
    public class Resource : ScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
    }
}