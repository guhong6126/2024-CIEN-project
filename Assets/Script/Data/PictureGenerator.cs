//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI; // Image 컴포넌트를 사용하기 위해 추가

//// 같은 방법에 대한 그림도 여러개 만들 줄 알고 이렇게 짰는디 이럴 필요가 없어진듯
//public class SpriteInfo
//{
//    public Sprite sprite; // 이미지 스프라이트
//    public bool isSandglass; // 모래시계 참/거짓 여부를 담는 속성
//    public Terror_methods methodName; // 방법 이름을 담아두는 속성 (이걸로 걸러야 하니까)

//    public SpriteInfo(Sprite sprite, bool isSandglass, Terror_methods methodName)
//    {
//        this.sprite = sprite;
//        this.isSandglass = isSandglass;
//        this.methodName = methodName;
//    }
//}

//public class PictureGenerator : MonoBehaviour
//{
//    public Integrity real_integrity;
//    public FalseElements false_elt;
//    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기
//    public List<SpriteInfo> spriteInfos;
//    private MessageGenerator messageGenerator;
//    public List<SpriteInfo> snspictures;

//    // Image 컴포넌트를 참조할 변수
//    public Image imageComponent;

//    // Start is called before the first frame update
//    void Start()
//    {
//        persistentData = PersistentData.Instance;
//        if (persistentData == null)
//        {
//            Debug.LogError("PersistentData instance is null. Please check the initialization.");
//            return;
//        }
//        messageGenerator = FindObjectOfType<MessageGenerator>(); // MessageGenerator 인스턴스 찾기
//        if (messageGenerator == null)
//        {
//            Debug.LogError("MessageGenerator instance is not found in the scene.");
//            return;
//        }

//        // Image 컴포넌트가 설정되었는지 확인
//        if (imageComponent == null)
//        {
//            imageComponent = GetComponent<Image>();
//            if (imageComponent == null)
//            {
//                Debug.LogError("Image component is not assigned and could not be found on the same GameObject. Please assign it in the inspector.");
//                return;
//            }
//        }
//        snspictures = new List<SpriteInfo>(); //초기화

//        LoadAllSprites();
//    }
    
//    void LoadAllSprites()
//    {
//        Sprite[] sprites = Resources.LoadAll<Sprite>("Pictures/SNSpicture");
//        spriteInfos = new List<SpriteInfo>();

//        foreach (var sprite in sprites)
//        {
//            bool isSandglass = !sprite.name.EndsWith("_f"); // _f로 끝나면 모래시계가 아닌 것
//            if (sprite.name.StartsWith("breaking"))
//            {

//            }
            
//            Terror_methods methodName = GetMethodNameFromSpriteName(sprite.name);
//            spriteInfos.Add(new SpriteInfo(sprite, isSandglass, methodName));
//        }

//        if (spriteInfos.Count == 0)
//        {
//            Debug.LogError("No sprites found in 'Pictures' folder.");
//        }
//    }

//    Terror_methods GetMethodNameFromSpriteName(string spriteName)
//    {
//        if (spriteName.Contains("hostage")) return Terror_methods.인질극;
//        if (spriteName.Contains("kidnapping")) return Terror_methods.납치;
//        if (spriteName.Contains("bomb")) return Terror_methods.폭탄테러;
//        if (spriteName.Contains("cyber")) return Terror_methods.사이버테러;
//        if (spriteName.Contains("bioterror")) return Terror_methods.바이오테러;
//        if (spriteName.Contains("nuclear")) return Terror_methods.핵공격;
//        // if (spriteName.Contains("breaking")) return sth; //ㅈㅁ 내가 이 코드를,, 왜 넣었더라
//        //if (spriteName.Contains("sabotage")) return Terror_methods.사보타주;

//        return Terror_methods.인질극; // 위에서 if문에 안 걸렸다면 오류니까 일단은 인질극으로 설정
//    }

//    void SetRandomPicture()
//    {
//        List<SpriteInfo> candidateSprites;

//        if(real_integrity == Integrity.속보)
//        {
//            candidateSprites = spriteInfos;
//        }
//        if (real_integrity == Integrity.거짓 && false_elt == FalseElements.picture)
//        {
//            Terror_methods[] allMethods = (Terror_methods[])System.Enum.GetValues(typeof(Terror_methods));
//            List<Terror_methods> excludedMethods = new List<Terror_methods>();
//            foreach (Terror_methods method in allMethods)
//            {
//                if (!persistentData.current_methods.Contains(method))
//                {
//                    excludedMethods.Add(method);
//                }
//            }

//            candidateSprites = spriteInfos.FindAll(spriteInfo => !spriteInfo.isSandglass || excludedMethods.Contains(spriteInfo.methodName));
//        }
//        else
//        {
//            if (real_integrity == Integrity.참 || (real_integrity == Integrity.거짓 && false_elt == FalseElements.location))
//            {
//                candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && spriteInfo.methodName == messageGenerator.current_method);
//            }
//            else
//            {
//                candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && persistentData.current_methods.Contains(spriteInfo.methodName));
//            }
//        }

//        if (candidateSprites.Count > 0)
//        {
//            //SpriteInfo selectedSpriteInfo = candidateSprites[Random.Range(0, candidateSprites.Count)];
//            //imageComponent.sprite = selectedSpriteInfo.sprite; // Image 컴포넌트에 스프라이트 할당
//        }
//        else
//        {
//            Debug.LogWarning("No suitable sprite found.");
//        }
//    }
//}
