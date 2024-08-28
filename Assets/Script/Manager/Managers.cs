using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    // string 타입 리스트 선언
    List<string> _stringList = new List<string>();

    // 외부에서 리스트에 접근할 수 있는 프로퍼티
    public static List<string> StringList { get { return Instance._stringList; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }

    // 리스트에 문자열을 추가하는 메서드
    public static void AddStringToList(string item)
    {
        StringList.Add(item);
    }

    // 리스트를 비우는 메서드
    public static void ClearStringList()
    {
        StringList.Clear();
    }
}