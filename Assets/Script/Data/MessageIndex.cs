using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageIndex : MonoBehaviour
{
    public int index; //MessageAssign에서 인덱스 부여함
    private TextMeshProUGUI nicknameText;
    private TextMeshProUGUI contentsText;

    // Start is called before the first frame update
    void Start()
    {
        nicknameText = transform.Find("username").GetComponent<TextMeshProUGUI>();
        contentsText = transform.Find("message").GetComponent<TextMeshProUGUI>();

        if (nicknameText == null)
        {
            Debug.LogWarning("Nickname Text not found on " + gameObject.name);
        }

        if (contentsText == null)
        {
            Debug.LogWarning("Contents Text not found on " + gameObject.name);
        }

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


        if (MessageGenerator.Instance.nicknameList.Count == 0 || MessageGenerator.Instance.printed_messages.Count == 0)
        {
            Debug.LogError("Nickname list or printed messages list is empty when OnDataInitialized is called.");
            return;
        }
        // MessageGenerator의 데이터가 준비된 후에 처리해야 함

        // 자꾸 인덱스가 안 맞는대서 
        if (index >= 0 && index < MessageGenerator.Instance.nicknameList.Count)
        {
            if (nicknameText != null)
            {
                nicknameText.text = MessageGenerator.Instance.nicknameList[index];
            }
            if (contentsText != null)
            {
                contentsText.text = MessageGenerator.Instance.printed_messages[index];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (MessageGenerator.Instance != null)
        {
            MessageGenerator.Instance.OnDataInitialized -= OnDataInitialized;
        }
    }
}
