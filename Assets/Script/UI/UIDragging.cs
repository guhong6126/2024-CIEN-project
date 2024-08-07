using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDraggingWithLayerMask : MonoBehaviour
{
    private LayerMask uiLayerMask;
    private GameObject selectedObject;
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    private Canvas canvas;

    void Start()
    {
        uiLayerMask = LayerMask.GetMask("WhiteBoard_SNS");
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
        }

        if (selectedObject != null)
        {
            DragObject();
        }
    }

    void SelectObject()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject hitObject = result.gameObject;
            if (((1 << hitObject.layer) & uiLayerMask) != 0 && hitObject.GetComponent<Image>() != null)
            {
                selectedObject = hitObject;
                Debug.Log($"Selected object: {selectedObject.name}");
                break;
            }
        }
    }

    void DragObject()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );
        selectedObject.GetComponent<RectTransform>().localPosition = localPoint;
    }
}
