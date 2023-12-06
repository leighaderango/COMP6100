using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enter : MonoBehaviour
{

    public string dir;

    void OnCollisionEnter2D(Collision2D col){
        Debug.Log(col);
        if(col.gameObject.tag == "Player") {
            GameObject dungeon = GameObject.FindGameObjectWithTag("Dungeon");
            Debug.Log(dungeon);
            DungeonGeneration dg = dungeon.GetComponent<DungeonGeneration>();
            Room r = dg.getCurrentRoom();
            dg.MoveToNextRoom(r.neighbor(this.dir));
            SceneManager.LoadScene("TheDungeon");
            Debug.Log("Entered new room");
        }
    }
}
