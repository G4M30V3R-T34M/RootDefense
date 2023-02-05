using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public TileController[][] map;

    [SerializeField] TileController[] row4;
    [SerializeField] TileController[] row3;
    [SerializeField] TileController[] row2;
    [SerializeField] TileController[] row1;
    [SerializeField] TileController[] row0;

    private void Awake() {
        map = new TileController[][] {
            row0, row1, row2, row3, row4
        };
    }

    public TileController GetTile(int row, int column) {
        return map[row][column];
    }

    public int GetShortestColumn() {
        return Mathf.Min(
            map[0].Length,
            map[1].Length,
            map[2].Length,
            map[3].Length,
            map[4].Length
            );
    }

    public int GetRowCount() {
        return map.Length;
    }
}
