using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting2D : MonoBehaviour
{
    private GameObject selectedObject;
    private LayerMask mask;

    void Start()
    {
        mask = LayerMask.GetMask("SNS");
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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, mask);

        if (hit.collider != null)
        {
            selectedObject = hit.collider.gameObject;
            Debug.Log($"Selected object: {selectedObject.name}");
        }
    }

    void DragObject()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectedObject.transform.position = mousePosition;
    }
}
