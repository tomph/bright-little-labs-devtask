using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    protected abstract void OnInitialised();
    protected abstract void OnMoved(GridObject tile);

    public void Init(int _x, int _y)
    {
        x = _x;
        y = _y;

        OnInitialised();
    }

    public void Move(GridObject tile)
    {
        x = tile.x;
        y = tile.y;

        OnMoved(tile);
    }

    public int x { get; private set; }
    public int y { get; private set; }
}
