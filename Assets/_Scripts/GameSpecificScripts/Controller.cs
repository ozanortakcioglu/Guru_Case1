using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera mainCamera;

    private bool controlsEnabled = true;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        if (controlsEnabled)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPos = screenToGroundPoint(Input.mousePosition);
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
