using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public  void PlayGame()
    {
        SceneManager.LoadScene("Demo0");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Demo0");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
