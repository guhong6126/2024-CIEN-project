using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageScaleOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.5f;  // 마우스를 올렸을 때의 크기 배율
    private Vector3 originalScale;

    void Start()
    {
        // 처음에 이미지의 원래 크기를 저장
        originalScale = transform.localScale;
    }

    // 마우스 올릴때
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 이미지 확대
        transform.localScale = originalScale * hoverScale;
    }

    // 마우스 뗄 때
    public void OnPointerExit(PointerEventData eventData)
    {
        // 되돌리기
        transform.localScale = originalScale;
    }
}
