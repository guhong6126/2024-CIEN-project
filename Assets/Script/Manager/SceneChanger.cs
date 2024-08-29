using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static int SceneCounter { get; private set; } = 1;

    public void ChangeSNS()
    {
        if (SceneCounter >= 7)
        {
            SceneManager.LoadScene("Result");
        }
        else
        {
            SceneManager.LoadScene($"SNS {SceneCounter}");
            SceneCounter++;
        }
    }

    public void ChangeSubmit()
    {
        SceneManager.LoadScene("Submit");
    }

    public void Main()
    {
        PersistentData.Instance = null;
        SceneManager.LoadScene("Main");
    }

    public void goSNS1()
    {
        SceneCounter = 2; // SNS 1 이후로는 SNS 2 씬으로 넘어가게 설정
        SuccessCounter.counter = 0;
        Managers.ClearStringList();
        SceneManager.LoadScene("SNS 1");
    }
}