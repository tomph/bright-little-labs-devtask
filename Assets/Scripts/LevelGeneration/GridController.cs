using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridController : MonoBehaviour
{
    [SerializeField]
    private Tile _tilePrefab;

    private Tile[,] _tiles;
    private int _height;

    public int width { get; private set; }
    public int height { get; private set; }

    public void Init(int _width, int _height)
    {
        width = _width;
        height = _height;

        _tiles = new Tile[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Tile tile = Instantiate(_tilePrefab, transform);
                tile.Init(x, y);

                _tiles[x, y] = tile;
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public Tile GetClosestTile(Vector3 position)
    {
        float closest = Mathf.Infinity;
        Tile tile = null;

        foreach(Tile t in _tiles)
        {
            float d = Vector3.Distance(position, t.transform.position);
            if(d < closest)
            {
                closest = d;
                tile = t;
            }
        }

        return tile;
    }

    internal Tile GetTileInDirection(GridObject obj, Vector2 direction)
    {
        //current
        Tile t = _tiles[obj.x, obj.y];

        //horizontal
        if(direction.y == 0)
        {
            if (direction.x > 0) t = _tiles[width-1, t.y];
            else t = _tiles[0, t.y];
        }
        //vertical
        else
        {
            if (direction.y > 0) t = _tiles[t.x, height-1];
            else t = _tiles[t.x, 0];
        }

        return t;
    }

    internal bool[,] GenerateMap()
    {
        bool[,] map = new bool[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = !_tiles[x, y].Occupied;
            }
        }

        return map;
    }

    internal Tile GetUnoccupiedTile(int[] rangeX = null, int[] rangeY = null, GridObject[] exclusions = null)
    {
        //TODO
        //do a check to see if completely filled first...

        int[] xTiles = rangeX == null ? new int[] { 0, 1, 2, 3 } : rangeX;
        int[] yTiles = rangeY == null ? new int[] { 0, 1, 2, 3 } : rangeY;

        int randomX = xTiles[UnityEngine.Random.Range(0, xTiles.Length)];
        int randomY = yTiles[UnityEngine.Random.Range(0, yTiles.Length)];

        while (_tiles[randomX, randomY].Occupied == true || (exclusions != null && Array.IndexOf(exclusions, _tiles[randomX, randomY]) >= 0))
        {
            randomX = xTiles[UnityEngine.Random.Range(0, xTiles.Length)];
            randomY = yTiles[UnityEngine.Random.Range(0, yTiles.Length)];
        }

        return _tiles[randomX, randomY];
    }

    public void OccupyPreferredTile(int[] preferredX, int[] preferredY, GridObject gridObject)
    {
        Tile tile = GetUnoccupiedTile(preferredX, preferredY);
        //tile.Occupy();

        gridObject.Move(tile);
    }
}
