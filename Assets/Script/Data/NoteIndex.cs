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
    private TextMeshProUGUI indexText; // 인덱스 (#n) 출력할 텍스트..인데 여기다 내용 다 담아도 될 것 같은디 (위치, 방법, 사진 정확 여부)
    private MessageGenerator messageGenerator; //MessageGenerator에서 값 가져올 거임

    // Start is called before the first frame update
    void Start()
    {
        indexText = transform.Find("Index").GetComponent<TextMeshProUGUI>();

        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Pictures/Notes");

        // sprites 배열이 비어있는지 확인
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites found in the specified path.");
            return; // sprites 배열이 비어있을 경우 메서드를 종료
        }



        imageComponent.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        indexText.text = "#" + (index + 1); // + $"\n{messageGenerator.locations[index]}\n{messageGenerator.methods[index]}\n{messageGenerator.ispictureCorrect[index]};"; //대~충 이렇게
    }

   
}
