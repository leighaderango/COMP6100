using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Chunk : MonoBehaviour
{
    public static int size = 20;
    private static string[,] grid = new string[size, size];
    public TilePainter tp;

    // Wall Info. These are parallel lists.
    private List<Vector2Int> WallPositions = new List<Vector2Int>();
    private List<string> TileCodes = new List<string>();
    private List<string> DoorDirections = new List<string>();

    // All Positions of tiles
    private List<Vector2Int> positions = new List<Vector2Int>();

    public void createChunk(string direction)
    {
        Reset();
        initializeChunkGrid();
        setDoors(direction);
        createDungeonWalls();

        tp.VisualizeChunk(grid, size, WallPositions, TileCodes, direction);
    }

    public void initializeChunkGrid()
    {
        // Sets the floors and walls of the grid.
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                //Record the position
                Vector2Int currentPosition = new Vector2Int(x, y);
                positions.Add(currentPosition);
                //Debug.Log(currentPosition);

                // Outermost cells will be set to wall.
                if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                {
                    grid[x, y] = "1"; // Set to wall
                }
                else
                {
                    grid[x, y] = "0"; // Set to floor
                }

            }
        }

    }

    private void setDoors(string directions)
    {
        DoorDirections = directions.Select(c => c.ToString()).ToList();

        foreach (string direction in DoorDirections)
        {
            switch(direction)
            {
                case "N":
                    // Set position to floor to create a door.
                    //Vector2Int left = new Vector2Int(0, size / 2);
                    //Vector2Int right = new Vector2Int(0, size / 2);
                    
                    grid[(int)(size / 2) - 1, size-1] = "0";
                    grid[(int)(size / 2), size-1] = "0";
                    //WallPositions.Remove(pos);
                    // Set wallCornerLeft and wallCornerRight to 1
                    break;
                case "S":
                    // Set position to floor to create an open door.
                    grid[(int)(size / 2) - 1, 0] = "0";
                    grid[(int)(size / 2), 0] = "0";
                    // Set wallCornerLeft and wallCornerRight to 1
                    break;
                case "W":
                    // Set position to floor to create an open door.
                    grid[0, (int)(size / 2)] = "0";
                    grid[0, (int)(size / 2)+1] = "0";
                    // Set wallCornerLeft and wallCornerRight to 1
                    break;
                case "E":
                    // Set position to floor to create an open door.
                    grid[size - 1, (int)(size / 2)] = "0";
                    grid[size - 1, (int)(size / 2) + 1] = "0";
                    // Set wallCornerLeft and wallCornerRight to 1
                    break;
            }

        }
        
    }

    private void createDungeonWalls()
    {
        foreach (var p in positions)
        {
            // Check if current positon is a wall.
            if (grid[p.x, p.y] == "1")
            {
                // Add wall position to list.
                WallPositions.Add(p);
                CheckAdjacentCells(p);
            }
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
            int row = cp.x + dx[d];
            int col = cp.y + dy[d];

            if (row >= 0 && row < size && col >= 0 && col < size)
            {
                // Look for adjacent floors. Record their position.
                if (grid[row, col] == "0")
                {
                    tileCode += d;
                }
            }
        }

        // Add tile code to list.
        TileCodes.Add(tileCode);
        tileCode = "";
    }

    public void Reset()
    {
        Array.Clear(grid, 0, grid.Length);
        WallPositions.Clear();
        TileCodes.Clear();
        positions.Clear();
        // tp.Clear();
    }
}
