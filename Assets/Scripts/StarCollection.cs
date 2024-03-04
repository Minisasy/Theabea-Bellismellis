using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StarCollection : MonoBehaviour
{
    private int Star = 0;

    public TextMeshProUGUI starText;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Star")
        {
            Star++;
            starText.text = "Stars: " + Star.ToString();
            Debug.Log(Star);
            Destroy(other.gameObject);
        }
    }
}
