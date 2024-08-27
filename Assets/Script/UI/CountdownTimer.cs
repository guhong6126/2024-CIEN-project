using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; 
    public TextMeshProUGUI countdownText; 

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("CountdownText missing");
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // ���� �ð��� ����
            UpdateCountdownText(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            UpdateCountdownText(timeRemaining);
            GameOverNewGame();
        }
    }

    void UpdateCountdownText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); 
        int seconds = Mathf.FloorToInt(time % 60); 
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }

    public void GameOverNewGame()
    {
        SceneManager.LoadScene("ResultF");
    }
}
