using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGeneration : MonoBehaviour
{
    // Set number of rooms you would like to spawn
    public int numRooms;
    // 2D array of Room objects representing the layout of the dungeon.
    private Room[,] allRooms;

    private Room currentRoom;
    private static DungeonGeneration instance = null;

    void Awake() {
        // Since loading a new scene destroys all current scene objects. We mus make the dungeon object persistent.
        // Ensure's that the dungeon will not be destroyed after restarting the scene.
        if(instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            this.currentRoom = GenerateDungeon();
        } else {
            string roomPrefab = instance.currentRoom.PrefabRoomName();
            GameObject roomObj = (GameObject) Instantiate(Resources.Load(roomPrefab));
            Destroy(this.gameObject);
        }
    }

    void Start(){
        string roomPrefab = this.currentRoom.PrefabRoomName();
        // printDungeon();
        GameObject roomObj = (GameObject)Instantiate(Resources.Load(roomPrefab));
        Debug.Log(roomPrefab);
    }

    private Room GenerateDungeon(){
        // Multiply by 3 to ensure that all rooms fit on each axis.
        int size = 3 * numRooms;
        // 2D array to represent the dungeon grid.
        allRooms = new Room[size, size];

        // Initial spawn room will always be in the middle of the grid.
        Vector2Int initialSpawnCoords = new Vector2Int ((size/2)-1, (size/2)-1);

        Queue<Room> roomsLeft = new Queue<Room>();
        roomsLeft.Enqueue(new Room(initialSpawnCoords.x, initialSpawnCoords.y));
        // List of all created Rooms
        List<Room> createdRooms = new List<Room>();

        // Iterates through all rooms that need to be created (Queue) then appends them to created Rooms (List)
        while (roomsLeft.Count > 0 && createdRooms.Count < numRooms){
            Room currRoom = roomsLeft.Dequeue();
            // Marks the room in the grid as created
            this.allRooms[currRoom.roomCoords.x, currRoom.roomCoords.y] = currRoom;
            createdRooms.Add(currRoom);
            // Enqueue's the neighboring rooms for future processing.
            AddNeighbors(currRoom, roomsLeft);
        }

        // Connects neighboring rooms.
        foreach(Room r in createdRooms){
            List<Vector2Int> neighborCoords = r.neighborCoords();
            foreach(Vector2Int coord in neighborCoords){
                // If the neighbor coordinates match then the room is not null and we can connect the rooms
                Room n = this.allRooms[coord.x, coord.y];
                if(n!=null){
                    r.Connect(n);
                }
            }
        }

        return this.allRooms[initialSpawnCoords.x, initialSpawnCoords.y];
    }

    private void AddNeighbors(Room currRoom, Queue<Room> roomsLeft){
        List<Vector2Int> neighborCoords = currRoom.neighborCoords();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();

        // Checks each coordinate in neighbor coords.
        foreach(Vector2Int coords in neighborCoords){
            // Coords are only added if there is no room in that position.
            if(this.allRooms[coords.x, coords.y] == null){
                availableNeighbors.Add(coords);
            }
        }
        // Determines number of neighbors to be added
        int numNeighbors = (int)Random.Range(1, availableNeighbors.Count);

        for(int nIndex = 0; nIndex < numNeighbors; nIndex++){
            float randomVal = Random.value;  // Range(0,1)
            float roomFrac = 1f / (float)availableNeighbors.Count;
            Vector2Int chosenNeighbor = new Vector2Int(0,0);

            foreach (Vector2Int coord in availableNeighbors){
                if(randomVal < roomFrac){
                    chosenNeighbor = coord;
                    break;
                } else {
                    roomFrac += 1f /(float)availableNeighbors.Count;
                }
            }

            roomsLeft.Enqueue(new Room(chosenNeighbor));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    private void printDungeon() {
        for (int row = 0; row < this.allRooms.GetLength(1); row++) {
            string r = "";
            for (int col = 0; col < this.allRooms.GetLength(0); col++) {
                if (this.allRooms[col, row] == null) {
                    r += "X";
                }else {
                    r += "R";
                }
            }
            Debug.Log(r);
        }
    }

    // Sets the room that the player has moved to.
    public void MoveToNextRoom(Room room) {
        this.currentRoom = room;
    }
    // Getter to help identify the next room for the doors using Enter.cs
    public Room getCurrentRoom() {
        return this.currentRoom;
    }

    public void ResetDungeon() {
        this.currentRoom = GenerateDungeon();
    }
}
