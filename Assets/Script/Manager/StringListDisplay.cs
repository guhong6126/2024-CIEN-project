using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class StringListDisplay : MonoBehaviour
{
    public TextMeshProUGUI tmpText;  // TMP 텍스트 객체 레퍼런스

    private void Start()
    {
        if (tmpText == null)
        {
            Debug.LogError("TMP 텍스트가 연결되지 않았습니다. 인스펙터에서 TMP 텍스트를 연결하세요.");
            return;
        }

        UpdateTMPText();
    }

    // TMP 텍스트를 업데이트하는 메서드
    public void UpdateTMPText()
    {
        // Managers.StringList에 저장된 문자열을 가져와서 TMP에 표시
        List<string> stringList = Managers.StringList;  // Managers에서 StringList 가져오기
        tmpText.text = "";  // TMP 텍스트 초기화
        int DayCount = 1;

        foreach (string str in stringList)
        {
            tmpText.text += $"Day {DayCount}" + str + "\n";  // 각 문자열을 줄 바꿈과 함께 TMP 텍스트에 추가
            DayCount++;
        }
    }
}