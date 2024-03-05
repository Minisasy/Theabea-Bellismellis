using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StarCollection : MonoBehaviour
{


    private int starRed = 0;
    private int starBlue = 0;


    [SerializeField] TextMeshProUGUI redStarScoreText = null;
    [SerializeField] TextMeshProUGUI blueStarScoreText = null;

    private void Start()
    {
        redStarScoreText.text = starRed.ToString();
        blueStarScoreText.text = starBlue.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        
            if (other.tag == "Blue")
            {
              
                starBlue++;
               // blueStarScoreText.text = "Stars: " + starBlue.ToString();
                Destroy(other.gameObject);
            }
            if (other.tag == "Red")
            {
                starRed++;
                //redStarScoreText.text = "Stars: " + starRed.ToString();
                Destroy(other.gameObject);
            }
        
    }
  
}
