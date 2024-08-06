using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public enum Authenticity
{
    true1, false1, breaking
}

public enum FalseElements
{
    location, methods, picture
}

public class MessageGenerator : MonoBehaviour
{
    public Authenticity real_authenticity;
    public FalseElements false_elt;
    private PersistentData persistentData = PersistentData.Instance; //PersistentData 인스턴스에 접근하기

    public Terror_methods current_method;


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
        "고니","안졸리나젤리","MrBeast","뿌뿌뽕"
    };
    private List<string> presses = new List<string>
    {
        "BBC","CNN","The New York Times"
    };

    // Start is called before the first frame update
    void Start()
    {
        //나중에 클래스 만들고 나서 전부 바꿔야 함
        persistentData = PersistentData.Instance;
        if (persistentData == null)
        {
            Debug.LogError("PersistentData instance is null. Please check the initialization.");
            return;
        }

        Terror_scale scale = persistentData.current_scale;
        Terror_location location = persistentData.current_location;
        List<Terror_methods> methods = persistentData.current_methods;


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Authenticity[] authenticities = (Authenticity[])System.Enum.GetValues(typeof(Authenticity));
            real_authenticity = authenticities[Random.Range(0, authenticities.Length)];
            Debug.Log($"authenticity: {real_authenticity}");
            if (real_authenticity != Authenticity.breaking)
            {
                SetRandomMessage();
            }
            else
            {
                SetBreakingNews();
            }
            
        }

    }

    




    void SetRandomMessage()
    {
        string printed_message = string.Empty; //출력할 메시지(아직은 empty)

        string selected_message = messages[Random.Range(0, messages.Count)]; // 게시물 멘트 랜덤 선택

        // 가짜로 적을 항목 랜덤 선택
        if (real_authenticity == Authenticity.false1)
        {
            FalseElements[] false_elts = (FalseElements[])System.Enum.GetValues(typeof(FalseElements));
            false_elt = false_elts[Random.Range(0, false_elts.Length)];
            Debug.Log($"false element: {false_elt}");
        }
        

        // 닉네임도 뽑아야 함...
        string nickname = nicknames[Random.Range(0, nicknames.Count)]; // 닉네임 랜덤 선택


        // 문구 대충 틀만..

        if (real_authenticity == Authenticity.true1 || (real_authenticity == Authenticity.false1 && false_elt== FalseElements.picture)) // 진위 여부가 참이거나 [진위여부는 거짓인데 그림이 거짓]인 경우
        {
            current_method = persistentData.current_methods[Random.Range(0, persistentData.current_methods.Count)]; // 게시물에 담을 방법(참) 랜덤 선택
            printed_message = string.Format(selected_message, persistentData.current_location, current_method); // 게시물 문구(참) 만들기
        }
        else {
            if(false_elt == FalseElements.location)
            {
                Terror_methods current_method = persistentData.current_methods[Random.Range(0, persistentData.current_methods.Count)]; // 게시물에 담을 방법(참) 랜덤 선택

                // 거짓지역 랜덤 선택
                List<Terror_location> allLocations = ((Terror_location[])System.Enum.GetValues(typeof(Terror_location))).ToList(); // 일단 모든 지역 가져오기
                allLocations.Remove(persistentData.current_location); // 거기서 현재 지역 지우기
                Terror_location false_location = allLocations[Random.Range(0, allLocations.Count)]; // 지역 하나 랜덤 선택


                printed_message = string.Format(selected_message, false_location, current_method); // 게시물 문구(거짓 지역) 만들기
            }

            else if(false_elt == FalseElements.methods)
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

                printed_message = string.Format(selected_message, persistentData.current_location, false_method); // 게시물 문구(거짓 방법) 만들기

            }
        }
        

        Debug.Log(printed_message);
        
    }
    

    void SetBreakingNews()
    {
        // 속보인지 체크는 얘를 호출하는 함수에서 해둘 예정... if(authenticity==속보){출력메시지=SetBreakingNews();} else{출력메시지=SetRandomMessage();} 이렇게
        // 그래서 여기서는 내용만 구성해주면 될듯

        // 언론사 랜덤 선택 구현하기
        string pressname = presses[Random.Range(0, presses.Count)]; // 언론사명 랜덤 선택


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
        Debug.Log(printed_message);
        

        // 그림은 SetRandomPicture 쓰면 됨

    }
}
