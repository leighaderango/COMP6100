using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionChest : MonoBehaviour
{
    
    public int ChestsInRange = 0;
    public Canvas InventoryCanvas;
    [SerializeField] private List<GameObject> chestsInArea;
    bool canvasIsOpen = false;

    void Update() {

        if(Input.GetButtonDown("Interact")) 
        {
            //Debug.Log("Button Pressed");
            // Pass items to inventory here.
            // Basically just pass the (List<string> chest) to your function and add to inventory.
            foreach(GameObject c in chestsInArea)
            {
                c.GetComponent<Chest>().isOpen = true;
            }

            ChestsInRange = 0;
        }

        if(Input.GetButtonDown("Inventory") && canvasIsOpen)
        {
            InventoryCanvas.GetComponent<Canvas>().enabled = false;

            canvasIsOpen = false;

            Debug.Log("Canvas OPEN");
        }


        if (Input.GetButtonDown("Inventory") && !canvasIsOpen)
        {
            InventoryCanvas.GetComponent<Canvas>().enabled = true;

            canvasIsOpen = true;
            Debug.Log("Canvas CLOSED");

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
