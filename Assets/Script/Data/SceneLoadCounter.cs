using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadCounter : MonoBehaviour
{
    public delegate void DataInitializedHandler();
    public event DataInitializedHandler OnCountInitialized;

    public static SceneLoadCounter Instance;
    public int sceneLoadCount;


    

    private void Awake()
    {
        Instance = this;

        if (SetSceneCount.Instance.IsInitialized == false)
        {
            int sceneLoadCount = PlayerPrefs.GetInt(SetSceneCount.SceneLoadCountKey, 0); // 초기화하는 함수는 메인화면의 SetSceneCount에
            sceneLoadCount++;
            Debug.Log($"Count: {sceneLoadCount}");
            PlayerPrefs.SetInt(SetSceneCount.SceneLoadCountKey, sceneLoadCount);
            PlayerPrefs.Save();
            SetSceneCount.Instance.IsInitialized = true; // sns창에서 넘어가면 다시 false로 바꿔주는 방식으로 해야할 것 같음
            OnCountInitialized?.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
