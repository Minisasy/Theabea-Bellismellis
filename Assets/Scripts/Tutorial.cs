using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI redWalk;
    [SerializeField] TextMeshProUGUI redJump;
    [SerializeField] TextMeshProUGUI redSprint;
    [SerializeField] TextMeshProUGUI blueWalk;
    [SerializeField] TextMeshProUGUI blueJump;
    [SerializeField] TextMeshProUGUI blueSprint;

    bool redWalked = false;
    bool redJumped = false;
    bool redSprinted = false;
    bool blueWalked = false;
    bool blueJumped = false;
    bool blueSprinted = false;

    public void Walked(bool red)
    {
        if (red)
        {
            redWalk.fontStyle = FontStyles.Strikethrough;
            redWalked = true;
        }
        else
        {
            blueWalk.fontStyle = FontStyles.Strikethrough;
            blueWalked = true;
        }
    }
    public void Jumped(bool red)
    {

        if (red)
        {
            redJump.fontStyle = FontStyles.Strikethrough;
            redJumped = true;
        }
        else
        {
            blueJump.fontStyle = FontStyles.Strikethrough;
            blueJumped = true;
        }
    }
    public void Sprinted(bool red)
    {
        if (red)
        {
            redSprint.fontStyle = FontStyles.Strikethrough;
            redSprinted = true;
        }
        else
        {
            blueSprint.fontStyle = FontStyles.Strikethrough;
            blueSprinted = true;
        }
    }
    private void Update()
    {
        if (redWalked && redSprinted && redJumped && blueWalked && blueSprinted && blueJumped)
        {
            StartCoroutine(ChangeScenes());
        }
    }
    IEnumerator ChangeScenes()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
