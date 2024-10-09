using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GatherersExterior
{
    [CreateAssetMenu(fileName = "EquipmentType_", menuName = "GatherersExterior/EquipmentType")]
    public class EquipmentType : ScriptableObject
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}