using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Chunk : MonoBehaviour
{
    // Local position for the tiles in the chunk.
    public static int size = 20;
    private static string[,] grid = new string[size, size];

    public TilePainter tilePainter;
    public int numberOfChests = 1;

    // Wall Info. These are parallel lists.
    private List<Vector2Int> WallPositions = new List<Vector2Int>();
    private List<string> TileCodes = new List<string>();
    private List<string> DoorDirections = new List<string>();
    List<Vector2Int> DoorPositions = new List<Vector2Int>();

    // World Positions of grid.
    private List<Vector2Int> positions = new List<Vector2Int>();

    // World Floor position of tiles.
    private List<Vector2Int> FloorPositions = new List<Vector2Int>();

    public IEnumerable<Vector2Int> createChunk(string direction, Vector2Int spawnPosition)
    {
        Reset();
        initializeChunkGrid(spawnPosition, direction);
        createDungeonWalls();
        tilePainter.VisualizeChunk(FloorPositions, WallPositions, TileCodes, size);
        //printPositions();

        return FloorPositions;
    }

    public void initializeChunkGrid(Vector2Int startPosition, string dir)
    {
        int top = (int)(size / 2) - 1;
        int middle = (int)(size / 2);
        int bottom = (int)(size / 2) + 1;
        int length = size - 1;
        // Sets the floors and walls of the grid.
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                // Record world position of the tile.
                Vector2Int currentilePosition = new Vector2Int(startPosition.x + x, startPosition.y + y);
                positions.Add(currentilePosition);
                //Debug.Log(currentilePosition);

                // Outermost cells will be set to wall.
                if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                {
                    grid[x, y] = "1"; // Set to wall
                    WallPositions.Add(currentilePosition);
                }
                else
                {
                    grid[x, y] = "0"; // Set to floor
                    FloorPositions.Add(currentilePosition);
                }

                // To spawn a door. Override the wall positions and replace with floor positions.
                // Overwrite any walls and set equal to floor.
                if ( dir.Contains('N') && (grid[top, length] == "1" || grid[middle, length] == "1" || grid[bottom, length] == "1"))
                {
                    grid[x, y] = "0";
                    FloorPositions.Add(currentilePosition);
                    WallPositions.Remove(currentilePosition);
                }

                if (dir.Contains('S') && (grid[top, 0] == "1" || grid[middle, 0] == "1" || grid[bottom, 0] == "1"))
                {
                    grid[x, y] = "0";
                    FloorPositions.Add(currentilePosition);
                    WallPositions.Remove(currentilePosition);
                }

                if(dir.Contains('W') && (grid[0, top] == "1" || grid[0, middle] == "1" || grid[0, bottom] == "1"))
                {
                    grid[x, y] = "0";
                    FloorPositions.Add(currentilePosition);
                    WallPositions.Remove(currentilePosition);
                }
                
                if(dir.Contains('E') && (grid[length, top] == "1" || grid[length, middle] == "1" || grid[length, bottom] == "1"))
                {
                    grid[x, y] = "0";
                    FloorPositions.Add(currentilePosition);
                    WallPositions.Remove(currentilePosition);
                }

            }
        }
    }

    private void createDungeonWalls()
    {
        // Using 2 separate coords. Need to use grid.
        foreach (Vector2Int p in WallPositions)
        {
            // Check if current positon is a wall.
            CheckAdjacentCells(p);
        }
    }

    private void CheckAdjacentCells(Vector2Int cp)
    {

        // 0 1 2
        // 3 c 4
        // 5 6 7 
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        string tileCode = "";

        for (int d = 0; d < 8; d++)
        {
            // Positions to check each adjacent cell
            int row = cp.x + dx[d];
            int col = cp.y + dy[d];
            Vector2Int currentPos = new Vector2Int(row, col);
            if (FloorPositions.Contains(currentPos))
            {
                tileCode += d;
            }
        }

        // Add tile code to list.
        TileCodes.Add(tileCode);
        tileCode = "";
    }

    void printPositions()
    {
        Debug.Log("========= CHUNK FLOOR POSITIONS =========");
        foreach (Vector2Int pos in FloorPositions)
        {
            Debug.Log(pos.x + ", " + pos.y);
        }
    }

    public void Reset()
    {
        Array.Clear(grid, 0, grid.Length);
        WallPositions.Clear();
        FloorPositions.Clear();
        TileCodes.Clear();
        positions.Clear();
        DoorDirections.Clear();
        
        // tilePainter.Clear();
    }
}
