using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static UnityEditor.FilePathAttribute;

public class ImageClick : MonoBehaviour, IPointerClickHandler
{
    public bool isCondition;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isCondition)
        {
            SceneManager.LoadScene("ResultT");
        }
        else
        {
            SceneManager.LoadScene("ResultF");
        }

        PersistentData.Instance = null;
    }

    

    void Start()
    {
        Terror_location currentLocation = PersistentData.Instance.current_location;

        // 현재 게임 오브젝트의 이름과 비교
        bool isMatching = CompareLocation(currentLocation);

        if (isMatching)
        {
            isCondition = true;
        }
        else
        {
            isCondition = false;
        }
    }

    bool CompareLocation(Terror_location location)
    {
        return location.ToString().Equals(this.gameObject.name, System.StringComparison.OrdinalIgnoreCase);

    }
}
