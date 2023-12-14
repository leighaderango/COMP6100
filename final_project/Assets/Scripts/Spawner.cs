using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> chests = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public GameObject ControlCollider;

    private List<Vector2Int> chestPositions = new List<Vector2Int>();
    
    private List<GameObject> spawnedChests = new List<GameObject>();
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private GameObject spawnedController;

    public int numOfChests = 10;
    public int numOfEnemies = 20;

    public void Start()
    {
        numOfChests = Random.Range(5, 20);
    }

    public void SpawnEnemies(List<Vector2Int> availableSpawnPositions)
    {
        for(int i = 0; i < numOfEnemies; i++)
        {
            int randPosition = Random.Range(0, availableSpawnPositions.Count);
            int pickRandEnemy = Random.Range(0, enemies.Count);
            if (!chestPositions.Contains(availableSpawnPositions[randPosition]))
            {
                GameObject obj = Instantiate(enemies[pickRandEnemy], (Vector3Int)availableSpawnPositions[randPosition], Quaternion.identity);
                switch (pickRandEnemy)
                {
                    case 0:
                        obj.name = "FlyingEye";
                        break;
                    case 1:
                        obj.name = "Goblin";
                        break;
                    case 2:
                        obj.name = "Necromancer";
                        break;
                    case 3:
                        obj.name = "Skeleton";
                        break;
                }
                spawnedEnemies.Add(obj);
            } 
        }
    }

    public void starterChest()
    {
        Vector2Int startPos = new Vector2Int(10, 10);
        GameObject obj = Instantiate(chests[1], (Vector3Int)startPos, Quaternion.identity);
        obj.name = "chest_gold";
        spawnedChests.Add(obj);
        Debug.Log("Starter chest spawned");
    }

    public void SpawnControlCollider(string dir)
    {
        switch (dir)
        {
            case "N":
                spawnedController = Instantiate(ControlCollider, new Vector3(10.5f, 20f, 0f), Quaternion.identity);
                break;
            case "S":
                spawnedController = Instantiate(ControlCollider, new Vector3(10.5f, 0f, 0f), Quaternion.identity);
                break;
            case "E":
                spawnedController = Instantiate(ControlCollider, new Vector3(20f, 10.5f, 0f), Quaternion.identity);
                spawnedController.transform.Rotate(0f, 0f, 90f, Space.Self);
                break;
            case "W":
                spawnedController = Instantiate(ControlCollider, new Vector3(20f, 10.5f, 0f), Quaternion.identity);
                spawnedController.transform.Rotate(0f, 0f, 90f, Space.Self);
                break;
        }
    }

    public void SpawnChests(List<Vector2Int> availableSpawnPositions)
    {
        for (int i = 0; i < numOfChests; i++)
        {
            int randPosition = Random.Range(0, availableSpawnPositions.Count);
            int pickRandChest = Random.Range(0, chests.Count);
            GameObject obj = Instantiate(chests[pickRandChest], (Vector3Int)availableSpawnPositions[randPosition], Quaternion.identity);
            switch (pickRandChest)
            {
                case 0:
                    obj.name = "chest_blue";
                    break;
                case 1:
                    obj.name = "chest_gold";
                    break;
                case 2:
                    obj.name = "chest_red";
                    break;
            }
            //Debug.Log(availableSpawnPositions[randPosition]);
            spawnedChests.Add(obj);
            chestPositions.Add(availableSpawnPositions[randPosition]);
        }

        //printChestPositions();
    }

    private void printChestPositions()
    {
        Debug.Log("CHEST POSITIONS:");
        foreach (Vector2Int position in chestPositions)
        {
            Debug.Log(position.x + ", " + position.y);
        }
    }

    public void Clear()
    {
       foreach (GameObject obj in spawnedChests)
       {
            DestroyImmediate(obj, true);
       }
       foreach(GameObject obj in spawnedEnemies)
       {
            DestroyImmediate(obj, true);
        }
       DestroyImmediate(spawnedController, true);
        chestPositions.Clear();
    }
    
}
