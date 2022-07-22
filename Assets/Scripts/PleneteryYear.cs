using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleneteryYear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Sun")
        {
            Debug.Log(other.gameObject.name + " " + "revolved around the sun."); //Each tour around sun. Script in "Wall" object
        }
        
        
    }
}
