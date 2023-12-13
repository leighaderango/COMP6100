using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionChest : MonoBehaviour
{

    public int ChestsInRange = 0;
    [SerializeField] private List<GameObject> chestsInArea;

    void Update() {

        if(Input.GetButtonDown("Interact")) 
        {
            Debug.Log("Button Pressed");
            // Pass items to inventory here.
            // Basically just pass the (List<string> chest) to your function and add to inventory.
            foreach(GameObject c in chestsInArea)
            {
                c.GetComponent<Chest>().isOpen = true;
            }

            ChestsInRange = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Entered = " + other.name);

        if(other.name == "chest_red" || other.name == "chest_blue" || other.name == "chest_gold") {
            ChestsInRange++;
            chestsInArea.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Exited = " + other.name);

         if(other.name == "chest_red" || other.name == "chest_blue" || other.name == "chest_gold") {
            ChestsInRange--;
            chestsInArea.Remove(other.gameObject);
        }
    }

    
}
