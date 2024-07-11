using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    static Manager s_instance; // ������
    public static Manager Instance { get {Init();  return s_instance; } } // ������Ƽ ���
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {

                go = new GameObject { name = "Managers" };
                go.AddComponent<Manager>();
            }   

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Manager>();
            
        }

    }
}
