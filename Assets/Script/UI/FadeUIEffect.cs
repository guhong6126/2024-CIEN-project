using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshProUGUI를 사용하기 위해 추가
using System.Collections;

public class FadeUIEffect : MonoBehaviour
{
    public Image image;  // Image 컴포넌트를 인스펙터에서 연결합니다.
    public TextMeshProUGUI textMeshPro;  // TextMeshProUGUI 컴포넌트를 인스펙터에서 연결합니다.
    public float fadeDuration = 1f;  // 페이드 아웃 지속 시간
    public float displayTime = 2f;   // 화면에 표시될 시간

    void Start()
    {
        // 초기 알파 값을 1로 설정하여 이미지와 텍스트가 보이도록 합니다.
        SetAlpha(1f);

        // 지정된 시간이 지난 후 페이드 아웃을 시작합니다.
        StartCoroutine(FadeOutAfterDelay());
    }

    private IEnumerator FadeOutAfterDelay()
    {
        // 이미지와 텍스트가 표시된 상태에서 대기 시간
        yield return new WaitForSeconds(displayTime);
        
        // 페이드 아웃
        yield return StartCoroutine(Fade(1, 0));

        // 페이드 아웃이 끝나면 오브젝트를 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        Color imageColor = image.color;
        Color textColor = textMeshPro.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // 이미지와 텍스트의 알파 값 변경
            image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha);

            yield return null;
        }

        // 최종 알파 값을 설정합니다.
        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, endAlpha);
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        // 초기 알파 값을 설정하여 시작할 때 이미지와 텍스트가 보이도록 합니다.
        Color imageColor = image.color;
        Color textColor = textMeshPro.color;

        image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
        textMeshPro.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
    }
}
