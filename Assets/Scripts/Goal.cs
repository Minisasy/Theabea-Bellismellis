using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] bool red = false;
    [SerializeField] bool blue = false;
    [SerializeField] ParticleSystem ps;

    int points = 1;
    float time = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (blue == true)
            {
                ps.Play();
                FindObjectOfType<GameController>().RedTotalScore(points);
                points = 0;
                StartCoroutine(ChangeScenes());
            }
            if (red == true)
            {
                ps.Play();
                FindObjectOfType<GameController>().BlueTotalScore(points);
                points = 0;
                StartCoroutine(ChangeScenes());
            }
        }
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
