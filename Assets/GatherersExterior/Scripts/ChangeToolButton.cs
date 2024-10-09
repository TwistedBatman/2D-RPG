using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GatherersExterior
{
    public class ChangeToolButton : MonoBehaviour
    {
        [SerializeField] private Equipable _equipItem;

        public Equipable EquipItem
        {
            get
            {
                return _equipItem;
            }
            set
            {
                _equipItem = value;
                _image.sprite = EquipItem.Icon;
            }
        }

        private EquipTool _tool;
        private Image _image;

        // Start is called before the first frame update
        private void Start()
        {
            // Find the Equip Tool game object in the scene (presumably on the player)
            _tool = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<EquipTool>();
            _image = GetComponent<Image>();

            // Trigger updates where necessary
            EquipItem = _equipItem;
        }

        public void EquipItemToToolSlot()
        {
            if (_tool != null)
            {
                _tool.EquippedTool = EquipItem;
            }
            else
            {
                Debug.LogError($"There is no tool slot in {name}. Has one been added to the player?");
            }
        }
    }
}