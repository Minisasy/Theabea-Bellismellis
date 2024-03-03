using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject line;

    //Scores
    private int redTotalScore;
    private int blueTotalScore;
    [SerializeField] TextMeshProUGUI redTotalScoreText = null;
    [SerializeField] TextMeshProUGUI blueTotalScoreText = null;

    //Character
    public int controllerSelected;
    public int keyboardSelected;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameController>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        redTotalScoreText.text = redTotalScore.ToString();
        blueTotalScoreText.text = blueTotalScore.ToString();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            line.SetActive(true);
        }
        else
        {
            line.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            redTotalScoreText.gameObject.SetActive(false);
            blueTotalScoreText.gameObject.SetActive(false);
        }
        else
        {
            redTotalScoreText.gameObject.SetActive(true);
            blueTotalScoreText.gameObject.SetActive(true);
        }
        if (redTotalScore == 2 || blueTotalScore == 2)
        {
            WinSequence();
        }
    }
    void WinSequence()
    {
        if (redTotalScore == 2)
        {
            SceneManager.LoadScene(9);
            Destroy(gameObject);
        }
        if (blueTotalScore == 2)
        {
            SceneManager.LoadScene(10);
            Destroy(gameObject);
        }
    }

    public void RedTotalScore(int score)
    {
        redTotalScore += score;
        redTotalScoreText.text = redTotalScore.ToString();
    }
    public void BlueTotalScore(int score)
    {
        blueTotalScore += score;
        blueTotalScoreText.text = blueTotalScore.ToString();
    }
    public void ResetGameSession()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SaveControllerSelection(int selectecChar)
    {
        controllerSelected = selectecChar;
    }
    public void SaveKeyboardSelection(int selectedChar)
    {
        keyboardSelected = selectedChar;
    }
}
