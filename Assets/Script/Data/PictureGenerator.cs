using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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




public class PictureGenerator : MonoBehaviour
{
    public Authenticity real_authenticity;
    public FalseElements false_elt;
    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기
    public List<SpriteInfo> spriteInfos;
    private MessageGenerator messageGenerator;

    // Start is called before the first frame update
    void Start()
    {
        persistentData = PersistentData.Instance;
        if (persistentData == null)
        {
            Debug.LogError("PersistentData instance is null. Please check the initialization.");
            return;
        }
        messageGenerator = FindObjectOfType<MessageGenerator>(); // MessageGenerator 인스턴스 찾기
        if (messageGenerator == null)
        {
            Debug.LogError("MessageGenerator instance is not found in the scene.");
            return;
        }
        
        
        LoadAllSprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (messageGenerator != null)
            {
                real_authenticity = messageGenerator.real_authenticity;
                false_elt = messageGenerator.false_elt;
            }
            else
            {
                Debug.LogError("MessageGenerator instance is not available.");
            }
            SetRandomPicture();
        }
    }

    /// <summary>
    /// 폴더 내의 스프라이트 전체를 배열에 담는 함수
    /// </summary>
    void LoadAllSprites()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Pictures");
        spriteInfos = new List<SpriteInfo>();

        foreach (var sprite in sprites)
        {
            bool isSandglass = !sprite.name.EndsWith("_f"); // _f로 끝나면 모래시계가 아닌 것
            Terror_methods methodName = GetMethodNameFromSpriteName(sprite.name);
            spriteInfos.Add(new SpriteInfo(sprite, isSandglass, methodName));
        }

        if (spriteInfos.Count == 0)
        {
            Debug.LogError("No sprites found in 'Pictures' folder.");
        }
    }

    /// <summary>
    /// 스프라이트 이름에 포함된 단어에 해당하는 값을 반환하는 함수
    /// </summary>
    /// <param name="spriteName">스프라이트 이름</param>
    /// <returns>Terror_methods 값</returns>
    Terror_methods GetMethodNameFromSpriteName(string spriteName)
    {
        if (spriteName.Contains("hostage")) return Terror_methods.인질극;
        if (spriteName.Contains("kidnapping")) return Terror_methods.납치;
        if (spriteName.Contains("bomb")) return Terror_methods.폭탄테러;
        if (spriteName.Contains("cyber")) return Terror_methods.사이버테러;
        if (spriteName.Contains("bioterror")) return Terror_methods.바이오테러;
        if (spriteName.Contains("nuclear")) return Terror_methods.핵공격;
        if (spriteName.Contains("sabotage")) return Terror_methods.사보타주;
        
        return Terror_methods.사보타주; // 위에서 if문에 안 걸렸다면 오류니까 일단은 사보타주로 설정
    }

    /// <summary>
    /// 출력할 그림을 정하는 함수
    /// </summary>
    void SetRandomPicture()
    {
        List<SpriteInfo> candidateSprites;

        
        if (real_authenticity == Authenticity.false1 && false_elt == FalseElements.picture) // if (게시물==거짓 && 거짓항목==그림) i.e. 거짓 그림을 출력해야 하는 경우
        {
            //출력그림 = ( 모래시계==0 || 거짓 방법 )에 해당하는 그림

            //Debug.Log("Selecting false picture"); // 여기로 진입했는지 확인 (지금 이 조건인데 올바른 그림이 출력되고 있음)

            // 거짓방법만 담은 리스트 생성     
            Terror_methods[] allMethods = (Terror_methods[])System.Enum.GetValues(typeof(Terror_methods)); // 모든 method 가져오기
            List<Terror_methods> excludedMethods = new List<Terror_methods>();
            foreach (Terror_methods method in allMethods)
            {
                if (!persistentData.current_methods.Contains(method))
                {
                    excludedMethods.Add(method);
                }
            }
            //Debug.Log("Excluded methods: " + string.Join(", ", excludedMethods)); // 거짓 방법 목록 출력 

            // 모래시계가 거짓이거나 거짓 방법에 해당하는 그림들을 담은 리스트 생성 (isSandglass가 false이거나 false_method와 일치하는 그림)
            candidateSprites = spriteInfos.FindAll(spriteInfo => !spriteInfo.isSandglass || excludedMethods.Contains(spriteInfo.methodName));

            //Debug.Log("Number of candidate sprites for false picture: " + candidateSprites.Count); // 후보 그림 개수 출력


        }
        else //참인 그림 출력
        {
            //Debug.Log("Current real_authenticity: " + real_authenticity);
            //Debug.Log("Current false_elt: " + false_elt);
            if (real_authenticity == Authenticity.true1 || (real_authenticity == Authenticity.false1 && false_elt == FalseElements.location)) // 메시지가 참인 메시지면 거기서 언급한 특정 방법의 그림을 띄워야 함
            {
                //Debug.Log("Selecting true logo & method picture");
                candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && spriteInfo.methodName== messageGenerator.current_method); 

            }
            else
            { // 메시지가 거짓이지만 거짓항목이 그림이 아닌 경우 혹은 메시지가 속보인 경우 => 출력그림 = (모래시계=1 && 참인 방법)에 해당하는 그림
                //Debug.Log("Selecting true picture");
                candidateSprites = spriteInfos.FindAll(spriteInfo => spriteInfo.isSandglass && persistentData.current_methods.Contains(spriteInfo.methodName));
            }
            
            
        }
        
        if (candidateSprites.Count > 0) //리스트에 여러 장이 들어있으므로 여기에 걸려야 함
        {
            SpriteInfo selectedSpriteInfo = candidateSprites[Random.Range(0, candidateSprites.Count)]; //랜덤으로 하나 뽑고
            GetComponent<SpriteRenderer>().sprite = selectedSpriteInfo.sprite; //출력ㄱ
            //Debug.Log("Selected sprite: " + selectedSpriteInfo.sprite.name);
        }
        else // 여기로 가면 뭔가가 잘못된 거임 이거 뜨면 좌절하셈
        {
            Debug.LogWarning("No suitable sprite found.");
        }

    }



}
