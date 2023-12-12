using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGeneration : MonoBehaviour
{
    
    public TilePainter tp;
    public Algorithms algorithm;
    public Chunk chunk;
    public Transform Player;

    public int MazeComplexity;
    public int numberOfRooms = 8;

    // Dungeon Size
    public static int gridSize = 50;
    private static string[,] grid = new string[gridSize,gridSize];

    // Path Directions
    private List<string> path = new List<string>();

    // Wall Info. These are parallel lists.
    private List<Vector2Int> WallPositions = new List<Vector2Int>();
    private List<string> TileCodes = new List<string>();

    // All Positions of tiles
    private List<Vector2Int> positions = new List<Vector2Int>();

    // Position of all blocks generated to create maze effect.
    private HashSet<Vector2Int> blocks = new HashSet<Vector2Int>();

    void Start() {
        Reset();
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        Reset();
        path = algorithm.GeneratePathDirections();

        initializeDungeonGrid(path);

        
        //printDungeon();

        //SetEnemies();
        //SetCollectibles();

        /*var unique = TileCodes.Distinct().ToList();
        foreach (var tileCode in unique)
        {
            //Debug.Log(tileCode);
        }*/
    }


    public void initializeDungeonGrid(List<string> path)
    {
        // Do we want to handle spawning here? Yes
        foreach(string dir in path) {

            chunk.createChunk(dir);
        }
    }

    // =====================================================

    public void Reset()
    {
        Array.Clear(grid, 0, grid.Length);
        WallPositions.Clear();
        TileCodes.Clear();
        positions.Clear();
        blocks.Clear();
        tp.Clear();
    }
    public void printDungeon() {
        for(int x = 0; x < grid.GetLength(0); x++) {
            string r = "";
            for(int y = 0; y <  grid.GetLength(1); y++) {
                r += grid[x, y];
            }
            Debug.Log(r);
        }
    }
}