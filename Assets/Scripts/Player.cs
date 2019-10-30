using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GridObject
{
    private TrailRenderer _trail;
    private Vector3 _walkTarget;

    [SerializeField]
    private float _walkSpeed = 0.04f;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    protected override void OnInitialised()
    {
        _trail.Clear();
        _walkTarget = transform.position;
    }

    protected override void OnMoved(GridObject tile)
    {
        this.transform.position = tile.transform.position;
        current = tile as Tile;
    }

    public void Walk(GridObject tile)
    {
        _walkTarget = tile.transform.position;
    }

    private void Update()
    {
        if (_walkTarget != null && !paused) transform.position = Vector3.MoveTowards(transform.position, _walkTarget, _walkSpeed);
    }

    public Tile current { get; private set;}
    

    //Events
    public DirectionEvent ON_DIRECTION_CHANGE { get { return GetComponent<PlayerControl>().ON_DIRECTION;}}

    public bool paused { get; set; }
}
