using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class PictureIndex : MonoBehaviour
{
    public Image imageComponent;
    public int p_index;

    // Start is called before the first frame update
    void Start()
    {
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>(); //�̹��� ������Ʈ ����
        }

        StartCoroutine(WaitForMessageGenerator());
    }
    private IEnumerator WaitForMessageGenerator()
    {
        // �ν��Ͻ��� ������ ������ ���
        while (MessageGenerator.Instance == null || !MessageGenerator.Instance.IsInitialized || PictureAssign.Instance == null || !PictureAssign.Instance.IsPicturelistInit)
        {
            yield return null; // ������ ���
        }
        // MessageGenerator �ν��Ͻ��� ������ �� �̺�Ʈ ����
        MessageGenerator.Instance.OnDataInitialized += OnDataInitialized;
        OnDataInitialized();
    }
    private void OnDataInitialized()
    {

        MessageIndex parentScript = GetComponentInParent<MessageIndex>(); //�θ� ��ũ��Ʈ �����ϱ�

        if (parentScript != null)
        {
            p_index = parentScript.index; // �θ� ��ũ��Ʈ�� ����(index) �������� -> ������� ����
            //Debug.Log($"picture index: {p_index}");
        }
        if (PictureAssign.Instance.pic_list == null || PictureAssign.Instance.pic_list.Count == 0)
        {
            Debug.LogError("pic_list is null or empty.");
            return;
        }
        if (p_index < 0 || p_index >= PictureAssign.Instance.pic_list.Count)
        {
            Debug.LogError($"p_index {p_index} is out of range.");
            return;
        }
        if (imageComponent == null)
        {
            Debug.LogError("imageComponent is null.");
            return;
        }
        if (PictureAssign.Instance.pic_list[p_index] == null)
        {
            Debug.LogError($"Sprite at index {p_index} is null.");
        }
        imageComponent.sprite = PictureAssign.Instance.pic_list[p_index]; // �̰� �� ����
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
