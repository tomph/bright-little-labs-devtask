# Tom Platten-Higgins Dev Task (Bright Little Labs)

A minigame built in Unity that generates it's own levels

## Getting Started

To load the game, please go to https://www.tomph.com/staging/brightlittlelabs/

### Controls

UP/DOWN/RIGHT/LEFT arrow keys

### Automatic Level Generation

The game uses an A* Pathfinding algorithm to spawn enemies onto unoccupied tiles, while always leaving a clear path between player and target objects.  Once a level is completed, an additional enemy is added and a brand new (random) level is generated.

This process is not perfect, as it can occasionally produce easy levels later in the game (a clear left-to-right path for example).  The algorithm would need further work in order to ensure a reliable difficulty curve.

A 4x4 grid is able to produce 9 unique levels, on the basis of one enemy being added each time.  

### Manual Level Generation

Alternatively, levels could be created manually using a tile editor, such as Tiled (https://www.mapeditor.org/).  This software is capable of exporting many data formats, but the preferred type for Unity would be CSV, wrapped in a JSON file.  The advantages of manual level generation are:

1.  Finer control of design and level difficulty.
2.  Static data is permanent (level 5 could always be the same)
3.  Level data is portable and could be generated and hosted remotely  
4.  Level design could be abstracted, while the game is still in development

Screenshot of Tiled and it's exported data:

![Screenshot of Tiled and it's exported data](https://www.tomph.com/staging/brightlittlelabs/Github/tiled_level_editor.png)

## Built With

* [Unity](http://www.unity3d.com) - Unity

## Authors

* **Tom Platten-Higgins**

## Acknowledgments

* [2dTileBasedPathFinding](https://github.com/RonenNess/UnityUtils/tree/master/Controls/PathFinding/2dTileBasedPathFinding) - Thanks to RonenNess for the excellent Pathfinding library

