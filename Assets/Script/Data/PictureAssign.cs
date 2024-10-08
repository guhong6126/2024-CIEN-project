﻿using System;
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

public class PictureAssign : MonoBehaviour
{
    public static PictureAssign Instance;

    public Sprite[] sprites;
    public List<SpriteInfo> spriteInfos; // SpriteInfo들을 담을 리스트 (여기서 랜덤으로 선택할 거임)
    public List<Sprite> pic_list;
    public bool IsPicturelistInit = false;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Pictures/SNSpicture");
        IsPicturelistInit = false;
        pic_list = new List<Sprite>();
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
        spriteInfos = new List<SpriteInfo>();

        for (int i = 0; i < 20; i++)
        {

            sprites = Resources.LoadAll<Sprite>("Pictures/SNSpicture"); // 초기화
            if (MessageGenerator.Instance.post_list[i] != Integrity.속보)
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
            SetRandomPicture(i);
        }
        IsPicturelistInit = true;
    }

    Terror_methods GetMethodNameFromSpriteName(string spriteName)
    {
        if (spriteName.Contains("hostage")) return Terror_methods.인질극;
        if (spriteName.Contains("kidnapping")) return Terror_methods.납치;
        if (spriteName.Contains("bomb")) return Terror_methods.폭탄테러;
        if (spriteName.Contains("cyber")) return Terror_methods.사이버테러;
        if (spriteName.Contains("bioterror")) return Terror_methods.바이오테러;
        if (spriteName.Contains("nuclear")) return Terror_methods.핵공격;
        //if (spriteName.Contains("sabotage")) return Terror_methods.사보타주;

        return Terror_methods.인질극; // 위에서 if문에 안 걸렸다면 오류니까 일단은 인질극으로 설정
    }

    void SetRandomPicture(int i)
    {
        List<SpriteInfo> candidateSprites;
        //Debug.Log($"Picture {p_index} 받은 인자: {MessageGenerator.Instance.post_list[p_index]}");
        
        if (MessageGenerator.Instance.post_list[i] == Integrity.속보) // 속보일 경우
        {
            pic_list.Add(sprites.FirstOrDefault(sprite => sprite.name == "breaking"));
        }
        else //속보가 아닐 경우
        {
            // 사진이 거짓인 게시물일 경우
            if (MessageGenerator.Instance.post_list[i] == Integrity.거짓 && MessageGenerator.Instance.p_false_elts[i] == FalseElements.picture)
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
                candidateSprites = spriteInfos.FindAll(spriteInfo => !spriteInfo.isSandglass && spriteInfo.methodName == MessageGenerator.Instance.m_methods[i]);
            }
            else //사진이 거짓이 아닐 경우
            {
                // 참인 문구거나 위치가 거짓인 사진일 경우
                if (MessageGenerator.Instance.post_list[i] == Integrity.참 || (MessageGenerator.Instance.post_list[i] == Integrity.거짓 && MessageGenerator.Instance.p_false_elts[i] == FalseElements.location))
                {
                    candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && spriteInfo.methodName == MessageGenerator.Instance.m_methods[i]);
                }
                else //남은 게 방법이 거짓인 경우인가? 이 경우에 참인 방법을 써야 함? 거짓인 방법에 맞춰서 사진을 띄워야 함? -> 걍 랜덤으로 출력
                {
                    //candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && PersistentData.Instance.current_methods.Contains(spriteInfo.methodName));
                    candidateSprites = spriteInfos;
                }
            }

            if (candidateSprites.Count > 0)
            {
                SpriteInfo selectedSpriteInfo = candidateSprites[UnityEngine.Random.Range(0, candidateSprites.Count)];
                pic_list.Add(selectedSpriteInfo.sprite);
            }
            else
            {
                Debug.LogWarning("No suitable sprite found."); //이거 걸리면 오열하셈
            }

        }

        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
