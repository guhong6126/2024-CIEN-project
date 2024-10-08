﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Image 컴포넌트를 사용하기 위해 추가
using TMPro;


public class NoteIndex : MonoBehaviour
{
    public int index; //MessageAssign에서 인덱스 부여함
    public Sprite sprite; // 이미지 스프라이트
    public List<SpriteInfo> spriteInfos;
    public Image imageComponent;

    // 실험용
    public Integrity m_integrity; // 진위여부 담을 변수
    public Terror_scale m_scale; // 규모 담을 변수
    public Terror_location m_location; // 위치 담을 변수
    public Terror_methods m_method; // 방법 담을 변수

    private TextMeshProUGUI indexText; // 인덱스 (#n) 출력할 텍스트
    private TextMeshProUGUI contentText; // 내용 담을 텍스트 (위치, 방법) 속보는 '속보', 규모
    private MessageGenerator messageGenerator; //MessageGenerator에서 값 가져올 거임
    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기



    void Awake()
    {
        indexText = transform.Find("Index").GetComponent<TextMeshProUGUI>();
        contentText = transform.Find("Content").GetComponent<TextMeshProUGUI>();

        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Pictures/Notes"); // 포스트잇 스프라이트 불러오기
        imageComponent.sprite = sprites[Random.Range(0, sprites.Length)]; // 랜덤으로 적용

        StartCoroutine(WaitForMessageGenerator());
    }


    // Start is called before the first frame update
    void Start()
    {

    }


    private IEnumerator WaitForMessageGenerator()
    {
        // 인스턴스가 생성될 때까지 대기
        while (MessageGenerator.Instance == null || !MessageGenerator.Instance.IsInitialized)
        {
            yield return null; // 프레임 대기
        }
        //while (messageGenerator.post_list == null || messageGenerator.m_locations == null) //여기서 걸리는 것 같음
        //{
        //    yield return null; // 프레임 대기
        //}
        // MessageGenerator 인스턴스가 존재할 때 이벤트 구독
        MessageGenerator.Instance.OnDataInitialized += OnDataInitialized;
        //Debug.Log("NTID 이벤트 호출 ㄱㄱ");  //얘는 왜 포스트잇이 생성된 이후에 호출되는 걸까 -> 포스트잇 오브젝트가 화이트보드 버튼을 클릭하기 전까지는 비활성화 상태라서 Awake가 실행되지 않기 때문.. 인 것 같은데 그럼 왜 밑에 Index 코드는 잘 작동하는 거지?? >> 외부에서 값을 미리 할당해놔서 ㅇㅋ 그러면 밑 코드를 따로 빼보자
        OnDataInitialized();
    }


    private void OnDataInitialized()
    {

        indexText.text = "#" + (index + 1);

        //if (MessageGenerator.Instance.nicknameList.Count == 0 || MessageGenerator.Instance.printed_messages.Count == 0)
        //{
        //    Debug.LogError("Nickname list or printed messages list is empty when OnDataInitialized is called.");
        //    return;
        //}
        //// MessageGenerator의 데이터가 준비된 후에 처리해야 함

        //Debug.Log("NTID OnDataInitialized 메시지 생성 ㄱㄱ"); // 여기까지 왔는데 아래로 안 넘어감 끼발  NullReferenceException뜸


        //if (messageGenerator.post_list == null) // 여기서 에러 뜸
        //{
        //    Debug.LogError("post_list is null.");
        //    return;
        //}

        //if (messageGenerator.m_locations == null)
        //{
        //    Debug.LogError("m_locations is null.");
        //    return;
        //}

        //if (messageGenerator.m_methods == null)
        //{
        //    Debug.LogError("m_methods is null.");
        //    return;
        //}

        //if (messageGenerator.post_list.Count <= index)
        //{
        //    Debug.LogError($"post_list index {index} is out of range.");
        //    return;
        //}

        //if (contentText == null)
        //{
        //    Debug.LogError("contentText is null.");
        //    return;
        //}

        //Debug.Log("Null check~!");

        //if (messageGenerator.post_list[index] == Integrity.속보)
        //{
        //    contentText.text = $"속보\n⌛ = {persistentData.current_scale}규모";
        //    Debug.Log($"속보\n⌛ = {persistentData.current_scale}규모 할당완");
        //}
        //else
        //{
        //    contentText.text = $"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]}";
        //    Debug.Log($"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]} 할당완");
        //}

        if (m_integrity == Integrity.속보)
        {
            contentText.text = $"속보\n모래시계 = {m_scale}규모";
        }
        else
        {
            contentText.text = $"{m_location}\n{m_method}";
        }


        //if (index >= 0 && index < MessageGenerator.Instance.nicknameList.Count) //이 조건은 없애도 될 것 같긴 함
        //{
        //    if (messageGenerator.post_list[index] == Integrity.속보)
        //    {
        //        contentText.text = $"속보\n⌛ = {persistentData.current_scale}규모";
        //        Debug.Log($"속보\n⌛ = {persistentData.current_scale}규모 할당완");
        //    }
        //    else
        //    {
        //        contentText.text = $"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]}";
        //        Debug.Log($"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]} 할당완");
        //    }
        //}


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
