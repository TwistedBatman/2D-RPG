using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTool : MonoBehaviour
{
    //[SerializeField] List<Sprite> sprites = new List<Sprite>();

    public void EquipTool(Sprite sprite)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }
}
