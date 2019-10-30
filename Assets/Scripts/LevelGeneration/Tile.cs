using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : GridObject
{
    protected override void OnInitialised()
    {
        this.name = "Tile: " + (x + y).ToString();
        Occupied = false;
    }

    public bool Occupy()
    {
        if(!Occupied)
        {
            Occupied = true;
            return true;
        }

        return false;
    }

    protected override void OnMoved(GridObject tile)
    {
        
    }

    public bool Occupied { get; private set; }

}
