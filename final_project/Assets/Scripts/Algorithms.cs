using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms : MonoBehaviour
{
    

    public List<string> GeneratePathDirections()
    {
        List<string> directions = new List<string>() { "N", "E", "S", "W" };

        string path = "";

        int pathLength = 10;

        // Pick a random direction to start
        string currentDirection = directions[Random.Range(0, directions.Count)];
        // Add initial direction to path
        path = path + currentDirection;
        pathLength--;
        // {N, SN, SW, E}
        for (int i = 0; i < pathLength; i++)
        {   
            // Skip over until last iteration
            if (i == pathLength - 1)
            {
                // Add one last room with dead end.
                path  = path + "," + GetCorrespondingDirection(currentDirection);
                Debug.Log(path);
                string[] splitArr = path.Split(',');
                List<string> p = new List<string>(splitArr);
                //printPath(p);
                return p;
            }

            //Get the corresponding directon {N -> S}, {E -> W}... etc.
            string next = GetCorrespondingDirection(currentDirection);

            // Add next direction to path N,S
            path = path + "," + next;

            //Update currentDirectin to get the next corresponding direction.
            currentDirection = AddRoomDirection(next, directions);

            // Add the new direction to the path. Ex. {N, SW, EN}
            path = path + currentDirection;
        }

        return new List<string>();
    }

    private static string AddRoomDirection(string direction, List<string> dirs)
    {
        // Remove current direction from list then pick a direction 
        dirs.Remove(direction);

        return dirs[Random.Range(0, dirs.Count)];
    }


    private static string GetCorrespondingDirection(string direction)
    {
        switch(direction)
        {
            case "N": return "S";
            case "S": return "N";
            case "E": return "W";
            case "W": return "E";
        }

        return null;
    }

    private void printPath(List<string> path)
    {
        foreach (string dir in path)
        {
            Debug.Log(dir); 
        }
    }
}
