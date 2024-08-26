using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Video; 

public class VideoControl : MonoBehaviour
{
    public VideoPlayer videoPlayer; 
    public string nextSceneName; 

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextScene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Main"); 
    }
}