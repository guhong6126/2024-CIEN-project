using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    static int SceneCounter = 1;
    public void ChangeSNS()
    {
        if (SceneCounter >= 7)
            SceneManager.LoadScene("Result");
        
        SceneManager.LoadScene($"SNS {SceneCounter}");
        SceneCounter++;
        
    }

    public void ChangeSubmit()
    {
        SceneManager.LoadScene("Submit");
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
    }
}