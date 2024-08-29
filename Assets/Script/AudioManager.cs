using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private int initialSceneIndex;
    private bool hasMovedToNextScene = false;

    void Awake()
    {
        // 오디오 매니저의 인스턴스가 이미 있다면 새로 생성되는 것을 파괴
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 현재 오브젝트를 instance로 지정하고 파괴되지 않도록 설정
        instance = this;
        DontDestroyOnLoad(gameObject);

        // 초기 씬 인덱스 저장
        initialSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnEnable()
    {
        // 씬 로딩 완료 이벤트에 대한 리스너 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 씬 로딩 완료 이벤트에 대한 리스너 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 오디오가 다음 씬으로 넘어갔는지 체크
        if (!hasMovedToNextScene)
        {
            // 처음 씬에서 다른 씬으로 넘어갔을 때만 hasMovedToNextScene을 true로 설정
            if (scene.buildIndex != initialSceneIndex)
            {
                hasMovedToNextScene = true;
            }
        }
        else
        {
            // 이미 다음 씬으로 넘어갔다면 오브젝트 파괴
            Destroy(gameObject);
        }
    }
}