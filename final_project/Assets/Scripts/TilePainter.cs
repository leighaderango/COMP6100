using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    public Tilemap terrain;
    public TileBase floor;
    public TileBase end;
    public TileBase[] walls;

    /*private int worldSpawnPositionX = 0;
    private int worldSpawnPositionY = 0;*/

    private Dictionary<string, int> codes = new Dictionary<string, int>()
    {
        // Top 
        {"67",  0   },
        {"567", 0   },
        {"56",  0   },
        // Bottom 
        {"12",  1   },
        {"012", 1   },
        {"01",  1   },
        // Right
        {"47",  2   },
        {"247", 2   },
        {"24",  2   },
        // Left
        {"35",  3   },
        {"035", 3   },
        {"03",  3   },
        // Bottom Left
        {"2",  4   },
        // Top Left
        {"7",  5   },
        // Top right
        {"5",  6   },
        // Bottom Right
        {"0",  7   },
        //Bottom Left Corner
        {"01235", 8    },
        {"0135",  8    },
        {"0123",  8    },
        // Top Left Corner
        {"1247",  9    },
        {"0124",  9    },
        {"01247", 9    },
        // Bottom Right Corner
        {"3567",  10   },
        {"03567", 10   },
        {"0356",  10   },
        
        // Top Right Corner
        {"2467",  11   },
        {"24567", 11   },
        {"4567",  11   }

    };

    public void VisualizeChunk(List<Vector2Int> FloorPositions, List<Vector2Int> WallPositions, List<string> TileCodes, int size)
    {
        paintFloors(FloorPositions);
        // printWallPositions(WallPositions);
        paintWalls(WallPositions, TileCodes);
    }

    void paintFloors(List<Vector2Int> FloorPositions)
    {
        foreach (Vector2Int position in FloorPositions)
        {
            paintTile(floor, position.x, position.y);
        }
    }
    
    public void paintEndRoom(Vector2Int endPosition)
    {
        paintTile(end, endPosition.x, endPosition.y);
    }

    public void printWallPositions(List<Vector2Int> wp)
    {
        foreach(Vector2Int wall in wp)
        {
            Debug.Log(wall.x + ", " + wall.y);
        }
    }

    public void paintWalls(List<Vector2Int> WallPositions, List<string> TileCodes)
    {
        // 30 
        for(int i = 0; i < WallPositions.Count; i++)
        {
            paintSpecificWall(WallPositions[i], TileCodes[i]);
        }
    }

    private void paintSpecificWall(Vector2Int wp, string tc)
    {
        // turn String into TileBase
        int x = wp.x;
        int y = wp.y;
        paintTile(walls[codes[tc]], x, y);
    }

    private void paintTile(TileBase tile, int x, int y)
    {
        // Sets the position of where the tile will be spawned.
        Vector3Int position = new Vector3Int(x, y, 0);
        var tilePosition = terrain.WorldToCell(position);

        //Debug.Log("Tile Position = (" + tilePosition.x + ", " + tilePosition.y + ")");

        terrain.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        //worldSpawnPositionX = 0; worldSpawnPositionY = 0;
        terrain.ClearAllTiles();
    }
}
