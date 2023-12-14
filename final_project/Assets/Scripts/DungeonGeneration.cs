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
    public Spawner spawner;
    public Transform Player;

    // Path Directions
    private List<string> path = new List<string>();
    public List<Vector2Int> WorldPositions;
    private Vector2Int spawnPosition = new Vector2Int();
    private List<Vector2Int> firstRoom = new List<Vector2Int>();
    private List<Vector2Int> secondRoom = new List<Vector2Int>();

    // Track the chunk and all its positions.
    // Spawn 2 rooms.
    // When the player enters a new door. Spawn the List[room + 1] until we reach the end of the list.

    void Start() 
    {
        Reset();
        spawnPosition = new Vector2Int(0, 0);
        path = algorithm.GeneratePathDirections();
        SpawnChunks(path);
        spawner.SpawnChests(WorldPositions);
        spawner.SpawnEnemies(WorldPositions);
		Time.timeScale = 1;
    }

    public void GenerateDungeon()
    {
        Reset();
        spawnPosition = new Vector2Int(0,0);
        path = algorithm.GeneratePathDirections();
        SpawnChunks(path);
        spawner.starterChest();
        spawner.SpawnChests(WorldPositions);
        spawner.SpawnEnemies(WorldPositions);

    }


    public void SpawnChunks(List<string> path)
    {
        //Spawn chunks here.
        int idx = 0;
        foreach (string dir in path)
        {
            // Pass a world position. Must be IEnumerable so we can use AddRange.
            IEnumerable<Vector2Int> currentChunkFloorPositions = chunk.createChunk(dir, spawnPosition);
            // Combine the last spawned chunk positions with the rest of the World Floor positions.
            if (idx >= 2)
            {
                WorldPositions.AddRange(currentChunkFloorPositions);
            }
            else
            {
                if(idx == 1)
                {
                    
                    secondRoom.AddRange(currentChunkFloorPositions);
                    printWorldPositions();
                }

                idx++;
            }

            // Set spawn position for next chunk
            adjustSpawnPosition(dir);
        }

        //printWorldPositions();
    }

    private void adjustSpawnPosition(string dir)
    {
        // Set the spawn position for the next chunk.
        char lastChar = dir[dir.Length - 1];

        int increment = 20;
        switch (lastChar)
        {
            case 'N':
                spawnPosition.y = spawnPosition.y + increment;
                break;
            case 'S':
                spawnPosition.y = spawnPosition.y - increment;
                break;
            case 'E':
                spawnPosition.x = spawnPosition.x + increment;
                break;
            case 'W':
                spawnPosition.x = spawnPosition.x - increment;
                break;
        }

        //Debug.Log(spawnPosition.x + ", " + spawnPosition.y);
    }

    private void printWorldPositions()
    {
        Debug.Log("========= WORLD POSITIONS =========");
        foreach (Vector2Int position in secondRoom)
        {
            //Debug.Log(position.x + ", " + position.y);
        }
    }

    

    public void Reset()
    {
        tp.Clear();
        spawner.Clear();
        path.Clear();
        WorldPositions.Clear();
    }
}