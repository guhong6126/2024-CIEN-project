using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ImageClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Result");
    }
}
