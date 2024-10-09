using UnityEngine;

namespace GatherersExterior
{
    public class ToolEquip : MonoBehaviour
    {
        [SerializeField] private Equipable _equippedItem;
        [SerializeField] private SpriteRenderer _toolRenderer;

        public Equipable EquippedItem
        {
            get
            {
                return _equippedItem;
            }

            set
            {
                if (_equippedItem != value)
                {
                    _equippedItem = value;

                    // Update tool game object
                    UpdateTool();
                }
            }
        }

        private void UpdateTool()
        {
            _toolRenderer.sprite = _equippedItem.Sprite;
        }

        private void Start()
        {
            UpdateTool();
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}