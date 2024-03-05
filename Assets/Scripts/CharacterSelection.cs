using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    private int currentChar = 0;

    [SerializeField] bool controller;
    [SerializeField] ScriptableVariableSave variableSave;

    [Header("Duck")]
    [SerializeField] GameObject duck1;
    [SerializeField] GameObject duck2;
    [SerializeField] GameObject duck3;
    [SerializeField] GameObject duck4;

    [Header("Bear")]
    [SerializeField] GameObject bear1;
    [SerializeField] GameObject bear2;
    [SerializeField] GameObject bear3;
    [SerializeField] GameObject bear4;

    [Header("Dog")]
    [SerializeField] GameObject dog1;
    [SerializeField] GameObject dog2;
    [SerializeField] GameObject dog3;
    [SerializeField] GameObject dog4;

    private void SelectChar(int index)
    {
        if (index == 0)
        {
            duck1.SetActive(true);
            duck2.SetActive(true);
            duck3.SetActive(true);
            duck4.SetActive(true);

            bear1.SetActive(false);
            bear2.SetActive(false);
            bear3.SetActive(false);
            bear4.SetActive(false);

            dog1.SetActive(false);
            dog2.SetActive(false);
            dog3.SetActive(false);
            dog4.SetActive(false);
        }

        if (index == 1)
        {
            duck1.SetActive(false);
            duck2.SetActive(false);
            duck3.SetActive(false);
            duck4.SetActive(false);

            bear1.SetActive(true);
            bear2.SetActive(true);
            bear3.SetActive(true);
            bear4.SetActive(true);

            dog1.SetActive(false);
            dog2.SetActive(false);
            dog3.SetActive(false);
            dog4.SetActive(false);
        }
        if(index == 2)
        {
            duck1.SetActive(false);
            duck2.SetActive(false);
            duck3.SetActive(false);
            duck4.SetActive(false);

            bear1.SetActive(false);
            bear2.SetActive(false);
            bear3.SetActive(false);
            bear4.SetActive(false);

            dog1.SetActive(true);
            dog2.SetActive(true);
            dog3.SetActive(true);
            dog4.SetActive(true);
        }
    }

    public void ChangeCharacter(int change)
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        currentChar += change;
        if (currentChar < 0)
        {
            currentChar = 2;
            SelectChar(currentChar);
        }
        if (currentChar > 2)
        {
            currentChar = 0;
            SelectChar(currentChar);
        }
        SelectChar(currentChar);
    }

    public void CharacterSelect()
    {
        if (controller == true)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
            variableSave.controllerSelected = currentChar;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (controller == false)
        {
            FindObjectOfType<AudioManager>().Play("ButtonSound");
            variableSave.keyboardSelected = currentChar;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void StartGameChar()
    {
        if (controller == true)
        {
            //int charSelect = FindObjectOfType<GameController>().controllerSelected;
            SelectChar(variableSave.controllerSelected);
        }
        if(controller == false)
        {
            //int charSelect = FindObjectOfType<GameController>().keyboardSelected;
            SelectChar(variableSave.keyboardSelected);
        }
    }
}
