using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWin : MonoBehaviour
{
    [SerializeField] bool ifTouchLose;
    [SerializeField] Vector3 respawn;
    [SerializeField] ParticleSystem psRed;
    [SerializeField] ParticleSystem psBlue;

    float time = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (ifTouchLose == true)
        {
            if (other.tag == "Blue" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().RedTotalScore(1);
                StartCoroutine(ChangeScenes());
            }
            if (other.tag == "Red" && this.gameObject.tag != "Death")
            {
                FindObjectOfType<GameController>().BlueTotalScore(1);
                StartCoroutine(ChangeScenes());
            }
        }


        if (ifTouchLose == false)
        {
            if (other.tag == "Blue" && this.gameObject.tag != "Death")
            {
                psBlue.Play();
                FindObjectOfType<GameController>().BlueTotalScore(1);
                StartCoroutine(ChangeScenes());
            }
            if (other.tag == "Red" && this.gameObject.tag != "Death")
            {
                psRed.Play();
                FindObjectOfType<GameController>().RedTotalScore(1);
                StartCoroutine(ChangeScenes());

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

        IEnumerator ChangeScenes()
        {
            yield return new WaitForSecondsRealtime(time);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
