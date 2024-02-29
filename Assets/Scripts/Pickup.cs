using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Blue")
        {
            Destroy(gameObject);
        }
    }
}
