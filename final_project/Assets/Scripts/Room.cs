using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2Int roomCoords;
    public Dictionary<string, Room> neighbors;

    // Initialize dictionary with neighbor info and room coords.
    public Room (int x, int y) {
        this.roomCoords = new Vector2Int(x, y);
        this.neighbors = new Dictionary<string, Room>();
    }

    public Room (Vector2Int coords) {
        this.roomCoords = coords;
        this.neighbors = new Dictionary<string, Room>();
    }

    public List<Vector2Int> neighborCoords() {
        List<Vector2Int> neighborCoords = new List<Vector2Int>();
        neighborCoords.Add(new Vector2Int(this.roomCoords.x, this.roomCoords.y - 1));
        neighborCoords.Add(new Vector2Int(this.roomCoords.x + 1, this.roomCoords.y));
        neighborCoords.Add(new Vector2Int(this.roomCoords.x, this.roomCoords.y + 1));
        neighborCoords.Add(new Vector2Int(this.roomCoords.x - 1, this.roomCoords.y));

        return neighborCoords;
    }

    public void Connect(Room neighbor) {
        string dir = "";
        if(neighbor.roomCoords.y < this.roomCoords.y) {
            dir = "N";
        }
         if(neighbor.roomCoords.x > this.roomCoords.x) {
            dir = "E";
        }
         if(neighbor.roomCoords.y > this.roomCoords.y) {
            dir = "S";
        }
         if(neighbor.roomCoords.x < this.roomCoords.x) {
            dir = "W";
        }

        this.neighbors.Add(dir, neighbor);
    }

    // Getter: Access's neighbors dictionary to get nieghbors direction.
    public Room neighbor(string dir) {
        return this.neighbors[dir];
    }

    public string PrefabRoomName() {
        string name = "Room_";
        foreach (KeyValuePair<string, Room> neighborPair in neighbors) {
            name+=neighborPair.Key;
        }

        return name;
    }
}
