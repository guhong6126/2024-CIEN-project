using System.Collections;
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
        // MessageGenerator 인스턴스가 존재할 때 이벤트 구독
        MessageGenerator.Instance.OnDataInitialized += OnDataInitialized;
        Debug.Log("NTID 이벤트 호출 ㄱㄱ");  //얘는 왜 포스트잇이 생성된 이후에 호출되는 걸까
        OnDataInitialized();
    }

    private void OnDataInitialized()
    {

        indexText.text = "#" + (index + 1); //얘는 왜 

        if (MessageGenerator.Instance.nicknameList.Count == 0 || MessageGenerator.Instance.printed_messages.Count == 0)
        {
            Debug.LogError("Nickname list or printed messages list is empty when OnDataInitialized is called.");
            return;
        }
        // MessageGenerator의 데이터가 준비된 후에 처리해야 함

        Debug.Log("NTID OnDataInitialized 메시지 생성 ㄱㄱ"); 
        if (index >= 0 && index < MessageGenerator.Instance.nicknameList.Count) //이 조건은 없애도 될 것 같긴 함
        {
            if (messageGenerator.post_list[index] == Integrity.속보)
            {
                contentText.text = $"속보\n⌛ = {persistentData.current_scale}규모";
                Debug.Log($"속보\n⌛ = {persistentData.current_scale}규모 할당완");
            }
            else
            {
                contentText.text = $"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]}";
                Debug.Log($"\n{messageGenerator.m_locations[index]}\n{messageGenerator.m_methods[index]} 할당완");
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
