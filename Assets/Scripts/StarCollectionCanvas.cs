using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarCollectionCanvas : MonoBehaviour
{
    int points = 1;
    float time = 1.5f;

    private int redStarScore;
    private int blueStarScore;

    [SerializeField] TextMeshProUGUI redStarScoreText = null;
    [SerializeField] TextMeshProUGUI blueStarScoreText = null;

    private void Start()
    {
        redStarScoreText.text = redStarScore.ToString();
        blueStarScoreText.text = blueStarScore.ToString();
    }
    private void Update()
    {
        if (redStarScore == 2 || blueStarScore == 2)
        {
            StarWin();
        }

    }

    void StarWin()
    {
        if (redStarScore == 2)
        {
            Debug.Log("Meow");
            FindObjectOfType<GameController>().RedTotalScore(points);
            points = 0;
            StartCoroutine(ChangeScenes());
        }
        if (blueStarScore == 2)
        {
            Debug.Log("Meow");
            FindObjectOfType<GameController>().BlueTotalScore(points);
            points = 0;
            StartCoroutine(ChangeScenes());
        }
    }

    public void RedStarScore(int stars)
    {
        redStarScore += stars;
        redStarScoreText.text = redStarScore.ToString();
    }
    public void BlueStarScore(int stars)
    {
        blueStarScore += stars;
        blueStarScoreText.text = blueStarScore.ToString();
    }

    IEnumerator ChangeScenes()
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
