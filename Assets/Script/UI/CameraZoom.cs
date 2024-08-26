using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // UI 관련 클래스 사용을 위해 추가

public class CameraZoom : MonoBehaviour
{
    public RectTransform uiElement;  // 크기를 변경할 UI 요소의 RectTransform
    public Vector3 targetScale = new Vector3(1.2f, 1.2f, 1.2f);  // 목표 스케일
    public float duration = 0.5f;  // 확대 애니메이션 지속 시간

    private Vector3 initialScale;  // UI 요소의 초기 스케일

    void Start()
    {
        if (uiElement != null)
        {
            initialScale = uiElement.localScale;  // UI 요소의 초기 스케일 저장
        }
    }

    // 버튼 클릭 시 호출할 함수
    public void OnButtonClick()
    {
        if (uiElement != null)
        {
            StopAllCoroutines();  // 다른 스케일링 코루틴이 있으면 정지
            StartCoroutine(ScaleUI(uiElement, targetScale, duration));  // 확대 코루틴 시작
        }
    }

    // UI 확대 효과를 위한 코루틴
    private IEnumerator ScaleUI(RectTransform element, Vector3 target, float time)
    {
        Vector3 startScale = element.localScale;
        float elapsedTime = 0f;

        // 지정된 시간 동안 점진적으로 스케일을 변경
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            element.localScale = Vector3.Lerp(startScale, target, elapsedTime / time);
            yield return null;
        }

        element.localScale = target;  // 애니메이션이 끝나면 정확히 목표 스케일로 설정
        
        SceneManager.LoadScene("Submit 1");
    }

    // 애니메이션을 초기화하고 원래 크기로 돌아가는 함수 (필요 시 사용)
    public void ResetScale()
    {
        if (uiElement != null)
        {
            StopAllCoroutines();
            uiElement.localScale = initialScale;
        }
    }
}