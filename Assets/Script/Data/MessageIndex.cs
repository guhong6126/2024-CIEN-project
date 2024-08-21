using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageIndex : MonoBehaviour
{
    public int index; //MessageAssign에서 인덱스 부여함
    // private MessageGenerator messageGenerator; //MessageGenerator에서 값 가져올 거임
    //private MessageGenerator messageGenerator = MessageGenerator.Instance; //PersistentData 인스턴스에 접근하기
    //private TextMeshProUGUI nicknameText;
    //private TextMeshProUGUI contentsText;

    // Start is called before the first frame update
    void Start()
    {
        //messageGenerator = MessageGenerator.Instance; //초기화
        //nicknameText = transform.Find("nicknameText").GetComponent<TextMeshProUGUI>();
        //contentsText = transform.Find("contentsText").GetComponent<TextMeshProUGUI>();

        //if (nicknameText == null)
        //{
        //    Debug.LogWarning("Nickname Text not found on " + gameObject.name);
        //}

        //if (contentsText == null)
        //{
        //    Debug.LogWarning("Contents Text not found on " + gameObject.name);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //if (messageGenerator == null)
        //{
        //    Debug.LogError("MessageGenerator instance is not found.");
        //    return;
        //}


        //if (nicknameText != null)
        //{
        //    nicknameText.text = messageGenerator.nicknameList[index];
        //}
        //if (contentsText != null)
        //{
        //    contentsText.text = messageGenerator.printed_messages[index];
        //}
    }
    private void LateUpdate()
    {
        
    }
}
