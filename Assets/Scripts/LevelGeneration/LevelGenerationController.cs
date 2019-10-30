using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NesScripts.Controls.PathFind;

public class LevelGenerationController : MonoBehaviour
{
    [SerializeField]
    private GridController _grid;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Target _target;

    [SerializeField]
    private Enemy _enemyPrefab;

    [SerializeField]
    private Transform _enemyContainer;

    private bool[,] _map;

    // Start is called before the first frame update
    public void Build(int level)
    {
        BuildGrid(4,4);
        InitPlayerAndTarget();
        AddEnemies(level);
    }

    private void AddEnemies(int count)
    {
        //generate a* map
        _map = _grid.GenerateMap();

        //generate grid
        NesScripts.Controls.PathFind.Grid grid = new NesScripts.Controls.PathFind.Grid(_map);
       
        //path tests
        NesScripts.Controls.PathFind.Point _from = new NesScripts.Controls.PathFind.Point(_player.x, _player.y);
        NesScripts.Controls.PathFind.Point _to = new NesScripts.Controls.PathFind.Point(_target.x, _target.y);

        int _c = count;

        List<Tile> _uTiles = _grid.GetUnoccupiedTiles(null, null, new GridObject[] { _player.current, _target.current });

        for (int i = 0; i < _uTiles.Count; i++)
        {
            Tile tile = _uTiles[i];
            _map[tile.x, tile.y] = false;
            grid.UpdateGrid(_map);

            if (NesScripts.Controls.PathFind.Pathfinding.FindPath(grid, _from, _to, Pathfinding.DistanceType.Manhattan).Count > 0)
            {
                //occupy tile
                tile.Occupy();

                //add and move enemy 
                Enemy e = Instantiate(_enemyPrefab, _enemyContainer.transform);
                e.Move(tile);

                _c--;


                if (_c == 0) break;
            }
            else
            {
                _map[tile.x, tile.y] = true;
            }
        }

    }

    private void InitPlayerAndTarget()
    {
        //horizontal 
        if(Rnd == true)
        {
            //left?
            if(Rnd == true)
            {
                _grid.OccupyPreferredTile(new int[] {0}, new int[] {0,1,2,3}, _player);
                _grid.OccupyPreferredTile(new int[] {3}, new int[] { 0, 1, 2, 3 }, _target);
            }
            //right?
            else
            {
                _grid.OccupyPreferredTile(new int[] {3}, new int[] { 0, 1, 2, 3 }, _player);
                _grid.OccupyPreferredTile(new int[] {0}, new int[] { 0, 1, 2, 3 }, _target);
            }
        }
        //vertical
        else
        {
            //top?
            if(Rnd == true)
            {
                _grid.OccupyPreferredTile(new int[] { 0, 1, 2, 3 }, new int[] { 0 }, _player);
                _grid.OccupyPreferredTile(new int[] { 0, 1, 2, 3 }, new int[] { 3 }, _target);

            }
            //bottom?
            else
            {
                _grid.OccupyPreferredTile(new int[] { 0, 1, 2, 3 }, new int[] {3}, _player);
                _grid.OccupyPreferredTile(new int[] { 0, 1, 2, 3 }, new int[] { 0 }, _target);
            }
        }

        _player.Init(_player.x, _player.y);
        _target.Init(_target.x, _target.y);
    }

    private void BuildGrid(int width, int height)
    {
        _grid.Init(width, height);
    }

    private bool Rnd
    {
        get
        {
            return UnityEngine.Random.Range(0f, 100f) <= 50f;
        }
    }

    public Player player { get { return _player; } }
    public Target target { get { return _target; } }
    public GridController grid { get { return _grid; } }
}
