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
    public List<Vector2Int> WorldPositions = new List<Vector2Int>();
    private Vector2Int spawnPosition = new Vector2Int();
    private List<Vector2Int> secondRoom = new List<Vector2Int>();
    private List<Vector2Int> lastRoom = new List<Vector2Int>();

    void Start() 
    {
        Reset();
        spawnPosition = new Vector2Int(0, 0);
        path = algorithm.GeneratePathDirections();
        SpawnChunks(path);  // For loop that spawns all the chunks.
        spawner.starterChest();
        spawner.SpawnControlCollider(path[0]);
        spawner.SpawnChests(WorldPositions);
        spawner.SpawnEnemies(WorldPositions);
		Time.timeScale = 1;
    }

    public void GenerateDungeon()
    {
        Reset();
        spawnPosition = new Vector2Int(0,0);
        path = algorithm.GeneratePathDirections();
        SpawnChunks(path);  // For loop that spawns all the chunks.
        spawner.starterChest();
        spawner.SpawnControlCollider(path[0]);
        spawner.SpawnChests(WorldPositions);
        spawner.SpawnEnemies(WorldPositions);
        Time.timeScale = 1;
        //printWorldPositions();

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
            
            if (idx == 1)
            {

                secondRoom.AddRange(currentChunkFloorPositions);
            }

            if (idx == path.Count-1)
            {
                lastRoom.AddRange(currentChunkFloorPositions);
                tp.paintEndRoom(lastRoom[175]);
                spawner.SpawnEndGameCollider(lastRoom[175]);
            }

            idx++;

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
        int i = 0;
        Debug.Log("========= WORLD POSITIONS =========");
        foreach (Vector2Int position in lastRoom)
        {
            Debug.Log(i + " => " + position.x + ", " + position.y);
            i++;
        }
    }

    public void Reset()
    {
        lastRoom.Clear();
        tp.Clear();
        spawner.Clear();
        path.Clear();
        WorldPositions.Clear();
    }
}