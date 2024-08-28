using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageIndex : MonoBehaviour
{
    public int index; //MessageAssign���� �ε��� �ο���
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
        // �ν��Ͻ��� ������ ������ ���
        while (MessageGenerator.Instance == null || !MessageGenerator.Instance.IsInitialized)
        {
            yield return null; // ������ ���
        }
        // MessageGenerator �ν��Ͻ��� ������ �� �̺�Ʈ ����
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
        // MessageGenerator�� �����Ͱ� �غ�� �Ŀ� ó���ؾ� ��

        // �ڲ� �ε����� �� �´´뼭 
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
        // �̺�Ʈ ���� ����
        if (MessageGenerator.Instance != null)
        {
            MessageGenerator.Instance.OnDataInitialized -= OnDataInitialized;
        }
    }
}
