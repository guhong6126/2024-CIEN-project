using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSNS()
    {
        SceneManager.LoadScene("SNS");
    }

    public void ChangeWhiteBoard()
    {
        SceneManager.LoadScene("WhiteBoard");
    }

    public void ChangeSubmit()
    {
        SceneManager.LoadScene("Submit");
    }

    public void GameOverNewGame()
    {
        SceneManager.LoadScene("Main");
    }
}