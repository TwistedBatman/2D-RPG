using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
    [CreateAssetMenu(fileName = "Equippable", menuName = "GatherersExterior/Equippable")]
    public class Equipable : ScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, Tooltip("In game display")] public Sprite Sprite { get; private set; }
        [field: SerializeField, Tooltip("UI / Inventory graphic")] public Sprite Icon { get; private set; }
        [field: SerializeField] public EquipmentType Type { get; private set; }
    }
}