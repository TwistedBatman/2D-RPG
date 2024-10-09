using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GatherersExterior
{
    public class Harvestable : MonoBehaviour
    {
        [field: SerializeField] public List<EquipmentType> HarvestableByTypes { get; private set; }
        [field: SerializeField] public GameObjectEmitter ResourceEmitter { get; private set; }

        [field: SerializeField, Range(0f, 5f), Tooltip("Increase possible distance from center to HarvestedItemPrefab spawn location")]
        public float SpawnLocationOffsetRange { get; private set; } = 0f;

        [field: SerializeField] public int AmountAvailable { get; private set; } = 1;

        [field: SerializeField, Tooltip("Spawns on Depletion")] public GameObject DepletedEffectPrefab { get; private set; }
        [field: SerializeField] public bool DestroyOnDepletion { get; private set; } = true;
        [field: SerializeField] public UnityEvent OnHarvestableDestroyed { get; private set; }

        public bool CanHarvestWith(EquipmentType type) => HarvestableByTypes.Contains(type);

        private Animator _animator;
        private Collider2D _collider;
        private int _amountHarvested;

        private void Start()
        {
            _collider = GetComponent<Collider2D>();
        }

        public void Harvest(int amount)
        {
            if (AmountAvailable > 0)
            {
                int emitAmount = Mathf.Min(amount, AmountAvailable);
                ResourceEmitter.Emit(emitAmount);
                _amountHarvested += emitAmount;
                Debug.Log($"Harvested: {_amountHarvested} total from {name}");
                AmountAvailable -= emitAmount;

                // Check if resource is depleted
                if (AmountAvailable <= 0)
                {
                    _collider.enabled = false;

                    // Play the depleted effect if any
                    if (DepletedEffectPrefab)
                    {
                        GameObject effect = Instantiate(DepletedEffectPrefab);
                        effect.transform.position = transform.position;
                    }

                    if (DestroyOnDepletion)
                    {
                        DestroyHarvestable();
                    }
                }

                Debug.Log($"{name} was harvested");
            }
            else
            {
                Debug.LogWarning($"Trying to harvest harvestable {name} but there is nothing left to harvest.");
            }
        }

        /// <summary>
        /// Can be used for animation event to destroy the game object when the depletion animation is complete.
        /// </summary>
        public void DestroyHarvestable()
        {
            OnHarvestableDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}