using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro 네임스페이스 추가

public class SuccessCounter : MonoBehaviour
{
    public static int counter = 0;
    public TextMeshProUGUI tmpCounterText = null;  // TMP 텍스트 객체 참조 추가
    public TextMeshProUGUI tmpGradeText = null;    // 등급을 표시할 TMP 텍스트 객체 참조 추가

    private void Start()
    {
        // TMP 텍스트 객체가 연결되었는지 확인
        if (tmpCounterText == null || tmpGradeText == null)
        {
            Debug.LogError("TMP 텍스트가 연결되지 않았습니다. 인스펙터에서 TMP 텍스트를 연결하세요.");
        }

        UpdateCounterText();  // 초기 텍스트 업데이트
        UpdateGradeText();    // 초기 등급 업데이트
    }

    public void Success()
    {
        Managers.AddStringToList("성공");
        counter++;
        UpdateCounterText();  // counter 값이 변경되었으므로 TMP 텍스트 업데이트
        UpdateGradeText();    // counter 값이 변경되었으므로 등급 TMP 텍스트 업데이트
    }

    public void Failed()
    {
        Managers.AddStringToList("실패");
        // 실패 시에는 counter를 변경하지 않으므로 텍스트 업데이트를 하지 않습니다.
    }

    // TMP 텍스트를 업데이트하는 메서드
    void UpdateCounterText()
    {
        if (tmpCounterText != null)
        {
            tmpCounterText.text = $"{counter}";
        }
    }

    // 등급을 계산하고 TMP 텍스트를 업데이트하는 메서드
    void UpdateGradeText()
    {
        if (tmpGradeText != null)
        {
            tmpGradeText.text = GetGrade(counter);
        }
    }

    // counter 값에 따라 등급을 반환하는 메서드
    string GetGrade(int counter)
    {
        switch (counter)
        {
            case 7: return "S";
            case 6: return "A";
            case 5: return "B";
            case 4: return "C";
            case 3: return "D";
            case 2: return "E";
            case 1: return "F";
            default: return "";  // counter가 0이거나 범위를 벗어났을 때 빈 문자열 반환
        }
    }
}