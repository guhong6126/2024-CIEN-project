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
            imageComponent = GetComponent<Image>(); //이미지 컴포넌트 설정
        }

        StartCoroutine(WaitForMessageGenerator());
    }
    private IEnumerator WaitForMessageGenerator()
    {
        // 인스턴스가 생성될 때까지 대기
        while (MessageGenerator.Instance == null || !MessageGenerator.Instance.IsInitialized || PictureAssign.Instance == null || !PictureAssign.Instance.IsPicturelistInit)
        {
            yield return null; // 프레임 대기
        }
        // MessageGenerator 인스턴스가 존재할 때 이벤트 구독
        MessageGenerator.Instance.OnDataInitialized += OnDataInitialized;
        OnDataInitialized();
    }
    private void OnDataInitialized()
    {

        MessageIndex parentScript = GetComponentInParent<MessageIndex>(); //부모 스크립트 접근하기

        if (parentScript != null)
        {
            p_index = parentScript.index; // 부모 스크립트의 변수(index) 가져오기 -> 여기까진 ㅇㅋ
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
        imageComponent.sprite = PictureAssign.Instance.pic_list[p_index]; // 이게 안 됐음
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
