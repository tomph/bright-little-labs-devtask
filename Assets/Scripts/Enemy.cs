using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GridObject
{
    protected override void OnInitialised()
    {
        
    }

    protected override void OnMoved(GridObject tile)
    {
        this.transform.position = tile.transform.position;

    }
}
