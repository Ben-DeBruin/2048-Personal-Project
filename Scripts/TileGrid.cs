using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int width => rows.Length;
    public int height => size / width;
    private void Awake()
    {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].cells.Length; x++)
            {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetCell(int x, int y)
    {

        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return rows[y].cells[x];

    }
    public TileCell GetCell(Vector2Int coordinates)
    {//Overload to allow alternate arguments

        return GetCell(coordinates.x, coordinates.y);

    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y; //Minus because Up is positive, but grid is indexed from top down

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int remember = index;
        while (cells[index].occupied)
        {
            index++;
            if (index >= cells.Length)
            {
                index = 0;
            }
            if (index == remember)
            {
                return null;
            }
        }


        return cells[index];
    }

    
}
