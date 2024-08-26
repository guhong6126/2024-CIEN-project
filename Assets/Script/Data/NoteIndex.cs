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
    private TextMeshProUGUI indexText; // 인덱스 (#n) 출력할 텍스트..인데 여기다 내용 다 담아도 될 것 같은디 (위치, 방법) 속보는 '속보', 규모
    private TextMeshProUGUI contentText;
    private MessageGenerator messageGenerator; //MessageGenerator에서 값 가져올 거임
    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기

    // Start is called before the first frame update
    void Start()
    {
        indexText = transform.Find("Index").GetComponent<TextMeshProUGUI>();

        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Pictures/Notes"); // 포스트잇 스프라이트 불러오기
        imageComponent.sprite = sprites[Random.Range(0, sprites.Length)]; // 랜덤으로 적용
    }

    // Update is called once per frame
    void Update()
    {
        indexText.text = "#" + (index + 1);

        //대충 틀
        //if (messageGenerator.post_list[index] == Integrity.속보)
        //{
        //    contentText.text = $"속보\n⌛ = {persistentData.current_scale}규모";
        //}
        //else
        //{
        //    contentText.text = $"\n{messageGenerator.locations[index]}\n{messageGenerator.methods[index]}";
        //}

    }


}
