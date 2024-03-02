using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] string start;
    [SerializeField] string controls;
    [SerializeField] string credits;
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        SceneManager.LoadScene(3);
    }

    public void Controls()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("ButtonSound");
        Debug.Log("Quit");
        Application.Quit();
    }
}
