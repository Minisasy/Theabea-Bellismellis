using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWin : MonoBehaviour
{
    [SerializeField] bool ifTouchLose;
    [SerializeField] Vector3 respawn;
    private void OnTriggerEnter(Collider other)
    {
        if (ifTouchLose == true)
        {
            if (other.tag == "Blue" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().RedTotalScore(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.tag == "Red" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().BlueTotalScore(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


        if (ifTouchLose == false)
        {
            if (other.tag == "Blue" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().BlueTotalScore(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            if (other.tag == "Red" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().RedTotalScore(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }


        if (this.gameObject.tag == "Death" && other.gameObject.tag != "Ball")
        {
            other.GetComponent<PlayerController>().PlayerRespawn(respawn);
        }
        if (other.gameObject.tag == "Ball")
        {
            other.gameObject.SetActive(false);
        }
    }
}
