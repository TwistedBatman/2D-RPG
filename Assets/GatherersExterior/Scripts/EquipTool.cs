using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GatherersExterior
{
    public class EquipTool : MonoBehaviour
    {
        [SerializeField] private Equipable _equippedTool;
        private SpriteRenderer _spriteRenderer;

        public Equipable EquippedTool
        {
            get
            {
                return _equippedTool;
            }
            set
            {
                if (_equippedTool != value)
                {
                    _equippedTool = value;

                    // Update sprite
                    _spriteRenderer.sprite = _equippedTool.Sprite;
                }
            }
        }

        [field: SerializeField] public int HarvestMin { get; private set; } = 1;
        [field: SerializeField] public int HarvestMax { get; private set; } = 4;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Harvestable harvestable) && harvestable.CanHarvestWith(EquippedTool.Type))
            {
                // Harvest a random amount of the resource from the harvestable object
                harvestable.Harvest(Random.Range(HarvestMin, HarvestMax));
            }
        }
    }
}