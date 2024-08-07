using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour
{
    public PanelHandler popupWindow;

    public void OnButtonClick()
    {
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() => {
            popupWindow.Show();
        });
    }
}