using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] bool red = false;
    [SerializeField] bool blue = false;

    int points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (blue == true)
            {
                FindObjectOfType<GameController>().RedTotalScore(points);
                points = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (red == true)
            {
                FindObjectOfType<GameController>().BlueTotalScore(points);
                points = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
