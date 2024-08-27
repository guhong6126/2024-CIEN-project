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
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("SceneLoadCounter instance �̹� ����");
            //Destroy(gameObject); // �̹� �����ϴ� �̱����� �ִٸ� ���ο� �ν��Ͻ��� �ı��մϴ�.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SetSceneCount.Instance.IsInitialized == false)
        {
            int sceneLoadCount = PlayerPrefs.GetInt(SetSceneCount.SceneLoadCountKey, 0); // �ʱ�ȭ�ϴ� �Լ��� ����ȭ���� SetSceneCount��
            sceneLoadCount++;
            Debug.Log($"Count: {sceneLoadCount}");
            PlayerPrefs.SetInt(SetSceneCount.SceneLoadCountKey, sceneLoadCount);
            PlayerPrefs.Save();
            SetSceneCount.Instance.IsInitialized = true; // snsâ���� �Ѿ�� �ٽ� false�� �ٲ��ִ� ������� �ؾ��� �� ����
            OnCountInitialized?.Invoke(); // MessageGenerator���� ����
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
