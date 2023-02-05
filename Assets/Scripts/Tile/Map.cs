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

    private void Start() {
        for(int row = 0; row < map.Length; row++) {
            for (int column = 0; column < map[0].Length; column++) {
                map[row][column].SetCoordinates(row, column);
            }
        }
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

    public bool IsReachable(int row, int col) {
        if (row%2 == 0) {
            return 
                CheckHasRoot(row + 1, col) ||
                CheckHasRoot(row + 1, col + 1) ||
                CheckHasRoot(row, col - 1) ||
                CheckHasRoot(row, col + 1) ||
                CheckHasRoot(row - 1, col) ||
                CheckHasRoot(row - 1, col + 1);
        } else {
            return 
                CheckHasRoot(row + 1, col - 1) ||
                CheckHasRoot(row + 1, col) ||
                CheckHasRoot(row, col - 1) ||
                CheckHasRoot(row, col + 1) ||
                CheckHasRoot(row - 1, col - 1) ||
                CheckHasRoot(row - 1, col);
        }
    }

    public TileController GetClosestReachable(int row, int col) {
        if (!IsReachable(row, col)) { return null; }

        Vector2Int closest = new Vector2Int();
        int distance = int.MaxValue;

        if (row % 2 == 0) {
            ClosestDistanceCheck(row + 1, col, ref closest, ref distance);
            ClosestDistanceCheck(row + 1, col + 1, ref closest, ref distance);
            ClosestDistanceCheck(row, col - 1, ref closest, ref distance);
            ClosestDistanceCheck(row, col + 1, ref closest, ref distance);
            ClosestDistanceCheck(row - 1, col, ref closest, ref distance);
            ClosestDistanceCheck(row - 1, col + 1, ref closest, ref distance);
        } else {
            ClosestDistanceCheck(row + 1, col - 1, ref closest, ref distance);
            ClosestDistanceCheck(row + 1, col, ref closest, ref distance);
            ClosestDistanceCheck(row, col - 1, ref closest, ref distance);
            ClosestDistanceCheck(row, col + 1, ref closest, ref distance);
            ClosestDistanceCheck(row - 1, col - 1, ref closest, ref distance);
            ClosestDistanceCheck(row - 1, col, ref closest, ref distance);
        }

        return map[closest.x][closest.y];
    }

    private bool CheckHasRoot(int row, int col) {
        if (row < 0 || row >= map.Length || col < 0 || col >= GetShortestColumn()) {
            return false;
        }

        return map[row][col].HasRoot;
    }

    private void ClosestDistanceCheck(int row, int col, ref Vector2Int position, ref int distance) {
        if (row < 0 || row >= map.Length || col < 0 || col >= GetShortestColumn()) {
            return;
        }

        if (map[row][col].HasRoot && map[row][col].DistanceToTree < distance) {
            position.x = row;
            position.y = col;
            distance = map[row][col].DistanceToTree;
        }
    }

}

