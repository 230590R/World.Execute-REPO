using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Options selected"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
