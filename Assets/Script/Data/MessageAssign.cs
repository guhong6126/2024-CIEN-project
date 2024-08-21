using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class MessageAssign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        // 프리팹 불러오기
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SNSmessage");
        objs = objs.OrderBy(obj => obj.name).ToArray(); // 이거 안 넣으니까 순서가 뒤죽박죽임...

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
        StartCoroutine(ActivatePrefabs(objs)); 

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
