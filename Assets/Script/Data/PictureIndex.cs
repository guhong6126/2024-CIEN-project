using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SpriteInfo
{
    public Sprite sprite; // 이미지 스프라이트
    public bool isSandglass; // 모래시계 참/거짓 여부를 담는 속성
    public Terror_methods methodName; // 방법 이름을 담아두는 속성 (이걸로 걸러야 하니까)

    public SpriteInfo(Sprite sprite, bool isSandglass, Terror_methods methodName)
    {
        this.sprite = sprite;
        this.isSandglass = isSandglass;
        this.methodName = methodName;
    }
}

public class PictureIndex : MonoBehaviour
{
    public Image imageComponent;
    public int p_index;
    public Sprite[] sprites;
    public List<SpriteInfo> spriteInfos;

    // Start is called before the first frame update
    void Start()
    {
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>(); //이미지 컴포넌트 설정
        }

        sprites = Resources.LoadAll<Sprite>("Pictures/SNSpicture");

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

        MessageIndex parentScript = GetComponentInParent<MessageIndex>(); //부모 스크립트 접근하기

        if (parentScript != null)
        {
            p_index = parentScript.index; // 부모 스크립트의 변수(index) 가져오기
            //Debug.Log($"picture index: {p_index}");
        }

        LoadAllSprites();
        SetRandomPicture();
    }
    void LoadAllSprites()
    {
               

        spriteInfos = new List<SpriteInfo>();
        

        if (MessageGenerator.Instance.post_list[p_index] != Integrity.속보)
        {
            sprites = sprites.Where(sprite => sprite.name != "breaking").ToArray(); //속보용 이미지 지우기
        }

        foreach (var sprite in sprites)
        {
            bool isSandglass = !sprite.name.EndsWith("_f"); // _f로 끝나면 모래시계가 아닌 것
            Terror_methods methodName;
            
            methodName = GetMethodNameFromSpriteName(sprite.name);

            spriteInfos.Add(new SpriteInfo(sprite, isSandglass, methodName));
        }

        
    }

    Terror_methods GetMethodNameFromSpriteName(string spriteName)
    {
        if (spriteName.Contains("hostage")) return Terror_methods.인질극;
        if (spriteName.Contains("kidnapping")) return Terror_methods.납치;
        if (spriteName.Contains("bomb")) return Terror_methods.폭탄테러;
        if (spriteName.Contains("cyber")) return Terror_methods.사이버테러;
        if (spriteName.Contains("bioterror")) return Terror_methods.바이오테러;
        if (spriteName.Contains("nuclear")) return Terror_methods.핵공격;
        // if (spriteName.Contains("breaking")) return sth; //ㅈㅁ 내가 이 코드를,, 왜 넣었더라
        //if (spriteName.Contains("sabotage")) return Terror_methods.사보타주;

        return Terror_methods.인질극; // 위에서 if문에 안 걸렸다면 오류니까 일단은 인질극으로 설정
    }

    void SetRandomPicture()
    {
        List<SpriteInfo> candidateSprites;
        //Debug.Log($"Picture {p_index} 받은 인자: {MessageGenerator.Instance.post_list[p_index]}");
        // 속보용
        if (MessageGenerator.Instance.post_list[p_index] == Integrity.속보)
        {
            imageComponent.sprite = sprites.FirstOrDefault(sprite => sprite.name == "breaking");
        }
        else //속보가 아닐 경우
        {
            // 사진이 거짓인 게시물일 경우
            if (MessageGenerator.Instance.post_list[p_index] == Integrity.거짓 && MessageGenerator.Instance.p_false_elts[p_index] == FalseElements.picture)
            {
                //Terror_methods[] allMethods = (Terror_methods[])Enum.GetValues(typeof(Terror_methods));
                //List<Terror_methods> excludedMethods = new List<Terror_methods>();
                //foreach (Terror_methods method in allMethods)
                //{
                //    if (!PersistentData.Instance.current_methods.Contains(method))
                //    {
                //        excludedMethods.Add(method);
                //    }
                //}
                // ↑  (1번 사진 + 게시물의 문구에서 언급하지 않은 방법의 사진)에서 랜덤 선택 하는 코드였는디 
                // ↓ 게시물의 문구에서 언급한 방법이랑 동일한 방법의 사진인데 모래시계 로고만 없는 사진을 출력하는 게 맞다고 하셔서 지움 
                candidateSprites = spriteInfos.FindAll(spriteInfo => !spriteInfo.isSandglass && spriteInfo.methodName == MessageGenerator.Instance.current_method);
            }
            else //사진이 거짓이 아닐 경우
            {
                // 참인 문구거나 위치가 거짓인 사진일 경우
                if (MessageGenerator.Instance.post_list[p_index] == Integrity.참 || (MessageGenerator.Instance.post_list[p_index] == Integrity.거짓 && MessageGenerator.Instance.p_false_elts[p_index] == FalseElements.location))
                {
                    candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && spriteInfo.methodName == MessageGenerator.Instance.current_method);
                }
                else //남은 게 방법이 거짓인 경우인가? 이 경우에 참인 방법을 써야 함? 거짓인 방법에 맞춰서 사진을 띄워야 함? -> 걍 랜덤으로 출력
                {
                    // candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && PersistentData.Instance.current_methods.Contains(spriteInfo.methodName));
                    candidateSprites = spriteInfos;
                }
            }

            if (candidateSprites.Count > 0)
            {
                SpriteInfo selectedSpriteInfo = candidateSprites[UnityEngine.Random.Range(0, candidateSprites.Count)];
                imageComponent.sprite = selectedSpriteInfo.sprite; // Image 컴포넌트에 스프라이트 할당
            }
            else
            {
                Debug.LogWarning("No suitable sprite found.");
            }

        }

        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
