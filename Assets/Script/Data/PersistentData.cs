using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/** 처음에 인코딩을 다르게 해서 한글 주석이 깨져가지고 영어로 주석을 적어뒀는데
 * 게시물에 넣을 문구를 적으면서 생각해보니 고치긴 해야 할 문제라... 해결하면서 영어 주석을 다시 한국어로 바꿔뒀습니다
 * 시야 문제로 놓친 번역이 있을 수도 있습니다. 혹시 이해되지 않는 부분이 있다면 언제든 말씀해주세요~
 * */

public enum Terror_scale
{
    small, medium, large
}
public enum Terror_location
{
    east, west, south, north, city
}
public enum Terror_methods
{
    // 인질극, 납치, 폭탄, 사이버, 바이오, 핵, 사보타주
    hostage, kidnapping, bomb, cyber, bioterror, nuclear, sabotage
    // 대규모일 경우 게시물에 거짓으로 나타낼 방법을 추가해야 함 (테스트를 위해 임시로 sabotage 추가해둠)
}

public class PersistentData : MonoBehaviour
{
    // Start is called before the first frame update
    public static PersistentData Instance;
    public Terror_scale current_scale;
    public List<Terror_methods> current_methods;
    public Terror_location current_location;

    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetCurrentScale();
            SetCurrentTLocation(); // 게임 메인 화면에서 클릭 입력 받았을 때 실행되게 바꿔야 함 (지금은 테스트하려고 Awake에 넣어둠)
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// function to randomly set terror scale (테러 규모를 랜덤으로 설정하는 함수)
    /// </summary>
    public void SetCurrentScale()
    {
        Terror_scale[] terror_scales = (Terror_scale[])System.Enum.GetValues(typeof(Terror_scale)); // create a list containing terror scales (테러 규모를 담는 리스트 생성)
        current_scale = terror_scales[Random.Range(0, terror_scales.Length)]; // select a random element from the list and set it as the current terror scale (리스트에서 규모 하나 랜덤 선택)
        SetTerrorMethods();
    }
    /// <summary>
    /// function to randomly set terror location (테러 위치를 랜덤으로 설정하는 함수)
    /// </summary>
    public void SetCurrentTLocation()
    {
        Terror_location[] terror_locations = (Terror_location[])System.Enum.GetValues(typeof(Terror_location)); // create a list containing terror locations (테러 위치 리스트 생성)
        current_location = terror_locations[Random.Range(0, terror_locations.Length)]; // select a random element from the list and set it as the current terror location (리스트에서 랜덤 선택)
    }
    /// <summary>
    /// function to update list of terror methods (테러 방법 리스트를 업데이트하는 함수)
    /// </summary>
    void SetTerrorMethods()
    {
        current_methods = new List<Terror_methods>(); // 실행 가능한 테러 방법을 담을 리스트

        switch (current_scale) // 지금 규모가 뭐냐
        {
            case Terror_scale.small:
                current_methods.Add(Terror_methods.hostage);
                current_methods.Add(Terror_methods.kidnapping);
                break;
            case Terror_scale.medium:
                current_methods.Add(Terror_methods.hostage);
                current_methods.Add(Terror_methods.kidnapping);
                current_methods.Add(Terror_methods.bomb);
                current_methods.Add(Terror_methods.cyber);
                break;
            case Terror_scale.large:
                current_methods.Add(Terror_methods.hostage);
                current_methods.Add(Terror_methods.kidnapping);
                current_methods.Add(Terror_methods.bomb);
                current_methods.Add(Terror_methods.cyber);
                current_methods.Add(Terror_methods.bioterror);
                current_methods.Add(Terror_methods.nuclear);
                break;
        }

        /** can use if-statement instead of switch-statement like below (아래처럼 if문으로 작성도 가능)
            idk which one is more readable... (뭐가 더 가독성이 좋은지 모르겠음)
        **/

        //if(current_scale == terror_scale.large)
        //{
        //    current_methods.Add(terror_methods.bioterror);
        //    current_methods.Add(terror_methods.nuclear);
        //}
        //if (current_scale == terror_scale.large|| current_scale == terror_scale.medium)
        //{
        //    current_methods.Add(terror_methods.bomb);
        //    current_methods.Add(terror_methods.cyber);
        //}
        //if (current_scale == terror_scale.large || current_scale == terror_scale.medium || current_scale == terror_scale.small)
        //{
        //    current_methods.Add(terror_methods.hostage);
        //    current_methods.Add(terror_methods.kidnapping);
        //}



    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
