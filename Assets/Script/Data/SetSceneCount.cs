using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneCount : MonoBehaviour
{
    // Start is called before the first frame update
    public const string SceneLoadCountKey = "SceneLoadCount";
    public static SetSceneCount Instance { get; private set; } 

    public bool IsInitialized { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
        IsInitialized = false;
    }

    private void Start()
    {
        if (Instance == this)
        {
            PlayerPrefs.SetInt(SceneLoadCountKey, 0);
            PlayerPrefs.Save();
            IsInitialized = false;
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
