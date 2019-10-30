using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : GridObject
{
    public Tile current { get; private set; }


    protected override void OnInitialised()
    {
        
    }

    protected override void OnMoved(GridObject tile)
    {
        this.transform.position = tile.transform.position;
        current = tile as Tile;
    }

}
