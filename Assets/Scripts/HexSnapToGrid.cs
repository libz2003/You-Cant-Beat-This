using System;
using UnityEngine;

[ExecuteAlways]
public class HexSnapToGrid : MonoBehaviour
{
    private Grid grid;

    private void Start()
    {
    }

    void Update()
    {
        grid = transform.parent.GetComponent<Grid>();
        if (grid == null) return;

        // Convert current world position to a cell, then back to world
        Vector3Int cell = grid.WorldToCell(transform.position);
        Vector3 snappedPos = grid.CellToWorld(cell);
        transform.position = snappedPos;
    }
}