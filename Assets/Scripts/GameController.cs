using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject line;

    //Scores
    private int redTotalScore;
    private int blueTotalScore;
    [SerializeField] TextMeshProUGUI redTotalScoreText = null;
    [SerializeField] TextMeshProUGUI blueTotalScoreText = null;

    //Stamina bar
    [SerializeField] ScriptableVariableSave variableSave;
    [SerializeField] Slider sliderRed;
    [SerializeField] Slider sliderBlue;

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
        if (SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 9 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            line.SetActive(true);
            sliderRed.gameObject.SetActive(true);
            sliderBlue.gameObject.SetActive(true);
        }
        else
        {
            line.SetActive(false);
            sliderRed.gameObject.SetActive(false);
            sliderBlue.gameObject.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 5)
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
            SceneManager.LoadScene(13);
            Destroy(gameObject);
        }
        if (blueTotalScore == 2)
        {
            SceneManager.LoadScene(14);
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

    public void StaminaBarRed(bool sprintOn, bool timerOn)
    {
        if (sprintOn == true && timerOn == false)
        {
            sliderRed.maxValue = 4f;
            sliderRed.value = 4f;
        }
        if(sprintOn == false && timerOn == true)
        {
            sliderRed.maxValue = 14f;
            sliderRed.value = 0f;
        }
        if(sprintOn == true && timerOn == true)
        {
            sliderRed.value = variableSave.timeLeftRed;
        }
        if(sprintOn == false && timerOn == false)
        {
            sliderRed.maxValue = 15f;
            sliderRed.value = variableSave.sprintRechargeRed;
        }
    }
    public void StaminaBarBlue(bool sprintOn, bool timerOn)
    {
        if (sprintOn == true && timerOn == false)
        {
            sliderBlue.maxValue = 4f;
            sliderBlue.value = 4f;
        }
        if (sprintOn == false && timerOn == true)
        {
            sliderBlue.maxValue = 14f;
            sliderBlue.value = 0f;
        }
        if (sprintOn == true && timerOn == true)
        {
            sliderBlue.value = variableSave.timeLeftBlue;
        }
        if (sprintOn == false && timerOn == false)
        {
            sliderBlue.maxValue = 15f;
            sliderBlue.value = variableSave.sprintRechargeBlue;
        }
    }
}
