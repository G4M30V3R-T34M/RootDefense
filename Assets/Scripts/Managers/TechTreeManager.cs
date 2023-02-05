using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeManager : MonoBehaviour
{
    [SerializeField] Map tileMap;

    [SerializeField] RootRewardsScriptableObject rootRewards;
    List<TurretType> rewardsToAssign;

    private void Awake() {
        rewardsToAssign = new List<TurretType>();
        CopyRootRewards();
    }

    private void Start() {
        PlaceRewards();
    }

    private void CopyRootRewards() {
        for (int i = 0; i < rootRewards.rewards.Count; i++) {
            rewardsToAssign.Add(rootRewards.rewards[i]);
        }
    }

    private void PlaceRewards() {
        int shortestColumn = tileMap.GetShortestColumn() - 1;
        int rows = tileMap.GetRowCount() - 1;
        int column = shortestColumn;

        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        while (rewardsToAssign.Count > 0) {
            int i = UnityEngine.Random.Range(0, rewardsToAssign.Count);
            TurretType reward = rewardsToAssign[i];
            rewardsToAssign.RemoveAt(i);

            TileController tile;
            do {
                int assignRow = UnityEngine.Random.Range(0, rows);
                tile = tileMap.GetTile(assignRow, column);
            } while (tile.HasReward);
            tile.SetReward(reward);

            if (--column < 0 ) {
                column = shortestColumn;
            }
        }
    }
}
