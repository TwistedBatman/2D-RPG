using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    public Tilemap map;

    [SerializeField]
    public List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    // Get the tiles data
    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tileData in tileDatas)
            foreach(var tile in tileData.tiles)
                dataFromTiles.Add(tile, tileData);
    }

    // Get the speed depending on the tile
    public float GetTileSpeed(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);

        if (tile == null) // Normal speed if the tile isn't special
            return 1f;
        
        float speed = dataFromTiles[tile].walkingSpeed;
        return speed;
    }
}
