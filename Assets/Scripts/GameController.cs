using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GridController _grid;
    private Player _player;
    private Target _target;

    private bool _complete = false;

    private Enemy[] _enemies;

    public static int LEVEL = 1;

    [SerializeField]
    private LevelGenerationController _levelGenerator;

    [SerializeField]
    private WinPanelController _winPanel;

    private void Awake()
    {
        _winPanel.ON_RESTART.AddListener(OnRestart);
    }

    void Start()
    {
        _levelGenerator.Build(LEVEL);
        Init(_levelGenerator.grid, _levelGenerator.player, _levelGenerator.target);

        _enemies = GetComponentsInChildren<Enemy>();

    }

    private void OnRestart()
    {
        LEVEL = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void Init(GridController grid, Player player, Target target)
    {
        _grid = grid;
        _player = player;
        _target = target;

        _player.ON_DIRECTION_CHANGE.AddListener(OnPlayerDirectionChange);
    }

    private void OnPlayerDirectionChange(Vector2 direction)
    {
        //re-assign closest tile as current
        Tile closest = _grid.GetClosestTile(_player.transform.position);
        if (closest != null) _player.Move(closest);

        //get tile in direction
        Tile tile = _grid.GetTileInDirection(_player, direction);

        //move to tile
        _player.Walk(tile);
    }

    private void Update()
    {
        //Check win state
        if(Vector3.Distance(_player.transform.position, _target.transform.position) < .1f && !_complete)
        {
            _complete = true;

            if(LEVEL < 9)
            {
                LEVEL++;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                //completed the game
                _player.paused = true;
                _winPanel.show = true;
            }
        }


        //Check lose state
        foreach(Enemy e in _enemies)
        {
            if (Vector3.Distance(_player.transform.position, e.transform.position) < .1f && !_complete)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        


    }

    public void Clear()
    {
        _player.ON_DIRECTION_CHANGE.RemoveAllListeners();
    }
}
