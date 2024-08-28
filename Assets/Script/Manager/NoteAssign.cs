using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NoteAssign : MonoBehaviour
{
    private GameObject[] notes;
    // Start is called before the first frame update
    void Start()
    {
        // WhiteboardImage 프리팹 불러오기 (처음에 Hide한 상태라 위 방법으로는 못 찾음)
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        notes = allObjects
            .Where(obj => obj.CompareTag("Notes") && obj.name.StartsWith("WhiteboardImage ("))
            .OrderBy(note => note.name)
            .ToArray();

        StartCoroutine(WaitForMessageGenerator());
    }
    private IEnumerator WaitForMessageGenerator()
    {
        // 인스턴스가 생성될 때까지 대기
        while (MessageGenerator.Instance == null || !MessageGenerator.Instance.IsInitialized)
        {
            yield return null; // 프레임 대기
        }
        // MessageGenerator 인스턴스가 존재할 때 이벤트 구독
        MessageGenerator.Instance.OnDataInitialized += OnDataInitialized;
        OnDataInitialized();
    }

    private void OnDataInitialized()
    {


        foreach (GameObject note in notes)
        {

            note.GetComponent<NoteIndex>().m_integrity = MessageGenerator.Instance.post_list[Array.IndexOf(notes, note)];
            note.GetComponent<NoteIndex>().m_scale = PersistentData.Instance.current_scale;
            note.GetComponent<NoteIndex>().m_location = MessageGenerator.Instance.m_locations[Array.IndexOf(notes, note)];
            note.GetComponent<NoteIndex>().m_method = MessageGenerator.Instance.m_methods[Array.IndexOf(notes, note)];
            

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        StopAllCoroutines(); // 코루틴 중지

        // 모든 오브젝트를 파괴
        foreach (GameObject note in notes)
        {
            Destroy(note);
        }
    }
}
