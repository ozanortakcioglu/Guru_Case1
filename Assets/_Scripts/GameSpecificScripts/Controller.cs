using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera mainCamera;

    private bool controlsEnabled = true;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        SetCameraForNewGrid(gridManager.size);
        UIManager.onRebuildButtonClick += SetCameraForNewGrid;
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                //var worldPos = screenToGroundPoint(Input.mousePosition);  
                var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var gridPos = gridManager.GetGridPosFromWorld(worldPos);
                if (gridManager.isOnTheGrid(gridPos))
                {
                    gridManager.ActivateGridCell(gridPos);
                }
                else
                    Debug.Log("outside of the grid");
            }
        }
    }

    private void SetCameraForNewGrid(int size)
    {
        mainCamera.orthographicSize = size + ((float)size / 5);
        var pos = mainCamera.transform.position;
        pos.x = (float)size / 2;
        mainCamera.transform.position = pos;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private Vector3 screenToGroundPoint(Vector3 screenPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(screenPos), out hit, 1000f, groundMask))
        {
            return hit.point;
        }
        else
            return Vector3.one * -1;
    }
}
