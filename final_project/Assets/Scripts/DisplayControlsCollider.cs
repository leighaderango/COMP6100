using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayControlsCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Set controls canvas to true.
        Debug.Log("Show control canvas");
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Set Controls canvas to false
        Debug.Log("Hide control canvas");

    }
}
