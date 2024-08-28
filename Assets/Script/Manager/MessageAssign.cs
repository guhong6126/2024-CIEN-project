using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessageAssign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // SNSmessage 프리팹 불러오기
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SNSmessage");
        objs = objs.OrderBy(obj => obj.name).ToArray(); // 이름순 정렬

        // WhiteboardImage 프리팹 불러오기 (처음에 Hide한 상태라 위 방법으로는 못 찾음)
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject[] notes = allObjects
            .Where(obj => obj.CompareTag("Notes") && obj.name.StartsWith("WhiteboardImage ("))
            .OrderBy(note => note.name)
            .ToArray(); 

        // CanvasGroup과 인덱스 설정
        foreach (GameObject obj in objs)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = obj.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0; // 안 보이게 설정
            obj.GetComponent<MessageIndex>().index = System.Array.IndexOf(objs, obj);
        }

        foreach (GameObject note in notes)
        {
            CanvasGroup canvasGroup = note.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = note.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0; // 안 보이게 설정
            canvasGroup.interactable = false; // 상호작용 불가
            canvasGroup.blocksRaycasts = false; // 마우스 클릭 등 차단
            note.GetComponent<NoteIndex>().index = System.Array.IndexOf(notes, note);
        }

        // 코루틴 시작
        StartCoroutine(ActivatePrefabs(objs));
        StartCoroutine(ActivatePrefabs(notes));
    }

    IEnumerator ActivatePrefabs(GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            yield return new WaitForSeconds(9); // 9초 대기

            // 오브젝트가 이미 파괴되었는지 확인
            if (objs[i] == null)
            {
                continue; // 오브젝트가 파괴된 경우 다음 루프로 넘어갑니다.
            }

            CanvasGroup canvasGroup = objs[i].GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1; // 보이게 설정
                
            }
            if (objs[i].CompareTag("Notes"))
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            if (objs[i].CompareTag("SNSmessage"))
            {
                objs[i].transform.SetSiblingIndex(0);
            }
        }
    }

    // 씬 전환 시 오브젝트 파괴
    private void OnDestroy()
    {
        StopAllCoroutines(); // 코루틴 중지

        // 오브젝트 배열 정리
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("SNSmessage");
        //GameObject[] notes = Resources.FindObjectsOfTypeAll<GameObject>()
        //    .Where(obj => obj.CompareTag("Notes") && obj.name.StartsWith("WhiteboardImage ("))
        //    .ToArray();

        //모든 오브젝트를 파괴
        //foreach (GameObject obj in objs)
        //{
        //    Destroy(obj);
        //}

        //foreach (GameObject note in notes)
        //{
        //    Destroy(note);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
