using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 180; 
    public TextMeshProUGUI countdownText; 

    void Start()
    {
        if (countdownText == null)
        {
            Debug.LogError("CountdownText를 할당하지 않았습니다!");
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime; // 남은 시간을 감소
            UpdateCountdownText(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            UpdateCountdownText(timeRemaining);
            
        }
    }

    void UpdateCountdownText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); 
        int seconds = Mathf.FloorToInt(time % 60); 
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 
    }
}
