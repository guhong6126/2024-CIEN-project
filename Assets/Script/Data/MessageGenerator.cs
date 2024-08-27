using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Linq;
using static UnityEditor.FilePathAttribute;


public enum Integrity
{
    참, 거짓, 속보
}

public enum FalseElements
{
    location, methods, picture
}



public class MessageGenerator : MonoBehaviour
{

    public delegate void DataInitializedHandler();
    public event DataInitializedHandler OnDataInitialized;
    public bool IsInitialized { get; private set; } = false;
    private bool isSubscribed = false;

    public static MessageGenerator Instance;
    public Integrity real_integrity;
    public List<Integrity> post_list;

    public FalseElements false_elt;
    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기

    public Terror_methods current_method;
    public List<string> nicknameList;
    public List<string> printed_messages;
    public List<Terror_location> m_locations;
    public List<Terror_methods> m_methods;

    private List<string> messages = new List<string>
    {
        "{0}에서 {1} 하는 테러 집단 봄..ㄷㄷ 뭐임? 모래시계 같은데",
        "나 지금 {0}인데 {1} 하는 장면 목격한 것 같음... 개무섭다;",
        "혹시 이거 {1} 하는 거 맞나요? 지금 {0}인데 걱정 되네요..."
    };
    private List<string> news = new List<string>
    {
        "[속보] 모래시계, {0}규모의 테러 조직으로 판명",
        "[속보] 모래시계는 {0}규모 테러 조직"
    };
    private List<string> nicknames = new List<string>
    {
        "고라니","안졸리나젤리","붕방붕","화양연화","고랭이","라단조","물붓기시험","수마트라섬","지구가까운점","소망",
        "니르","삐걱","여흘여흘","뚱싯뚱싯","흐르르흐르르","어처구니없는","누쀠","채레","퐈크퓨","몽땅",
        "인공눈물","믿기마우스","진진자라","펭귄","읽씹백천만","무더위","이조명온도습도","듄듄","고영희",
        "Sodium","Kitsch","rizz","fratty","JellyBeans","Limited_Edition","Math_","英吉利","倫敦","bandit"
    };
    private List<string> presses = new List<string>
    {
        "BBA","DNN","The York Times","Mando Times"
    };


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Debug.Log("instance 이미 존재");
            //Destroy(gameObject); //근데 이러면 ... 하나만 잘 있는데도 삭제하던데 
        }
        isSubscribed = false;
    }

    void Start()
    {


        //나중에 클래스 만들면 전부 바꿔야 함
        persistentData = PersistentData.Instance;
        if (persistentData == null)
        {
            Debug.LogError("PersistentData Instance is null. Please check the initialization.");
            return;
        }

        Terror_scale scale = persistentData.current_scale;
        Terror_location location = persistentData.current_location;
        List<Terror_methods> methods = persistentData.current_methods;


        //닉네임, 문구, 참거짓 리스트 초기화
        nicknameList = new List<string>();
        printed_messages = new List<string>();
        post_list = new List<Integrity>();
        m_locations = new List<Terror_location>();
        m_methods = new List<Terror_methods>();

        StartCoroutine(WaitForSceneLoadCounter());
    }
    private IEnumerator WaitForSceneLoadCounter()
    {
        // 인스턴스가 생성될 때까지 대기
        while (SceneLoadCounter.Instance == null || !SetSceneCount.Instance.IsInitialized)
        {
            //if(SceneLoadCounter.Instance == null) { Debug.Log("SceneLoadCounter.Instance == null"); }
            // if (!SetSceneCount.Instance.IsInitialized) { Debug.Log("!SetSceneCount.Instance.IsInitialized"); }

            yield return null; // 프레임 대기
        }
        if (!isSubscribed)
        {
            isSubscribed = true;
            SceneLoadCounter.Instance.OnCountInitialized += OnCountInitialized;
            OnCountInitialized();
        }

        
    }

    private void OnCountInitialized()
    {
        int sceneLoadCount = PlayerPrefs.GetInt(SetSceneCount.SceneLoadCountKey, 0);
        Debug.Log($"STG Count: {sceneLoadCount}");
        // 스테이지마다 적용할 거
        (int num_of_true, int num_breaking) = SetAuthRatio(sceneLoadCount);
        Debug.Log($"true: {num_of_true}개, 속보: {num_breaking}번째");
        post_list = IntegrityRatio(num_of_true, num_breaking); // 참거짓 여부를 담은 리스트


        foreach (Integrity elt in post_list)
        {
            if (elt != Integrity.속보)
            {
                SetRandomMessage();
            }
            else
            {
                SetBreakingNews();
            }
        }

        if (nicknameList.Count == 20 && printed_messages.Count == 20 && IsInitialized == false) //리스트 다 만들었으면
        {
            IsInitialized = true;
            //Debug.Log($"IsInitialized : {IsInitialized}, 이벤트 호출 ㄱ");
            OnDataInitialized?.Invoke(); //MessageIndex에 이벤트 넘길거
            //Debug.Log("이벤트 호출 끝");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (nicknameList.Count == 20 && printed_messages.Count == 20 && IsInitialized == false)
        //{
        //    IsInitialized = true;
        //    OnDataInitialized?.Invoke();
        //}

        //if (Input.GetKeyDown(KeyCode.P)) // 테스트용
        //{
        //    Integrity[] authenticities = (Integrity[])System.Enum.GetValues(typeof(Integrity));
        //    real_integrity = authenticities[Random.Range(0, authenticities.Length)];
        //    Debug.Log($"진위여부: {real_integrity}");
        //    if (real_integrity != Integrity.속보)
        //    {
        //        SetRandomMessage();
        //    }
        //    else
        //    {
        //        SetBreakingNews();
        //    }

        //}



    }


    /// <summary>
    /// 몇 번째 스테이지인지 체크해서 num_of_true랑 num_breaking 정하는 함수
    /// </summary>
    /// <param name="num"></param>
    /// <returns>(참인 게시물 개수, 속보 순서)</returns>
    private (int, int) SetAuthRatio(int num)
    {
        //Debug.Log($"받은 인자: {num}");
        int n_true, n_brk;
        n_true = 19 - num * 2; // 예시임 비율은 기획한테 물어보기
        n_brk = 5 + (num - 1) * 2;
        return (n_true, n_brk);
    }

    /// <summary>
    /// 참인 게시물 개수랑 속보가 들어갈 위치를 받아 참,거짓,속보 순서 리스트를 만드는 함수
    /// </summary>
    /// <param name="num_of_true">19개 중 참인 게시물의 개수</param>
    /// <param name="num_breaking">속보의 위치 (카운트는 1부터 시작)</param>
    /// <returns>(참, 거짓, 속보) 순서 리스트</returns>
    private List<Integrity> IntegrityRatio(int num_of_true, int num_breaking)
    {
        List<Integrity> posts_lists = new List<Integrity>();

        for (int i = 0; i < 19; i++) // 19개만 생성 (1개는 속보니까)
        {
            if (i < num_of_true)
            {
                posts_lists.Add(Integrity.참);
            }
            else
            {
                posts_lists.Add(Integrity.거짓);
            }
        }
        // 리스트 셔플
        System.Random rand = new System.Random();
        List<Integrity> posts_list = posts_lists.OrderBy(_ => rand.Next()).ToList();

        posts_list.Add(Integrity.속보); //속보 추가

        //if (num_breaking < 1 || num_breaking > posts_list.Count)
        //{
        //    Debug.LogError($"Invalid num_breaking value: {num_breaking}. It must be between 1 and {posts_list.Count}.");
        //    return posts_list; // 오류 발생 시 현재 리스트 반환
        //}


        //switch (num_breaking번째랑 속보랑 위치 바꾸기)
        (posts_list[posts_list.Count - 1], posts_list[num_breaking - 1]) = (posts_list[num_breaking - 1], posts_list[posts_list.Count - 1]);

        return posts_list;
    }



    /// <summary>
    /// 랜덤 게시물을 생성하는 함수 (지금은 Debug.Log로 출력하지만 UI에 출력되게 바꿔야 함)
    /// </summary>
    void SetRandomMessage()
    {
        string printed_message = string.Empty; // 출력할 메시지(아직은 empty)

        Terror_location location = persistentData.current_location;
        Terror_methods m_method = Terror_methods.인질극;

        string selected_message = messages[Random.Range(0, messages.Count)]; // 게시물 멘트 랜덤 선택

        // 가짜로 적을 항목 랜덤 선택
        if (real_integrity == Integrity.거짓)
        {
            FalseElements[] false_elts = (FalseElements[])System.Enum.GetValues(typeof(FalseElements));
            var falseEltsList = new List<FalseElements>(false_elts);
            if (persistentData.current_scale == Terror_scale.대)
            {
                falseEltsList.Remove(FalseElements.methods);
            }
            false_elt = falseEltsList[Random.Range(0, falseEltsList.Count)];
            //Debug.Log($"거짓 항목: {false_elt}");
        }

        // 닉네임도 뽑아야 함...
        string nickname = nicknames[Random.Range(0, nicknames.Count)]; // 닉네임 랜덤 선택
        nicknameList.Add(nickname);

        // 문구 대충 틀만..
        if (real_integrity == Integrity.참 || (real_integrity == Integrity.거짓 && false_elt == FalseElements.picture)) // 진위 여부가 참이거나 [진위여부는 거짓인데 그림이 거짓]인 경우
        {
            current_method = persistentData.current_methods[Random.Range(0, persistentData.current_methods.Count)]; // 게시물에 담을 방법(참) 랜덤 선택
            m_method = current_method;
            printed_message = string.Format(selected_message, persistentData.current_location, current_method); // 게시물 문구(참) 만들기
        }
        else
        {
            if (false_elt == FalseElements.location)
            {
                current_method = persistentData.current_methods[Random.Range(0, persistentData.current_methods.Count)]; // 게시물에 담을 방법(참) 랜덤 선택
                m_method = current_method;

                // 거짓지역 랜덤 선택
                List<Terror_location> allLocations = ((Terror_location[])System.Enum.GetValues(typeof(Terror_location))).ToList(); // 일단 모든 지역 가져오기
                allLocations.Remove(persistentData.current_location); // 거기서 현재 지역 지우기
                Terror_location false_location = allLocations[Random.Range(0, allLocations.Count)]; // 지역 하나 랜덤 선택

                location = false_location;

                printed_message = string.Format(selected_message, false_location, current_method); // 게시물 문구(거짓 지역) 만들기
            }
            else if (false_elt == FalseElements.methods)
            {
                // 거짓방법 랜덤 선택        
                Terror_methods[] allMethods = (Terror_methods[])System.Enum.GetValues(typeof(Terror_methods)); // 모든 method 가져오기
                List<Terror_methods> excludedMethods = new List<Terror_methods>();
                foreach (Terror_methods method in allMethods)
                {
                    if (!persistentData.current_methods.Contains(method))
                    {
                        excludedMethods.Add(method);
                    }
                }
                Terror_methods false_method = excludedMethods[Random.Range(0, excludedMethods.Count)];
                m_method = false_method;
                printed_message = string.Format(selected_message, persistentData.current_location, false_method); // 게시물 문구(거짓 방법) 만들기
            }
        }
        //Debug.Log(printed_message);
        printed_messages.Add(printed_message);
        m_locations.Add(location); 
        m_methods.Add(m_method);
    }

    /// <summary>
    /// 속보 문구를 생성하는 함수
    /// </summary>
    void SetBreakingNews()
    {
        // 속보인지 체크는 얘를 호출하는 함수에서 해둘 예정... if(integrity==속보){출력메시지=SetBreakingNews();} else{출력메시지=SetRandomMessage();} 이렇게
        // 그래서 여기서는 내용만 구성해주면 될듯

        // 언론사 랜덤 선택 구현하기
        string pressname = presses[Random.Range(0, presses.Count)]; // 언론사명 랜덤 선택
        nicknameList.Add(pressname);

        // 문구 대충 틀만..

        string selected_message = news[Random.Range(0, news.Count)]; // 게시물 멘트 랜덤 선택
        // 변수명 영어로 바꿔야 하면 아래 코드 쓰기
        //public string t_scale;
        //switch (persistentData.current_scale)
        //{
        //    case Terror_scale.small:
        //        t_scale = "소";
        //        break;
        //    case Terror_scale.medium:
        //        t_scale = "중";
        //        break;
        //    case Terror_scale.large:
        //        t_scale = "대";
        //        break;
        //}
        string printed_message = string.Format(selected_message, persistentData.current_scale); // 게시물 문구(참) 만들기
        //Debug.Log($"속보: {printed_message}");
        printed_messages.Add(printed_message);

        m_locations.Add(persistentData.current_location);
        m_methods.Add(Terror_methods.인질극);

    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (SceneLoadCounter.Instance != null)
        {
            isSubscribed = false;
            SceneLoadCounter.Instance.OnCountInitialized -= OnCountInitialized;
        }
    }
}
