using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageIndex : MonoBehaviour
{
    public int index; //MessageAssign���� �ε��� �ο���
    // private MessageGenerator messageGenerator; //MessageGenerator���� �� ������ ����
    //private MessageGenerator messageGenerator = MessageGenerator.Instance; //PersistentData �ν��Ͻ��� �����ϱ�
    //private TextMeshProUGUI nicknameText;
    //private TextMeshProUGUI contentsText;

    // Start is called before the first frame update
    void Start()
    {
        //messageGenerator = MessageGenerator.Instance; //�ʱ�ȭ
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
