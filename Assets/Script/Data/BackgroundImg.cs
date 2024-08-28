using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundImg : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite[] backgroundSprites;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        if (backgroundImage == null)
        {
            backgroundImage = GetComponent<Image>();
        }
        backgroundSprites = Resources.LoadAll<Sprite>("Pictures/MessageBG"); // 배경 스프라이트 불러오기
        backgroundImage.sprite = backgroundSprites[Random.Range(0, backgroundSprites.Length)]; // 랜덤으로 적용
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
