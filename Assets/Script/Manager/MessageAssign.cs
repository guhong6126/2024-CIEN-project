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
        objs = objs.OrderBy(obj => obj.name).ToArray(); // 이거 안 넣으니까 순서가 뒤죽박죽임...

        // WhiteboardImage 프리팹 불러오기 (처음에 Hide한 상태라 위 방법으로는 못 찾음)
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        GameObject[] notes = allObjects.Where(obj => obj.CompareTag("Notes") && obj.name.StartsWith("WhiteboardImage (")) //태그가 Notes고 WhiteboardImage (로 시작하는 애들
            .OrderBy(note => note.name) //이름순 정렬
            .ToArray(); 


        foreach (GameObject obj in objs)
        {
            CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = obj.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0; // 안 보이게
            obj.GetComponent<MessageIndex>().index=System.Array.IndexOf(objs, obj);

        }

        foreach (GameObject note in notes)
        {
            CanvasGroup canvasGroup = note.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = note.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0;
            note.GetComponent<NoteIndex>().index = System.Array.IndexOf(notes, note);

        }


        StartCoroutine(ActivatePrefabs(objs));
        StartCoroutine(ActivatePrefabs(notes));

    }

    IEnumerator ActivatePrefabs(GameObject[] objs)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            yield return new WaitForSeconds(9); //9초 대기
            CanvasGroup canvasGroup = objs[i].GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1; // 보이게
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
